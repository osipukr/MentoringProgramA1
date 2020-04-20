using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Server.WebApi.ViewModels.Order
{
    public class OrderCreateView
    {
        [MaxLength(5)]
        public string CustomerId { get; set; }

        [Range(1, int.MaxValue)]
        public int? EmployeeId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Range(1, int.MaxValue)]
        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        [MaxLength(40)]
        public string ShipName { get; set; }

        [MaxLength(60)]
        public string ShipAddress { get; set; }

        [MaxLength(15)]
        public string ShipCity { get; set; }

        [MaxLength(15)]
        public string ShipRegion { get; set; }

        [MaxLength(10)]
        public string ShipPostalCode { get; set; }

        [MaxLength(15)]
        public string ShipCountry { get; set; }
    }
}