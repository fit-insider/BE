using System;
using System.Collections.Generic;
using System.Linq;
using FI.Data;
using FI.Data.Models.Meals;
using FI.Data.Models.Meals.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FI.Business.Meals.Utils
{
    public class MealsGenerator
    {
        private FIContext _context;
        private MealPreferences _preferences;

        public MealsGenerator(FIContext context, MealPreferences preferences)
        {
            _context = context;
            _preferences = preferences;
        }

        public ICollection<Meal> getBreakfastMeals()
        {
            return getMeals("breakfast");
        }

        public ICollection<Meal> getLunchMeals()
        {
            return getMeals("lunch");
        }

        public ICollection<Meal> getDinnerMeals()
        {
            return getMeals("dinner");
        }

        public ICollection<Meal> getSnackMeals()
        {
            return getMeals("snack");
        }

        public ICollection<Meal> getMeals(string type)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            var meals = _context.BaseMeals.Where(m => m.MealTypes.Contains(type));
  

            if (_preferences.Type != "general")
            {
                meals = meals.Where(meal => meal.HealthLabels.Contains(_preferences.Type));
            }

            foreach (string excludedFood in _preferences.ExcludedFoods)
            {
                if (excludedFood != "nothing")
                {
                    meals = meals.Where(meal => meal.HealthLabels.Contains(excludedFood));
                }
            }

            var mealIds = meals.Select(m => m.Id).ToArray();
            List<string> chosenIds = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                chosenIds.Add(mealIds.ElementAt(rand.Next(mealIds.Length)));
            }

            var finalMeals = _context.BaseMeals
                .Where(m => chosenIds.Contains(m.Id))
                .Include(m => m.Ingredients)
                .Include(m => m.Nutrients)
                .Select(m => m.toMeal()).ToList();

            return finalMeals;
        }

    }
}
