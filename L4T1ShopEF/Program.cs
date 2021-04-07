using L4T1ShopEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace L4T1ShopEF
{
    internal class Program
    {
        private static void Main()
        {
            using (var db = new ShopContext())
            {
                db.Database.EnsureCreated();

                SetBeginProductSetData(db);

               // db.SaveChanges();

                var categories = db.Categories.ToList();

                foreach (var c in categories)
                {
                    Console.WriteLine($"Category: {c.Name}");

                    foreach (var p in c.ProductCategories)
                    {
                        Console.WriteLine($"product: {p.Id} - {p.ProductId} - {p.Product.Id} -{p.CategoryId} - {p.Category.Id} - {p.Product.Name}");
                    }

                    Console.WriteLine("-------------------");
                }

                Console.WriteLine("----ПРОЙДЕМСЯ ПО ПРОДУКТАМ--------");

                var products = db.Products.ToList();
                foreach (var p in products)
                {
                    Console.WriteLine($"product: {p.Name} - {p.Price}");
                    Console.WriteLine("Id - ProductId - Product.Id - CategoryId - Category.Id - Name");

                    foreach (var c in p.ProductCategories)
                    {
                        Console.WriteLine($"category: {c.Id} - {c.ProductId} - {c.Product.Id} -{c.CategoryId} - {c.Category.Id} - {c.Category.Name}");
                    }

                    Console.WriteLine("-------------------");
                }


                var productCount = db.Products.Count();
                Console.WriteLine($"Всего {productCount} товаров");

                Console.WriteLine("Удалим базу данных.");
                Console.ReadKey();

                db.Database.EnsureDeleted();
            }
        }

        private static void SetBeginProductSetData(ShopContext db)
        {
            AddProduct(db, "Сок", new List<string> {"Питье"},  50);
            AddProduct(db, "Молоко", new List<string> {"Питье"},  80);
            AddProduct(db, "Вода", new List<string> {"Питье"},  20);
            AddProduct(db, "Кефир", new List<string> {"Питье", "Еда"}, 600);
            AddProduct(db, "Суп", new List<string> {"Питье", "Еда"},  15);
            AddProduct(db, "Мясо", new List<string> {"Еда"}, 105);
            AddProduct(db, "Сыр", new List<string> {"Еда"}, 505);
            AddProduct(db, "Бублик", new List<string> {"Еда"}, 35);
            AddProduct(db, "ЭнерджиГель", new List<string> {"Химия", "Питье", "Еда"}, 350);
            AddProduct(db, "Сода", new List<string> {"Химия", "Еда"}, 1500);
            AddProduct(db, "Мыло", new List<string> {"Химия"},  300);
            AddProduct(db, "Пемолюкс", new List<string> {"Химия"},  750);
        }

        private static IEnumerable<Category> GetCategories(ShopContext db, IEnumerable<string> categoryNames)
        {
            var categories = new List<Category>();

            foreach (var name in categoryNames)
            {
                var category = db.Categories.FirstOrDefault(c => c.Name == name) ?? new Category { Name = name };
                categories.Add(category);
            }

            return categories;
        }

        private static void AddProduct(ShopContext db, string productName, IEnumerable<string> categoryNames, decimal price)
        {
            var product = db.Products.FirstOrDefault(p => p.Name == productName);

            if (product != null)
            {
                return;
            }

            product = new Product { Name = productName, Price = price };
            var categories = GetCategories(db, categoryNames);

            foreach (var category in categories)
            {
                var productCategory = new ProductCategory { Category = category, Product = product };
                product.ProductCategories.Add(productCategory);
            }

            db.Products.Add(product);
            db.SaveChanges();
        }


        //private static void AddBuyer(ShopContext db, string name, string phone,string email)
        //{
        //    var product = db.Products.FirstOrDefault(p => p.Name == productName);

        //    if (product != null)
        //    {
        //        return;
        //    }

        //    product = new Product { Name = productName, Price = price };
        //    var categories = GetCategories(db, categoryNames);

        //    foreach (var category in categories)
        //    {
        //        var productCategory = new ProductCategory { Category = category, Product = product };
        //        product.ProductCategories.Add(productCategory);
        //        category.ProductCategories.Add(productCategory);
        //    }

        //    db.Products.Add(product);
        //    db.SaveChanges();
        //}


        //private void AddProduct(DbContext db, Product product, Category category)
        //{


        //    //var book = new Book
        //    //{
        //    //    Title = "Quantum Networking",
        //    //    Description = "faster-than-light data communications",
        //    //    PublishedOn = new DateTime(2057, 1, 1),
        //    //    Price = 220
        //    //};
        //    //var author = new Author { Name = "Future Person" };
        //    //book.AuthorsLink = new List<BookAuthor>
        //    //{
        //    //    new BookAuthor {
        //    //        Author = author,
        //    //        Book = book,
        //    //        Order = 0
        //    //    }
        //    //};

        //    ////Now add this book, with all its relationships, to the database
        //    //context.Books.Add(book);

        //}
    }
}
