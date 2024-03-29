﻿using System;
using System.Linq;
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
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, BusinessModel.UserDetail>
    {
        private readonly FIContext _context;
        private DataModel.User _dbUser;

        public EditUserCommandHandler(FIContext context)
        {
            _context = context;
        }
        
        public async Task<BusinessModel.UserDetail> Handle(EditUserCommand command, CancellationToken token)
        {
            await ValidateIfUserExists(command.Id);
            
            UpdateUserDetails(command);
            
            _context.Users.Update(_dbUser);
            await _context.SaveChangesAsync(token);
            
            return command.ToUserDetails();
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

        private void UpdateUserDetails(EditUserCommand command)
        {
            _dbUser.Detail.FirstName = command.FirstName;
            _dbUser.Detail.LastName = command.LastName;
        }
    }
}
