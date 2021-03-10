using System;

namespace L1T5Compilation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Задача «Условная компиляция»");
            Console.WriteLine("Попробуйте условную компиляцию");
            Console.WriteLine("Если дебажная конфигурация, пусть выполнится один код");
            Console.WriteLine("Иначе другой код");
            Console.WriteLine();
            Console.WriteLine("Тут же попробуйте регионы");
#region TestRegion
#if DEBUG
            Console.WriteLine("Это DEBUG конфигурация");
#else
            Console.WriteLine("Это RELESE конфигурация");
#endif
#endregion
            Console.ReadKey();
        }
    }
}