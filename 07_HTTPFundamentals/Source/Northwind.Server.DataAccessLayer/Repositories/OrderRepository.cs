using Northwind.Server.DataAccessLayer.Contexts;
using Northwind.Server.DataAccessLayer.Entities;
using Northwind.Server.DataAccessLayer.Interfaces;
using Northwind.Server.DataAccessLayer.Repositories.Base;

namespace Northwind.Server.DataAccessLayer.Repositories
{
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {
        public OrderRepository(NorthwindContext context) : base(context)
        {
        }
    }
}