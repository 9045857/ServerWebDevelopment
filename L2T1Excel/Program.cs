using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;

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

        private static void SetTitleStyle(ExcelRange excelRange)
        {
                excelRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                excelRange.Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
           
                excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excelRange.Style.Font.Bold = true;

                excelRange.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#be0006"));
           
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

                var title = worksheet.Cells[row, nameColumn, row, phoneColumn];

                title.LoadFromArrays(new object[][]
                {
                        new[] { "Имя", "Фамилия", "Возраст", "Телефон" }
                });

                SetTitleStyle(title);

                SetPeopleTable(people, row, worksheet, nameColumn, surnameColumn, ageColumn, phoneColumn);

                //var cell = worksheet.Cells["E1"];
                //cell.Style.Font.Name = "Calibri";
                //cell.Style.Font.Size = 11;
                //cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffc7ce"));
                //cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //cell.Font.Color.SetColor(ColorTranslator.FromHtml("#be0006"));

                //cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffc7ce"));
                //cell.Style.Fill.PatternType = ExcelFillStyle.Solid;


                var fi = new FileInfo(@"File.xlsx");
                excelPackage.SaveAs(fi);
            }

        }

        private static void SetPeopleTable(List<Person> people, int row, ExcelWorksheet worksheet, int nameColumn, int surnameColumn,
            int ageColumn, int phoneColumn)
        {
            foreach (var person in people)
            {
                row++;

                worksheet.Cells[row, nameColumn].Value = person.Name;
                worksheet.Cells[row, surnameColumn].Value = person.Surname;
                worksheet.Cells[row, ageColumn].Value = person.Age;
                worksheet.Cells[row, phoneColumn].Value = person.PhoneNumber;
            }
        }
    }
}
