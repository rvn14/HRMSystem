using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HRM_System.Models
{
    internal class DBManager : IDisposable
    {
        private readonly string _dbName;
        private readonly string[] _tableNames = { "users", };
        private MySqlConnection conn = null;

        public DBManager(Key key) {
            // if auth
            conn = new MySqlConnection();
            conn.ConnectionString = deObfuscateConnStr();
            conn.Open();
        }

        public void Dispose()
        {
            conn?.Close();
            conn?.Dispose();
            conn = null;
        }

        

        private string deObfuscateConnStr()
        {
            return "Server=127.0.0.1;Database=voltexdb;Uid=root;"; // This can be improved to make the database more secure.
        }
    }
}
