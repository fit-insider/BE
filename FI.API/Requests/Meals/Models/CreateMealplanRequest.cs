using FluentValidation;

namespace FI.API.Requests.Meals
{
    public class CreateMealplanRequest
    {
        public string UserId { get; set; }
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

    public class CreateMealplanRequestValidator : AbstractValidator<CreateMealplanRequest>
    {
        public CreateMealplanRequestValidator()
        {
            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Invalid gender!")
                .Matches(@"^(male|female)$").WithMessage("Invalid gender!");

            RuleFor(x => x.Height)
               .GreaterThanOrEqualTo(54).WithMessage("Invalid height!")
               .When(x => x.HeightUnit == "CM", ApplyConditionTo.CurrentValidator)
               .LessThanOrEqualTo(272).WithMessage("Invalid height!")
               .When(x => x.HeightUnit == "CM", ApplyConditionTo.CurrentValidator)
               .GreaterThanOrEqualTo(1.7).WithMessage("Invalid height!")
               .When(x => x.HeightUnit == "FEET", ApplyConditionTo.CurrentValidator)
               .LessThanOrEqualTo(8.93).WithMessage("Invalid height!")
               .When(x => x.HeightUnit == "FEET", ApplyConditionTo.CurrentValidator);

            RuleFor(x => x.Weight)
               .GreaterThanOrEqualTo(20).WithMessage("Invalid weight!")
               .When(x => x.WeightUnit == "KG", ApplyConditionTo.CurrentValidator)
               .LessThanOrEqualTo(635).WithMessage("Invalid weight!")
               .When(x => x.WeightUnit == "KG", ApplyConditionTo.CurrentValidator)
               .GreaterThanOrEqualTo(44).WithMessage("Invalid weight!")
               .When(x => x.WeightUnit == "LBS", ApplyConditionTo.CurrentValidator)
               .LessThanOrEqualTo(1400).WithMessage("Invalid weight!")
               .When(x => x.WeightUnit == "LBS", ApplyConditionTo.CurrentValidator);

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(16).WithMessage("Invalid age!")
                .LessThanOrEqualTo(70).WithMessage("Invalid age!");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Invalid body type!")
                .Matches(@"^(ectomorph|mesomorph|endomorph)$").WithMessage("Invalid body type!");

            RuleFor(x => x.UsualActivity)
              .GreaterThanOrEqualTo(1).WithMessage("Invalid usual activity!")
              .LessThanOrEqualTo(4).WithMessage("Invalid usual activity!");

            RuleFor(x => x.PhisicalActivity)
                .GreaterThanOrEqualTo(0).WithMessage("Invalid phisical activity!")
                .LessThanOrEqualTo(8).WithMessage("Invalid phisical activity!");

            RuleFor(x => x.Sleep)
                .GreaterThanOrEqualTo(4).WithMessage("Invalid sleep!")
                .LessThanOrEqualTo(10).WithMessage("Invalid sleep!");

            RuleFor(x => x.WaterIntake)
             .GreaterThanOrEqualTo(0).WithMessage("Invalid water intake!")
             .LessThanOrEqualTo(3).WithMessage("Invalid water intake!");

            RuleFor(x => x.MealplanType)
               .NotEmpty().WithMessage("Invalid mealplan type!")
               .Matches(@"^(general|vegetarian|vegan)$").WithMessage("Invalid mealplan type!");

            RuleFor(x => x.MealsCount)
           .LessThanOrEqualTo(6).WithMessage("Invalid meals count!")
           .GreaterThanOrEqualTo(2).WithMessage("Invalid meals count!");
        }
    }
}
