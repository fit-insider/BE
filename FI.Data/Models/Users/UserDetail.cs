namespace FI.Data.Models.Users
{
    public class UserDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public User User { get; set; }
    }
}