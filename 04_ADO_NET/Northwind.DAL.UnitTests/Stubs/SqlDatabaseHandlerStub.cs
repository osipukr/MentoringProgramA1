using System.Data;
using System.Data.SqlClient;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.UnitTests.Stubs
{
    public class SqlDatabaseHandlerStub : IDatabaseHandler
    {
        private const string CONNECTION_STRING = @"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;";

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(CONNECTION_STRING);
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new SqlCommand(commandText, (SqlConnection)connection)
            {
                CommandType = commandType
            };
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection, IDbTransaction transaction)
        {
            var command = CreateCommand(commandText, commandType, connection);

            command.Transaction = transaction;

            return command;
        }

        public IDbDataParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        public void CloseConnection(IDbConnection connection)
        {
            connection.Close();
        }
    }
}