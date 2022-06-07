using FluentValidation;

namespace FI.API.Requests.Meals.Models
{
    public class CreateShoppingListRequest
    {
        public string UserId { get; set; }
        public string MealplanId { get; set; }
    }

    public class CreateShoppingListRequestValidator : AbstractValidator<CreateShoppingListRequest>
    {
        public CreateShoppingListRequestValidator()
        {
            RuleFor(x => x.MealplanId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
