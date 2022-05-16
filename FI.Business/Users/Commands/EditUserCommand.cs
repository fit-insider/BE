using FI.Business.Users.Models;
using MediatR;

namespace FI.Business.Users.Commands
{
    public class EditUserCommand : IRequest<UserDetail>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
