using Northwind.Server.DataAccessLayer.Contexts;
using Northwind.Server.DataAccessLayer.Entities;
using Northwind.Server.DataAccessLayer.Interfaces;
using Northwind.Server.DataAccessLayer.Repositories.Base;

namespace Northwind.Server.DataAccessLayer.Repositories
{
    public class CustomerRepository : Repository<Customer, string>, ICustomerRepository
    {
        public CustomerRepository(NorthwindContext context) : base(context)
        {
        }
    }
}