using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.Data.Models.Meals.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI.Data.Configurations.Meals
{
    public class BaseNutrientConfiguration : IEntityTypeConfiguration<BaseNutrient>
    {
        public void Configure(EntityTypeBuilder<BaseNutrient> builder)
        {
            builder.HasOne<BaseMeal>()
                .WithMany(meal => meal.Nutrients)
                .HasForeignKey(nutrient => nutrient.BaseMealId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
