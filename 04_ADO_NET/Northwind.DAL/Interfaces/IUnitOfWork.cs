namespace Northwind.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
    }
}