using System.Collections.Generic;
using L5T2UnitOfWork.Models;

namespace L5T2UnitOfWork.Interfaces
{
    public interface IBuyerRepository: IRepository<Buyer>
    {
        Dictionary<string, decimal?> GetEachExpenses();
    }
}
