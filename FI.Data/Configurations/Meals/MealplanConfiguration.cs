using FI.Data.Models.Meals;
using FI.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI.Data.Configurations.Meals
{
    public class MealplanConfiguration : IEntityTypeConfiguration<Mealplan>
    {
        public void Configure(EntityTypeBuilder<Mealplan> builder)
        {
            builder.Property(a => a.Calories)
                .IsRequired();

            builder.Property(a => a.Protein)
                .IsRequired();

            builder.Property(a => a.Carb)
                .IsRequired();

            builder.Property(a => a.Fat)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
