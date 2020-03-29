using Northwind.DAL.Abstractions.Services;
using Northwind.DAL.Contexts;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Repositories;

namespace Northwind.DAL.Services
{
    public class UnitOfWork : UnitOfWork<NorthwindContext>
    {
        public UnitOfWork(NorthwindContext context) : base(context)
        {
            _repositories.Add(typeof(ICategoryRepository), typeof(CategoryRepository));
            _repositories.Add(typeof(IOrderRepository), typeof(OrderRepository));
        }
    }
}