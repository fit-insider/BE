using FI.Data.Configurations.ApplicationVersions;
using FI.Data.Configurations.Users;
using FI.Data.Configurations.Meals;
using FI.Data.Models.ApplicationVersions;
using FI.Data.Models.Meals;
using FI.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using FI.Data.Models.Meals.Base;

namespace FI.Data
{
    public class FIContext : DbContext
    {
        public FIContext()
        {
        }

        public FIContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ApplicationVersion> ApplicationVersions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Mealplan> Mealplans { get; set; }
        public virtual DbSet<BaseMeal> BaseMeals { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Nutrient> Nutrients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationVersionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new MealplanConfiguration());
            builder.ApplyConfiguration(new IngredientConfiguration());
            builder.ApplyConfiguration(new NutrientConfiguration());
            builder.ApplyConfiguration(new MealConfiguration());
            builder.ApplyConfiguration(new DayConfiguration());
            builder.ApplyConfiguration(new BaseIngredientConfiguration());
            builder.ApplyConfiguration(new BaseNutrientConfiguration());
        }
    }
}
