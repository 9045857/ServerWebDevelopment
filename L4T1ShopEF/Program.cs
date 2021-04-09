using L4T1ShopEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

                SetBeginProductSetData(db);
                SetBeginOrderBuyers(db);

                //var categories = db.Categories
                //    .Include(pc => pc.ProductCategories)
                //    .ThenInclude(p => p.Product)
                //    .ToList();

                //foreach (var c in categories)
                //{
                //    Console.WriteLine($"Category: {c.Name}");

                //    foreach (var p in c.ProductCategories)
                //    {
                //        Console.WriteLine($"product: {p.Id} - {p.ProductId} - {p.Product.Id} -{p.CategoryId} - {p.Category.Id} - {p.Product.Name}");
                //    }

                //    Console.WriteLine("-------------------");
                //}

                //Console.WriteLine("----ПРОЙДЕМСЯ ПО ПРОДУКТАМ--------");

                //var products = db.Products
                //    //.Include(pc=>pc.ProductCategories)
                //    //.ThenInclude(c=>c.Category)
                //    .ToList();

                //foreach (var p in products)
                //{
                //    Console.WriteLine($"product: {p.Name} - {p.Price}");
                //    Console.WriteLine("Id - ProductId - Product.Id - CategoryId - Category.Id - Name");

                //    foreach (var c in p.ProductCategories)
                //    {
                //        Console.WriteLine($"category: {c.Id} - {c.ProductId} - {c.Product.Id} -{c.CategoryId} - {c.Category.Id} - {c.Category.Name}");
                //    }

                //    Console.WriteLine("-------------------");
                //}


                Console.WriteLine("----ПРОЙДЕМСЯ ПО ПОКУПКАМ--------");

                var orders = db.Orders
                    .Include(po => po.ProductOrders)
                    .ThenInclude(p => p.Product)
                    .ToList();

                foreach (var o in orders)
                {
                    Console.WriteLine($"order: {o.BoughtOn} | {o.Buyer.Name} | Total");
                    Console.WriteLine("Id - Count- Price - Total - Product ");

                    foreach (var pc in o.ProductOrders)
                    {
                        var total = pc.Product.Price * pc.Count;
                        Console.WriteLine($"{pc.Id} - {pc.Count} - {pc.Product.Price} -{total} - {pc.Product.Name}");
                    }

                    Console.WriteLine("-------------------");
                }

                //var productCount = db.Products.Count();
                //Console.WriteLine($"Всего {productCount} товаров");

                Console.WriteLine("Удалим базу данных.");
                Console.ReadKey();

                //  db.Database.EnsureDeleted();
            }
        }

        private static void SetBeginProductSetData(ShopContext db)
        {
            AddProduct(db, "Сок", new List<string> { "Питье" }, 50);
            AddProduct(db, "Молоко", new List<string> { "Питье" }, 80);
            AddProduct(db, "Вода", new List<string> { "Питье" }, 20);
            AddProduct(db, "Кефир", new List<string> { "Питье", "Еда" }, 600);
            AddProduct(db, "Суп", new List<string> { "Питье", "Еда" }, 15);
            AddProduct(db, "Мясо", new List<string> { "Еда" }, 105);
            AddProduct(db, "Сыр", new List<string> { "Еда" }, 505);
            AddProduct(db, "Бублик", new List<string> { "Еда" }, 35);
            AddProduct(db, "ЭнерджиГель", new List<string> { "Химия", "Питье", "Еда" }, 350);
            AddProduct(db, "Сода", new List<string> { "Химия", "Еда" }, 1500);
            AddProduct(db, "Мыло", new List<string> { "Химия" }, 300);
            AddProduct(db, "Пемолюкс", new List<string> { "Химия" }, 750);
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
            var product = GetProduct(db, productName);

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

        private static Product GetProduct(ShopContext db, string productName)
        {
            return db.Products
                .FirstOrDefault(p => p.Name == productName);
        }

        private static void SetBeginOrderBuyers(ShopContext db)
        {
            var buyer1 = new Buyer
            {
                Name = "Иванов Иван Иваныч",
                Phone = "111111111111",
                Email = "1@mail.ru"
            };
            var buyer2 = new Buyer
            {
                Name = "Сидоров Сидр Сидорович",
                Phone = "22222222",
                Email = "2@mail.ru"
            };
            var buyer3 = new Buyer
            {
                Name = "Петров Петр Петрович",
                Phone = "3333333333",
                Email = "3@mail.ru"
            };

            db.Buyers.AddRange(buyer1, buyer2, buyer3);

            var order1 = new Order
            {
                Buyer = buyer1,
                BoughtOn = new DateTime(2001, 1, 1),
            };
            var order2 = new Order
            {
                Buyer = buyer2,
                BoughtOn = new DateTime(2002, 2, 2)
            };
            var order3 = new Order
            {
                Buyer = buyer3,
                BoughtOn = new DateTime(2003, 3, 3)
            };
            var order4 = new Order
            {
                Buyer = buyer1,
                BoughtOn = new DateTime(2004, 4, 4)
            };

            db.Orders.AddRange(order1, order2, order3, order4);

            order1.ProductOrders = new List<ProductOrder>
            {
                new ProductOrder
                {
                    Count = 1,
                    Product = GetProduct(db, "Сок")
                },
                new ProductOrder
                {
                    Count = 1,
                    Product = GetProduct(db, "Кефир")
                },
                new ProductOrder
                {
                    Count = 1,
                    Product = GetProduct(db, "Бублик")
                }
            };

            order2.ProductOrders = new List<ProductOrder>
            {
                new ProductOrder
                {
                    Count = 2,
                    Product = GetProduct(db, "Суп")
                },
                new ProductOrder
                {
                    Count = 2,
                    Product = GetProduct(db, "Мясо")
                },
                new ProductOrder
                {
                    Count = 2,
                    Product = GetProduct(db, "Пемолюкс")
                }
            };

            order3.ProductOrders = new List<ProductOrder>
            {
                new ProductOrder
                {
                    Count = 3,
                    Product = GetProduct(db, "ЭнерджиГель")
                },
                new ProductOrder
                {
                    Count = 3,
                    Product = GetProduct(db, "Суп")
                },
                new ProductOrder
                {
                    Count = 3,
                    Product = GetProduct(db, "Сода")
                }
            };

            order4.ProductOrders = new List<ProductOrder>
            {
                new ProductOrder
                {
                    Count = 4,
                    Product = GetProduct(db, "Мыло")
                },
                new ProductOrder
                {
                    Count = 4,
                    Product = GetProduct(db, "Молоко")
                },
                new ProductOrder
                {
                    Count = 4,
                    Product = GetProduct(db, "Кефир")
                },
                new ProductOrder
                {
                    Count = 4,
                    Product = GetProduct(db, "Мясо")
                },
                new ProductOrder
                {
                    Count = 4,
                    Product = GetProduct(db, "Бублик")
                },
                new ProductOrder
                {
                    Count = 4,
                    Product = GetProduct(db, "Сода")
                },
                new ProductOrder
                {
                    Count = 4,
                    Product = GetProduct(db, "Вода")
                }
            };

            db.SaveChanges();
        }

        //private static void AddOrder(ShopContext db, Buyer buyer, List<ProductOrder> productOrders)
        //{
        //    var person = db.Buyers.FirstOrDefault(b => b.Name == buyer.Name);

        //    if (person != null)
        //    {
        //        return;//TODO AddBuyer
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
