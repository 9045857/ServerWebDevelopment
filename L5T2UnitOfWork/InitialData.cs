using System;
using System.Collections.Generic;
using System.Linq;
using L5T2UnitOfWork.Model;

namespace L5T2UnitOfWork
{
    internal class InitialData
    {
        public static void GetInitialData(ShopContext db)
        {
            SetBeginProductSetData(db);
            SetBeginOrderBuyers(db);
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
            return categoryNames.Select(name => db.Categories.FirstOrDefault(c => c.Name == name) ?? new Category { Name = name }).ToList();
        }

        private static void AddProduct(ShopContext db, string productName, IEnumerable<string> categoryNames,
            decimal price)
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
                Email = "1@mail.ru",
                //Birthday = new DateTime(1981, 1, 1)
            };
            var buyer2 = new Buyer
            {
                Name = "Сидоров Сидр Сидорович",
                Phone = "22222222",
                Email = "2@mail.ru",
                //Birthday = new DateTime(1982, 2, 2)
            };
            var buyer3 = new Buyer
            {
                Name = "Петров Петр Петрович",
                Phone = "3333333333",
                Email = "3@mail.ru",
                //Birthday = new DateTime(1983, 3, 3)
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
