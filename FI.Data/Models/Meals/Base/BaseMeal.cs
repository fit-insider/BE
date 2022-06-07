using System;
using System.Collections.Generic;
using System.Linq;

namespace FI.Data.Models.Meals.Base
{
    public class BaseMeal
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string HealthLabels { get; set; }
        public string DishTypes { get; set; }
        public string CuisineTypes { get; set; }
        public string MealTypes { get; set; }
        public string Cautions { get; set; }
        public List<BaseNutrient> Nutrients { get; set; }
        public List<BaseIngredient> Ingredients { get; set; }

        public double getNutrientValue(string name)
        {
            double totalWeight = 0;
            Ingredients.ForEach(x => totalWeight += x.Weight);
            var nutrientValue = Nutrients.Find(x => x.Name == name).Quantity;
            return 100 * nutrientValue / totalWeight;
        }

        public Meal toMeal()
        {
            return new Meal
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                ImageData = ImageData,
                Ingredients = Ingredients.Select(i => i.toIngredient()).ToList(),
                Nutrients = Nutrients.Select(n => n.toNutrient()).ToList(),
                HealthLabels = HealthLabels,
                DishTypes = DishTypes,
                MealTypes = MealTypes,
                CuisineTypes = CuisineTypes,
                Cautions = Cautions
            };
        }
    }
}
