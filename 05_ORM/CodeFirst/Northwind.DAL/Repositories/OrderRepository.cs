using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Abstractions.Repositories;
using Northwind.DAL.Entities;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Repositories
{
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
    }
}