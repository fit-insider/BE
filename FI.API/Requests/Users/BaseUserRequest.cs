using FluentValidation;

namespace FI.API.Requests.Users
{
    public class BaseUserRequest
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class BaseUserRequestValidator<T> : AbstractValidator<T> where T : BaseUserRequest
    {
        public BaseUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can not be empty!")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
                .WithMessage("Email should have a valid format!")
                .Length(7, 74).WithMessage("Email should have between 7 and 74 characters!");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name can not be empty!")
                .Matches(@"^[A-Za-z-]{1,99}[a-z]$")
                .WithMessage("First name should have between 2 and 100 alpha characters!");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name can not be empty!")
                .Matches(@"^[A-Za-z-]{1,99}[a-z]$")
                .WithMessage("Last name should have between 2 and 100 alpha characters!");
        }
    }
}
