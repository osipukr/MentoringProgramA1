using Northwind.Server.DataAccessLayer.Entities;
using Northwind.Server.DataAccessLayer.Interfaces.Base;

namespace Northwind.Server.DataAccessLayer.Interfaces
{
    public interface IOrderRepository : IRepository<Order, int>
    {
    }
}