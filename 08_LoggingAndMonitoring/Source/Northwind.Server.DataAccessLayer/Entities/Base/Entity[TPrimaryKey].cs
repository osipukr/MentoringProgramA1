using System;

namespace Northwind.Server.DataAccessLayer.Entities.Base
{
    public abstract class Entity<TPrimaryKey> : Entity
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}