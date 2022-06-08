using FI.Business.Users.Models;
using MediatR;

namespace FI.Business.Users.Commands
{
    public class EditUserCommand : IRequest<UserDetail>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
