using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Abstractions.Interfaces
{
    public interface IRepository<TEntity> : IRepository where TEntity : Entity
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null);
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
    }
}