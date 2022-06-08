using FluentValidation;

namespace FI.API.Requests.Meals.Models
{
    public class ExportMealplanRequest
    {
        public string UserId { get; set; }
        public string MealplanId { get; set; }
    }

    public class ExportMealplanRequestValidator : AbstractValidator<ExportMealplanRequest>
    {
        public ExportMealplanRequestValidator()
        {
            RuleFor(x => x.MealplanId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
