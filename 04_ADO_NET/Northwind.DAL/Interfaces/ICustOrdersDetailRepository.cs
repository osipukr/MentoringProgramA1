using System.Collections.Generic;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Interfaces
{
    public interface ICustOrdersDetailRepository : IRepository<CustOrdersDetail>
    {
        IEnumerable<CustOrdersDetail> CustOrderDetail(int orderId);
    }
}