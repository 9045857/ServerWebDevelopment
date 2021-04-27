using System;
using System.Collections.Generic;
using System.Text;
using L5T2UnitOfWork.Models;

namespace L5T2UnitOfWork.Interfaces
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Dictionary<Category, int> GetCategoriesSales();
    }
}
