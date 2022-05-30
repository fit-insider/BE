using FI.Data.Models.Meals;
using MediatR;

namespace FI.Business.Meals.Queries
{
    public class GetMealplanQuery : IRequest<Mealplan>
    {
        public string UserId { get; set; }
        public string MealplanId { get; set; }
    }
}
