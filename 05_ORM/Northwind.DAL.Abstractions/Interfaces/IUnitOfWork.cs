using System;
using System.Threading.Tasks;

namespace Northwind.DAL.Abstractions.Interfaces
{
    public interface IUnitOfWork : IRepositoryFactory, IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}