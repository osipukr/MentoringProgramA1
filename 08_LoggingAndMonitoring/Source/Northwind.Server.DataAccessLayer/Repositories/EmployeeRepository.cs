using Microsoft.Extensions.Logging;
using Northwind.Server.DataAccessLayer.Contexts;
using Northwind.Server.DataAccessLayer.Entities;
using Northwind.Server.DataAccessLayer.Interfaces;
using Northwind.Server.DataAccessLayer.Repositories.Base;

namespace Northwind.Server.DataAccessLayer.Repositories
{
    public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(NorthwindContext context, ILogger<IEmployeeRepository> logger) : base(context, logger)
        {
        }
    }
}