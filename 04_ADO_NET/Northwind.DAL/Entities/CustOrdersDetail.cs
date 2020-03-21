using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Entities
{
    public class CustOrdersDetail : IEntity
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public decimal ExtendedPrice { get; set; }
    }
}