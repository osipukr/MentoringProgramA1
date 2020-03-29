using Northwind.Console.ViewModels.Product;

namespace Northwind.Console.ViewModels.OrderDetails
{
    public class OrderDetailsViewModel
    {
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public ProductViewModel Product { get; set; }
    }
}