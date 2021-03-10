using System;

namespace L1T1Resharper
{
    internal class  Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const int t = 3;

            Console.Write("Введите координату: ");
            var f = Convert.ToInt32(Console.ReadLine());

            var p=new Point(t,f);

            Console.WriteLine(p);

            Console.ReadLine();
        }
    }
}
