using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Server.WebApi.ViewModels.Order
{
    [Serializable]
    public class OrderUpdateView : View
    {
        [MaxLength(5)] public string CustomerId;
        [Range(1, int.MaxValue)] public int? EmployeeId;
        [Range(1, int.MaxValue)] public int? ShipVia;

        public DateTime? OrderDate;
        public DateTime? RequiredDate;
        public DateTime? ShippedDate;
        public decimal? Freight;

        [MaxLength(40)] public string ShipName;
        [MaxLength(60)] public string ShipAddress;
        [MaxLength(15)] public string ShipCity;
        [MaxLength(15)] public string ShipRegion;
        [MaxLength(10)] public string ShipPostalCode;
        [MaxLength(15)] public string ShipCountry;
    }
}