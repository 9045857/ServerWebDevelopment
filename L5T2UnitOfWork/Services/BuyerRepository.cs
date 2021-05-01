using System.Collections.Generic;
using System.Linq;
using L5T2UnitOfWork.Interfaces;
using L5T2UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace L5T2UnitOfWork.Services
{
    public class BuyerRepository : BaseEfRepository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(DbContext db) : base(db)
        {
        }

        public Dictionary<Buyer, decimal?> GetEachExpenses()
        {
            return DbSet
                .Include(b => b.Orders)
                .Select(b => new
                {
                    b,
                    expense = b.Orders.SelectMany(o => o.ProductOrders)
                        .Select(po => po.Product.Price * po.Count)
                        .Sum()
                })
                .ToDictionary(x => x.b, x => x.expense);
        }

        public Dictionary<Product, int> GetProducts(Buyer buyer)
        {
            return DbSet
                        .Where(b => b == buyer)
                        .SelectMany(b => b.Orders)
                        .SelectMany(o => o.ProductOrders)
                        .Select(po => new
                        {
                            po.Product,
                            po.Count
                        })
                        .ToList()
                        .GroupBy(x => x.Product)
                        .ToDictionary(x => x.Key,
                                      x => x
                                                                         .Select(y => y.Count)
                                                                         .Sum());
        }
        
        public Buyer GetByName(string name)
        {
            return DbSet.FirstOrDefault(b => EF.Functions.Like(b.Name, name));
        }
    }
}
