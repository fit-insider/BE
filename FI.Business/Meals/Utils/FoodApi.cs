using System;
using System.Collections.Generic;
using FI.Data.Models.Meals;
using FI.Data.Models.Meals.Types;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace FI.Business.Meals.Utils
{
    public class FoodApi
    {
        private const string URL = "https://api.edamam.com/api/recipes/v2";
        private const string APP_ID = "2c18b86b";
        private const string API_KEY = "daba27e6aa68e401c49078d9aabf3eee";

        private MealPreferences _preferences;

        public FoodApi(MealPreferences preferences)
        {
            _preferences = preferences;
        }

        public ICollection<Meal> getBreakfastMeals()
        {
            return getMeals("Breakfast");
        }

        public ICollection<Meal> getLunchMeals()
        {
            return getMeals("Lunch");
        }

        public ICollection<Meal> getDinnerMeals()
        {
            return getMeals("Dinner");
        }

        public ICollection<Meal> getSnackMeals()
        {
            return getMeals("Snack");
        }

        public ICollection<Meal> getMeals(string type)
        {
            var client = new RestClient(URL);


            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.AddQueryParameter("app_id", APP_ID);
            request.AddQueryParameter("app_key", API_KEY);
            request.AddQueryParameter("type", "public");
            request.AddQueryParameter("q", "\\.");
            request.AddQueryParameter("random", "true");
            request.AddQueryParameter("health", "alcohol-free");
            request.AddQueryParameter("dishType", "Biscuits and cookies");
            request.AddQueryParameter("dishType", "Bread");
            request.AddQueryParameter("dishType", "Cereals");
            request.AddQueryParameter("dishType", "Main course");
            request.AddQueryParameter("dishType", "Pancake");
            request.AddQueryParameter("dishType", "Preps");
            request.AddQueryParameter("dishType", "Salad");
            request.AddQueryParameter("dishType", "Sandwiches");
            request.AddQueryParameter("dishType", "Side dish");
            request.AddQueryParameter("dishType", "Soup");
            request.AddQueryParameter("mealType", type);

            if (_preferences.Type != "general")
            {
                request.AddQueryParameter("health", _preferences.Type);
            }

            foreach (string excludedFood in _preferences.ExcludedFoods)
            {
                request.AddQueryParameter("health", excludedFood);
            }

            var apiResponse = client.ExecuteGetAsync(request).Result;

            var meals = new List<Meal>();

            var response = JObject.Parse(apiResponse.Content);


            var receipes = (JArray)response["hits"];

            foreach (JObject receipe in receipes)
            {
                var recipe = (JObject)receipe["recipe"];
                var name = (string)recipe["label"];
                var image = (string)recipe["image"];

                var apiCautions = (JArray)recipe["cautions"];
                var cautions = new List<Caution>();
                foreach (string caution in apiCautions)
                {
                    cautions.Add(new Caution { Name = caution });
                }

                var apiCuisineTypes = (JArray)recipe["cuisineType"];
                var cuisineTypes = new List<CuisineType>();
                foreach (string cuisineType in apiCuisineTypes)
                {
                    cuisineTypes.Add(new CuisineType { Name = cuisineType });
                }

                var apiMealTypes = (JArray)recipe["mealType"];
                var mealTypes = new List<MealType>();
                foreach (string mealType in apiMealTypes)
                {
                    mealTypes.Add(new MealType { Name = mealType });
                }


                var apiDishType = (JArray)recipe["dishType"];
                var dishTypes = new List<DishType>();
                if (apiDishType != null)
                {
                    foreach (string dishType in apiDishType)
                    {
                        dishTypes.Add(new DishType { Name = dishType });
                    }
                }

                var ingredients = new List<Ingredient>();
                var apiIngredients = (JArray)recipe["ingredients"];
                foreach (JObject ingredient in apiIngredients)
                {
                    ingredients.Add(new Ingredient
                    {
                        Text = (string)ingredient["text"],
                        Category = (string)ingredient["foodCategory"],
                        Quantity = (double)ingredient["quantity"],
                        Weight = (double)ingredient["weight"],
                        Unit = (string)ingredient["measure"]

                    });
                }

                var nutrients = new List<Nutrient>();


                var apiNutrients = (JObject)recipe["totalNutrients"];

                var calories = (JObject)apiNutrients["ENERC_KCAL"];
                var fat = (JObject)apiNutrients["FAT"];
                var protein = (JObject)apiNutrients["PROCNT"];
                var carb = (JObject)apiNutrients["CHOCDF"];

                nutrients.Add(new Nutrient
                {
                    Name = (string)calories["label"],
                    Quantity = (double)calories["quantity"],
                    Unit = (string)calories["unit"]

                });

                nutrients.Add(new Nutrient
                {
                    Name = (string)fat["label"],
                    Quantity = (double)fat["quantity"],
                    Unit = (string)fat["unit"]

                });

                nutrients.Add(new Nutrient
                {
                    Name = (string)protein["label"],
                    Quantity = (double)protein["quantity"],
                    Unit = (string)protein["unit"]

                });

                nutrients.Add(new Nutrient
                {
                    Name = (string)carb["label"],
                    Quantity = (double)carb["quantity"],
                    Unit = (string)carb["unit"]

                });

                meals.Add(new Meal
                {
                    Name = name,
                    ImageURL = image,
                    Ingredients = ingredients,
                    Nutrients = nutrients,
                    DishTypes = dishTypes,
                    MealTypes = mealTypes,
                    CuisineTypes = cuisineTypes,
                    Cautions = cautions
                }); ;
            }

            //string nextURI = (string)response["_links"]["next"]["href"];
            //var nextClient = new RestClient(nextURI);
            //var nextRequest = new RestRequest();
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Accept", "application/json");
            //var nextPage = nextClient.ExecuteGetAsync(nextRequest).Result;
            //response = JObject.Parse(nextPage.Content);

            return meals;
        }
    }
}
