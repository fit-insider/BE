using FluentValidation;

namespace FI.API.Requests.Users
{
    public class AddUserRequest : BaseUserRequest
    {
        public string Password { get; set; }
    }

    public class AddUserRequestValidator : BaseUserRequestValidator<AddUserRequest>
    {
        public AddUserRequestValidator() : base()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can not be empty!")
                .Length(2, 20).WithMessage("Password should have between 2 and 20 characters!");
        }
    }
}
