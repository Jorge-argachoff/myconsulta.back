using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infra.Dapper
{
    public class ConsultaDb : IDisposable
    {
        private SqlConnection _sqlConnection;

        public SqlConnection SqlConnection { get { return _sqlConnection; } }

        public ConsultaDb(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }


        public void Dispose()
        {
            _sqlConnection.Close();
        }
    }
}
