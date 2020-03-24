using System;
using System.Data;
using System.Data.SqlClient;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.UnitTests.Stubs
{
    public class SqlDatabaseHandlerStub : IDatabaseHandler, IDisposable
    {
        private const string CONNECTION_STRING = @"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;";

        private readonly IDbConnection _connection;

        public SqlDatabaseHandlerStub()
        {
            _connection = new SqlConnection(CONNECTION_STRING);
        }

        public IDbConnection CreateConnection()
        {
            return _connection;
        }

        public void CloseConnection(IDbConnection connection)
        {
            connection?.Close();
        }

        public void Dispose()
        {
            CloseConnection(_connection);
        }
    }
}