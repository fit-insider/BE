using FI.Business.Users.Models;
using FI.Business.Users.Queries;
using FI.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FI.Infrastructure.Models.Exceptions;

namespace FI.Business.Users.Handlers
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserIdentifier>
    {
        private readonly FIContext _context;

        public GetUserDetailsQueryHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<UserIdentifier> Handle(GetUserDetailsQuery request, CancellationToken token)
        {
            var result = await _context.Users
                .Include(user => user.Detail)
                .Where(user => user.Id == request.UserId)
                .ToUserIdentifier()
                .FirstOrDefaultAsync(token);

            if(result is null)
            {
                throw new CustomException(ErrorCode.GetUserDetails_User, "User does not exist!");
            }

            return result;
        }
    }
}
