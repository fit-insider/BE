using FluentValidation;

namespace FI.API.Requests.Users
{
    public class EditUserRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(x => x.FirstName)
             .NotEmpty().WithMessage("First name can not be empty!")
             .Matches(@"^[A-Za-z- ]{1,99}[a-z]$")
             .WithMessage("First name should have between 2 and 100 alpha characters!");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name can not be empty!")
                .Matches(@"^[A-Za-z- ]{1,99}[a-z]$")
                .WithMessage("Last name should have between 2 and 100 alpha characters!");
        }
    }
}
