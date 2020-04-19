using Microsoft.Extensions.Logging;
using Northwind.Server.DataAccessLayer.Contexts;
using Northwind.Server.DataAccessLayer.Entities;
using Northwind.Server.DataAccessLayer.Interfaces;
using Northwind.Server.DataAccessLayer.Repositories.Base;

namespace Northwind.Server.DataAccessLayer.Repositories
{
    public class ShipperRepository : Repository<Shipper, int>, IShipperRepository
    {
        public ShipperRepository(NorthwindContext context, ILogger<IShipperRepository> logger) : base(context, logger)
        {
        }
    }
}