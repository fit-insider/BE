﻿// <auto-generated />
using System;
using FI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FI.Migrations.Migrations
{
    [DbContext(typeof(FIContext))]
    [Migration("20220530094425_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FI.Data.Models.ApplicationVersions.ApplicationVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ApplicationVersions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Create Skeleton",
                            Version = "1.0.1"
                        });
                });

            modelBuilder.Entity("FI.Data.Models.Meals.DailyMeals", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MealplanId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MealplanId");

                    b.ToTable("DailyMeals");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MealId")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DailyMealsId")
                        .HasColumnType("int");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DailyMealsId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Mealplan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<double>("Carb")
                        .HasColumnType("float");

                    b.Property<double>("Fat")
                        .HasColumnType("float");

                    b.Property<int?>("MealplanDataId")
                        .HasColumnType("int");

                    b.Property<double>("Protein")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MealplanDataId");

                    b.HasIndex("UserId");

                    b.ToTable("Mealplans");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.MealplanData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<string>("HeightUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MealplanType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MealsCount")
                        .HasColumnType("int");

                    b.Property<int>("PhisicalActivity")
                        .HasColumnType("int");

                    b.Property<int>("Sleep")
                        .HasColumnType("int");

                    b.Property<string>("Target")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsualActivity")
                        .HasColumnType("int");

                    b.Property<int>("WaterIntake")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<string>("WeightUnit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MealplanData");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Nutrient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("Nutrient");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Types.Caution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("Caution");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Types.CuisineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("CuisineType");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Types.DishType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("DishType");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Types.MealType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("MealType");
                });

            modelBuilder.Entity("FI.Data.Models.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(74)
                        .HasColumnType("nvarchar(74)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FI.Data.Models.Users.UserDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.DailyMeals", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.Mealplan", null)
                        .WithMany("DailyMeals")
                        .HasForeignKey("MealplanId");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Ingredient", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.Meal", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Meal", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.DailyMeals", null)
                        .WithMany("Meals")
                        .HasForeignKey("DailyMealsId");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Mealplan", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.MealplanData", "MealplanData")
                        .WithMany()
                        .HasForeignKey("MealplanDataId");

                    b.HasOne("FI.Data.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("MealplanData");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Nutrient", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.Meal", null)
                        .WithMany("Nutrients")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Types.Caution", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.Meal", null)
                        .WithMany("Cautions")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Types.CuisineType", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.Meal", null)
                        .WithMany("CuisineTypes")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Types.DishType", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.Meal", null)
                        .WithMany("DishTypes")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Types.MealType", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.Meal", null)
                        .WithMany("MealTypes")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("FI.Data.Models.Users.UserDetail", b =>
                {
                    b.HasOne("FI.Data.Models.Users.User", "User")
                        .WithOne("Detail")
                        .HasForeignKey("FI.Data.Models.Users.UserDetail", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.DailyMeals", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Meal", b =>
                {
                    b.Navigation("Cautions");

                    b.Navigation("CuisineTypes");

                    b.Navigation("DishTypes");

                    b.Navigation("Ingredients");

                    b.Navigation("MealTypes");

                    b.Navigation("Nutrients");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Mealplan", b =>
                {
                    b.Navigation("DailyMeals");
                });

            modelBuilder.Entity("FI.Data.Models.Users.User", b =>
                {
                    b.Navigation("Detail");
                });
#pragma warning restore 612, 618
        }
    }
}