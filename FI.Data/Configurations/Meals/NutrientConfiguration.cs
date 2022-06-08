using FI.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI.Data.Configurations.Meals
{
    public class NutrientConfiguration : IEntityTypeConfiguration<Nutrient>
    {
        public void Configure(EntityTypeBuilder<Nutrient> builder)
        {
            builder.HasOne<Meal>()
                .WithMany(meal => meal.Nutrients)
                .HasForeignKey(nutrient => nutrient.MealId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
