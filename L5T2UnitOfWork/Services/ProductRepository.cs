using System.Collections.Generic;
using System.Linq;
using L5T2UnitOfWork.Interfaces;
using L5T2UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace L5T2UnitOfWork.Services
{
    public class ProductRepository : BaseEfRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }

        public List<Product> GetBestseller()
        {
            return DbSet
                .Where(p => p
                                .ProductOrders
                                .Select(po => po.Count)
                                .Sum()
                                == DbSet
                                .Select(p1 => p1
                                                .ProductOrders
                                                .Select(po1 => po1.Count)
                                                .Sum())
                                .OrderByDescending(s => s)
                                .FirstOrDefault())
                .ToList();
        }

        public int GetMaxCountSales()
        {
            return DbSet
                .Select(p => p
                    .ProductOrders
                    .Select(po => po.Count)
                    .Sum())
                .OrderByDescending(s => s)
                .FirstOrDefault();
        }

        public Dictionary<Buyer, int> GetBuyersBuysCount(string productName)
        {
            return DbSet
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Order)
                .FirstOrDefault(p => EF.Functions.Like(p.Name, productName))
                ?.ProductOrders
                .Select(po => po.Order.Buyer)
                .ToDictionary(
                    b => b,
                    b =>
                        b.Orders
                         .SelectMany(o => o.ProductOrders)
                         .Where(po => EF.Functions.Like(po.Product.Name, productName))
                         .Sum(po => po.Count)
                        );
        }

        public Product GetByName(string productName)
        {
            return  DbSet.FirstOrDefault(p => EF.Functions.Like(p.Name, productName));
        }

        public void SetName(Product product, string newName)
        {
            if (product == null)
            {
                return;
            }

            product.Name = newName;

            Db.SaveChanges();
        }

        public void SetName(string currentName, string newName)
        {
            var product = GetByName(currentName);

            if (product == null)
            {
                return;
            }

            product.Name = newName;

            Db.SaveChanges();
        }
    }
}
