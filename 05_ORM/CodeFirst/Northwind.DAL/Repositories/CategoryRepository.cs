using Northwind.DAL.Abstractions.Repositories;
using Northwind.DAL.Contexts;
using Northwind.DAL.Entities;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Repositories
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(NorthwindContext context) : base(context)
        {
        }
    }
}