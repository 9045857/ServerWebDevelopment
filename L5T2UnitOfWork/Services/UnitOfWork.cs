using System;
using System.Collections.Generic;
using System.Text;
using L5T2UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L5T2UnitOfWork.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _db;

        public UnitOfWork(DbContext db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public T GetRepository<T>() where T : class
        {
            if (typeof(T) == typeof(IProductRepository))
            {
                return new ProductRepository(_db) as T;
            }

            if (typeof(T) == typeof(IBuyerRepository))
            {
                return new BuyerRepository(_db) as T;
            }

            if (typeof(T) == typeof(IOrderRepository))
            {
                return new OrderRepository(_db) as T;
            }

            if (typeof(T) == typeof(ICategoryRepository))
            {
                return new CategoryRepository(_db) as T;
            }

            throw new Exception("Неизвестный тип репозитория: " + typeof(T));
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
