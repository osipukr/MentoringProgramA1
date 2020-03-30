using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Entities
{
    public class CustomerCustomerDemo : Entity
    {
        public string CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerDemographics CustomerType { get; set; }
    }
}