using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Entities
{
    public class OrderDetails : IEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public Product Product { get; set; }
    }
}