using System.Linq;
using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Abstractions.Repositories;
using Northwind.DAL.DatabaseFirst.Contexts;
using Northwind.DAL.DatabaseFirst.Entities;
using Northwind.DAL.DatabaseFirst.Interfaces;
using Northwind.DAL.DatabaseFirst.Models;

namespace Northwind.DAL.DatabaseFirst.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(NorthwindContext context) : base(context)
        {
        }

        public IQueryable<CustOrderHist> CustOrderHist(string customerId)
        {
            const string procedureSql = "EXECUTE [dbo].[CustOrderHist] @customerId";

            return _context.Set<CustOrderHist>().FromSqlRaw(procedureSql, customerId);
        }

        public IQueryable<CustOrdersDetail> CustOrderDetail(int orderId)
        {
            const string procedureSql = "EXECUTE [dbo].[CustOrdersDetail] @orderId";

            return _context.Set<CustOrdersDetail>().FromSqlRaw(procedureSql, orderId);
        }
    }
}