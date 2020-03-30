using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Abstractions.Interfaces;

namespace Northwind.DAL.Abstractions.Services
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        protected TContext _context;
        protected Dictionary<Type, Type> _repositories;
        protected bool _disposed;

        protected UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

            var repository = Activator.CreateInstance(repositoryType, _context);

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
                    _context.Dispose();

                    _repositories = null;
                    _context = null;
                }
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}