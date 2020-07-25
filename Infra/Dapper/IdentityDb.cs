using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Infra.Dapper
{
    public class IdentityDb : IDisposable
    {
        private MySqlConnection _sqlConnection;

        public MySqlConnection SqlConnection { get { return _sqlConnection; } }

        public IdentityDb(string connectionString)
        {
            _sqlConnection = new MySqlConnection(connectionString);
        }


        public void Dispose()
        {
            _sqlConnection.Close();
        }
    }
}