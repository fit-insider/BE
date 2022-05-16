using MediatR;

namespace FI.Business.Users.Commands
{
    public class AddUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
