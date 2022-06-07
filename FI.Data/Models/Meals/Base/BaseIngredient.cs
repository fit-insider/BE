using System;

namespace FI.Data.Models.Meals.Base
{
    public class BaseIngredient
    {
        public string Id { get; set; }
        public string BaseMealId { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public double Quantity { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }

        public Ingredient toIngredient()
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
