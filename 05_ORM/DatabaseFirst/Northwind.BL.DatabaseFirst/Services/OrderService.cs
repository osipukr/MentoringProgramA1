using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.BL.DatabaseFirst.Interfaces;
using Northwind.BL.DatabaseFirst.Properties;
using Northwind.BL.Infrastructure.Exceptions;
using Northwind.DAL.Abstractions.Interfaces;
using Northwind.DAL.DatabaseFirst.Contexts;
using Northwind.DAL.DatabaseFirst.Entities;
using Northwind.DAL.DatabaseFirst.Interfaces;
using Northwind.DAL.DatabaseFirst.Models;

namespace Northwind.BL.DatabaseFirst.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork<NorthwindContext> _unitOfWork;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IUnitOfWork<NorthwindContext> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            _orderRepository = _unitOfWork.GetRepository<IOrderRepository>();
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            var orders = await _orderRepository.GetAll().ToListAsync();

            if (orders == null)
            {
                throw new NorthwindException(Resources.OrderService_ListAsync_The_orders_not_found_);
            }

            return orders;
        }

        public async Task<Order> FindAsync(int orderId)
        {
            if (orderId < 1)
            {
                throw new NorthwindException(Resources.OrderService_FindAsync_Invalid_order_id_);
            }

            var order = await _orderRepository.GetFirstOrDefaultAsync(entity => entity.OrderId == orderId);

            if (order == null)
            {
                throw new NorthwindException(string.Format(Resources.OrderService_FindAsync_NotFoundById, orderId));
            }

            return order;
        }

        public async Task<Order> AddAsync(Order order)
        {
            if (order == null)
            {
                throw new NorthwindException(Resources.OrderService_AddAsync_Invalid_order_model_);
            }

            var result = await _orderRepository.InsertAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Order> UpdateAsync(int orderId, Order order)
        {
            if (order == null)
            {
                throw new NorthwindException(Resources.OrderService_UpdateAsync_Invalid_order_model_to_update_);
            }

            var result = await FindAsync(orderId);

            var resultOrderStatus = GetOrderStatus(result);

            if (resultOrderStatus != OrderStatus.New)
            {
                throw new NorthwindException(Resources.OrderService_UpdateAsync_Only_orders_with_New_status_can_be_changed_);
            }

            if (order.OrderDate != result.OrderDate || order.ShippedDate != result.ShippedDate)
            {
                throw new NorthwindException(Resources.OrderService_UpdateAsync_Orders_with_statuses_InWork_and_Completed_cannot_be_changed_);
            }

            result.CustomerId = order.CustomerId;
            result.EmployeeId = order.EmployeeId;
            result.OrderDate = order.OrderDate;
            result.RequiredDate = order.RequiredDate;
            result.ShippedDate = order.ShippedDate;
            result.ShipVia = order.ShipVia;
            result.Freight = order.Freight;
            result.ShipName = order.ShipName;
            result.ShipAddress = order.ShipAddress;
            result.ShipCity = order.ShipCity;
            result.ShipRegion = order.ShipCity;
            result.ShipPostalCode = order.ShipPostalCode;
            result.ShipCountry = order.ShipCountry;

            _orderRepository.Update(result);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<Order> DeleteAsync(int orderId)
        {
            var order = await FindAsync(orderId);

            var orderStatus = GetOrderStatus(order);

            if (orderStatus == OrderStatus.Completed)
            {
                throw new NorthwindException(Resources.OrderService_DeleteAsync_Orders_with_Completed_status_cannot_be_deleted_);
            }

            _orderRepository.Delete(order);

            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        public OrderStatus GetOrderStatus(Order order)
        {
            if (order == null)
            {
                throw new NorthwindException(Resources.OrderService_GetOrderStatus_Invalid_order_model_);
            }

            return order.OrderDate == null
                ? OrderStatus.New
                : order.ShippedDate == null
                    ? OrderStatus.InWork
                    : OrderStatus.Completed;
        }

        public async Task<Order> SetInWorkStatusAsync(int orderId)
        {
            var order = await FindAsync(orderId);

            var orderStatus = GetOrderStatus(order);

            if (orderStatus != OrderStatus.New)
            {
                throw new NorthwindException(Resources.OrderService_SetInWorkStatusAsync_Only_in_the_order_with_the_NewStatus_can_the_status_be_changed_to_InWork_);
            }

            order.OrderDate = DateTime.UtcNow;

            return await UpdateAsync(orderId, order);
        }

        public async Task<Order> SetCompletedStatusAsync(int orderId)
        {
            var order = await FindAsync(orderId);

            var orderStatus = GetOrderStatus(order);

            if (orderStatus != OrderStatus.InWork)
            {
                throw new NorthwindException(Resources.OrderService_SetCompletedStatusAsync_Status_can_t_be_changed_to_CompletedStatus_);
            }

            order.ShippedDate = DateTime.UtcNow;

            return await UpdateAsync(orderId, order);
        }

        public async Task<IEnumerable<CustOrderHist>> GetCustomerOrderHistoryAsync(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new NorthwindException(Resources.OrderService_GetCustomerOrderHistoryAsync_Invalid_customer_id_);
            }

            var result = await _orderRepository.CustOrderHist(customerId).ToListAsync();

            if (result == null)
            {
                throw new NorthwindException(Resources.OrderService_GetCustomerOrderHistoryAsync_The_customer_order_hist_not_found_);
            }

            return result;
        }

        public async Task<IEnumerable<CustOrdersDetail>> GetCustomerOrderDetailAsync(int orderId)
        {
            if (!await _orderRepository.ExistAsync(order => order.OrderId == orderId))
            {
                throw new NorthwindException(string.Format(Resources.OrderService_GetCustomerOrderDetailAsync_InvalidOrderId, orderId));
            }

            var result = await _orderRepository.CustOrderDetail(orderId).ToListAsync();

            if (result == null)
            {
                throw new NorthwindException(Resources.OrderService_GetCustomerOrderDetailAsync_The_customer_orders_details_not_found_);
            }

            return result;
        }
    }
}