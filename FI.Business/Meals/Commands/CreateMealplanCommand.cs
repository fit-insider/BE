using FI.Data.Models.Meals;
using MediatR;

namespace FI.Business.Meals.Commands
{
    public class CreateMealplanCommand : IRequest<Mealplan>
    {
        public int UserId { get; set; }
        public string Gender { get; set; }
        public string Target { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public string HeightUnit { get; set; }
        public string WeightUnit { get; set; }
        public string Body { get; set; }
        public int UsualActivity { get; set; }
        public int PhisicalActivity { get; set; }
        public int Sleep { get; set; }
        public int WaterIntake { get; set; }
        public string MealplanType { get; set; }
        public int MealsCount { get; set; }
        public string[] ExcludedFoods { get; set; }
    }
}
