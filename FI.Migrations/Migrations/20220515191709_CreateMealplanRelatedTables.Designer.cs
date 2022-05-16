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
    [Migration("20220515191709_CreateMealplanRelatedTables")]
    partial class CreateMealplanRelatedTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DailyMealsMeal", b =>
                {
                    b.Property<int>("DailyMealsId")
                        .HasColumnType("int");

                    b.Property<int>("MealsId")
                        .HasColumnType("int");

                    b.HasKey("DailyMealsId", "MealsId");

                    b.HasIndex("MealsId");

                    b.ToTable("DailyMealsMeal");
                });

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

                    b.Property<int?>("MealplanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MealplanId");

                    b.ToTable("DailyMeals");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.FoodEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<double>("Carb")
                        .HasColumnType("float");

                    b.Property<double>("Fat")
                        .HasColumnType("float");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.Property<double>("Protein")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("FoodEntities");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Mealplan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<double>("Carb")
                        .HasColumnType("float");

                    b.Property<double>("Fat")
                        .HasColumnType("float");

                    b.Property<double>("Protein")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Mealplans");
                });

            modelBuilder.Entity("FI.Data.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("FoodEntityMeal", b =>
                {
                    b.Property<int>("FoodEntitiesId")
                        .HasColumnType("int");

                    b.Property<int>("mealsId")
                        .HasColumnType("int");

                    b.HasKey("FoodEntitiesId", "mealsId");

                    b.HasIndex("mealsId");

                    b.ToTable("FoodEntityMeal");
                });

            modelBuilder.Entity("DailyMealsMeal", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.DailyMeals", null)
                        .WithMany()
                        .HasForeignKey("DailyMealsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FI.Data.Models.Meals.Meal", null)
                        .WithMany()
                        .HasForeignKey("MealsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FI.Data.Models.Meals.DailyMeals", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.Mealplan", null)
                        .WithMany("DailyMeals")
                        .HasForeignKey("MealplanId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FI.Data.Models.Meals.Mealplan", b =>
                {
                    b.HasOne("FI.Data.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FI.Data.Models.Users.UserDetail", b =>
                {
                    b.HasOne("FI.Data.Models.Users.User", "User")
                        .WithOne("Detail")
                        .HasForeignKey("FI.Data.Models.Users.UserDetail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodEntityMeal", b =>
                {
                    b.HasOne("FI.Data.Models.Meals.FoodEntity", null)
                        .WithMany()
                        .HasForeignKey("FoodEntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FI.Data.Models.Meals.Meal", null)
                        .WithMany()
                        .HasForeignKey("mealsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
