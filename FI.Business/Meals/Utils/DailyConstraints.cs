using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Business.Meals.Utils
{
    public class DailyConstraints
    {
        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Carb { get; set; }
        public double Fat { get; set; }

        public double getContraint(string name)
        {
            if(name == "Energy")
            {
                return Kcal;
            }
            if (name == "Protein")
            {
                return Protein;
            }
            if (name == "Carbs")
            {
                return Carb;
            }
            if (name == "Fat")
            {
                return Fat;
            }
            return 1.0;
        }
    }
}
