using System.Collections.Generic;
using FI.Business.Meals.Utils;
using FI.Data;
using FI.Data.Models.Meals;

namespace FI.Business.Meals.MealsService
{
    public class MealsCreator
    {
        FIContext _context;

        public MealsCreator(FIContext context)
        {
            _context = context;
        }
        public void generateMealsToDB()
        {
            FoodApi foodApi = new FoodApi(_context);

            foodApi.getApiMeals();
        }
    }
}
