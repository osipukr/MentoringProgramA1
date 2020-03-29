using System;
using Northwind.DAL.Abstractions.Interfaces;

namespace Northwind.DAL.Abstractions.Entities
{
    public abstract class Entity<TPrimaryKey> : IEntity
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}