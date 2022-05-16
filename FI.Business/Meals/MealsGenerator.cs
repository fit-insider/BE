using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.Data;
using FI.Data.Models.Meals;
using IronPython.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FI.Business.Meals
{
    public class MealsGenerator
    {
        FIContext _context;
        public MealsGenerator(FIContext context)
        {
            _context = context;
        }

        public async Task<int> getMealsAsync(double kcal, double protein, double carb, double fat)
        {

            List<Meal> meals = await _context.Meals.ToListAsync();
            Dictionary<string, KeyValuePair<double, double>> dietaryConstraints = new Dictionary<string, KeyValuePair<double, double>>();



            return 1;
        }

    }
}
