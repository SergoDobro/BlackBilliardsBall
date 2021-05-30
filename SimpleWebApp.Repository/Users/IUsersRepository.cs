namespace WebApplication1_2021_02_17_secondASPNET.Repository.Users
{
    public interface IUsersRepository
    {
        void CreateUser(string login, string password, string email, UserRole roles);
        User GetUser(string email);
        User GetUserByLogin(string login);
        void UpdateUser(string login, string email, UserRole role);
        void DeleteUser(string email);
        bool DoesUserExist(string login);
    }

    public enum UserRole
    {
        NoOne,
        User,
        Admin
    }
}