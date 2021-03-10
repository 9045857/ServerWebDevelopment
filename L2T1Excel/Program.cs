using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace L2T1Excel
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>()
            {
                new Person("Иван", "Иванов", 1, "111111111"),
                new Person("Семен", "Семенов", 2, "2222222"),
                new Person("Петр", "Петров", 3, "3333333333"),
                new Person("Александр", "Александров", 4, "4444444444"),
                new Person("Геннадий", "Геннадов", 5, "555555555"),
                new Person("Олег", "Олегов", 6, "6666666666"),
                new Person("Сидр", "Сидоров", 7, "77777777"),
                new Person("Прокл", "Проклов", 8, "8888888")
            };

            CreateXlsx(people);
        }

        private static void CreateXlsx(List<Person> people)
        {
            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "VDWWD";
                excelPackage.Workbook.Properties.Title = "Title of Document";
                excelPackage.Workbook.Properties.Subject = "EPPlus demo export data";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                //Add some text to cell A1
                //worksheet.Cells["A1"].Value = "My first EPPlus spreadsheet!";
                ////You could also use [line, column] notation:
                //worksheet.Cells[1, 2].Value = "This is cell B1!";


                const int nameColumn = 1;
                const int surnameColumn = 2;
                const int ageColumn = 3;
                const int phoneColumn = 4;
                var row = 1;

                worksheet.Cells[row, nameColumn].Value = "Имя";
                worksheet.Cells[row, surnameColumn].Value = "Фамилия";
                worksheet.Cells[row, ageColumn].Value = "Возраст";
                worksheet.Cells[row, phoneColumn].Value = "Телефон";

                foreach (var person in people)
                {
                    row++;

                    worksheet.Cells[row, nameColumn].Value = person.Name;
                    worksheet.Cells[row, surnameColumn].Value = person.Surname;
                    worksheet.Cells[row, ageColumn].Value = person.Age;
                    worksheet.Cells[row, phoneColumn].Value = person.PhoneNumber;
                }

                var fi = new FileInfo(@"File.xlsx");
                excelPackage.SaveAs(fi);
            }

        }

    }
}
