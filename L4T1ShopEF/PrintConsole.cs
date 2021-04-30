using L4T1ShopEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace L4T1ShopEF
{
    public class PrintConsole
    {
        private static void WritePressAnyKey()
        {
            Console.WriteLine();
            Console.WriteLine("...press any key");
            Console.ReadKey();
            Console.WriteLine("************************************");
            Console.WriteLine();
        }

        public static void ShowCategorySales(IQueryable<dynamic> categoryProductsBought)
        {
            Console.WriteLine("ЗАДАНИЕ: Найдем сколько товаров каждой категории купили.");
            Console.WriteLine("РЕШЕНИЕ. ");

            const int countWidth = 10;
            const int nameWidth = -10;

            Console.WriteLine($"{"Количество",countWidth} | {"Категория",nameWidth}");
            Console.WriteLine("-----------|-------------");

            foreach (var category in categoryProductsBought)
            {
                Console.WriteLine($"{category.boughtProductsCount,countWidth} | {category.Name,nameWidth} ");
            }

            Console.WriteLine("-------------------------");

            WritePressAnyKey();
        }

        public static void ShowSalesProduct(Product product, string searchedProduct)
        {
            Console.WriteLine($"ЗАДАНИЕ: Поиск покупателей товара '{searchedProduct}'");

            if (product == null)
            {
                Console.WriteLine($"РЕЗУЛЬТАТ: Покупатели {searchedProduct} отсутствуют.");
                return;
            }

            Console.WriteLine($"РЕШЕНИЕ. {searchedProduct} покупали:");
            Console.WriteLine();

            const int countWidth = 10;
            const int nameWidth = -10;

            Console.WriteLine($"{"Количество",countWidth} | {"Покупатель",nameWidth}");
            Console.WriteLine("-----------|-------------");

            foreach (var po in product.ProductOrders)
            {
                Console.WriteLine($"{po.Count,countWidth:D} | {po.Order.Buyer.Name,nameWidth}");
            }

            Console.WriteLine("-------------------------");

            WritePressAnyKey();
        }

        public static void ShowProductPriceChanges(Product product, decimal oldPrice, decimal newPrice)
        {
            Console.WriteLine($"ЗАДАНИЕ: Изменим стоимость '{product.Name}' на {newPrice}");

            Console.WriteLine($"РЕШЕНИЕ: У продукта 'Сок' была цена {oldPrice:F2}, а стала {product.Price:F2}");

            WritePressAnyKey();
        }

        public static void ShowSearchProductError(string name)
        {
            Console.WriteLine($"РЕЗУЛЬТАТ: ОШИБКА. Продукт {name} не найден!");

            WritePressAnyKey();
        }

        public static void ShowProductsAfterDelete(List<string> products, List<string> productsAfterDeleting, string deletedProduct, string deletingType)
        {
            Console.WriteLine($"ЗАДАНИЕ: Удалим '{deletedProduct}' через {deletingType}");

            var productCount = products.Count >= productsAfterDeleting.Count ? products.Count : productsAfterDeleting.Count;

            Console.WriteLine($"РЕШЕНИЕ. Список ДО и ПОСЛЕ удаления '{deletedProduct}'.");
            Console.WriteLine();

            const int beforeWidth = 13;
            const int afterWidth = -13;

            Console.WriteLine($"{"Было",beforeWidth} | {"Стало",afterWidth}");
            Console.WriteLine("--------------|-------------");

            for (var i = 0; i < productCount; i++)
            {
                var name = i < products.Count ? products[i] : "";
                var name2 = i < productsAfterDeleting.Count ? productsAfterDeleting[i] : "";

                if (name == deletedProduct)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{name,beforeWidth}");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"{name,beforeWidth}");
                }

                Console.WriteLine($" | {name2,afterWidth}");
            }

            Console.WriteLine("----------------------------");

            WritePressAnyKey();
        }

        public static void ShowBestseller(List<Product> bestsellers, int salesCount)
        {
            Console.WriteLine("ЗАДАНИЕ: Найдем самый часто покупаемый товар");

            if (bestsellers == null)
            {
                Console.WriteLine("ОШИБКА. null список бестселлеров!");
                return;
            }

            if (bestsellers.Count == 0)
            {
                Console.WriteLine("ОШИБКА. Ни одного товара не продано!");
                return;
            }

            Console.WriteLine("РЕЗУЛЬТАТ.");
            Console.WriteLine();
            Console.WriteLine($"Продукты, которые купили {salesCount} раз.");
            Console.WriteLine("------------------------");

            foreach (var product in bestsellers)
            {
                Console.WriteLine($"\t{product.Name}");
            }

            Console.WriteLine("-------------------------");

            WritePressAnyKey();
        }

        public static void ShowExpenses(IQueryable<dynamic> totalCosts)
        {
            Console.WriteLine("ЗАДАНИЕ: Найдем траты каждого клиента.");
            Console.WriteLine("РЕШЕНИЕ.");
            Console.WriteLine();

            const int expenseWidth = 10;
            const int nameWidth = -10;

            Console.WriteLine($"{"Сумма",expenseWidth} | {"Клиент",nameWidth}");
            Console.WriteLine("-----------|-------------");

            foreach (var buyerCosts in totalCosts)
            {
                Console.WriteLine($"{buyerCosts.costs,expenseWidth:F2} | {buyerCosts.name,nameWidth}");
            }

            Console.WriteLine("-------------------------");

            WritePressAnyKey();
        }
    }
}
