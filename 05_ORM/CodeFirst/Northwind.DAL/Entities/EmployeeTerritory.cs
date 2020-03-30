using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Entities
{
    public class EmployeeTerritory : Entity
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Territory Territory { get; set; }
    }
}