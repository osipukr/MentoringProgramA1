using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Server.DataAccessLayer.Interfaces.Base;

namespace Northwind.Server.DataAccessLayer.Services.Base
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        protected TContext _context;

        private readonly ILoggerFactory _loggerFactory;
        private Dictionary<Type, Type> _repositories;
        private Dictionary<Type, object> _instanceRepositories;

        protected UnitOfWork(TContext context, ILoggerFactory loggerFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));

            _repositories = new Dictionary<Type, Type>();
            _instanceRepositories = new Dictionary<Type, object>();
        }

        public TContext DbContext => _context;

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            var interfaceType = typeof(TRepository);

            if (_repositories == null || !_repositories.ContainsKey(interfaceType))
            {
                throw new KeyNotFoundException(nameof(TRepository));
            }

            var repositoryType = _repositories[interfaceType];

            object repository;

            if (_instanceRepositories.ContainsKey(repositoryType))
            {
                repository = _instanceRepositories[repositoryType];
            }
            else
            {
                var logger = CreateLogger<TRepository>();

                repository = Activator.CreateInstance(repositoryType, _context, logger);

                _instanceRepositories.Add(repositoryType, repository);
            }

            if (repository == null)
            {
                throw new ArgumentException(null, nameof(TRepository));
            }

            return (TRepository)repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected void AddRepository<TService, TImplementation>() where TImplementation : IRepository, TService
        {
            AddRepository(typeof(TService), typeof(TImplementation));
        }

        protected void AddRepository(Type serviceType, Type implementationType)
        {
            _repositories.Add(serviceType, implementationType);
        }

        protected ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }

        #region IDisposable Implementation

        protected bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositories?.Clear();
                    _instanceRepositories?.Clear();
                    _context.Dispose();

                    _repositories = null;
                    _instanceRepositories = null;
                    _context = null;
                }
            }

            _disposed = true;
        }

        #endregion
    }
}