using System;
using System.Collections.Generic;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Repositories;

namespace Northwind.DAL.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDatabaseHandler _databaseHandler;
        private IDataMapper _dataMapper;

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

            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            if (!InterfaceToRepositoryMatcher.ContainsKey(interfaceType))
            {
                throw new ArgumentException(nameof(TRepository));
            }

            var repositoryType = InterfaceToRepositoryMatcher[interfaceType];

            var repository = Activator.CreateInstance(repositoryType, _databaseHandler, _dataMapper);

            return (TRepository)repository;
        }

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
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