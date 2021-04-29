using System;
using System.Collections.Generic;
using System.Text;

namespace L5T2UnitOfWork.Interfaces
{
    public interface IRepository<T>: IRepo where T:class
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        T[] GetAll();

        T GetById (int id);
    }
}
