using FI.Data.Models.Meals.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI.Data.Configurations.Meals
{
    public class BaseIngredientConfiguration : IEntityTypeConfiguration<BaseIngredient>
    {
        public void Configure(EntityTypeBuilder<BaseIngredient> builder)
        {

            builder.HasOne<BaseMeal>()
                .WithMany(meal => meal.Ingredients)
                .HasForeignKey(ingredient => ingredient.BaseMealId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
