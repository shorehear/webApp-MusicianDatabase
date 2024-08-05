namespace MusiciansAPI.Database
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; }
    }
}
