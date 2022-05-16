using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Data.Models.Meals
{
    public class MealFoodEntity
    {
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public int FoodId { get; set; }
        public FoodEntity FoodEntity { get; set; }
    }
}
