using System.Data;

namespace Northwind.DAL.Interfaces
{
    public interface IDatabaseHandler
    {
        IDbConnection CreateConnection();
        void CloseConnection(IDbConnection connection);
    }
}