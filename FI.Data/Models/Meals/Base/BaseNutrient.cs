using System;

namespace FI.Data.Models.Meals.Base
{
    public class BaseNutrient
    {
        public string Id { get; set; }
        public string BaseMealId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }

        public Nutrient toNutrient()
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
