using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI.Data.Configurations.Meals
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {

            builder.HasOne<Meal>()
                .WithMany(meal => meal.Ingredients)
                .HasForeignKey(ingredient => ingredient.MealId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
