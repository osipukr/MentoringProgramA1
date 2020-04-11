using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Server.DataAccessLayer.Entities.Base;
using Northwind.Server.DataAccessLayer.Interfaces.Base;

namespace Northwind.Server.DataAccessLayer.Repositories.Base
{
    public abstract class Repository<TEntity, TPrimaryKey> : Repository<TEntity>, IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected Repository(DbContext context) : base(context)
        {
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await GetAsync(entity => entity.Id.Equals(id));
        }

        public virtual async Task<bool> ExistAsync(TPrimaryKey id)
        {
            return await ExistAsync(entity => entity.Id.Equals(id));
        }
    }
}