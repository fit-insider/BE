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
    public class DailyMealsConfiguration : IEntityTypeConfiguration<DailyMeals>
    {
        public void Configure(EntityTypeBuilder<DailyMeals> builder)
        {
            builder.HasOne<Mealplan>()
                .WithMany(e => e.DailyMeals)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
