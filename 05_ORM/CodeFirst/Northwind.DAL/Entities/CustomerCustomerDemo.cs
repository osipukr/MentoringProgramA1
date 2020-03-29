using Northwind.DAL.Abstractions.Interfaces;

namespace Northwind.DAL.Entities
{
    public class CustomerCustomerDemo : IEntity
    {
        public string CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerDemographics CustomerType { get; set; }
    }
}