using System.Collections.Generic;
using Northwind.DAL.Abstractions.Entities;

namespace Northwind.DAL.Entities
{
    public class Category : Entity<int>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}