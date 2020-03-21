using System.Data;

namespace Northwind.DAL.Interfaces
{
    public interface IDatabaseHandler
    {
        IDbConnection CreateConnection();
        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);
        IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection direction);
        void CloseConnection(IDbConnection connection);
    }
}