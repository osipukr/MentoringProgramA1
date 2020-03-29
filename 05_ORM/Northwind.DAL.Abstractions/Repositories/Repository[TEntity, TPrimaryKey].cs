using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Abstractions.Entities;
using Northwind.DAL.Abstractions.Interfaces;

namespace Northwind.DAL.Abstractions.Repositories
{
    public abstract class Repository<TEntity, TPrimaryKey> : Repository<TEntity>, IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected Repository(DbContext context) : base(context)
        {
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetFirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public virtual async Task<bool> ExistAsync(TPrimaryKey id)
        {
            return await ExistAsync(entity => entity.Id.Equals(id));
        }
    }
}