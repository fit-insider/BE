using FluentValidation;

namespace FI.API.Requests.Meals
{
    public class GetMealplanRequest
    {
        public int UserId { get; set; }
        public int MealplanId { get; set; }
    }

    public class GetMealplanRequestValidator : AbstractValidator<GetMealplanRequest>
    {
        public GetMealplanRequestValidator()
        {
            RuleFor(x => x.MealplanId)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
