using System.Collections.Generic;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Interfaces
{
    public interface ICustOrderHistRepository : IRepository<CustOrderHist>
    {
        IEnumerable<CustOrderHist> CustOrderHist(string customerId);
    }
}