using Microsoft.EntityFrameworkCore;

namespace Northwind.DAL.Abstractions.Interfaces
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext DbContext { get; }
    }
}