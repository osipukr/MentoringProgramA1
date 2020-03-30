using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.DAL.DatabaseFirst.Entities;
using Northwind.DAL.DatabaseFirst.Models;

namespace Northwind.BL.DatabaseFirst.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> ListAsync();
        Task<Order> FindAsync(int orderId);
        Task<Order> AddAsync(Order order);
        Task<Order> UpdateAsync(int orderId, Order order);
        Task<Order> DeleteAsync(int orderId);

        OrderStatus GetOrderStatus(Order order);
        Task<Order> SetInWorkStatusAsync(int orderId);
        Task<Order> SetCompletedStatusAsync(int orderId);
        Task<IEnumerable<CustOrderHist>> GetCustomerOrderHistoryAsync(string customerId);
        Task<IEnumerable<CustOrdersDetail>> GetCustomerOrderDetailAsync(int orderId);
    }
}