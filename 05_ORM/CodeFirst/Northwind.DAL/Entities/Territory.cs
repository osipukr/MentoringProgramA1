using System.Collections.Generic;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Entities
{
    public class Territory : Entity<string>
    {
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}