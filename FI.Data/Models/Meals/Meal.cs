using System.Collections.Generic;
using FI.Data.Models.Meals.DTOs;
using System;
using System.Linq;

namespace FI.Data.Models.Meals
{
    public class Meal
    {
        public string Id { get; set; }
        public string DayId { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string HealthLabels { get; set; }
        public string DishTypes { get; set; }
        public string CuisineTypes { get; set; }
        public string MealTypes { get; set; }
        public string Cautions { get; set; }
        public List<Nutrient> Nutrients { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public double getKcal()
        {
            return getNutrientValue("Energy");
        }

        public double getProtein()
        {
            return getNutrientValue("Protein");
        }

        public double getCarb()
        {
            return getNutrientValue("Carbs");
        }

        public double getFat()
        {
            return getNutrientValue("Fat");
        }

        public double getNutrientValue(string name)
        {
            double totalWeight = 0;
            Ingredients.ForEach(x => totalWeight += x.Weight);
            double nutrientValue = Nutrients.Find(x => x.Name == name).Quantity;
            return (100 * nutrientValue) / totalWeight;
        }

        public MealDTO toDTO()
        {
            return new MealDTO
            {
                Id = Id,
                Name = Name,
                ImageData = ImageData,
                Ingredients = Ingredients,
                Nutrients = Nutrients,
                HealthLabels = HealthLabels.Split(';'),
                DishTypes = DishTypes.Split(';'),
                MealTypes = MealTypes.Split(';'),
                CuisineTypes = CuisineTypes.Split(';'),
                Cautions = Cautions.Split(';')
            };
        }

        public Meal clone()
        {
            return new Meal
            {
                Id = Guid.NewGuid().ToString(),
                DayId = DayId,
                Name = Name,
                ImageData = ImageData,
                Ingredients = Ingredients.Select(Ingredient => Ingredient.clone()).ToList(),
                Nutrients = Nutrients.Select(nutrient => nutrient.clone()).ToList(),
                HealthLabels = HealthLabels,
                DishTypes = DishTypes,
                MealTypes = MealTypes,
                CuisineTypes = CuisineTypes,
                Cautions = Cautions
            };
        }
    }
}
