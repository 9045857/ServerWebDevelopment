using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using L5T2UnitOfWork.Models;
using L5T2UnitOfWork.Repositories;

namespace L5T2UnitOfWork
{
    internal class Program
    {
        private static void Main()
        {
            using (var db = new L4ShopContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                //-Через EF заполните эту БД данными 
                InitialData.GetInitialData(db);

                //-Попробуйте поиск, редактирование, удаление данных
                //search 
                //SolveSearchingProductName(db);

                ////edit
                //SolveEditingProduct(db);

                ////delete
                //SolveDeletingProduct(db);

                ////При помощи LINQ
                ////•Найти самый часто покупаемый товар
                //SolveBestsellerSearch(db);

                //•Найти сколько каждый клиент потратил денег за все время
                var br = new BuyerRepository(db);
                PrintConsole.ShowBuyersExpenses(br.GetEachExpenses());

                //•Вывести сколько товаров каждой категории купили
                var cr = new CategoryRepository(db);
                PrintConsole.ShowCategorySales(cr.GetCategoriesSales());
            }
        }

        private static void SolveCategorySales(L4ShopContext db)
        {
            Console.WriteLine("ЗАДАНИЕ: Найдем сколько товаров каждой категории купили.");

            //var categoryProductsBought = db.Categories
            //    .Select(c => new
            //    {
            //        c.Name,
            //        boughtProductsCount = c.ProductCategories
            //            .Select(pc => pc.Product)
            //            .SelectMany(p => p.ProductOrders)
            //            .Select(po => po.Count).Sum()
            //    });

            //using (var br = new BuyerRepository(db))
            //{
            //    Console.WriteLine("Кол-во купленных товаров  /  Категория");

            //    foreach (var (key, value) in br.GetEachExpenses())
            //    {
            //        Console.WriteLine($"{value}   {key} ");
            //    }

            //    PrintConsole.BlockEndBreak();
            //}
        }


        private static void SolveBestsellerSearch(L4ShopContext db)
        {
            Console.WriteLine("ЗАДАНИЕ: Найдем самый часто покупаемый товар");

            var maxBoughtProductCount = db.Products
                .OrderByDescending(p => p.ProductOrders.Count)
                .First()
                .ProductCategories
                .Count;

            var maxBoughtProducts = db.Products
                .Where(p => p.ProductOrders.Count == maxBoughtProductCount);

            Console.WriteLine($"РЕШЕНИЕ: Список продуктов, которые купили по {maxBoughtProductCount} раз:");
            foreach (var product in maxBoughtProducts)
            {
                Console.WriteLine($"{product.Name}");
            }

            PrintConsole.BlockEndBreak();
        }

        private static void SolveDeletingProduct(L4ShopContext db)
        {
            Console.WriteLine("ЗАДАНИЕ: Удалим 'Сок'");
            PrintConsole.Break();

            var searchProductName = "Сок";
            var editingProduct = db.Products.FirstOrDefault(p => p.Name == searchProductName);

            if (editingProduct != null)
            {
                db.Entry((object)editingProduct).State = EntityState.Deleted;
                db.SaveChanges();

                PrintConsole.CategoryProduct(db);
            }

            Console.WriteLine("Удалим 'ЭнерджиГель'/ Он есть во всех категориях.");
            Console.WriteLine("удаление сделаем напрямую через SQL");
            PrintConsole.Break();

            try
            {
                searchProductName = "ЭнерджиГель";
                db.Database.ExecuteSqlInterpolated($"DELETE FROM Products WHERE Name = {searchProductName}");

                PrintConsole.CategoryProduct(db);

                Console.WriteLine("------");
                Console.WriteLine("ВОПРОС!");
                Console.WriteLine("Почему не видно удаления? Реально в базе все удалилось. ");

                PrintConsole.Break();

                PrintConsole.Products(db);

                PrintConsole.BlockEndBreak();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка удаления");
                Console.WriteLine(e.Message);
            }
        }

        private static void SolveEditingProduct(L4ShopContext db)
        {
            Console.WriteLine("ЗАДАНИЕ: Изменим стоимость 'Сок' на 345");
            PrintConsole.Break();

            const string searchProductName = "Сок";
            var editingProduct = db.Products.FirstOrDefault(p => p.Name == searchProductName);

            if (editingProduct?.Price == null) return;

            var oldPrice = (decimal)editingProduct.Price;

            const decimal newPrice = 345;
            editingProduct.Price = newPrice;

            db.SaveChanges();

            if (db.Products != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                var actualPrice = db.Products
                    .FirstOrDefault(p => p.Name == searchProductName)
                    .Price;

                Console.WriteLine($"РЕШЕНИЕ: У продукта 'Сок' была цена {oldPrice}, а стала {actualPrice}");
            }

            PrintConsole.Break();
        }

        private static void SolveSearchingProductName(L4ShopContext db)
        {
            Console.WriteLine("ЗАДАНИЕ: Поиск покупателей товара 'Мясо'");
            PrintConsole.Break();

            const string searchProductName = "Мясо";
            var searchProduct = db.Products
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Order)
                .FirstOrDefault(p => p.Name == searchProductName);

            if (searchProduct != null)
            {
                Console.WriteLine($"РЕЗУЛЬТАТ. {searchProduct.Name} покупали:");

                foreach (var po in searchProduct.ProductOrders)
                {
                    Console.WriteLine($"{po.Count}шт  {po.Order.Buyer.Name}");
                }
            }

            PrintConsole.BlockEnd();
        }


    }
}
