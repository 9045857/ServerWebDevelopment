using System;

namespace L2T4Log
{
    public class NumberWord
    {
        public int Number { get; set; }
        public string Word { get; set; }

        public NumberWord(int number)
        {
            Number = number;
            ParseToString(number);
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
