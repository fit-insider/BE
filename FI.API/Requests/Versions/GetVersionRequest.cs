using FluentValidation;

namespace FI.API.Requests.Versions
{
    public class GetVersionRequest
    {
        public string Version { get; set; }
    }

    public class GetVersionRequestValidator : AbstractValidator<GetVersionRequest>
    {
        public GetVersionRequestValidator()
        {
            RuleFor(x => x.Version).NotEmpty();
        }
    }
}
