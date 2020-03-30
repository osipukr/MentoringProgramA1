using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Northwind.BL.Infrastructure.Exceptions;
using Northwind.BL.Interfaces;
using Northwind.Console.Extensions;
using Northwind.Console.Settings;
using Northwind.Console.ViewModels.Order;

namespace Northwind.Console
{
    public class Application
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<Application> _logger;

        public Application(IOrderService orderService, IMapper mapper, ILogger<Application> logger, AppSettings appSettings)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;

            appSettings.ConfigureApplication();
        }

        public async Task RunAsync()
        {
            try
            {
                await RunInternalAsync();
            }
            catch (NorthwindException exception)
            {
                _logger.LogError(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }

        private async Task RunInternalAsync()
        {
            const int categoryId = 1;

            var orders = await _orderService.GetOrdersWithDetailsAsync(categoryId);

            var ordersView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            var ordersJson = JsonConvert.SerializeObject(ordersView, Formatting.Indented);

            System.Console.WriteLine(ordersJson);
        }
    }
}