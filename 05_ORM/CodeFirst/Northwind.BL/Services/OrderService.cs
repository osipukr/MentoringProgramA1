using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.BL.Infrastructure.Exceptions;
using Northwind.BL.Interfaces;
using Northwind.BL.Properties;
using Northwind.DAL.Entities;
using Northwind.DAL.Interfaces;

namespace Northwind.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICategoryRepository _categoryRepository;

        public OrderService(IOrderRepository orderRepository, ICategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync(int categoryId)
        {
            if (!await _categoryRepository.ExistAsync(categoryId))
            {
                throw new NorthwindException(string.Format(Resources.OrderService_GetOrdersWithDetailsAsync_InvalidCategoryId, categoryId));
            }

            var orders = await _orderRepository.GetAll(order => 
                    order.OrderDetails.All(orderDetails => orderDetails.Product.CategoryId == categoryId))
                //.Include(order => order.OrderDetails)
                //    .ThenInclude(orderDetails => orderDetails.Product)
                //.Include(order => order.Customer)
                .ToListAsync();

            if (orders == null)
            {
                throw new NorthwindException(string.Format(Resources.OrderService_GetOrdersWithDetailsAsync_OrdersNotFound, categoryId));
            }

            return orders;
        }
    }
}