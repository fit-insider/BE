using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FI.Business.Users.Commands;
using FI.Data;
using FI.Infrastructure.Models.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FI.Business.Users.Handlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly FIContext _context;

        public AddUserCommandHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddUserCommand request, CancellationToken token)
        {
            await ValidateIfUserExists(request.Email);

            _context.Users.Add(request.ToUser());

            await _context.SaveChangesAsync(token);
            return true;
        }

        private async Task ValidateIfUserExists(string email)
        {
            var users = await _context.Users
                .Include(x => x.Detail)
                .Where(u => u.Email == email)
                .ToListAsync();

            if (users == null)
            {
                return;
            }
            if (users.Any(u => u.Email == email))
            {
                throw new CustomException(ErrorCode.AddUser_Email, "Email should be unique");
            }
        }
    }
}
