using System;
using System.Collections.Generic;
using System.Linq;
using FI.Business.Meals.Utils;
using FI.Data;
using FI.Data.Models.Meals;
using Google.OrTools.LinearSolver;

namespace FI.Business.Meals
{
    public class MealsGenerator
    {
        private MealPreferences _preferences;
        private DailyConstraints _constraints;

        private const int NUM_SAMPLES = 1500;
        private const int TOP_SAMPLES_TO_KEEP = 7;
        private const double CALORIE_EPSILON = 0.2;

        public MealsGenerator(MealPreferences preferences, DailyConstraints constraints)
        {
            _preferences = preferences;
            _constraints = constraints;
        }

        public ICollection<DailyMeals> getDailyMeals()

        {
            FoodApi foodApi = new FoodApi(_preferences);
            ICollection<Meal> breakfastMeals = foodApi.getBreakfastMeals();
            ICollection<Meal> dinnerMeals = foodApi.getDinnerMeals();
            ICollection<Meal> lunchMeals = new List<Meal>();
            ICollection<Meal> snackMeals = new List<Meal>();

            if (_preferences.MealsCount >= 3)
            {
                lunchMeals = foodApi.getLunchMeals();
            }

            if (_preferences.MealsCount > 3)
            {
                snackMeals = foodApi.getSnackMeals();
            }

            List<SolverResult> solutions = new List<SolverResult>();
            foreach (int _ in Enumerable.Range(1, NUM_SAMPLES))
            {
                DailyMeals day = sampleDay(breakfastMeals, lunchMeals, dinnerMeals, snackMeals);

                solutions.Add(optimizeDay(day));
            }

            SolverResultErrorComparer comparer = new SolverResultErrorComparer();
            List<SolverResult> filteredSolutions = solutions.Where(x => !containsAnormalMeals(x.SolutionValues)).ToList();
            filteredSolutions.Sort(comparer);

            List<DailyMeals> dailyMeals = new List<DailyMeals>();

            for (int i = 0; i < TOP_SAMPLES_TO_KEEP; i++)
            {
                SolverResult result = filteredSolutions.ElementAt(i);
                ICollection<Meal> processedMeals = processResultMeals(result);
                dailyMeals.Add(new DailyMeals
                {
                    Id = i,
                    Meals = processedMeals
                });

            }

            return dailyMeals;
        }

        private bool containsAnormalMeals(List<double> values)
        {
            for(int i = 0; i < values.Count; i++)
            {
                if(values.ElementAt(i) < 1 || values.ElementAt(i) > 8)
                {
                    return true;
                }
            }

            return false;
        }

        private ICollection<Meal> processResultMeals(SolverResult result)
        {
            ICollection<Meal> resultMeals = result.Meals;

            for (int i = 0; i < resultMeals.Count; i++)
            {
                Meal meal = resultMeals.ElementAt(i);
                meal.Id = i;

                double solutionValue = result.SolutionValues.ElementAt(i);

                double totalWeight = 0;
                foreach (Ingredient ingredient in meal.Ingredients)
                {
                    totalWeight += ingredient.Weight;
                }

                // Change percentage = 100 grams * solution variable / total meal weight => all the attributes must be multiplied with changePercentage
                double changePercentage = (100 * solutionValue / totalWeight);


                foreach (Ingredient ingredient in meal.Ingredients)
                {
                    ingredient.Weight *= changePercentage;
                    ingredient.Quantity *= changePercentage;
                }

                foreach (Nutrient nutrient in meal.Nutrients)
                {
                    nutrient.Quantity *= changePercentage;
                }
            }

            return result.Meals;
        }


        private DailyMeals sampleDay(
            ICollection<Meal> breakfastMeals,
            ICollection<Meal> lunchMeals,
            ICollection<Meal> dinnerMeals,
            ICollection<Meal> snackMeals
        )
        {
            Random random = new Random();
            List<Meal> dailyMeals = new List<Meal>();
            dailyMeals.Add(breakfastMeals.ElementAt(random.Next(breakfastMeals.Count)));
            dailyMeals.Add(dinnerMeals.ElementAt(random.Next(dinnerMeals.Count)));

            if (_preferences.MealsCount >= 3)
            {
                dailyMeals.Add(lunchMeals.ElementAt(random.Next(lunchMeals.Count)));
            }

            for (int i = 4; i <= _preferences.MealsCount; i++)
            {
                dailyMeals.Add(snackMeals.ElementAt(random.Next(snackMeals.Count)));
            }


            return new DailyMeals()
            {
                Meals = dailyMeals
            };
        }


        private SolverResult optimizeDay(DailyMeals day)
        {
            Solver solver = Solver.CreateSolver("CLP");
            const double INF = double.PositiveInfinity;
            Random rand = new Random();
            LinearExpr objective = new LinearExpr();

            // Create variables
            List<Variable> solution = new List<Variable>();
            for (int i = 0; i < day.Meals.Count; i++)
            {
                var x = solver.MakeNumVar(rand.Next(3) + 1, double.PositiveInfinity, day.Meals.ElementAt(i).Name);
                solution.Add(x);
            }


            // Defining the constraints
            Dictionary<string, Tuple<double, double>> dietaryConstraints = new Dictionary<string, Tuple<double, double>>();
            dietaryConstraints.Add("Energy", new Tuple<double, double>(_constraints.Kcal - (5 / 100 * _constraints.Kcal), _constraints.Kcal + (5 / 100 * _constraints.Kcal)));
            dietaryConstraints.Add("Protein", new Tuple<double, double>(_constraints.Protein - (5 / 100 * _constraints.Protein), _constraints.Protein + (5 / 100 * _constraints.Protein)));


            // For every dietary constraint
            foreach (KeyValuePair<string, Tuple<double, double>> constraint in dietaryConstraints)
            {
                string nutrientName = constraint.Key;
                double low = constraint.Value.Item1;
                double high = constraint.Value.Item2;

                LinearExpr nutrientSum = new LinearExpr();

                for (int i = 0; i < solution.Count; i++)
                {
                    nutrientSum += solution[i] * day.Meals.ElementAt(i).getNutrientValue(nutrientName);
                }

                var low_positive = solver.MakeNumVar(0, INF, "over_lower_limit_" + nutrientName);
                var low_negative = solver.MakeNumVar(0, INF, "under_lower_limit_" + nutrientName);
                solver.Add(nutrientSum + low_positive - low_negative == low);

                var high_positive = solver.MakeNumVar(0, INF, "over_upper_limit_" + nutrientName);
                var high_negative = solver.MakeNumVar(0, INF, "under_upper_limit_" + nutrientName);
                solver.Add(nutrientSum + high_positive - high_negative == high);

                double scale = 1;
                if (nutrientName == "Energy")
                {
                    scale = 0.2;
                }

                objective += scale * (low_positive + high_negative);
            }

            solver.Minimize(objective);

            solver.Solve();

            double KcalResult = 0.0;
            double ProteinResult = 0.0;
            double CarbResult = 0.0;
            double FatResult = 0.0;

            List<double> solutionValues = new List<double>();
            for (int i = 0; i < solution.Count; ++i)
            {
                if (solution[i].SolutionValue() > 0.0)
                {
                    KcalResult += day.Meals.ElementAt(i).getKcal() * solution[i].SolutionValue();
                    ProteinResult += day.Meals.ElementAt(i).getProtein() * solution[i].SolutionValue();
                    CarbResult += day.Meals.ElementAt(i).getCarb() * solution[i].SolutionValue();
                    FatResult += day.Meals.ElementAt(i).getFat() * solution[i].SolutionValue();
                    solutionValues.Add(solution[i].SolutionValue());
                }
            }

            double error = 0;
            error += CALORIE_EPSILON * Math.Abs(KcalResult - _constraints.Kcal);
            error += Math.Abs(ProteinResult - _constraints.Protein);
            error += Math.Abs(CarbResult - _constraints.Carb);
            error += Math.Abs(FatResult - _constraints.Fat);

            return new SolverResult
            {
                Kcal = KcalResult,
                Protein = ProteinResult,
                Carb = CarbResult,
                Fat = FatResult,
                ObjectiveValue = solver.Objective().Value(),
                SolutionValues = solutionValues,
                Error = error,
                Meals = day.Meals
            };
        }
    }
}
