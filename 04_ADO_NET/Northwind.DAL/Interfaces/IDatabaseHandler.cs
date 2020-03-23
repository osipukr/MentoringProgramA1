using System.Data;

namespace Northwind.DAL.Interfaces
{
    public interface IDatabaseHandler
    {
        IDbConnection CreateConnection();
        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);
        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection, IDbTransaction transaction);
        IDbDataParameter CreateParameter(string name, object value);
        void CloseConnection(IDbConnection connection);
    }
}