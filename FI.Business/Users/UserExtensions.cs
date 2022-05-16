using System.Linq;
using FI.Business.Users.Commands;
using BusinessModel = FI.Business.Users.Models;
using DataModel = FI.Data.Models.Users;

namespace FI.Business.Users
{
    public static class UserExtensions
    {
        public static DataModel.User ToUser(this AddUserCommand command)
        {
            return new DataModel.User
            {
                Email = command.Email,
                Detail = new DataModel.UserDetail
                {
                    Password = command.Password,
                    FirstName = command.FirstName,
                    LastName = command.LastName
                }
            };
        }

        public static BusinessModel.UserDetail ToUserDetails(this EditUserCommand command)
        {
            return new BusinessModel.UserDetail
            {
                UserId = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName
            };
        }

        public static IQueryable<BusinessModel.UserIdentifier> ToUserIdentifier(this IQueryable<DataModel.User> query)
        {
            return query.Select(user=>new BusinessModel.UserIdentifier
            {
                Email = user.Email,
                Detail = new BusinessModel.UserDetail
                {
                    UserId = user.Id,
                    FirstName = user.Detail.FirstName,
                    LastName = user.Detail.LastName
                }
            });
        }

        public static BusinessModel.UserDetail ToUserDetail(this DataModel.User user)
        {
            return new BusinessModel.UserDetail
            {
                UserId = user.Id,
                FirstName = user.Detail.FirstName,
                LastName = user.Detail.LastName
            };
        }
    }
}
