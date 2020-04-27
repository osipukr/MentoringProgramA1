using Microsoft.Extensions.Logging;
using Northwind.Server.DataAccessLayer.Contexts;
using Northwind.Server.DataAccessLayer.Interfaces;
using Northwind.Server.DataAccessLayer.Repositories;
using Northwind.Server.DataAccessLayer.Services.Base;

namespace Northwind.Server.DataAccessLayer.Services
{
    public class NorthwindUnitOfWork : UnitOfWork<NorthwindContext>
    {
        public NorthwindUnitOfWork(NorthwindContext context, ILoggerFactory loggerFactory) : base(context, loggerFactory)
        {
            AddRepository<ICustomerRepository, CustomerRepository>();
            AddRepository<IEmployeeRepository, EmployeeRepository>();
            AddRepository<IOrderRepository, OrderRepository>();
            AddRepository<IShipperRepository, ShipperRepository>();
        }
    }
}