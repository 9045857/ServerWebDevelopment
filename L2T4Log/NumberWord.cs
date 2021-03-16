using System;
using NLog;

namespace L2T4Log
{
    public class NumberWord
    {
        public int Number { get; set; }
        public string Word { get; set; }

        public NumberWord(int number)
        {
            var logger = LogManager.GetCurrentClassLogger();

            Number = number;
            ParseToString(number);

            logger.Trace($"{Number}-{Word}");
        }

        private void ParseToString(int number)
        {
            switch (number)
            {
                case 0:
                    Word = "ноль";
                    break;
                case 1:
                    Word = "один";
                    break;
                case 2:
                    Word = "два";
                    break;
                case 3:
                    Word = "три";
                    break;
                default:
                    throw new Exception("Ошибка: Выход числа за границы.");
            }
        }
    }
}
