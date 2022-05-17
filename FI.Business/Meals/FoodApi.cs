using System.Collections.Generic;
using FI.Data.Models.Meals;
using FI.Data.Models.Meals.Types;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace FI.Business.Meals
{
    public class FoodApi
    {
        private const string URL = "https://api.edamam.com/api/recipes/v2";
        private const string APP_ID = "2c18b86b";
        private const string API_KEY = "daba27e6aa68e401c49078d9aabf3eee";

        public ICollection<Meal> getMeals()
        {
            var client = new RestClient(URL);


            var request = new RestRequest();
            request.AddQueryParameter("type", "public");
            request.AddQueryParameter("q", "chicken");
            request.AddQueryParameter("app_id", APP_ID);
            request.AddQueryParameter("app_key", API_KEY);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var apiResponse = client.ExecuteGetAsync(request).Result;


            var meals = new List<Meal>();

            JObject response = JObject.Parse(apiResponse.Content);
            JArray receipes = (JArray) response["hits"];
            foreach (JObject receipe in receipes)
            {
                JObject recipe = (JObject) receipe["recipe"];
                string name = (string) recipe["label"];
                string image = (string) recipe["image"];
   
                JArray apiCautions = (JArray) recipe["cautions"];
                List<Caution> cautions = new List<Caution>();
                foreach (string caution in apiCautions)
                {
                    cautions.Add(new Caution { Name = caution });
                }

                JArray apiCuisineTypes = (JArray) recipe["cuisineType"];
                List<CuisineType> cuisineTypes = new List<CuisineType>();
                foreach (string cuisineType in apiCuisineTypes)
                {
                    cuisineTypes.Add(new CuisineType { Name = cuisineType });
                }

                JArray apiMealTypes = (JArray) recipe["mealType"];
                List<MealType> mealTypes = new List<MealType>();
                foreach (string mealType in apiMealTypes)
                {
                    mealTypes.Add(new MealType { Name = mealType });
                }


                JArray apiDishType = (JArray) recipe["dishType"];
                List<DishType> dishTypes = new List<DishType>();
                foreach (string dishType in apiDishType)
                {
                    dishTypes.Add(new DishType { Name = dishType });
                }

                var ingredients = new List<Ingredient>();
                JArray apiIngredients = (JArray) recipe["ingredients"];
                foreach (JObject ingredient in apiIngredients)
                {
                    ingredients.Add(new Ingredient
                    {
                        Text = (string) ingredient["text"],
                        Category = (string) ingredient["foodCategory"],
                        Quantity = (double) ingredient["quantity"],
                        Weight = (double) ingredient["weight"],
                        Unit = (string) ingredient["measure"]

                    });
                }

                var nutrients = new List<Nutrient>();


                JObject apiNutrients = (JObject) recipe["totalNutrients"];

                JObject calories = (JObject) apiNutrients["ENERC_KCAL"];
                JObject fat = (JObject) apiNutrients["FAT"];
                JObject protein = (JObject) apiNutrients["PROCNT"];
                JObject carb = (JObject) apiNutrients["CHOCDF"];

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

            return meals;
        }
    }
}
