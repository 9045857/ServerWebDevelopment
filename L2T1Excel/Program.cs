using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace L2T1Excel
{
    internal class Program
    {
        private static void Main()
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
            excelRange.Style.Border.Bottom.Color.SetColor(Color.FromArgb(142, 180, 227));

            excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 112, 192));

            excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            excelRange.Style.Font.Bold = true;
            excelRange.Style.Font.Size = 12;

            excelRange.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#fff"));
        }

        private static void SetPeopleTableStyle(ExcelRange excelRange)
        {
            var rowTop = excelRange.Start.Row;
            var columnLeft = excelRange.Start.Column;
            var rowBottom = excelRange.End.Row;
            var columnRight = excelRange.End.Column;
            var rowsCount = excelRange.Rows;
            var columnsCount = excelRange.Columns;

            for (var i = 1; i < rowsCount; i += 2)
            {
                var rowIndex = rowTop + i;

                var rowRange = excelRange[rowIndex, columnLeft, rowIndex, columnRight];
                rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rowRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));
            }

            for (var i = columnLeft; i < columnsCount; i++)
            {
                var rowRange = excelRange[rowTop, i, rowBottom, i];
                rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rowRange.Style.Border.Right.Color.SetColor(Color.FromArgb(255, 255, 255));
            }

            var phonesRange = excelRange[rowTop, columnRight, rowBottom, columnRight];
            phonesRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            var agesRange = excelRange[rowTop, columnRight - 1, rowBottom, columnRight - 1];
            agesRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            var peopleRange = excelRange[rowTop, columnLeft, rowBottom, columnRight];
            peopleRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            peopleRange.Style.Font.Name = "Times New Roman";
            peopleRange.Style.Font.Size = 12;

            const double minWidth = 10.0;
            const double maxWidth = 20.0;
            excelRange[rowTop, columnLeft, rowBottom, columnRight].AutoFitColumns(minWidth, maxWidth);
        }

        private static void SetCaptionTable(ExcelRangeBase excelRange)
        {
            excelRange.Merge = true;

            excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            excelRange.Style.Font.Bold = true;
            excelRange.Style.Font.UnderLine = true;

            excelRange.Style.Font.Name = "Comic Sans MS";
            excelRange.Style.Font.Size = 14;

            const double rowHeightToText = 1.5;
            excelRange.Worksheet.Row(excelRange.Start.Row).Height *= rowHeightToText;
        }

        private static void SetCommonTableFeatures(ExcelRange title, ExcelRange mainTable)
        {
            var topRow = title.Start.Row;
            var leftColumn = title.Start.Column;

            var bottomRow = mainTable.End.Row;
            var rightColumn = mainTable.End.Column;

            var sheet = title.Worksheet;
            var table = sheet.Cells[topRow, leftColumn, bottomRow, rightColumn];

            table.Style.Border.BorderAround(ExcelBorderStyle.Medium,Color.CornflowerBlue);
        }

        private static void CreateXlsx(List<Person> people)
        {
            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "Student";
                excelPackage.Workbook.Properties.Title = "Task 1 - EPPLus";
                excelPackage.Workbook.Properties.Subject = "EPPlus task of second lecture.";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                var worksheet = excelPackage.Workbook.Worksheets.Add("Человеческая таблица");

                const int captionRow = 1;

                const int nameColumn = 1;
                const int surnameColumn = 2;
                const int ageColumn = 3;
                const int phoneColumn = 4;

                var captionRange = worksheet.Cells[captionRow, nameColumn, captionRow, phoneColumn];
                var caption = "Таблица людей";
                captionRange.Value = caption;

                SetCaptionTable(captionRange);

                const int firstRowTable = 2;
                const int titleRowsCount = 1;
                const int lastRowTitle = firstRowTable + titleRowsCount - 1;

                var title = worksheet.Cells[firstRowTable, nameColumn, lastRowTitle, phoneColumn];

                title.LoadFromArrays(new[]
                {
                        new[] { "Имя", "Фамилия", "Возраст", "Телефон" }
                });

                SetTitleStyle(title);

                const int firstRowPeople = lastRowTitle + 1;
                SetPeopleTable(people, firstRowPeople, worksheet, nameColumn, surnameColumn, ageColumn, phoneColumn);

                var lastRowPeople = lastRowTitle + people.Count;
                var peopleTableRange = worksheet.Cells[firstRowPeople, nameColumn, lastRowPeople, phoneColumn];

                SetPeopleTableStyle(peopleTableRange);

                SetCommonTableFeatures(title,peopleTableRange);

                try
                {
                    var fi = new FileInfo("File.xlsx");
                    excelPackage.SaveAs(fi);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        private static void SetPeopleTable(List<Person> people, int row, ExcelWorksheet worksheet, int nameColumn, int surnameColumn,
            int ageColumn, int phoneColumn)
        {
            foreach (var person in people)
            {
                worksheet.Cells[row, nameColumn].Value = person.Name;
                worksheet.Cells[row, surnameColumn].Value = person.Surname;
                worksheet.Cells[row, ageColumn].Value = person.Age;
                worksheet.Cells[row, phoneColumn].Value = person.PhoneNumber;

                row++;
            }
        }
    }
}
