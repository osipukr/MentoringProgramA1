using Northwind.DAL.Abstractions.Repositories;
using Northwind.DAL.Contexts;
using Northwind.DAL.Entities;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Repositories
{
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {
        public OrderRepository(NorthwindContext context) : base(context)
        {
        }
    }
}