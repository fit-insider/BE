using FI.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI.Data.Configurations.Meals
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasOne<Day>()
                .WithMany(day => day.Meals)
                .HasForeignKey(meal => meal.DayId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
