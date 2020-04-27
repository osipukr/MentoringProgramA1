using System.Collections.Generic;
using Northwind.Server.DataAccessLayer.Entities.Base;

namespace Northwind.Server.DataAccessLayer.Entities
{
    public class Customer : Entity<string>
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}