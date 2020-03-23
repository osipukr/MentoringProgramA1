using Northwind.DAL.Entities;

namespace Northwind.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void SetInWorkStatus(int id);
        void SetCompletedStatus(int id);
    }
}