﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.DatabaseFirst.Entities
{
    public partial class SalesTotalsByAmount : Entity
    {
        public decimal? SaleAmount { get; set; }
        public int OrderId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}