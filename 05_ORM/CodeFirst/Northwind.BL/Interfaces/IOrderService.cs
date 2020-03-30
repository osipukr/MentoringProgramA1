using Northwind.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.BL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersWithDetailsAsync(int categoryId);
    }
}