using System.Collections.Generic;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Entities
{
    public class CustomerDemographics : Entity<int>
    {
        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }

        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    }
}