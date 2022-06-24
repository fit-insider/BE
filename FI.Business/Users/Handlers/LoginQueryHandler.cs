using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FI.Business.Users.Queries;
using FI.Data;
using FI.Data.Models.Users;
using FI.Infrastructure.Models.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BusinessModel = FI.Business.Users.Models;

namespace FI.Business.Users.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, BusinessModel.UserDetail>
    {
        private readonly FIContext _context;
        private User _user;

        public LoginQueryHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<BusinessModel.UserDetail> Handle(LoginQuery request, CancellationToken token)
        {
            _user = await FetchUser(request.Identifier, token);

            ValidateUser(request.Password);

            return _user.ToUserDetail();
        }

        private void ValidateUser(string password)
        {
            if (_user is null)
            {
                throw new CustomException(ErrorCode.Login_Credentials, "Wrong credentials.");
            }

            if(!PasswordUtils.Verify(_user.Detail.Password, password))
            {
                throw new CustomException(ErrorCode.Login_Credentials, "Wrong credentials.");
            }
        }

        private async Task<User> FetchUser(string identifier, CancellationToken token)
        {
            return await _context.Users
                .Include(u => u.Detail)
                .Where(u => (identifier == u.Email))
                .FirstOrDefaultAsync(token);
        }
    }
}
