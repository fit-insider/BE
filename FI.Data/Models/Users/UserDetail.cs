namespace FI.Data.Models.Users
{
    public class UserDetail
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public User User { get; set; }
    }
}
