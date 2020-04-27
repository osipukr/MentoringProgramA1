using System;
using System.Threading.Tasks;
using Northwind.Server.DataAccessLayer.Entities.Base;

namespace Northwind.Server.DataAccessLayer.Interfaces.Base
{
    public interface IRepository<TEntity, in TPrimaryKey> : IRepository<TEntity>
        where TEntity : Entity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        Task<TEntity> GetAsync(TPrimaryKey id);

        Task<bool> ExistAsync(TPrimaryKey id);
    }
}