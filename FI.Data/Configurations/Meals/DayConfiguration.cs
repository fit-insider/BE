using FI.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI.Data.Configurations.Meals
{
    public class DayConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.HasOne<Mealplan>()
                .WithMany(mealplan => mealplan.DailyMeals)
                .HasForeignKey(day => day.MealplanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
