using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Fiby.Cloud.Web.Persistence.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IDbConnection _connection;

        public ConnectionFactory(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }

        }

        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connection.ConnectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                if (_connection.State != ConnectionState.Connecting)

                {
                    _connection.Open();

                }

            }

            return _connection;
        }
    }
}
