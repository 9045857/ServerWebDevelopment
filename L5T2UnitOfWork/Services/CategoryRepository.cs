using System.Collections.Generic;
using System.Linq;
using L5T2UnitOfWork.Interfaces;
using L5T2UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace L5T2UnitOfWork.Services
{
    public class CategoryRepository : BaseEfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext db) : base(db)
        {
        }

        public Dictionary<Category, int> GetCategoriesSales()
        {
            return DbSet
                .Select(c => new
                {
                    c,
                    salesProductsCount = c.ProductCategories
                        .Select(pc => pc.Product)
                        .SelectMany(p => p.ProductOrders)
                        .Select(po => po.Count).Sum()
                })
                .ToDictionary(x => x.c, x => x.salesProductsCount);
        }
    }
}
