using FI.API.Requests.Meals;
using FI.Business.Meals.Commands;
using FI.Business.Meals.Queries;

namespace FI.API.Requests.Meals
{
    public static class MealplanExtensions
    {
        public static CreateMealplanCommand ToCommand(this CreateMealplanRequest request)
        {
            return new CreateMealplanCommand
            {
                UserId = request.UserId,
                Gender = request.Gender,
                Target = request.Target,
                Height = request.Height,
                Weight = request.Weight,
                Age = request.Age,
                HeightUnit = request.HeightUnit,
                WeightUnit = request.WeightUnit,
                Body = request.Body,
                UsualActivity = request.UsualActivity,
                PhisicalActivity = request.PhisicalActivity,
                Sleep = request.Sleep,
                WaterIntake = request.WaterIntake,
                MealplanType = request.MealplanType,
                MealsCount = request.MealsCount,
                ExcludedFoods = request.ExcludedFoods
            };
        }

        public static GetMealplanQuery ToQuery(this GetMealplanRequest request)
        {
            return new GetMealplanQuery
            {
                UserId = request.UserId,
                MealplanId = request.MealplanId
            };
        }
        
    }
}
