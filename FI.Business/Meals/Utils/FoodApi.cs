using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using FI.Data;
using FI.Data.Models.Meals;
using FI.Data.Models.Meals.Base;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace FI.Business.Meals.Utils
{
    public class FoodApi
    {
        private readonly FIContext _context;
        private const string URL = "https://api.edamam.com/api/recipes/v2";
        private const string APP_ID = "2c18b86b";
        private const string API_KEY = "daba27e6aa68e401c49078d9aabf3eee";

        public FoodApi(FIContext context)
        {
            _context = context;
        }

        public void getApiMeals()
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
            request.AddQueryParameter("cuisineType", "American");
            request.AddQueryParameter("cuisineType", "British");
            request.AddQueryParameter("cuisineType", "Central Europe");
            request.AddQueryParameter("cuisineType", "Eastern Europe");
            request.AddQueryParameter("cuisineType", "French");
            request.AddQueryParameter("cuisineType", "Italian");
            request.AddQueryParameter("cuisineType", "Mediterranean");

            int i = 1;

            for (int j = 1; j < 500; j++)
            {
                var apiResponse = client.ExecuteGetAsync(request).Result;
                i++;
                wait(i, _context);

                var response = JObject.Parse(apiResponse.Content);

                var receipes = (JArray)response["hits"];

                foreach (JObject receipe in receipes)
                {
   
                    var recipe = (JObject)receipe["recipe"];
                    var name = (string)recipe["label"];
                    var images = (JObject)recipe["images"];
                    var image = (JObject)images["SMALL"];
                    var imageURL = (string)image["url"];
                    byte[] imageData;

                    using (WebClient webClient = new WebClient())
                    {
                        imageData = webClient.DownloadData(imageURL);
                    }
                    //i++;
                    //wait(i, _context);

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

                    var ingredients = new List<BaseIngredient>();
                    var apiIngredients = (JArray)recipe["ingredients"];
                    foreach (JObject ingredient in apiIngredients)
                    {
                        ingredients.Add(new BaseIngredient
                        {
                            Id = Guid.NewGuid().ToString(),
                            Text = (string)ingredient["text"],
                            Category = (string)ingredient["foodCategory"],
                            Quantity = (double)ingredient["quantity"],
                            Weight = (double)ingredient["weight"],
                            Unit = (string)ingredient["measure"]
                        });
                    }

                    var nutrients = new List<BaseNutrient>();


                    var apiNutrients = (JObject)recipe["totalNutrients"];

                    var calories = (JObject)apiNutrients["ENERC_KCAL"];
                    var fat = (JObject)apiNutrients["FAT"];
                    var protein = (JObject)apiNutrients["PROCNT"];
                    var carb = (JObject)apiNutrients["CHOCDF"];

                    nutrients.Add(new BaseNutrient
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = (string)calories["label"],
                        Quantity = (double)calories["quantity"],
                        Unit = (string)calories["unit"]

                    });

                    nutrients.Add(new BaseNutrient
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = (string)fat["label"],
                        Quantity = (double)fat["quantity"],
                        Unit = (string)fat["unit"]

                    });

                    nutrients.Add(new BaseNutrient
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = (string)protein["label"],
                        Quantity = (double)protein["quantity"],
                        Unit = (string)protein["unit"]

                    });

                    nutrients.Add(new BaseNutrient
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = (string)carb["label"],
                        Quantity = (double)carb["quantity"],
                        Unit = (string)carb["unit"]

                    });

                    BaseMeal mm = new BaseMeal
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

                    _context.BaseMeals.Add(mm);
                }
            }
        }

        public void wait(int i, FIContext context)
        {
            if (i % 9 == 0)
            {
                context.SaveChanges();
                Thread.Sleep(61000);
            }
        }

    }
}
