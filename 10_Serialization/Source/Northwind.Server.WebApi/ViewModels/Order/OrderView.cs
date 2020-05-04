using System;
using System.Runtime.Serialization;

namespace Northwind.Server.WebApi.ViewModels.Order
{
    [DataContract, Serializable]
    public class OrderView : View
    {
        [DataMember(Order = 1)] public int OrderId;
        [DataMember(Order = 2)] public string CustomerId;
        [DataMember(Order = 3)] public int? EmployeeId;
        [DataMember(Order = 4)] public DateTime? OrderDate;
        [DataMember(Order = 5)] public DateTime? RequiredDate;
        [DataMember(Order = 6)] public DateTime? ShippedDate;
        [DataMember(Order = 7)] public int? ShipVia;
        [DataMember(Order = 8)] public decimal? Freight;
        [DataMember(Order = 9)] public string ShipName;
        [DataMember(Order = 10)] public string ShipAddress;
        [DataMember(Order = 11)] public string ShipCity;
        [DataMember(Order = 12)] public string ShipRegion;
        [DataMember(Order = 13)] public string ShipPostalCode;
        [DataMember(Order = 14)] public string ShipCountry;
    }
}