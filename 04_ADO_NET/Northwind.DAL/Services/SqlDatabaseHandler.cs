using System;
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

        public void CloseConnection(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            ((SqlConnection)connection).Close();
        }
    }
}