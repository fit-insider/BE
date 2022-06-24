using FluentValidation;

namespace FI.API.Requests.Users
{
    public class ChangePasswordRequest
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Old password name can not be empty!");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Old password name can not be empty!");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Old password name can not be empty!");

            When(x => x.OldPassword != "" || x.NewPassword != "" || x.ConfirmPassword != "", () =>
            {
                RuleFor(x => x.OldPassword)
                    .NotEmpty().WithMessage("You must complete all 3 fields if one changed!");
                RuleFor(x => x.NewPassword)
                    .NotEmpty().WithMessage("You must complete all 3 fields if one changed!");
                RuleFor(x => x.ConfirmPassword)
                    .NotEmpty().WithMessage("You must complete all 3 fields if one changed!");

                RuleFor(x => x.NewPassword)
                    .NotEqual(x => x.OldPassword)
                    .WithMessage("New password must be different from Old!")
                    .Equal(x => x.ConfirmPassword)
                    .WithMessage("New and confirm passwords must match!");
            });
        }
    }
}
