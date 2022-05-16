using System.Collections.Generic;

namespace FI.Data.Models.Meals
{
    public class FoodEntity
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carb { get; set; }
        public double Fat { get; set; }
        public ICollection<Meal> meals { get; set; }
    }
}
