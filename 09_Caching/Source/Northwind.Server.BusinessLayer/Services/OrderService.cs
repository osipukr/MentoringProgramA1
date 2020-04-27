using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrderService> _logger;

        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IShipperRepository _shipperRepository;

        public OrderService(IUnitOfWork<NorthwindContext> unitOfWork, ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _orderRepository = _unitOfWork.GetRepository<IOrderRepository>();
            _customerRepository = _unitOfWork.GetRepository<ICustomerRepository>();
            _employeeRepository = _unitOfWork.GetRepository<IEmployeeRepository>();
            _shipperRepository = _unitOfWork.GetRepository<IShipperRepository>();
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            try
            {
                _logger.LogInformation($"Method {nameof(ListAsync)} has been invoke.");

                var orders = await _orderRepository.GetAll().OrderBy(order => order.Id).ToListAsync();

                if (orders == null)
                {
                    throw new NorthwindException(
                        Resources.OrderService_ListAsync_The_orders_not_found_,
                        ExceptionEventTypes.NotFound);
                }

                _logger.LogInformation($"Method {nameof(ListAsync)} has been successfully returned the value.");

                return orders;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(ListAsync)} has thrown an exception.");

                throw;
            }
        }

        public async Task<Order> FindAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(FindAsync)} has been invoke.");

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

                _logger.LogInformation($"Method {nameof(FindAsync)} has been successfully returned the value.");

                return order;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(FindAsync)} has thrown an exception.");

                throw;
            }
        }

        public async Task<Order> AddAsync(Order order)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(AddAsync)} has been invoke.");

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

                await _orderRepository.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Method {nameof(AddAsync)} has been successfully returned the value.");

                return order;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(AddAsync)} has thrown an exception.");

                throw;
            }
        }

        public async Task<Order> UpdateAsync(int id, Order order)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(UpdateAsync)} has been invoke.");

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

                _logger.LogInformation($"Method {nameof(UpdateAsync)} has been successfully returned the value.");

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(UpdateAsync)} has thrown an exception.");

                throw;
            }
        }

        public async Task<Order> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Method {nameof(DeleteAsync)} has been invoke.");

                var order = await FindAsync(id);

                _orderRepository.Delete(order);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Method {nameof(DeleteAsync)} has been successfully returned the value.");

                return order;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Method {nameof(DeleteAsync)} has thrown an exception.");

                throw;
            }
        }
    }
}