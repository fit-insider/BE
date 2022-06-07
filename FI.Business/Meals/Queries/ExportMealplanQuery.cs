using MediatR;

namespace FI.Business.Meals.Queries
{
    public class ExportMealplanQuery : IRequest<byte[]>
    {
        public string UserId { get; set; }
        public string MealplanId { get; set; }
    }
}
