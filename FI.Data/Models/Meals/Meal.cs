using System.Collections.Generic;

namespace FI.Data.Models.Meals
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FoodEntity> FoodEntities { get; set; }
        public ICollection<DailyMeals> DailyMeals { get; set; }
    }
}
