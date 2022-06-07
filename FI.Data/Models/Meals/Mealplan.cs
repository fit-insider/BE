using System.Collections.Generic;
using System.Linq;
using FI.Data.Models.Meals.DTOs;

namespace FI.Data.Models.Meals
{
    public class Mealplan
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carb { get; set; }
        public double Fat { get; set; }
        public virtual MealplanData MealplanData { get; set; }
        public ICollection<Day> DailyMeals { get; set; }
        public MealplanDTO toDTO()
        {
            return new MealplanDTO
            {
                Id = Id,
                UserId = UserId,
                Calories = Calories,
                Protein = Protein,
                Carb = Carb,
                Fat = Fat,
                MealplanData = MealplanData,
                DailyMeals = DailyMeals.Select(day => day.toDTO()).ToList()
            };
        }
    }
}
