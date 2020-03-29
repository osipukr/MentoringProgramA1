using System;
using System.Threading.Tasks;

namespace Northwind.DAL.Abstractions.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>() where TRepository : IRepository;

        Task<int> SaveChangesAsync();
    }
}