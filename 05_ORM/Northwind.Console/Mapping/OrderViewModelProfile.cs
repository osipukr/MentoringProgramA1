using AutoMapper;
using Northwind.Console.ViewModels.Order;
using Northwind.DAL.Entities;

namespace Northwind.Console.Mapping
{
    public class OrderViewModelProfile : Profile
    {
        public OrderViewModelProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(orderView => orderView.OrderId,
                    config => config.MapFrom(order => order.Id))
                .ForMember(orderView => orderView.OrderDetails,
                    config => config.MapFrom(order => order.OrderDetails));
        }
    }
}