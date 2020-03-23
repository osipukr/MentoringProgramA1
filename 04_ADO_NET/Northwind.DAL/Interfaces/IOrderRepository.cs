using System.Collections.Generic;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void SetInWorkStatus(int id);
        void SetCompletedStatus(int id);
        IEnumerable<CustOrderHist> CustOrderHist(string customerId);
        IEnumerable<CustOrdersDetail> CustOrderDetail(int orderId);
    }
}