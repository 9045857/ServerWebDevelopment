using System;
using System.Configuration;
using L1Classes;

namespace L1T2Config
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Input Name: ");
            var name = Console.ReadLine();

            Console.Write("Input Surname: ");
            var surname = Console.ReadLine();

            var person = new Person(name, surname);
            Console.WriteLine($"You inputed: {person}");

            var url = ConfigurationManager.AppSettings["SiteUrl"];
            Console.WriteLine($"URL from config: {url}");

            Console.ReadKey();
        }
    }
}
