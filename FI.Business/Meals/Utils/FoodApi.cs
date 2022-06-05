using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using FI.Data;
using FI.Data.Models.Meals;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace FI.Business.Meals.Utils
{
    public class FoodApi
    {
        private FIContext _context;
        private const string URL = "https://api.edamam.com/api/recipes/v2";
        private const string APP_ID = "2c18b86b";
        private const string API_KEY = "daba27e6aa68e401c49078d9aabf3eee";

        public FoodApi(FIContext context)
        {
            _context = context;
        }

        public ICollection<Meal> getApiMeals()
        {
            var client = new RestClient(URL);
            var request = new RestRequest();

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.AddQueryParameter("app_id", APP_ID);
            request.AddQueryParameter("app_key", API_KEY);
            request.AddQueryParameter("type", "public");
            request.AddQueryParameter("q", "\\.");
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
            request.AddQueryParameter("cuisineType", "American");
            request.AddQueryParameter("cuisineType", "British");
            request.AddQueryParameter("cuisineType", "Central Europe");
            request.AddQueryParameter("cuisineType", "Eastern Europe");
            request.AddQueryParameter("cuisineType", "French");
            request.AddQueryParameter("cuisineType", "Italian");
            request.AddQueryParameter("cuisineType", "Mediterranean");

            var apiResponse = client.ExecuteGetAsync(request).Result;

            var meals = new List<Meal>();

            var response = JObject.Parse(apiResponse.Content);


            var receipes = (JArray)response["hits"];

            int i = 1;
            while ((int)response["from"] < (int)response["to"])
            {
                foreach (JObject receipe in receipes)
                {
                    var recipe = (JObject)receipe["recipe"];
                    var name = (string)recipe["label"];
                    var imageURL = (string)recipe["image"];
                    byte[] imageData;

                    using (WebClient webClient = new WebClient())
                    {
                        imageData = webClient.DownloadData(imageURL);
                    }

                    i++;
                    wait(i, _context);

                    var apiHealthLabels = (JArray)recipe["healthLabels"];
                    var healthLabels = new List<string>();
                    foreach (string label in apiHealthLabels)
                    {
                        healthLabels.Add(label);
                    }

                    var apiCautions = (JArray)recipe["cautions"];
                    var cautions = new List<string>();
                    foreach (string caution in apiCautions)
                    {
                        cautions.Add(caution);
                    }

                    var apiCuisineTypes = (JArray)recipe["cuisineType"];
                    var cuisineTypes = new List<string>();
                    foreach (string cuisineType in apiCuisineTypes)
                    {
                        cuisineTypes.Add(cuisineType);
                    }

                    var apiMealTypes = (JArray)recipe["mealType"];
                    var mealTypes = new List<string>();
                    foreach (string mealType in apiMealTypes)
                    {
                        mealTypes.Add(mealType);
                    }


                    var apiDishType = (JArray)recipe["dishType"];
                    var dishTypes = new List<string>();
                    if (apiDishType != null)
                    {
                        foreach (string dishType in apiDishType)
                        {
                            dishTypes.Add(dishType);
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

                    Meal mm = new Meal
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = name,
                        ImageData = imageData,
                        Ingredients = ingredients,
                        Nutrients = nutrients,
                        HealthLabels = string.Join(";", healthLabels),
                        DishTypes = string.Join(";", dishTypes),
                        MealTypes = string.Join(";", mealTypes),
                        CuisineTypes = string.Join(";", cuisineTypes),
                        Cautions = string.Join(";", cautions)
                    };

                    _context.Meals.Add(mm);
                }

                string nextURI = (string)response["_links"]["next"]["href"];
                var nextClient = new RestClient(nextURI);
                var nextRequest = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                var nextPage = nextClient.ExecuteGetAsync(nextRequest).Result;
                response = JObject.Parse(nextPage.Content);

                i++;
                wait(i, _context);
            }

            return meals;

        }

        public void wait(int i, FIContext context)
        {
            if (i % 10 == 0)
            {
                context.SaveChanges();
                Thread.Sleep(61000);
            }
        }

    }
}
