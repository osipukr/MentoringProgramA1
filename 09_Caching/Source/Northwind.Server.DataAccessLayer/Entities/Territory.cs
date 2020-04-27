using System.Collections.Generic;
using Northwind.Server.DataAccessLayer.Entities.Base;

namespace Northwind.Server.DataAccessLayer.Entities
{
    public class Territory : Entity<string>
    {
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}