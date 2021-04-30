using L4T1ShopEF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace L4T1ShopEF
{
    internal class Program
    {
        private static void Main()
        {
            using (var db = new ShopContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                //-Через EF заполните эту БД данными 
                InitialData.SetDb(db);

                //-Попробуйте поиск, редактирование, удаление данных
                //search 
                SolveSearchingProductName(db);

                //edit
                SolveEditingProduct(db);

                //delete
                SolveDeletingProduct(db);

                //При помощи LINQ
                //•Найти самый часто покупаемый товар
                SolveBestsellerSearch(db);

                //•Найти сколько каждый клиент потратил денег за все время
                SolveExpenses(db);

                //•Вывести сколько товаров каждой категории купили
                SolveCategorySales(db);
            }
        }

        private static void SolveCategorySales(ShopContext db)
        {
            var categoryProductsBought = db.Categories
                .Select(c => new
                {
                    c.Name,
                    boughtProductsCount = c.ProductCategories
                        .Select(pc => pc.Product)
                        .SelectMany(p => p.ProductOrders)
                        .Select(po => po.Count).Sum()
                });

            PrintConsole.ShowCategorySales(categoryProductsBought);
        }

        private static void SolveExpenses(ShopContext db)
        {
          var totalCosts = db.Buyers
                .Select(b => new
                {
                    name = b.Name,
                    costs = b.Orders
                        .SelectMany(o => o.ProductOrders)
                        .Sum(po => po.Product.Price * po.Count)
                });


            PrintConsole.ShowExpenses(totalCosts);
        }

        private static void SolveBestsellerSearch(ShopContext db)
        {
            var bestsellers = db.Products
                .Include(p => p.ProductOrders)
                .Where(p =>
                    p.ProductOrders
                     .Sum(po => po.Count) 
                      == db.Products
                           .Select(p1 => p1.ProductOrders
                                                  .Sum(po1 => po1.Count))
                           .OrderByDescending(s => s)
                           .First())
                .ToList();

            var salesCount = bestsellers[0].ProductOrders.Sum(po => po.Count);

            PrintConsole.ShowBestseller(bestsellers, salesCount);
        }

        private static void SolveDeletingProduct(ShopContext db)
        {
            var productName = "Сок";
            var product = db.Products.FirstOrDefault(p => p.Name == productName);

            if (product == null)
            {
                PrintConsole.ShowSearchProductError(productName);
                return;
            }

            var products = db.Products.Select(p => p.Name).ToList();

            db.Entry((object)product).State = EntityState.Deleted;
            db.SaveChanges();

            var productsAfterDeleting = db.Products.Select(p => p.Name).ToList();

            var deletingType = ".State";
            PrintConsole.ShowProductsAfterDelete(products, productsAfterDeleting, productName, deletingType);

            try
            {
                productName = "ЭнерджиГель";

                products = db.Products.Select(p => p.Name).ToList();

                db.Database.ExecuteSqlInterpolated($"DELETE FROM Products WHERE Name = {productName}");

                productsAfterDeleting = db.Products.Select(p => p.Name).ToList();

                deletingType = ".ExecuteSqlInterpolated";
                PrintConsole.ShowProductsAfterDelete(products, productsAfterDeleting, productName, deletingType);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка удаления");
                Console.WriteLine(e.Message);
            }
        }

        private static void SolveEditingProduct(ShopContext db)
        {
            const string productName = "Сок";
            var product = db.Products.FirstOrDefault(p => p.Name == productName);

            if (product == null)
            {
                PrintConsole.ShowSearchProductError(productName);
                return;
            }

            var oldPrice = product.Price;

            const decimal newPrice = 345;
            product.Price = newPrice;

            db.SaveChanges();

            PrintConsole.ShowProductPriceChanges(product, oldPrice, newPrice);
        }

        private static void SolveSearchingProductName(ShopContext db)
        {
            const string productName = "Мясо";
            var product = db.Products
                .Include(p => p.ProductOrders)
                    .ThenInclude(po => po.Order)
                    .ThenInclude(o=>o.Buyer)
                .FirstOrDefault(p => p.Name == productName);

            PrintConsole.ShowSalesProduct(product, productName);
        }
    }
}
