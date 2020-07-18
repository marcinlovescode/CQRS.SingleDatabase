using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.SqlConnections
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            if (ConnectionOpened()) _connection.Dispose();
        }

        public IDbConnection GetConnection()
        {
            if (ConnectionOpened()) return _connection;
            _connection = new SqlConnection(_connectionString);
            _connection.Open();

            return _connection;
        }

        private bool ConnectionOpened()
        {
            return _connection != null && _connection.State == ConnectionState.Open;
        }
    }
}