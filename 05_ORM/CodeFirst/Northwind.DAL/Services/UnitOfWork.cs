using System;
using System.Collections.Generic;
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
            _repositories = new Dictionary<Type, Type>
            {
                {typeof(ICategoryRepository), typeof(CategoryRepository)},
                {typeof(IOrderRepository), typeof(OrderRepository)}
            };
        }
    }
}