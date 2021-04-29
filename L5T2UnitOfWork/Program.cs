using L5T2UnitOfWork.Interfaces;
using L5T2UnitOfWork.Models;
using L5T2UnitOfWork.Services;
using Microsoft.EntityFrameworkCore;

namespace L5T2UnitOfWork
{
    internal class Program
    {
        private static void Main()
        {
            using (var uow = new UnitOfWork(new L4ShopContext()))
            {
                var productRepo = uow.GetRepository<IProductRepository>();
                var categoryRepo = uow.GetRepository<ICategoryRepository>();
                var buyerRepo = uow.GetRepository<IBuyerRepository>();
                var orderRepo = uow.GetRepository<IOrderRepository>();


                
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                //-Через EF заполните эту БД данными 
                InitialData.GetInitialData(db);

                //-Попробуйте поиск, редактирование, удаление данных
                //search 
                var pr = new ProductRepository(db);

                const string searchedProduct = "МясО";
                PrintConsole.ShowBuyersBuysCount(pr.GetBuyersBuysCount(searchedProduct), searchedProduct);

                //edit
                const string newProductName = "Животный белок";
                PrintConsole.ShowProductNameChanges(pr, searchedProduct, newProductName);

                //delete
                const string deletedProductName = "Сок";
                pr.Delete(pr.GetByName(deletedProductName));
                pr.Save();

                PrintConsole.ShowProductsAfterDelete(pr, deletedProductName);

                //При помощи LINQ
                //•Найти самый часто покупаемый товар
                PrintConsole.ShowProducts(pr.GetBestseller(), pr.GetMaxCountSales());

                //•Найти сколько каждый клиент потратил денег за все время
                var br = new BuyerRepository(db);
                PrintConsole.ShowBuyersExpenses(br.GetEachExpenses());

                //•Вывести сколько товаров каждой категории купили
                var cr = new CategoryRepository(db);
                PrintConsole.ShowCategorySales(cr.GetCategoriesSales());
            }
        }
    }
}
