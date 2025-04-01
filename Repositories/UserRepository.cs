using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HRM_System.Models;
using MySql.Data.MySqlClient;

namespace HRM_System.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection()) // Ensure GetConnection() returns a MySqlConnection
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                // Query checks if a user exists with the given username and password
                command.CommandText = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                command.Parameters.AddWithValue("@Username", credential.UserName);
                command.Parameters.AddWithValue("@Password", credential.Password);

                // If ExecuteScalar returns non-null, a user was found
                validUser = command.ExecuteScalar() != null;
            }
            return validUser;
        }


        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
