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
            using (var db = new L4ShopContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                InitialData.LoadInDb(db);
            }

            using (var uow = new UnitOfWork(new L4ShopContext()))
            {
                var productRepo = uow.GetRepository<IProductRepository>();
                var categoryRepo = uow.GetRepository<ICategoryRepository>();
                var buyerRepo = uow.GetRepository<IBuyerRepository>();

                //-Попробуйте поиск, редактирование, удаление данных
                //search 
                const string searchedProduct = "Сок";
                var buyersBuysCount = productRepo.GetBuyersBuysCount(searchedProduct);

                PrintConsole.ShowBuyersBuysCount(buyersBuysCount, searchedProduct);

                //edit
                const string oldProductName = "МясО";

                var product = productRepo.GetByName(oldProductName);
                var oldName = product.Name;

                const string newProductName = "Животный белок";
                productRepo.SetName(product, newProductName);

                PrintConsole.ShowProductNameChanges(productRepo.GetByName(newProductName), oldName, newProductName);

                //delete
                const string deletedProductName = "Сок";
                product = productRepo.GetByName(deletedProductName);

                productRepo.Delete(product);
                uow.Save();

                var products = productRepo.GetAll();
                PrintConsole.ShowProductsAfterDelete(products, deletedProductName);

                const string buyerName = "Иванов Иван Иваныч";
                var buyer = buyerRepo.GetByName(buyerName);

                var buyerProducts = buyerRepo.GetProducts(buyer);
                PrintConsole.ShowBuyerProducts(buyer, buyerProducts);

                //При помощи LINQ
                //•Найти самый часто покупаемый товар
                PrintConsole.ShowProducts(productRepo.GetBestseller(), productRepo.GetMaxCountSales());

                //•Найти сколько каждый клиент потратил денег за все время
                PrintConsole.ShowBuyersExpenses(buyerRepo.GetEachExpenses());

                //•Вывести сколько товаров каждой категории купили
                PrintConsole.ShowCategorySales(categoryRepo.GetCategoriesSales());
            }
        }
    }
}
