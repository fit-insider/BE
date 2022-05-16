using FI.Data.Models.Meals;
using MediatR;

namespace FI.Business.Meals.Queries
{
    public class GetMealplanQuery : IRequest<Mealplan>
    {
        public int UserId { get; set; }
        public int MealplanId { get; set; }
    }
}
