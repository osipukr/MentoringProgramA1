using System;

namespace Northwind.DAL.Abstractions.Entities
{
    public abstract class Entity<TPrimaryKey> : Entity
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}