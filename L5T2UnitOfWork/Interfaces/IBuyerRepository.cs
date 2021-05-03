using L5T2UnitOfWork.Models;
using System.Collections.Generic;

namespace L5T2UnitOfWork.Interfaces
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Dictionary<Buyer, decimal?> GetEachExpenses();

        Dictionary<Product, int> GetProducts(Buyer buyer);

        Buyer GetByName(string name);
    }
}
