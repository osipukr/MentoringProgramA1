using System;
using System.Collections.Generic;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Entities
{
    public class Order : IEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        public OrderStatus Status =>
            OrderDate == null
                ? OrderStatus.New
                : ShippedDate == null
                    ? OrderStatus.InWork
                    : OrderStatus.Completed;

        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}