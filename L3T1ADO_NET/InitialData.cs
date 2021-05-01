using Microsoft.Data.SqlClient;
using System;

namespace L3T1ADO_NET
{
    public class InitialData
    {
        public static void CreateDb(SqlConnection connection, string dbName)
        {
            try
            {
                var query = $@"ALTER DATABASE {dbName} 
                                    SET SINGLE_USER
                                    WITH ROLLBACK IMMEDIATE;

                                    DROP DATABASE {dbName};";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("** Database Deleted Successfully");
                }
            }
            catch
            {
                // ignored
            }
            finally
            {
                var query = $"CREATE Database {dbName}";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("** Database Created Successfully");
                }
            }
        }

        public static void CreateTables(SqlConnection connection)
        {
            var queryCategory = $@"CREATE TABLE[dbo].[Category] (
                                         Id int IDENTITY(1,1) NOT NULL, 
                                         Name nvarchar (100) NOT NULL,
                                         CONSTRAINT PK_Categories PRIMARY KEY(Id))";

            using (var cmd = new SqlCommand(queryCategory, connection))
            {
                cmd.ExecuteNonQuery();
            }

            var queryProduct = $@"CREATE TABLE[dbo].[Product] (
                                        Id int IDENTITY(1,1) NOT NULL, 
                                        Name nvarchar (100) NOT NULL,
                                        Price decimal (18, 2) NOT NULL,
	                                    CategoryId int NOT NULL,
                                        CONSTRAINT PK_Products PRIMARY KEY (Id),
                                        CONSTRAINT FK_Category_Id FOREIGN KEY (CategoryId) REFERENCES dbo.Category (Id))";

            using (var cmd = new SqlCommand(queryProduct, connection))
            {
                cmd.ExecuteNonQuery();
            }

            FillDb(connection);
            Console.WriteLine("** Database Filled Successfully");
        }

        public static void FillDb(SqlConnection connection)
        {
            var queryCategories = string.Join
            (
                Environment.NewLine,
                "INSERT INTO [dbo].[Category] (Name) ",
                "VALUES ",
                "('Верхняя одежда'),",
                "('Платья и сарафаны'),",
                "('Свитера и кардиганы'),",
                "('Блузы и рубашки');"
            );

            using (var cmd = new SqlCommand(queryCategories, connection))
            {
                cmd.ExecuteNonQuery();
            }

            var queryProduct = string.Join
            (
                Environment.NewLine,
                "INSERT INTO [dbo].[Product] (Name, Price, CategoryId) ",
                "VALUES ",
                "('Плащ Violanti', 14497, 1),",
                "('Плащ Herno', 13484, 1),",
                "('Бомбер Y-3', 14193, 1),",
                "('Плащ Emporio Armani', 21225, 1),",
                "('ПлатьеAVEMOD', 2694, 2),",
                "('ПлатьеVittoria Vicci', 3459, 2),",
                "('ПлатьеEmansipe', 2907, 2),",
                "('ПлатьеAdzhedo', 3704, 2),",
                "('ПлатьеМодный дом Виктории Тишиной', 1479, 2),",
                "('ПлатьеПетербургский Швейный Дом', 3817, 2),",
                "('ДжемперPersona', 4550, 3),",
                "('СвитшотDesigners Remix', 4482, 3),",
                "('ДжемперCeline', 29691, 3),",
                "('КардиганGIORGIO GRATI', 9180, 3),",
                "('КардиганLuisa Spagnoli', 16884, 3),",
                "('ТуникаTuzzi', 4850, 4),",
                "('БлузаRiani', 4070, 4),",
                "('РубашкаEmporio Armani', 19368, 4),",
                "('ТуникаAngelo Marani', 6650, 4),",
                "('БлузаDior', 21195, 4),",
                "('БлузаMarc Aurel', 2950, 4);"
            );

            using (var cmd = new SqlCommand(queryProduct, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
