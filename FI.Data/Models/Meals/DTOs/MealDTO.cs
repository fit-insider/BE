using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Data.Models.Meals.DTOs
{
    public class MealDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string[] HealthLabels { get; set; }
        public string[] DishTypes { get; set; }
        public string[] CuisineTypes { get; set; }
        public string[] MealTypes { get; set; }
        public string[] Cautions { get; set; }
        public List<Nutrient> Nutrients { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public ICollection<Day> Days { get; set; }
    }
}
