using FI.Data.Configurations.ApplicationVersions;
using FI.Data.Configurations.Users;
using FI.Data.Configurations.Meals;
using FI.Data.Models.ApplicationVersions;
using FI.Data.Models.Meals;
using FI.Data.Models.Users;
using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<DailyMeals> DailyMeals { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<FoodEntity> FoodEntities { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationVersionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new MealplanConfiguration());
            builder.ApplyConfiguration(new DailyMealsConfiguration());
        }
    }
}
