using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FI.Business.Users.Commands;
using FI.Data;
using FI.Infrastructure.Models.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BusinessModel = FI.Business.Users.Models;
using DataModel = FI.Data.Models.Users;


namespace FI.Business.Users.Handlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, BusinessModel.Identifier>
    {
        private readonly FIContext _context;
        private DataModel.User _dbUser;

        public ChangePasswordCommandHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<BusinessModel.Identifier> Handle(ChangePasswordCommand command, CancellationToken token)
        {
            await ValidateIfUserExists(command.Id);

            UpdateUserPassword(command);

            _context.Users.Update(_dbUser);
            await _context.SaveChangesAsync(token);

            return command.ToIdentifier();
        }

        private void UpdateUserPassword(ChangePasswordCommand command)
        {
            if (command.OldPassword is null or "") return;

            ValidateIfOldAndExistingPasswordsMatch(command.OldPassword);
            _dbUser.Detail.Password = PasswordUtils.Hash(command.NewPassword);
        }

        private async Task ValidateIfUserExists(string userId)
        {
            _dbUser = await _context.Users
                .Include(u => u.Detail)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (_dbUser is null)
            {
                throw new CustomException(ErrorCode.EditUser_User, "User does not exist!");
            }
        }

        private void ValidateIfOldAndExistingPasswordsMatch(string oldPassword)
        {
            if (!PasswordUtils.Verify(_dbUser.Detail.Password, oldPassword))
            {
                throw new CustomException(ErrorCode.EditUser_Password, "Old password must match with existing!");
            }
        }

    }
}
