using System.Collections.Generic;
using FI.Data.Models.Meals.Types;

namespace FI.Data.Models.Meals
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public List<DishType> DishTypes { get; set; }
        public List<CuisineType> CuisineTypes { get; set; }
        public List<MealType> MealTypes { get; set; }
        public List<Caution> Cautions { get; set; }
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

    }
}
