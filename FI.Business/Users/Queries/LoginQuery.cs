using FI.Business.Users.Models;
using MediatR;

namespace FI.Business.Users.Queries
{
    public class LoginQuery : IRequest<UserDetail>
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
}