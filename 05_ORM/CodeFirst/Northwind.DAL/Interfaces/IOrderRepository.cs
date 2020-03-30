using Northwind.DAL.Abstractions.Interfaces;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order, int>
    {
    }
}