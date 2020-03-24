using System;
using System.Collections.Generic;
using System.Data;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Repositories;

namespace Northwind.DAL.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDatabaseHandler _databaseHandler;
        private IDataMapper _dataMapper;
        private IDbConnection _dbConnection;

        private static readonly Dictionary<Type, Type> InterfaceToRepositoryMatcher =
            new Dictionary<Type, Type>
            {
                {typeof(IOrderRepository), typeof(OrderRepository)}
            };

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="databaseHandler">Instance of database handler.</param>
        /// <param name="dataMapper"></param>
        public UnitOfWork(IDatabaseHandler databaseHandler, IDataMapper dataMapper)
        {
            _databaseHandler = databaseHandler ?? throw new ArgumentNullException(nameof(databaseHandler));
            _dataMapper = dataMapper ?? throw new ArgumentNullException(nameof(dataMapper));

            _dbConnection = _databaseHandler.CreateConnection();

            OrderRepository = CreateRepository<IOrderRepository>();
        }

        /// <summary>
        /// Gets order repository.
        /// </summary>
        public IOrderRepository OrderRepository { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private TRepository CreateRepository<TRepository>()
        {
            var interfaceType = typeof(TRepository);

            if (interfaceType == null || !InterfaceToRepositoryMatcher.ContainsKey(interfaceType))
            {
                throw new KeyNotFoundException(nameof(TRepository));
            }

            var repositoryType = InterfaceToRepositoryMatcher[interfaceType];

            var repository = Activator.CreateInstance(repositoryType, _dbConnection, _dataMapper);

            if (repository == null)
            {
                throw new ArgumentException(null, nameof(repository));
            }

            return (TRepository)repository;
        }

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _databaseHandler?.CloseConnection(_dbConnection);
                    _databaseHandler = null;
                    _dataMapper = null;
                }

                _isDisposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}