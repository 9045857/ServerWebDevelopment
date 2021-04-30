using L4T1ShopEF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace L4T1ShopEF
{
    public class PrintConsole
    {
        internal static void PrintBlockEndBreakConsole()
        {
            WriteBlockEnd();
            WritePressAnyKey();
        }

        private static void WriteBlockEnd()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
        }

        private static void WritePressAnyKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any Key...");
            Console.ReadKey();
            Console.WriteLine();
        }
        
        public static void ShowCategorySales(IQueryable<dynamic> categoryProductsBought)
        {
            Console.WriteLine("ЗАДАНИЕ: Найдем сколько товаров каждой категории купили.");

            Console.WriteLine("Кол-во купленных товаров  /  Категория");

            foreach (var category in categoryProductsBought)
            {
                Console.WriteLine($"{category.boughtProductsCount}   {category.Name} ");
            }

            PrintBlockEndBreakConsole();
        }

        public static void ShowSalesProduct(Product product, string searchedProduct)
        {
            Console.WriteLine($"ЗАДАНИЕ: Поиск покупателей товара '{searchedProduct}'");
            WritePressAnyKey();

            if (product == null)
            {
                Console.WriteLine($"РЕЗУЛЬТАТ: Покупатели {searchedProduct} отсутствуют.");
                WriteBlockEnd();
                return;
            }

            Console.WriteLine($"РЕЗУЛЬТАТ. {searchedProduct} покупали:");

            foreach (var po in product.ProductOrders)
            {
                Console.WriteLine($"{po.Count:D}шт  {po.Order.Buyer.Name}");
            }

            WriteBlockEnd();
        }

        public static void ShowProductPriceChanges(Product product, decimal oldPrice, decimal newPrice)
        {
            Console.WriteLine($"ЗАДАНИЕ: Изменим стоимость '{product.Name}' на {newPrice}");

            Console.WriteLine($"РЕШЕНИЕ: У продукта 'Сок' была цена {oldPrice:F2}, а стала {product.Price:F2}");

            PrintBlockEndBreakConsole();
        }

        public static void ShowSearchProductError(string name)
        {
            Console.WriteLine($"РЕЗУЛЬТАТ: ОШИБКА. Продукт {name} не найден!");

            PrintBlockEndBreakConsole();
        }

        public static void ShowProductsAfterDelete(List<string> products, List<string> productsAfterDeleting, string deletedProduct, string deletingType)
        {
            //Console.WriteLine("ЗАДАНИЕ: Удалим 'Сок' (.State) и 'ЭнерджиГель' (.ExecuteSqlInterpolated)");

            Console.WriteLine($"ЗАДАНИЕ: Удалим '{deletedProduct}' через {deletingType})");

            var productCount = products.Count >= productsAfterDeleting.Count ? products.Count : productsAfterDeleting.Count;

            for (var i = 0; i < productCount; i++)
            {
                var name = i < products.Count ? products[i] : "";
                var name2 = i < productsAfterDeleting.Count ? productsAfterDeleting[i] : "";

                Console.WriteLine($"{name}   {name2}");
            }
            PrintBlockEndBreakConsole();
        }

        public static void ShowBestseller(List<Product> bestsellers, int salesCount)
        {
            Console.WriteLine("ЗАДАНИЕ: Найдем самый часто покупаемый товар");
            WritePressAnyKey();


            if (bestsellers == null)
            {
                Console.WriteLine("ОШИБКА. null список бестселлеров!");
                PrintBlockEndBreakConsole();
                return;
            }

            if (bestsellers.Count == 0)
            {
                Console.WriteLine("ОШИБКА. Бестселлер не найден!");
                PrintBlockEndBreakConsole();
                return;
            }

            Console.WriteLine($"РЕЗУЛЬТАТ: Список продуктов, которые купили по {salesCount} раз:");

            foreach (var product in bestsellers)
            {
                Console.WriteLine($"{product.Name}");
            }

            PrintBlockEndBreakConsole();
        }

        public static void ShowExpenses(IQueryable<dynamic> totalCosts)
        {
            Console.WriteLine("ЗАДАНИЕ: Найдем траты каждого клиента.");
            WritePressAnyKey();

            Console.WriteLine("Сумма        Клиент");
            Console.WriteLine("--------------------");

            foreach (var buyerCosts in totalCosts)
            {
                Console.WriteLine($" {buyerCosts.costs}   {buyerCosts.name}");
            }

            PrintBlockEndBreakConsole();
        }
    }
}
