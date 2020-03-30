using AutoMapper;
using Northwind.Console.ViewModels.Product;
using Northwind.DAL.Entities;

namespace Northwind.Console.Mapping
{
    public class ProductViewModelProfile : Profile
    {
        public ProductViewModelProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(productView => productView.ProductId,
                    config => config.MapFrom(product => product.Id));
        }
    }
}