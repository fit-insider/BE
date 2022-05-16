namespace FI.Infrastructure.Models.Exceptions
{
    public enum ErrorCode : uint
    {
        GET_VERSION = 99,
        AddUser_Email = 100,

        EditUser_User = 200,

        EditUser_Password = 202,
        Login_Credentials = 300,

        GetUserDetails_User = 400,

        CreateMealplan_user = 501
    }
}
