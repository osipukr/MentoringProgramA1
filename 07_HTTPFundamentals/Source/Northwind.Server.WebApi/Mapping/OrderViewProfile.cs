using AutoMapper;
using Northwind.Server.DataAccessLayer.Entities;
using Northwind.Server.WebApi.ViewModels.Order;

namespace Northwind.Server.WebApi.Mapping
{
    public class OrderViewProfile : Profile
    {
        public OrderViewProfile()
        {
            CreateMap<Order, OrderView>()
                .ForMember(orderView => orderView.OrderId,
                    config => config.MapFrom(order => order.Id))
                .ReverseMap();

            CreateMap<Order, OrderCreateView>().ReverseMap();
            CreateMap<Order, OrderUpdateView>().ReverseMap();
        }
    }
}