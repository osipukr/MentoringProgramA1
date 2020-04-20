using System;
using System.Runtime.Serialization;

namespace Northwind.Server.WebApi.ViewModels.Order
{
    [DataContract]
    public class OrderView : View
    {
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        [DataMember(Order = 2)]
        public string CustomerId { get; set; }

        [DataMember(Order = 3)]
        public int? EmployeeId { get; set; }

        [DataMember(Order = 4)]
        public DateTime? OrderDate { get; set; }

        [DataMember(Order = 5)]
        public DateTime? RequiredDate { get; set; }

        [DataMember(Order = 6)]
        public DateTime? ShippedDate { get; set; }

        [DataMember(Order = 7)]
        public int? ShipVia { get; set; }

        [DataMember(Order = 8)]
        public decimal? Freight { get; set; }

        [DataMember(Order = 9)]
        public string ShipName { get; set; }

        [DataMember(Order = 10)]
        public string ShipAddress { get; set; }

        [DataMember(Order = 11)]
        public string ShipCity { get; set; }

        [DataMember(Order = 12)]
        public string ShipRegion { get; set; }

        [DataMember(Order = 13)]
        public string ShipPostalCode { get; set; }

        [DataMember(Order = 14)]
        public string ShipCountry { get; set; }
    }
}