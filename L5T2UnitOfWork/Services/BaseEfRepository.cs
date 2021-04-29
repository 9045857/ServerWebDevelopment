using System.Linq;
using L5T2UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L5T2UnitOfWork.Services
{
    public class BaseEfRepository<T> : IRepository<T> where T : class, IRepo
    {
        protected DbContext Db;
        protected DbSet<T> DbSet;

        public BaseEfRepository(DbContext db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }

        public virtual void Save()
        {
            Db.SaveChanges();
        }

        public void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            Db.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (Db.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);
        }

        public T[] GetAll()
        {
            return DbSet.ToArray();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }
    }
}
