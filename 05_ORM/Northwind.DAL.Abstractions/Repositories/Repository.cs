using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Abstractions.Entities;
using Northwind.DAL.Abstractions.Interfaces;

namespace Northwind.DAL.Abstractions.Repositories
{
    public abstract class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;

        protected Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(_set));
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return _set.Where(expression);
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await GetAsync(entity => entity.Id.Equals(id));
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _set.SingleOrDefaultAsync(expression);
        }

        public async Task<bool> ExistAsync(TPrimaryKey id)
        {
            return await ExistAsync(entity => entity.Id.Equals(id));
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _set.AnyAsync(expression);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _set.AddAsync(entity);

            return result.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var result = _set.Update(entity);

            return result.Entity;
        }

        public TEntity Delete(TEntity entity)
        {
            var result = _set.Remove(entity);

            return result.Entity;
        }
    }
}