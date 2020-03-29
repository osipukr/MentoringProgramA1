using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Entities
{
    public class OrderDetails : Entity<int>
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}