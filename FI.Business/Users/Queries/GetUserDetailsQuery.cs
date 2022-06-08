using FI.Business.Users.Models;
using MediatR;

namespace FI.Business.Users.Queries
{
    public class GetUserDetailsQuery : IRequest<UserIdentifier>
    {
        public string UserId { get; set; }
    }
}
