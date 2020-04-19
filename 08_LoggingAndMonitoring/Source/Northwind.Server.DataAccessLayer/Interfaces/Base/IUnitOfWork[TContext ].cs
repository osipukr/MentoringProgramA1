using Microsoft.EntityFrameworkCore;

namespace Northwind.Server.DataAccessLayer.Interfaces.Base
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext DbContext { get; }
    }
}