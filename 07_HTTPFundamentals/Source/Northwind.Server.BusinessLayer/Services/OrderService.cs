using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Server.BusinessLayer.Exceptions;
using Northwind.Server.BusinessLayer.Interfaces;
using Northwind.Server.BusinessLayer.Properties;
using Northwind.Server.DataAccessLayer.Contexts;
using Northwind.Server.DataAccessLayer.Entities;
using Northwind.Server.DataAccessLayer.Interfaces;
using Northwind.Server.DataAccessLayer.Interfaces.Base;

namespace Northwind.Server.BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork<NorthwindContext> _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IShipperRepository _shipperRepository;

        public OrderService(IUnitOfWork<NorthwindContext> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            _orderRepository = _unitOfWork.GetRepository<IOrderRepository>();
            _customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
            _employeeRepository = _unitOfWork.GetRepository<IEmployeeRepository>();
            _shipperRepository = _unitOfWork.GetRepository<IShipperRepository>();
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            var orders = await _orderRepository.GetAll().OrderBy(order => order.Id).ToListAsync();

            if (orders == null)
            {
                throw new NorthwindException(
                    Resources.OrderService_ListAsync_The_orders_not_found_,
                    ExceptionEventTypes.NotFound);
            }

            return orders;
        }

        public async Task<Order> FindAsync(int id)
        {
            if (id < 1)
            {
                throw new NorthwindException(
                    string.Format(Resources.OrderService_FindAsync_Invalid_order_id___0___, id),
                    ExceptionEventTypes.InvalidParameters);
            }

            var order = await _orderRepository.GetAsync(id);

            if (order == null)
            {
                throw new NorthwindException(
                    string.Format(Resources.OrderService_FindAsync_, id),
                    ExceptionEventTypes.NotFound);
            }

            return order;
        }

        public async Task<Order> AddAsync(Order order)
        {
            if (order == null)
            {
                throw new NorthwindException(
                    Resources.OrderService_AddAsync_Invalid_order_,
                    ExceptionEventTypes.InvalidParameters);
            }

            if (!string.IsNullOrWhiteSpace(order.CustomerId) && !await _customerRepository.ExistAsync(order.CustomerId))
            {
                throw new NorthwindException(
                    string.Format(Resources.OrderService_AddAsync_Invalid_customer_id___0___, order.CustomerId),
                    ExceptionEventTypes.InvalidParameters);
            }

            if (order.EmployeeId.HasValue && !await _employeeRepository.ExistAsync(order.EmployeeId.Value))
            {
                throw new NorthwindException(
                    string.Format(Resources.OrderService_AddAsync_Invalid_employee_id___0___, order.EmployeeId),
                    ExceptionEventTypes.InvalidParameters);
            }

            if (order.ShipVia.HasValue && !await _shipperRepository.ExistAsync(order.ShipVia.Value))
            {
                throw new NorthwindException(
                    string.Format(Resources.OrderService_AddAsync_Invalid_shipper_id___0___, order.ShipVia),
                    ExceptionEventTypes.InvalidParameters);
            }

            order.OrderDate = DateTime.UtcNow;

            await _orderRepository.InsertAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateAsync(int id, Order order)
        {
            var result = await FindAsync(id);

            result.RequiredDate = order.RequiredDate;
            result.ShippedDate = order.ShippedDate;
            result.Freight = order.Freight;
            result.ShipName = order.ShipName;
            result.ShipAddress = order.ShipAddress;
            result.ShipCity = order.ShipCity;
            result.ShipRegion = order.ShipRegion;
            result.ShipPostalCode = order.ShipPostalCode;
            result.ShipCountry = order.ShipCountry;

            _orderRepository.Update(result);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<Order> DeleteAsync(int id)
        {
            var order = await FindAsync(id);

            _orderRepository.Delete(order);

            await _unitOfWork.SaveChangesAsync();

            return order;
        }
    }
}