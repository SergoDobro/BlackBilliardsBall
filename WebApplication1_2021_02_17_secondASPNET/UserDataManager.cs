using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1_2021_02_17_secondASPNET.Repository.Users;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public class UserDataManager
    {
        IUsersRepository usersRepository;
        public UserDataManager(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        public void RegistrateUser(UserData userData) => usersRepository.CreateUser(userData.Login, userData.Password, userData.Email, userData.Role);
        public UserData GetUser(string login)
        {
            User user = usersRepository.GetUserByLogin(login);
            if (user == null) return null;
            UserData data = new UserData(user);

            return data;
        }

        public UserRole GetRole(string login)
        {
            User user = usersRepository.GetUserByLogin(login);
            if (user == null) return UserRole.NoOne;
            UserData data = new UserData(user);

            return data.Role;
        }
    }
}
