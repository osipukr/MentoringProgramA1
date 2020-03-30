using System.Collections.Generic;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Entities
{
    public class Region : Entity<int>
    {
        public string RegionDescription { get; set; }

        public virtual ICollection<Territory> Territories { get; set; }
    }
}