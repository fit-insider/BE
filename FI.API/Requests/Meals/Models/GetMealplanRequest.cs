using FluentValidation;

namespace FI.API.Requests.Meals
{
    public class GetMealplanRequest
    {
        public string UserId { get; set; }
        public string MealplanId { get; set; }
    }

    public class GetMealplanRequestValidator : AbstractValidator<GetMealplanRequest>
    {
        public GetMealplanRequestValidator()
        {
            RuleFor(x => x.MealplanId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
