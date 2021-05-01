using L5T2UnitOfWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static void ShowProductsAfterDelete(Product[] products, string deletedProduct)
        {
            Console.WriteLine($"ЗАДАНИЕ: Удалим из списка продуктов {deletedProduct}.");
            Console.WriteLine("СПИСОК ПРОДУКTOB");

            foreach (var product in products)
            {
                Console.WriteLine($"    {product.Name}");
            }

            PressAnyKey();
        }

        public static void ShowProductNameChanges(/*ProductRepository pr*/Product product, string oldName, string newName)
        {
            if (product == null)
            {
                Console.WriteLine($"Продукт '{newName}' не найден.");
                return;
            }

            Console.WriteLine($"ЗАДАНИЕ: Изменим имя у продукта '{oldName}'.");

            const int firstColumnWidth = 10;
            const int secondColumnWidth = 15;
            const int thirdColumnWidth = 15;

            Console.WriteLine("+{0}+{1}+{2}+", "".PadLeft(firstColumnWidth, '-'), "".PadLeft(secondColumnWidth, '-'), "".PadLeft(thirdColumnWidth, '-'));
            Console.WriteLine("|{0}|{1}|{2}|", "".PadLeft(firstColumnWidth, ' '), "Было    ".PadLeft(secondColumnWidth, ' '), "Стало    ".PadLeft(thirdColumnWidth, ' '));
            Console.WriteLine("+{0}+{1}+{2}+", "".PadLeft(firstColumnWidth, '-'), "".PadLeft(secondColumnWidth, '-'), "".PadLeft(thirdColumnWidth, '-'));
            Console.WriteLine("|{0}|{1}|{2}|", "Имя   ".PadLeft(firstColumnWidth, ' '), oldName.PadLeft(secondColumnWidth, ' '), product.Name.PadLeft(thirdColumnWidth, ' '));
            Console.WriteLine("+{0}+{1}+{2}+", "".PadLeft(firstColumnWidth, '-'), "".PadLeft(secondColumnWidth, '-'), "".PadLeft(thirdColumnWidth, '-'));

            PressAnyKey();
        }

        public static void ShowBuyersBuysCount(Dictionary<Buyer, int> buyersBuysCount, string searchedProduct)
        {
            var caption = $"ЗАДАНИЕ: Найти кто и сколько шт. купил '{searchedProduct}'.";
            const string firstColumnName = "Количество";
            const string secondColumnName = "Покупатель";

            ShowSolutionTable(buyersBuysCount, caption, firstColumnName, secondColumnName);
        }

        public static void ShowProducts(List<Product> products, int salesCount)
        {
            const string caption = "ЗАДАНИЕ: Найти самый продаваймый товар.";
            const string firstColumnName = "Количество";
            const string secondColumnName = "Категория";

            ShowSolutionTable(products, salesCount, caption, firstColumnName, secondColumnName);
        }

        public static void ShowCategorySales(Dictionary<Category, int> categorySales)
        {
            const string caption = "ЗАДАНИЕ: Найти сколько товаров каждой категории купили.";
            const string firstColumnName = "Количество";
            const string secondColumnName = "Категория";

            ShowSolutionTable(categorySales, caption, firstColumnName, secondColumnName);
        }

        public static void ShowBuyersExpenses(Dictionary<Buyer, decimal?> buyersExpenses)
        {
            const string caption = "ЗАДАНИЕ: Найти сколько каждый клиент потратил денег за все время.";
            const string firstColumnName = "Сумма";
            const string secondColumnName = "Покупатель";

            ShowSolutionTable(buyersExpenses, caption, firstColumnName, secondColumnName);
        }

        private static void ShowSolutionTable(IEnumerable<Product> products, int salesCount, string caption, string firstColumn, string secondColumn)
        {
            var productsSales = products.ToDictionary(product => product, product => salesCount);

            ShowSolutionTable(productsSales, caption, firstColumn, secondColumn);
        }

        public static void ShowBuyerProducts(Buyer buyer, Dictionary<Product, int> products)
        {
            Console.WriteLine($"Покупатель: {buyer.Name}");

            foreach (var pto in products)
            {
                Console.WriteLine($" {pto.Value} шт.   {pto.Key.Name}");
            }
        }

        private static void ShowSolutionTable<TKey, TValue>(Dictionary<TKey, TValue> keyValues, string caption, string firstColumn, string secondColumn)
        {
            Console.WriteLine(caption);

            PrintTableHBorder();
            PrintTableRow(firstColumn, secondColumn);
            PrintTableHBorder();

            var argumentsTypes = keyValues.GetType().GetGenericArguments();
            var keyType = argumentsTypes[0];

            if (keyType == typeof(Buyer))
            {
                foreach (var (key, value) in keyValues)
                {
                    PrintTableRow(value, (key as Buyer)?.Name);
                }
            }
            else if (keyType == typeof(Category))
            {
                foreach (var (key, value) in keyValues)
                {
                    PrintTableRow(value, (key as Category)?.Name);
                }
            }
            else if (keyType == typeof(Product))
            {
                foreach (var (key, value) in keyValues)
                {
                    PrintTableRow(value, (key as Product)?.Name);
                }
            }
            else
            {
                throw new Exception("Передан неизвестный тип ключа, для построения таблицы");
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
                Console.WriteLine($"|{"",valueSpace} | {key,keySpace} |");
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
    }
}
