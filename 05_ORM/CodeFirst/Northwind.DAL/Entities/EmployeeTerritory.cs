using Northwind.DAL.Abstractions.Interfaces;

namespace Northwind.DAL.Entities
{
    public class EmployeeTerritory : IEntity
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Territory Territory { get; set; }
    }
}