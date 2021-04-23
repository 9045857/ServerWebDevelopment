using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using L5T2UnitOfWork.Models;

namespace L5T2UnitOfWork
{
    public class PrintConsole
    {
        public static void BlockEndBreak()
        {
            BlockEnd();
            Break();
        }

        public static void BlockEnd()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
        }

        public static void Break()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            Console.WriteLine();
        }

        public static void Products(L4ShopContext db)
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

        public static void CategoryProduct(L4ShopContext db)
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
    }
}
