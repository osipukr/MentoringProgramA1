using System.Data;

namespace Northwind.DAL.Interfaces
{
    public interface IDataMapper
    {
        TEntity Map<TEntity>(IDataReader reader) where TEntity : IEntity, new();
    }
}