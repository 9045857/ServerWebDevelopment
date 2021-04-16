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

                //-Через EF заполните эту БД данными 
                SetBeginProductSetData(db);
                SetBeginOrderBuyers(db);

                //-Попробуйте поиск, редактирование, удаление данных
                Console.WriteLine("ЗАДАНИЕ: Поиск покупателей товара 'Мясо'");
                BreakConsole();

                var searchProductName = "Мясо";
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

                PrintBlockEnd();

                //edit
                Console.WriteLine("ЗАДАНИЕ: Изменим стоимость 'Сок' на 345");
                BreakConsole();

                searchProductName = "Сок";
                var editingProduct = db.Products.FirstOrDefault(p => p.Name == searchProductName);

                if (editingProduct?.Price != null)
                {
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

                    BreakConsole();
                }

                //delete
                Console.WriteLine("ЗАДАНИЕ: Удалим 'Сок'");
                BreakConsole();

                if (editingProduct != null)
                {
                    db.Entry((object)editingProduct).State = EntityState.Deleted;
                    db.SaveChanges();

                    PrintCategoryProduct(db);
                }

                Console.WriteLine("Удалим 'ЭнерджиГель'/ Он есть во всех категориях.");
                Console.WriteLine("удаление сделаем напрямую через SQL");
                BreakConsole();

                try
                {
                    searchProductName = "ЭнерджиГель";
                    db.Database.ExecuteSqlInterpolated($"DELETE FROM Products WHERE Name = {searchProductName}");

                    PrintCategoryProduct(db);

                    Console.WriteLine("------");
                    Console.WriteLine("ВОПРОС!");
                    Console.WriteLine("Почему не видно удаления? Реально в базе все удалилось. ");

                    BreakConsole();

                    PrintProducts(db);

                    PrintBlockEndBreakConsole();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка удаления");
                    Console.WriteLine(e.Message);
                }

                //При помощи LINQ
                //•Найти самый часто покупаемый товар
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

                PrintBlockEndBreakConsole();

                //•Найти сколько каждый клиент потратил денег за все время
                Console.WriteLine("ЗАДАНИЕ: Найдем сколько каждый клиент потратил денег за все время.");

                var totalCosts = db.Buyers
                    .Select(b => new
                    {
                        name = b.Name,
                        costs = b.Orders
                            .SelectMany(o => o.ProductOrders)
                            .Select(po => po.Product.Price * po.Count)
                            .Sum()
                    });

                Console.WriteLine("Сумма        Клиент");
                foreach (var buyerCosts in totalCosts)
                {
                    Console.WriteLine($" {buyerCosts.costs}   {buyerCosts.name}");
                }

                PrintBlockEndBreakConsole();

                //•Вывести сколько товаров каждой категории купили
                Console.WriteLine("ЗАДАНИЕ: Найдем сколько товаров каждой категории купили.");

                var categoryProductsBought = db.Categories
                    .Select(c => new
                    {
                        c.Name,
                        boughtProductsCount = c.ProductCategories
                            .Select(pc => pc.Product)
                            .SelectMany(p => p.ProductOrders)
                            .Select(po => po.Count).Sum()
                    });

                Console.WriteLine("Кол-во купленных товаров  /  Категория");
                foreach (var category in categoryProductsBought)
                {
                    Console.WriteLine($"{category.boughtProductsCount}   {category.Name} ");
                }

                PrintBlockEndBreakConsole();
            }
        }

        private static void PrintBlockEndBreakConsole()
        {
            PrintBlockEnd();
            BreakConsole();
        }

        private static void PrintBlockEnd()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
        }

        private static void BreakConsole()
        {
            Console.WriteLine();
            Console.WriteLine("нажми любую клавишу...");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void PrintProducts(ShopContext db)
        {
            var products = db.Products.FromSqlRaw("SELECT * FROM Products")
                .ToList();

            Console.WriteLine("База ПРОДУКТОВ: ");
            foreach (var p in products)
            {
                Console.WriteLine($"  {p.Price}      {p.Name}");
            }

            Console.WriteLine("- - - - - -");
        }

        private static void PrintCategoryProduct(ShopContext db)
        {
            var categories = db.Categories
                .Include(pc => pc.ProductCategories)
                .ThenInclude(p => p.Product)
                .ToList();

            foreach (var c in categories)
            {
                Console.WriteLine($"Category: {c.Name}");

                Console.WriteLine("   Цена           Название");

                foreach (var p in c.ProductCategories)
                {
                    Console.WriteLine($"  {p.Product.Price}      {p.Product.Name}");
                }

                Console.WriteLine("- - - - - -");
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
    }
}
