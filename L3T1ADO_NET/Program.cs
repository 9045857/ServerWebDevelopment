using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace L3T1ADO_NET
{
    internal class Program
    {
        private const int SpaceId = -5;
        private const int SpaceName = -35;
        private const int SpacePrice = 10;
        private const int SpaceCategory = -50;

        private static void Main()
        {
            const string dbName = "L3T1Shop";

            var connectionString = @"Data Source=.;Initial Catalog=master;Integrated Security=true;";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                InitialData.CreateDb(connection, dbName);
            }

            connectionString = $@"Data Source=.;Initial Catalog={dbName};Integrated Security=true;";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                InitialData.CreateTables(connection);

                Console.WriteLine();

                //• Вывести общее количество товаров
                const string sqlProductsCount = "SELECT COUNT(*) FROM dbo.Product";
                using (var command = new SqlCommand(sqlProductsCount, connection))
                {
                    Console.WriteLine("ЗАДАНИЕ 1: Вывести общее количество товаров");

                    var productsCount = (int)command.ExecuteScalar();
                    Console.WriteLine($"Всего разных продуктов = {productsCount}");
                    PrintPressAnyKey(true);
                }

                //• Создать некоторую категорию и товар
                const string printerCategory = "Принтеры";
                const string sqlInsertCategory = "INSERT INTO dbo.Category(Name) VALUES(@categoryName)";

                using (var command = new SqlCommand(sqlInsertCategory, connection))
                {
                    command.Parameters.Add(new SqlParameter("@categoryName", printerCategory) { SqlDbType = SqlDbType.NVarChar });
                    command.ExecuteNonQuery();
                }

                int categoryId;
                const string sqlSelectCategoryId = @"SELECT Id FROM dbo.Category 
                                                     WHERE Name=@categoryName";

                using (var command = new SqlCommand(sqlSelectCategoryId, connection))
                {
                    command.Parameters.Add(new SqlParameter("@categoryName", printerCategory) { SqlDbType = SqlDbType.NVarChar });
                    categoryId = (int)command.ExecuteScalar();
                }

                const string sqlInsertProduct = @"INSERT INTO dbo.Product(Name, Price, CategoryId) 
                                                VALUES
                                                ('HP Color LaserJet Pro M254dw', 1111,@categoryId),
                                                ('Samsung Xpress M2020', 2222,@categoryId),
                                                ('Brother HL-L2340DWR', 3333,@categoryId),
                                                ('Canon i-SENSYS LBP7018C', 4444,@categoryId),
                                                ('Canon i-SENSYS LBP7110Cw', 5555,@categoryId)";

                using (var command = new SqlCommand(sqlInsertProduct, connection))
                {
                    command.Parameters.Add(new SqlParameter("@categoryId", categoryId) { SqlDbType = SqlDbType.Int });
                    command.ExecuteNonQuery();
                }

                const string sqlShowCategory = "SELECT * FROM dbo.Category";
                using (var command = new SqlCommand(sqlShowCategory, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var columnName1 = reader.GetName(0);
                        var columnName2 = reader.GetName(1);

                        Console.WriteLine("ЗАДАНИЕ 2: Создать некоторую категорию и товар");
                        Console.WriteLine($"Создали категорию '{printerCategory}'");
                        Console.WriteLine("-=Таблица Категорий=-");

                        Console.WriteLine($"{columnName1,SpaceId}{columnName2,SpaceName}");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[columnName1],SpaceId}{reader[columnName2],SpaceName}");
                        }
                    }
                }

                Console.WriteLine();
                Console.WriteLine("-=Таблица Продуктов с новыми позициями=-");

                PrintCategoryProducts(categoryId, connection);
                PrintPressAnyKey(true);

                //• Отредактировать некоторый товар 'Samsung Xpress M2020' -> 8888
                const string sqlCorrectedCategory = @"UPDATE dbo.Product 
                                                   SET Price=@newPrice 
                                                   WHERE Name=@correctedProduct";

                const string correctedProduct = "Samsung Xpress M2020";
                const float newPrice = (float)8888.0;

                using (var command = new SqlCommand(sqlCorrectedCategory, connection))
                {
                    command.Parameters.Add(new SqlParameter("@newPrice", newPrice) { SqlDbType = SqlDbType.Decimal });
                    command.Parameters.Add(new SqlParameter("@correctedProduct", correctedProduct) { SqlDbType = SqlDbType.NVarChar });
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("ЗАДАНИЕ 3: Отредактировать некоторый товар");
                Console.WriteLine();
                Console.WriteLine($"Редактируем цену {correctedProduct} -> {newPrice}");
                Console.WriteLine("-=Таблица Продуктов с отредактированной позицией=-");

                PrintCategoryProducts(categoryId, connection);
                PrintPressAnyKey(true);

                //• Удалить некоторый товар
                const string sqlCutCategory = @"DELETE dbo.Product 
                                                WHERE Name=@cutProduct";

                const string cutProduct = "Brother HL-L2340DWR";

                using (var command = new SqlCommand(sqlCutCategory, connection))
                {
                    command.Parameters.Add(new SqlParameter("@cutProduct", cutProduct) { SqlDbType = SqlDbType.NVarChar });
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("ЗАДАНИЕ 4: Удалить некоторый товар");
                Console.WriteLine();

                Console.WriteLine($"Удаляем позицию {cutProduct}");
                Console.WriteLine("-=Таблица Продуктов после удаления позиции=-");

                PrintCategoryProducts(categoryId, connection);
                PrintPressAnyKey(true);

                //• Выгрузить весь список товаров вместе с именами категорий через
                //    reader, и распечатайте все данные в цикле
                const string sqlShowAllProducts = @"SELECT dbo.Product.Id, dbo.Product.Name, dbo.Product.Price, dbo.Category.Name AS 'Категория' 
                                                  FROM dbo.Product 
                                                  LEFT JOIN dbo.Category
                                                  ON dbo.Product.CategoryId=dbo.Category.Id";

                using (var command = new SqlCommand(sqlShowAllProducts, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var columnName1 = reader.GetName(0);
                        var columnName2 = reader.GetName(1);
                        var columnName3 = reader.GetName(2);
                        var columnName4 = reader.GetName(3);

                        Console.WriteLine("ЗАДАНИЕ 5: Выгрузить весь список товаров вместе с именами категорий через reader, и распечатайте все данные в цикле");
                        Console.WriteLine();

                        Console.WriteLine("-=Таблица продуктов через READER=-");
                        Console.WriteLine($"{columnName1,SpaceId}{columnName2,SpaceName}{columnName3,SpacePrice}  {columnName4,SpaceCategory}");

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[columnName1],SpaceId}{reader[columnName2],SpaceName}{reader[columnName3],SpacePrice}  {reader[columnName4],SpaceCategory}");
                        }
                        PrintPressAnyKey(true);
                    }
                }

                //• Выгрузить весь список товаров вместе с именами категорий в
                //DataSet через SqlDataAdapter, и распечатайте все данные в цикле
                const string sql = @"SELECT * FROM dbo.Product; 
                                     SELECT * FROM dbo.Category";

                using (var adapter = new SqlDataAdapter(sql, connection))
                {
                    adapter.TableMappings.Add("Table", "Product");
                    adapter.TableMappings.Add("Table1", "Category");

                    using (var ds = new DataSet())
                    {
                        adapter.Fill(ds);

                        Console.WriteLine("ЗАДАНИЕ 6: Выгрузить весь список товаров вместе с именами категорий в DataSet через SqlDataAdapter, и распечатайте все данные в цикле");
                        Console.WriteLine();

                        Console.WriteLine("-=Таблица продуктов через SqlDataAdapter=-");

                        foreach (DataRow row in ds.Tables["Product"].Rows)
                        {
                            var categoryName = GetCategoryName((int)row[3], ds.Tables["Category"]);
                            Console.WriteLine(
                                $"{row[0],SpaceId}{row[1],SpaceName}{row[2],SpacePrice}  {categoryName,SpaceCategory}");
                        }
                    }
                }
            }

            PrintPressAnyKey(false);
        }

        private static string GetCategoryName(int id, DataTable categoryTable)
        {
            const int idIndex = 0;
            const int idName = 1;

            foreach (DataRow row in categoryTable.Rows)
            {
                if (id == (int)row[idIndex])
                {
                    return (string)row[idName];
                }
            }

            return null;
        }

        private static void PrintPressAnyKey(bool willBeContinue)
        {
            Console.WriteLine();
            Console.WriteLine(willBeContinue
                ? "... press any key."
                : "КОНЕЦ. Чтобы выйти, нажмите любую клавишу.");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            Console.ReadKey();
        }

        private static void PrintCategoryProducts(int categoryId, SqlConnection connection)
        {
            var sqlShowPrinterCategory = $@"SELECT * FROM dbo.Product 
                                                  WHERE CategoryId=@categoryId";

            using (var command = new SqlCommand(sqlShowPrinterCategory, connection))
            {
                command.Parameters.Add(new SqlParameter("@categoryId", categoryId) { SqlDbType = SqlDbType.Int });

                using (var reader = command.ExecuteReader())
                {
                    var columnName1 = reader.GetName(0);
                    var columnName2 = reader.GetName(1);
                    var columnName3 = reader.GetName(2);

                    Console.WriteLine($"{columnName1,SpaceId}{columnName2,SpaceName}{columnName3,SpacePrice}");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[columnName1],SpaceId}{reader[columnName2],SpaceName}{reader[columnName3],SpacePrice}");
                    }
                }
            }
        }
    }
}
