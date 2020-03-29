using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Abstractions.Interfaces
{
    public interface IRepository<TEntity, in TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistAsync(TPrimaryKey id);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
    }
}