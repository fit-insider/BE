using System;
using System.Collections.Generic;
using System.Threading;
using FI.Business.Meals.Commands;
using FI.Business.Meals.MealsService;
using FI.Business.Meals.Utils;
using FI.Data;
using FI.Data.Models.Meals;
using FI.Data.Models.Meals.DTOs;

namespace FI.Business.Meals
{
    public class MealplanGenerator
    {
        const double USUAL_ACTIVITY_MODIFIER = 0.05;
        const double PHISICAL_ACTIVITY_MODIFIER = 0.025;
        const double SLEEP_MODIFIER = 0.01;
        const double WATER_INTAKE_MODIFIER = 0.001;

        FIContext _context;

        public MealplanGenerator(FIContext context)
        {
            _context = context;
        }

        public MealplanDTO GenerateMealPlan(CreateMealplanCommand command)
        {
            double rmb = calculateRmb(command);
            double kcal = calculateCalories(command, rmb);
            double protein = calculateProtein(command);
            double fat = calculateFat(command);
            double carb = calculateCarb(kcal, protein, fat);

            MealPreferences preferences = new MealPreferences { 
                Type = command.MealplanType,
                ExcludedFoods = command.ExcludedFoods,
                MealsCount = command.MealsCount
            };

            DailyConstraints constraints = new DailyConstraints
            {
                Kcal = kcal,
                Protein = protein,
                Carb = carb,
                Fat = fat
            };

            //MealsCreator mealsCreator = new MealsCreator(_context);
            //mealsCreator.generateMealsToDB();
            //ICollection<Day> dailyMeals = new List<Day>();


            ICollection<Day> dailyMeals;
            if(command.UseCustomMethod == true)
            {
                CustomSimplexMealsGenerator mealsGenerator = new CustomSimplexMealsGenerator(_context,
                    preferences, constraints);
                dailyMeals = mealsGenerator.getDailyMeals();
            } else
            {
                GoogleBasedMealsGenerator mealsGenerator = new GoogleBasedMealsGenerator(_context,
                    preferences, constraints);
                dailyMeals = mealsGenerator.getDailyMeals();
            }


            MealplanData mealplanData = new MealplanData
            {
                Gender = command.Gender,
                Target = command.Target,
                Height = command.Height,
                Weight = command.Weight,
                Age = command.Age,
                HeightUnit = command.HeightUnit,
                WeightUnit = command.WeightUnit,
                Body = command.Body,
                UsualActivity = command.UsualActivity,
                PhisicalActivity = command.PhisicalActivity,
                Sleep = command.Sleep,
                WaterIntake = command.WaterIntake,
                MealplanType = command.MealplanType,
                MealsCount = command.MealsCount
            };

            Mealplan mealplan = new Mealplan
            {
                Id = Guid.NewGuid().ToString(),
                UserId = command.UserId,
                Calories = kcal,
                Protein = protein,
                Fat = fat,
                Carb = carb,
                DailyMeals = dailyMeals,
                MealplanData = mealplanData
            };


            if (command.UserId != null)
            {
                _context.Mealplans.Add(mealplan);
                _context.SaveChanges();
            }

            return mealplan.toDTO();
        }

        private static double calculateRmb(CreateMealplanCommand command)
        {
            double weight = command.Weight;
            double height = command.Height;
            double rmb;

            if (command.HeightUnit == "FEET")
            {
                height = height * 30.48;
            }

            if (command.WeightUnit == "LBS")
            {
                weight = weight * 0.45359237;
            }

            if (command.Gender == "male")
            {
                rmb = 10 * weight + 6.5 * height - 5 * command.Age + 5;
            }
            else
            {
                rmb = 10 * weight + 6.5 * height - 5 * command.Age - 161;
            }

            return rmb;
        }

        private static double calculateCalories(CreateMealplanCommand command, double rmb)
        {
            double kcal = rmb;

            kcal = kcal * (1 + USUAL_ACTIVITY_MODIFIER * command.UsualActivity);
            kcal = kcal * (1 + PHISICAL_ACTIVITY_MODIFIER * command.PhisicalActivity);
            kcal = kcal * (1 - SLEEP_MODIFIER * command.Sleep);
            kcal = kcal * (1 + WATER_INTAKE_MODIFIER * command.WaterIntake);

            if (command.Body == "ectomorph")
            {
                kcal = kcal + 200;
            }

            if (command.Body == "endomorph")
            {
                kcal = kcal - 200;
            }

            if (command.Target == "loseWeight")
            {
                kcal = kcal - 300;
            }

            if(command.Target == "gainMass")
            {
                kcal = kcal + 300;
            }

            return kcal;
        }

        private static double calculateProtein(CreateMealplanCommand command)
        {
            double weight = command.Weight;
            double protein = 0;
  
            if (command.WeightUnit == "LBS")
            {
                weight = weight * 0.45359237;
            }

            if (command.Target == "loseWeight")
            {
                protein = 1.6 * weight;
            }

            if (command.Target == "tonifying")
            {
                protein = 1.8 * weight;
            }

            if (command.Target == "gainMass")
            {
                protein = 2 * weight;
            }

            return protein;
        }

        private static double calculateFat(CreateMealplanCommand command)
        {
            double weight = command.Weight;
            double fat = 0;

            if (command.WeightUnit == "LBS")
            {
                weight = weight * 0.45359237;
            }

            if (command.Target == "loseWeight")
            {
                fat = 0.4 * weight;
            }

            if (command.Target == "tonifying")
            {
                fat = 0.5 * weight;
            }

            if (command.Target == "gainMass")
            {
                fat = 0.6 * weight;
            }

            return fat;
        }

        private static double calculateCarb(double kcal, double protein, double fat)
        {
            return (kcal - (protein * 4) - (fat * 9)) / 4;
        }
    }
}
