using System;
using System.Threading.Tasks;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Abstractions.Interfaces
{
    public interface IRepository<TEntity, in TPrimaryKey> : IRepository<TEntity>
        where TEntity : Entity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        Task<TEntity> GetFirstOrDefaultAsync(TPrimaryKey id);
        Task<bool> ExistAsync(TPrimaryKey id);
    }
}