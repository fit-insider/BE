using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Business.Meals.Utils
{
    public class MealPreferences
    {
        public string Type { get; set; }
        public string[] ExcludedFoods { get; set; }
        public int MealsCount;
    }
}
