using System;
using NLog;

namespace L2T4Log
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Info("Старт программы.");

            const string exitWord = "q";

            Console.WriteLine("Введите число от 0 до 3.");
            Console.WriteLine("Закончить - \"q.\"");

            var word = "";

            while (word != exitWord)
            {
                Console.Write("Ввод: ");
                word = Console.ReadLine();

                logger.Trace($"Ввели: {word}");

                try
                {
                    if (int.TryParse(word, out var number) & word != exitWord)
                    {
                        logger.Debug($"Ввели число типа int: {number}");

                        var numberWord = new NumberWord(number);
                        Console.WriteLine($"Цифра {numberWord.Number} пишется, как \"{numberWord.Word}\"");
                    }
                    else if (word != exitWord)
                    {
                        throw new Exception("Ошибка ввода: Нужно вводить цифры");
                    }
                }
                catch (Exception e)
                {
                    logger.Error($"Вводилось значение: \"{word}\"" +
                                 Environment.NewLine +
                                 e.Message);

                    Console.WriteLine(e.Message);
                }
            }

            logger.Info("Конец программы.");
        }
    }
}
