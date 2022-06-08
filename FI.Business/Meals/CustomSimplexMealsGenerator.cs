using System;
using System.Collections.Generic;
using System.Linq;
using FI.Business.Meals.Utils;
using FI.Data.Models.Meals;
using FI.Business.Meals.SimplexImplementation.Model;
using FI.Business.Meals.SimplexImplementation.Service;
using SimplexMethod.Model;
using FI.Data;
using FI.Data.Models.Meals.DTOs;

namespace FI.Business.Meals
{
    public class CustomSimplexMealsGenerator
    {
        private FIContext _context;
        private MealPreferences _preferences;
        private DailyConstraints _constraints;

        private const int NUM_SAMPLES = 1500;
        private const int TOP_SAMPLES_TO_KEEP = 7;
        private const double CALORIE_EPSILON = 0.2;

        public CustomSimplexMealsGenerator(FIContext context, MealPreferences preferences, DailyConstraints constraints)
        {
            _context = context;
            _preferences = preferences;
            _constraints = constraints;
        }

        public ICollection<Day> getDailyMeals()
        {
            MealsGenerator mealsGenerator = new MealsGenerator(_context, _preferences);
            ICollection<Meal> breakfastMeals = mealsGenerator.getBreakfastMeals();
            ICollection<Meal> dinnerMeals = mealsGenerator.getDinnerMeals();
            ICollection<Meal> lunchMeals = new List<Meal>();
            ICollection<Meal> snackMeals = new List<Meal>();

            if (_preferences.MealsCount >= 3)
            {
                lunchMeals = mealsGenerator.getLunchMeals();
            }

            if (_preferences.MealsCount > 3)
            {
                snackMeals = mealsGenerator.getSnackMeals();
            }

            List<SolverResult> solutions = new List<SolverResult>();
            foreach (int _ in Enumerable.Range(1, NUM_SAMPLES))
            {
                Day day = sampleDay(breakfastMeals, lunchMeals, dinnerMeals, snackMeals);

                solutions.Add(optimizeDay(day));
            }

            SolverResultErrorComparer comparer = new SolverResultErrorComparer();
            List<SolverResult> filteredSolutions = solutions.Where(x => !containsAnormalMeals(x.SolutionValues)).ToList();
            filteredSolutions.Sort(comparer);
            List<Day> dailyMeals = new List<Day>();
            for (int i = 0; i < TOP_SAMPLES_TO_KEEP; i++)
            {
                SolverResult result = filteredSolutions.ElementAt(i);
                ICollection<Meal> processedMeals = processResultMeals(result);
                dailyMeals.Add(new Day
                {
                    Id = Guid.NewGuid().ToString(),
                    Meals = processedMeals
                });
            }


            return dailyMeals;
        }

        private bool containsAnormalMeals(List<double> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (values.ElementAt(i) < 1 || values.ElementAt(i) > 8)
                {
                    return true;
                }
            }

            return false;
        }

        private ICollection<Meal> processResultMeals(SolverResult result)
        {
            ICollection<Meal> resultMeals = result.Meals;
            ICollection<Meal> processedMeals = new List<Meal>();

            for (int i = 0; i < resultMeals.Count; i++)
            {
                Meal meal = resultMeals.ElementAt(i).clone();

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

                processedMeals.Add(meal);
            }

            return processedMeals;
        }


        private Day sampleDay(
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

            return new Day()
            {
                Meals = dailyMeals
            };
        }

        private SolverResult optimizeDay(Day day)
        {
            // Defining the constraints
            Dictionary<string, Tuple<double, double>> dietaryConstraints = new Dictionary<string, Tuple<double, double>>();
            dietaryConstraints.Add("Energy", new Tuple<double, double>(
                (_constraints.Kcal - (5.0 / 100.0 * _constraints.Kcal)),
                (_constraints.Kcal + (5.0 / 100.0 * _constraints.Kcal))));
            dietaryConstraints.Add("Protein", new Tuple<double, double>(
                (_constraints.Protein - (5.0 / 100.0 * _constraints.Protein)), 
                (_constraints.Protein + (5.0 / 100.0 * _constraints.Protein))));

            List<Constraint> constraints = new List<Constraint>();
            foreach (KeyValuePair<string, Tuple<double, double>> constraint in dietaryConstraints)
            {
                string nutrientName = constraint.Key;
                double low = constraint.Value.Item1;
                double high = constraint.Value.Item2;

                double[] coefficients = new double[day.Meals.Count];

                for (int i = 0; i < day.Meals.Count; i++)
                {
                    coefficients[i] = day.Meals.ElementAt(i).getNutrientValue(nutrientName);
                }
                constraints.Add(new Constraint(coefficients, high, "<="));
                constraints.Add(new Constraint(coefficients, low, ">="));
            }

            for (int i = 0; i < day.Meals.Count; i++)
            {
                double[] oneMealCoefficients = new double[day.Meals.Count];
                oneMealCoefficients[i] = 1;

                constraints.Add(new Constraint(oneMealCoefficients, 1.5, ">="));
                constraints.Add(new Constraint(oneMealCoefficients, 7.5, "<="));
            }

            // Define objective function coefficients
            double[] functionCoefficients = new double[day.Meals.Count];
            for (int i = 0; i < day.Meals.Count; i++)
            {
                if(i == 0)
                {
                    functionCoefficients[i] = 2;
                }
                if(i == 1)
                {
                    functionCoefficients[i] = 1.5;
                }
                if(i == 2)
                {
                    functionCoefficients[i] = 1.75;
                } 
                if(i > 2)
                {
                    functionCoefficients[i] = 1;
                }
            }

            Function function = new Function(functionCoefficients, 0, true);

            Simplex simplex = new Simplex(function, constraints.ToArray());
            Tuple<List<SimplexSnap>, SimplexResult> result = simplex.GetResult();

            var C = result.Item1.Last().C;
            var fVars = result.Item1.Last().fVars;
            var b = result.Item1.Last().b;

            List<double> solution = new List<double>();

            for (int i = 0; i < result.Item1.Last().C.Length; i++)
            {
                if (fVars[C[i]] > 0)
                {
                    solution.Add(b[i]);
                }
            }

            double KcalResult = 0.0;
            double ProteinResult = 0.0;
            double CarbResult = 0.0;
            double FatResult = 0.0;

            if (result.Item2 == SimplexResult.Found && solution.Count == day.Meals.Count)
            {
                for (int i = 0; i < solution.Count; ++i)
                {
                    if (solution[i]> 0.0)
                    {
                        KcalResult += day.Meals.ElementAt(i).getKcal() * solution.ElementAt(i);
                        ProteinResult += day.Meals.ElementAt(i).getProtein() * solution.ElementAt(i);
                        CarbResult += day.Meals.ElementAt(i).getCarb() * solution.ElementAt(i);
                        FatResult += day.Meals.ElementAt(i).getFat() * solution.ElementAt(i);
                    }
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
                SolutionValues = solution,
                Error = error,
                Meals = day.Meals
            };
        }
    }
}
