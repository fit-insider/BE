using System;

namespace FI.Data.Models.Meals
{
    public class Nutrient
    {
        public string Id { get; set; }
        public string MealId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }

        public Nutrient clone()
        {
            return new Nutrient
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Quantity = Quantity,
                Unit = Unit
            };
        }
    }
}
