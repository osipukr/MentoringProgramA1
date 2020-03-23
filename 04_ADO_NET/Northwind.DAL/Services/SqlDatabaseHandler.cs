using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Services
{
    /// <summary>
    /// Representation of <see cref="IDatabaseHandler"/> for the SQL Database.
    /// </summary>
    public class SqlDatabaseHandler : IDatabaseHandler
    {
        private readonly string _connectionString;

        public SqlDatabaseHandler(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (string.IsNullOrWhiteSpace(commandText))
            {
                throw new ArgumentException(string.Empty, nameof(commandText));
            }

            if (!Enum.IsDefined(typeof(CommandType), commandType))
            {
                throw new InvalidEnumArgumentException(nameof(commandType), (int) commandType, typeof(CommandType));
            }

            var sqlConnection = (SqlConnection)connection;

            return new SqlCommand(commandText, sqlConnection)
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
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            var sqlConnection = (SqlConnection)connection;

            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}