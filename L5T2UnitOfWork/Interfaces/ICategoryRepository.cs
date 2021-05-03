using L5T2UnitOfWork.Models;
using System.Collections.Generic;

namespace L5T2UnitOfWork.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Dictionary<Category, int> GetCategoriesSales();
    }
}
