﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.DatabaseFirst.Entities
{
    public partial class OrderDetailsExtended : Entity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal? ExtendedPrice { get; set; }
    }
}