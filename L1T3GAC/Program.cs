using System;
using L1Classes;

namespace L1T3GAC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var person = new Person("Ivan", "Petrov");
            Console.WriteLine($"Персона {person} создана помощью класса из стороннего проекта");
            
            Console.ReadKey();
        }
    }
}
