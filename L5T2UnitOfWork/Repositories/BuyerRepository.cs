using System;
using System.Collections.Generic;
using System.Linq;
using L5T2UnitOfWork.Interfaces;
using L5T2UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace L5T2UnitOfWork.Repositories
{
    public class BuyerRepository : BaseEfRepository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(DbContext db) : base(db)
        {
        }

        public Dictionary<Buyer, decimal?> GetEachExpenses()
        {
            //return DbSet.
            //    ToDictionary(b => b.Name,
            //    b => b.Orders
            //      .SelectMany(o => o.ProductOrders)
            //      .Select(po => po.Product.Price * po.Count)
            //      .Sum());
            ////return DbSet.Select(b => new KeyValuePair<string,decimal?>(b.Name,
            ////    b.Orders.SelectMany(o => o.ProductOrders)
            ////      .Select(po => po.Product.Price * po.Count)
            ////      .Sum())).ToDictionary(x=>x.Key,x=>x.Value);
            return DbSet.Select(b => new
                {
                    b,
                    expense = b.Orders.SelectMany(o => o.ProductOrders)
                        .Select(po => po.Product.Price * po.Count)
                        .Sum()
                })
                .ToDictionary(x => x.b, x => x.expense);
        }
    }
}
