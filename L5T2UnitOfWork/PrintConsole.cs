using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using L5T2UnitOfWork.Models;

namespace L5T2UnitOfWork
{
    public class PrintConsole
    {
        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            Console.WriteLine();
        }

        public static void ShowCategorySales(Dictionary<string, int> categorySales)
        {
            const string caption = "ЗАДАНИЕ: Найти сколько товаров каждой категории купили.";
            const string firstColumnName = "Количество";
            const string secondColumnName = "Категория";

            ShowSolutionTable(categorySales, caption, firstColumnName, secondColumnName);
        }

        public static void ShowBuyersExpenses(Dictionary<string, decimal?> buyersExpenses)
        {
            const string caption = "ЗАДАНИЕ: Найти сколько каждый клиент потратил денег за все время.";
            const string firstColumnName = "Сумма";
            const string secondColumnName = "Покупатель";

            ShowSolutionTable(buyersExpenses, caption, firstColumnName, secondColumnName);
        }

        private static void ShowSolutionTable<TValue>(Dictionary<string, TValue> keyValues, string caption, string firstColumn, string secondColumn)
        {
            Console.WriteLine(caption);

            PrintTableHBorder();
            PrintTableRow(firstColumn, secondColumn);
            PrintTableHBorder();

            foreach (var (key, value) in keyValues)
            {
                PrintTableRow(value, key);
            }

            PrintTableHBorder();

            PressAnyKey();
        }

        private static void PrintTableRow(object value, object key)
        {
            const int valueSpace = 10;
            const int keySpace = -25;

            if (value == null)
            {
                Console.WriteLine($"|{"0",valueSpace} | {key,keySpace} |");
                return;
            }

            switch (value)
            {
                case decimal _:
                    Console.WriteLine($"|{value,valueSpace:F} | {key,keySpace} |");
                    break;
                case int _:
                    Console.WriteLine($"|{value,valueSpace:d} | {key,keySpace} |");
                    break;
                default:
                    Console.WriteLine($"|{value,valueSpace} | {key,keySpace} |");
                    break;
            }
        }

        private static void PrintTableHBorder()
        {
            const int firstColumnWidth = 12;
            const int secondColumnWidth = 28;
            Console.WriteLine("+{0}{1}", "+".PadLeft(firstColumnWidth, '-'), "+".PadLeft(secondColumnWidth, '-'));
        }

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
