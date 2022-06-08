using System.Collections.Generic;

namespace FI.Data.Models.Meals.DTOs
{
    public class MealplanDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carb { get; set; }
        public double Fat { get; set; }
        public MealplanData MealplanData { get; set; }
        public ICollection<DayDTO> DailyMeals { get; set; }
    }
}
