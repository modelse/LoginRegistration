using System.Data;
using Dapper;
using loginReg.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace loginReg.Factories
{
    public class UserFactory : IFactory<User>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        
        public UserFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }

        internal IDbConnection Connection
        {
            get { return new MySqlConnection(MySqlConfig.Value.ConnectionString); }
        }

        public void Add(User Item)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = "INSERT into users (first_name, last_name, email, password, created_at, updated_at) VALUES (@first, @last, @email, @password, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(Query, Item);
            }
        }

        public User GetLatestUser()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = "SELECT * FROM users ORDER BY id DESC LIMIT 1";
                dbConnection.Open();
                return dbConnection.QuerySingleOrDefault<User>(Query);
            }
        }

        public User GetUserById(int Id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = $"SELECT * FROM users WHERE id = {Id}";
                dbConnection.Open();
                return dbConnection.QuerySingleOrDefault<User>(Query);
            }
        }

        public User GetuserByEmail(string Email)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = $"SELECT * FROM users WHERE Email = '{Email}'";
                dbConnection.Open();
                return dbConnection.QuerySingleOrDefault<User>(Query);
            }
        }
    }
}