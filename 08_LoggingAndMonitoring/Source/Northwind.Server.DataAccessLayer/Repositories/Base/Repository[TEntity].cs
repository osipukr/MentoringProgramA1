using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Server.DataAccessLayer.Entities.Base;
using Northwind.Server.DataAccessLayer.Interfaces.Base;

namespace Northwind.Server.DataAccessLayer.Repositories.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _context;
        protected readonly ILogger _logger;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(DbContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _dbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(GetAll)} has been invoke.");

                var query = expression == null
                    ? _dbSet
                    : _dbSet.Where(expression);

                _logger.LogInformation($"Method {nameof(GetAll)} has been successfully returned the value.");

                return query;
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception, $"Method {nameof(GetAll)} has thrown an exception.");

                throw;
            }
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(GetAsync)} has been invoke.");

                var entity = expression == null
                    ? await _dbSet.FirstOrDefaultAsync()
                    : await _dbSet.FirstOrDefaultAsync(expression);

                _logger.LogInformation($"Method {nameof(GetAsync)} has been successfully returned the value.");

                return entity;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(GetAsync)} has thrown an exception.");

                throw;
            }
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(ExistAsync)} has been invoke.");

                var isExist = expression == null
                    ? await _dbSet.AnyAsync()
                    : await _dbSet.AnyAsync(expression);

                _logger.LogInformation($"Method {nameof(ExistAsync)} has been successfully returned the value.");

                return isExist;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(ExistAsync)} has thrown an exception.");

                throw;
            }
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(CountAsync)} has been invoke.");

                var count = expression == null
                    ? await _dbSet.CountAsync()
                    : await _dbSet.CountAsync(expression);

                _logger.LogInformation($"Method {nameof(ExistAsync)} has been successfully returned the value.");

                return count;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(ExistAsync)} has thrown an exception.");

                throw;
            }
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(AddAsync)} has been invoke.");

                var entityEntry = await _dbSet.AddAsync(entity);

                _logger.LogInformation($"Method {nameof(AddAsync)} has been successfully returned the value.");

                return entityEntry.Entity;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(AddAsync)} has thrown an exception.");

                throw;
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(Update)} has been invoke.");

                _dbSet.Update(entity);

                _logger.LogInformation($"Method {nameof(AddAsync)} has been successfully completed.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(Update)} has thrown an exception.");

                throw;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(Delete)} has been invoke.");

                _dbSet.Remove(entity);

                _logger.LogInformation($"Method {nameof(Delete)} has been successfully completed.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(Delete)} has thrown an exception.");

                throw;
            }
        }
    }
}