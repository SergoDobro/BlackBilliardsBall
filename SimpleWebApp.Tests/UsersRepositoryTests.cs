using WebApplication1_2021_02_17_secondASPNET.Repository.Users;
using Xunit;

namespace SimpleWebApp.Tests
{
    public class UsersRepositoryTests
    {
        [Fact]
        public void CreateUserTest()
        {
            UsersDatabaseRepository repository = new UsersDatabaseRepository();
            repository.CreateUser("somelogin", "password", "somemail@mail.com", UserRole.User);
        }

        [Fact]
        public void GetUserTest()
        {
            UsersDatabaseRepository repository = new UsersDatabaseRepository();
            var user = repository.GetUser("somemail@mail.com");
        }

        [Fact]
        public void UpdateUserTest()
        {
            UsersDatabaseRepository repository = new UsersDatabaseRepository();
            repository.UpdateUser("login", "somemail@mail.com", UserRole.Admin);
        }

        [Fact]
        public void DeleteUserTest()
        {
            UsersDatabaseRepository repository = new UsersDatabaseRepository();
            repository.DeleteUser("somemail@mail.com");
        }
    }
}