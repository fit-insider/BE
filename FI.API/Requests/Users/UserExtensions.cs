using FI.Business.Users.Commands;
using FI.Business.Users.Queries;

namespace FI.API.Requests.Users
{
    public static class UserExtensions
    {
        public static AddUserCommand ToCommand(this AddUserRequest request)
        {
            return new AddUserCommand
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
        }

        public static EditUserCommand ToCommand(this EditUserRequest request)
        {
            return new EditUserCommand
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
        }

        public static ChangePasswordCommand ToCommand(this ChangePasswordRequest request)
        {
            return new ChangePasswordCommand
            {
                Id = request.Id,
                OldPassword = request.OldPassword,
                NewPassword = request.NewPassword,
                ConfirmPassword = request.ConfirmPassword

            };
        }

        public static LoginQuery ToQuery(this LoginRequest request)
        {
            return new LoginQuery
            {
                Identifier = request.Identifier,
                Password = request.Password
            };
        }
    }
}
