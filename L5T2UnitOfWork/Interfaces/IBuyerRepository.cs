using System.Collections.Generic;
using System.Linq;
using L5T2UnitOfWork.Models;

namespace L5T2UnitOfWork.Interfaces
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Dictionary<Buyer, decimal?> GetEachExpenses();

        Dictionary<Product, int> GetProducts(Buyer buyer);

        Buyer GetByName(string name);
    }
}
