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
       
    }
}
