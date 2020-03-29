using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Northwind.DAL.Abstractions.Entities;
using Northwind.DAL.Abstractions.Interfaces;

namespace Northwind.DAL.Abstractions.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _dbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return _dbSet;
            }

            return _dbSet.Where(expression);
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _dbSet.FirstOrDefaultAsync();
            }

            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _dbSet.AnyAsync();
            }

            return await _dbSet.AnyAsync(expression);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _dbSet.CountAsync();
            }

            return await _dbSet.CountAsync(expression);
        }

        public virtual async ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return await _dbSet.AddAsync(entity);
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(sql, parameters);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}