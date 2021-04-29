using System;
using Microsoft.Data.SqlClient;

namespace L3T2Transactions
{
    /// <summary>
    /// Лекция 3. Задача «Транзакции»
    /// </summary>
    internal class Program
    {
        private const string TableName = "dbo.Category";

        private static void Main()
        {
            //•Возьмите БД из предыдущей задачи
            const string connectionString = @"Data Source=.;Initial Catalog= ClothingStore;Integrated Security=true;";

            //•Подключитесь из кода к БД, начните транзакцию
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                //•Вставьте категорию
                try
                {
                    var sql1 = $"INSERT INTO {TableName}(Name) VALUES(N'Транзакция')";
                    var command1 = new SqlCommand(sql1, connection);
                    command1.Transaction = transaction;

                    Console.WriteLine("ЗАДАНИЕ 1: Вставим категорию 'Транзакция'.");

                    command1.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }

                PrintCategories(connection, true);

                //•Киньте исключение и отмените транзакцию
                //•Убедитесь, что категория не добавилась
                var transaction2 = connection.BeginTransaction();
                try
                {
                    var sql1 = $"INSERT INTO {TableName}(Name) VALUES(N'Исключение')";
                    var command1 = new SqlCommand(sql1, connection);
                    command1.Transaction = transaction2;

                    Console.WriteLine("ЗАДАНИЕ 2: Вставим категорию 'Исключение' и бросим исключение.");

                    command1.ExecuteNonQuery();

                    throw new Exception("Исключение брошено после выполнения команды ExecuteNonQuery, до коммита транзакции");
                }
                catch (Exception ex)
                {
                    transaction2.Rollback();

                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Выведем таблицу и проверим, добавилась ли новая категория.");
                }

                PrintCategories(connection, true);

                //•Потом уберите транзакцию, убедитесь, что категория
                //добавилась несмотря на ошибку(это пример как делать не нужно)
                try
                {
                    var sql1 = $"INSERT INTO {TableName}(Name) VALUES(N'ИсключениеБезТранзакции')";
                    var command1 = new SqlCommand(sql1, connection);

                    Console.WriteLine("ЗАДАНИЕ 3: Вставим категорию 'ИсключениеБезТранзакции' и бросим исключение.");

                    command1.ExecuteNonQuery();

                    throw new Exception("Исключение брошено после выполнения команды ExecuteNonQuery");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Выведем таблицу и проверим, добавилась ли новая категория.");
                }

                PrintCategories(connection, true);

                //удалим "лишние" категории
                var deletingTransaction = connection.BeginTransaction();
                try
                {
                    var sql = $@"DELETE {TableName} 
                                       WHERE Name=N'ИсключениеБезТранзакции' 
                                       OR Name=N'Транзакция' 
                                       OR Name=N'Исключение'";

                    var command = new SqlCommand(sql, connection);
                    command.Transaction = deletingTransaction;

                    Console.WriteLine("Очистили таблицу от тренеровочных категорий.");

                    command.ExecuteNonQuery();

                    deletingTransaction.Commit();
                }
                catch (Exception ex)
                {
                    deletingTransaction.Rollback();

                    Console.WriteLine(ex.Message);
                }

                PrintCategories(connection, false);
            }
        }

        private static void PrintPressKey(bool willBeContinue)
        {
            Console.WriteLine();
            Console.WriteLine(willBeContinue
                ? "Чтобы продолжить, нажмите любую клавишу."
                : "КОНЕЦ. Чтобы выйти, нажмите любую клавишу.");
            Console.WriteLine("----------------------------------------");

            Console.ReadKey();
        }

        private static void PrintCategories(SqlConnection connection, bool willBeContinue)
        {
            var sqlShowTable = $"SELECT * FROM {TableName}";
            using (var command = new SqlCommand(sqlShowTable, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    const int spaceId = -5;
                    const int spaceName = -35;

                    var columnName1 = reader.GetName(0);
                    var columnName2 = reader.GetName(1);

                    Console.WriteLine($"{columnName1,spaceId}{columnName2,spaceName}");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[columnName1],spaceId}{reader[columnName2],spaceName}");
                    }
                }
            }

            PrintPressKey(willBeContinue);
        }
    }
}


