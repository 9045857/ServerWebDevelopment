using System;
using System.Collections.Generic;
using System.Text;
using L5T2UnitOfWork.Models;

namespace L5T2UnitOfWork.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetBestseller();

        int GetMaxCountSales();

        Dictionary<Buyer, int> GetBuyersBuysCount(string productName);

        Product GetByName(string productName);

        void SetName(Product product, string newName);
    }
}
