using Microsoft.EntityFrameworkCore;
using Northwind.DAL.Abstractions.Repositories;
using Northwind.DAL.Entities;
using Northwind.DAL.Interfaces;

namespace Northwind.DAL.Repositories
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }
    }
}