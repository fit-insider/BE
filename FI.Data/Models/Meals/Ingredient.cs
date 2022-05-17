using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Data.Models.Meals
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public double Quantity { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }  
    }
}
