namespace Northwind.DAL.Abstractions.Interfaces
{
    public interface IRepositoryFactory
    {
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}