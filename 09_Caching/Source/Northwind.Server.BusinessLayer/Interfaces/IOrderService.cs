using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Server.DataAccessLayer.Entities;

namespace Northwind.Server.BusinessLayer.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> ListAsync();

        Task<Order> FindAsync(int id);

        Task<Order> AddAsync(Order order);

        Task<Order> UpdateAsync(int id, Order order);

        Task<Order> DeleteAsync(int id);
    }
}