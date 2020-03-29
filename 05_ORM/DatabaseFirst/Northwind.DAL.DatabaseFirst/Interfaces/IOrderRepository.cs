using System.Collections.Generic;
using System.Linq;
using Northwind.DAL.Abstractions.Interfaces;
using Northwind.DAL.DatabaseFirst.Entities;
using Northwind.DAL.DatabaseFirst.Models;

namespace Northwind.DAL.DatabaseFirst.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<CustOrderHist> CustOrderHist(string customerId);
        IQueryable<CustOrdersDetail> CustOrderDetail(int orderId);
    }
}