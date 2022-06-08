using System.Collections.Generic;
using FI.Data.Models.Meals;

namespace FI.Business.Meals.Utils
{
    public class SolverResult
    {
        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Carb { get; set; }
        public double Fat { get; set; }
        public double ObjectiveValue { get; set; }
        public List<double> SolutionValues { get; set; }
        public double Error { get; set; }
        public ICollection<Meal> Meals { get; set; }
    }
}
