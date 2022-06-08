using System;

namespace FI.Data.Models.Meals
{
    public class Ingredient
    {
        public string Id { get; set; }
        public string MealId { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public double Quantity { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }

        public Ingredient clone()
        {
            return new Ingredient
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Category = Category,
                Quantity = Quantity,
                Weight = Weight,
                Unit = Unit
            };
        }
    }
}
