using System;
using System.Collections.Generic;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Repositories;

namespace Northwind.DAL.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDatabaseHandler _databaseHandler;
        private readonly IDataMapper _dataMapper;
        private readonly IOrderRepository _orderRepository;

        private readonly Dictionary<Type, Type> _interfaceToRepositoryMatcher = new Dictionary<Type, Type>
        {
            {typeof(IOrderRepository), typeof(OrderRepository)}
        };

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="databaseHandler">Instance of database handler.</param>
        /// <param name="dataMapper">Instance of data mapper.</param>
        public UnitOfWork(IDatabaseHandler databaseHandler, IDataMapper dataMapper)
        {
            _databaseHandler = databaseHandler ?? throw new ArgumentNullException(nameof(databaseHandler));
            _dataMapper = dataMapper ?? throw new ArgumentNullException(nameof(dataMapper));

            _orderRepository = CreateRepository<IOrderRepository>();
        }

        /// <summary>
        /// Gets order repository.
        /// </summary>
        public IOrderRepository OrderRepository => _orderRepository;

        /// <summary>
        /// Implementation of <see cref="IDisposable"/> interface.
        /// </summary>
        public void Dispose()
        {
            // Do something
        }

        private TRepository CreateRepository<TRepository>()
        {
            var interfaceType = typeof(TRepository);

            if (!_interfaceToRepositoryMatcher.ContainsKey(interfaceType))
            {
                throw new NotImplementedException();
            }

            var repositoryType = _interfaceToRepositoryMatcher[interfaceType];
            var repository = Activator.CreateInstance(repositoryType, _databaseHandler, _dataMapper);

            return (TRepository)repository;
        }
    }
}