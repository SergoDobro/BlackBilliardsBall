using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace WebApplication1_2021_02_17_secondASPNET.Repository.Users
{
    public class UsersDatabaseRepository : IUsersRepository
    {
        const string ConnectionString = "Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;";

        public void CreateUser(string login, string password, string email, UserRole role)
        {
            using (IDbConnection db = new MySqlConnection(ConnectionString))
            {
                string sqlQuery = "INSERT INTO users (login, password, email, userRole) Values(@login, @password, @email, @role)";

                int rowsAffected = db.Execute(sqlQuery, new { login, password, email, role = role.ToString() });
            }
        }

        public void DeleteUser(string email)
        {
            using (IDbConnection db = new MySqlConnection(ConnectionString))
            {
                db.Query<PredictionDto>("DELETE FROM users WHERE email = @email", new { email }).ToList();
            }
        }

        public bool DoesUserExist(string login)
        {
            using (IDbConnection db = new MySqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT login, password, email, userRole FROM users WHERE email = @email";
                User user = db.QueryFirstOrDefault<User>(sqlQuery, new { login });
                return user.Email != null && user.Login != null && user.Password != null;
            }
        }

        public User GetUser(string email)
        {
            using (IDbConnection db = new MySqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT login, password, email, userRole FROM users WHERE email = @email";

                return db.QueryFirst<User>(sqlQuery, new { email });
            }
        }
        public User GetUserByLogin(string login)
        {
            using (IDbConnection db = new MySqlConnection(ConnectionString))
            {
                string sqlQuery = "SELECT login, password, email, userRole FROM users WHERE login = @login";

                User user = db.QueryFirstOrDefault<User>(sqlQuery, new { login });
                return user!=null && user.Email != null && user.Login != null && user.Password != null ? user : null;
            }
        }

        public void UpdateUser(string login, string email, UserRole role)
        {
            using (IDbConnection db = new MySqlConnection(ConnectionString))
            {
                db.Query<PredictionDto>("UPDATE users SET login = @login, email = @email, userRole = @role WHERE email = @email", new { login, email, role });
            }
        }
    }
}