﻿using System.Collections.Generic;
using System.Linq;
using FI.Data.Models.Meals.DTOs;

namespace FI.Data.Models.Meals
{
    public class Day
    {
        public int Id { get; set; }
        public ICollection<Meal> Meals { get; set; }

        public DayDTO toDTO()
        {
            return new DayDTO
            {
                Id = Id,
                Meals = Meals.Select(meal => meal.toDTO()).ToList()
            };
        }
    }
}
