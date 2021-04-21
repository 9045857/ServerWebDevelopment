using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace L5T1Migrations
{
    internal class Program
    {
        private static void Main()
        {
            using (var db = new ShopContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                InitialData.GetInitialData(db);

                var categories = db.Categories
                    .Select(c => new
                    {
                        c.Name,
                        products = c.ProductCategories
                            .Where(pc => pc.Product.ProductOrders.Count > 0)
                            .Select(pc => new
                            {
                                productName = pc.Product.Name,
                                countSales = pc.Product.ProductOrders.Sum(po => po.Count)
                            })
                    }).ToList();

                foreach (var category in categories)
                {
                    var name = category.Name.ToUpper();
                    Console.WriteLine($"{name}:");

                    foreach (var product in category.products)
                    {
                        Console.WriteLine($"{product.countSales} шт {product.productName}");
                    }
                }

                Console.ReadKey();
            }
        }
    }
}
