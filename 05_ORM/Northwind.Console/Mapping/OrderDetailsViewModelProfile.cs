using AutoMapper;
using Northwind.Console.ViewModels.OrderDetails;
using Northwind.DAL.Entities;

namespace Northwind.Console.Mapping
{
    public class OrderDetailsViewModelProfile : Profile
    {
        public OrderDetailsViewModelProfile()
        {
            CreateMap<OrderDetails, OrderDetailsViewModel>()
                .ForMember(orderDetailsView => orderDetailsView.Product,
                    config => config.MapFrom(orderDetails => orderDetails.Product));
        }
    }
}