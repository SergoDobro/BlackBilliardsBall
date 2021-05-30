namespace WebApplication1_2021_02_17_secondASPNET.Repository.Users
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole userRole { get; set; }
    }
}