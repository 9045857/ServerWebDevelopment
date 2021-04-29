using System;
using System.Collections.Generic;
using System.Text;

namespace L5T2UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        T GetRepository<T>() where T : class;
    }
}
