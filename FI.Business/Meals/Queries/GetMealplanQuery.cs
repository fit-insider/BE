using FI.Data.Models.Meals.DTOs;
using MediatR;

namespace FI.Business.Meals.Queries
{
    public class GetMealplanQuery : IRequest<MealplanDTO>
    {
        public string UserId { get; set; }
        public string MealplanId { get; set; }
    }
}
