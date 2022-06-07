using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Data.Models.Meals.DTOs
{
    public class DayDTO
    {
        public string Id { get; set; }
        public ICollection<MealDTO> Meals { get; set; }
    }
}
