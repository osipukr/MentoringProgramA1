using System;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Entities
{
    public class CreditCard : Entity<int>
    {
        public DateTime CardExpirationDate { get; set; }
        public string CardHolderName { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}