using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Northwind.Server.BusinessLayer.Interfaces;
using Northwind.Server.DataAccessLayer.Entities;
using Northwind.Server.WebApi.Controllers.Base;
using Northwind.Server.WebApi.ViewModels.Order;

namespace Northwind.Server.WebApi.Controllers
{
    /// <summary>
    /// The Order API controller.
    /// </summary>
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, IMapper mapper) : base(mapper)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>Returns all orders.</returns>
        [EnableQuery]
        [HttpGet]
        public async Task<IEnumerable<OrderView>> Get()
        {
            var orders = await _orderService.ListAsync();

            return _mapper.Map<IEnumerable<OrderView>>(orders);
        }

        /// <summary>
        /// Gets item by order id.
        /// </summary>
        /// <param name="id">The order id.</param>
        /// <returns>Returns found order.</returns>
        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<OrderView> Get(int id)
        {
            var order = await _orderService.FindAsync(id);

            return _mapper.Map<OrderView>(order);
        }

        /// <summary>
        /// Creates new item.
        /// </summary>
        /// <param name="model">The create model for order.</param>
        /// <returns>Returns created order.</returns>
        [HttpPost]
        public async Task<OrderView> Post(OrderCreateView model)
        {
            var order = _mapper.Map<Order>(model);

            var result = await _orderService.AddAsync(order);

            return _mapper.Map<OrderView>(result);
        }

        /// <summary>
        /// Updates item by id.
        /// </summary>
        /// <param name="id">The order id.</param>
        /// <param name="model">Update model for order.</param>
        /// <returns>Returns updated order.</returns>
        [HttpPut("{id}")]
        public async Task<OrderView> Put(int id, OrderUpdateView model)
        {
            var order = _mapper.Map<Order>(model);

            var result = await _orderService.UpdateAsync(id, order);

            return _mapper.Map<OrderView>(result);
        }

        /// <summary>
        /// Deletes item by id.
        /// </summary>
        /// <param name="id">The order id.</param>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _orderService.DeleteAsync(id);
        }
    }
}