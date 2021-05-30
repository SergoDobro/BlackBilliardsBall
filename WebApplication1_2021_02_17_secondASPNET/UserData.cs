using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1_2021_02_17_secondASPNET.Repository.Users;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public class UserData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public UserData()
        {
        }
        public UserData(User user)
        {
            Login = user.Login;
            Password = user.Password;
            Email = user.Email;
            Role = user.userRole;
        }
        public UserData(UserRegistrationData registrationData)
        {
            Login = registrationData.Login;
            Password = registrationData.Password;
            Email = registrationData.Email;
        }
        public UserData(UserRegistrationData registrationData, UserRole role)
        {
            Login = registrationData.Login;
            Password = registrationData.Password;
            Email = registrationData.Email;
            Role = role;
        }
    }
    public class UserRegistrationData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRegistrationData()
        {
        }
    }
}
