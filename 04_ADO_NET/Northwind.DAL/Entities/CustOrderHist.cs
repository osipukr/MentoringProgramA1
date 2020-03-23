using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Entities
{
    public class CustOrderHist : IEntity
    {
        public string ProductName { get; set; }
        public int Total { get; set; }
    }
}