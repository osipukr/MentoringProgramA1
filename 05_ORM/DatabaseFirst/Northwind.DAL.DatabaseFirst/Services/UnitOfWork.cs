﻿using System;
using System.Collections.Generic;
using Northwind.DAL.Abstractions.Services;
using Northwind.DAL.DatabaseFirst.Contexts;
using Northwind.DAL.DatabaseFirst.Interfaces;
using Northwind.DAL.DatabaseFirst.Repositories;

namespace Northwind.DAL.DatabaseFirst.Services
{
    public class UnitOfWork : UnitOfWork<NorthwindContext>
    {
        public UnitOfWork(NorthwindContext context) : base(context)
        {
            _repositories = new Dictionary<Type, Type>
            {
                {typeof(IOrderRepository), typeof(OrderRepository)}
            };
        }
    }
}