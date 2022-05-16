using System.Collections.Generic;

namespace FI.Data.Models.Meals
{
    public class DailyMeals
    {
        public int Id { get; set; }
        public ICollection<Meal> Meals { get; set; }
    }
}
