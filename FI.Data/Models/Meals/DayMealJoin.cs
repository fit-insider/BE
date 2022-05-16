using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Data.Models.Meals
{
    class DailyMealJoin
    {
        public int DailyMealsId { get; set; }
        public DailyMeals DailyMeals { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
