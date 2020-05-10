using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected ShoppingContext c;
        public GenericRepository(ShoppingContext _c) {
            c = _c;
        }
        public virtual T Add(T e)
        {
            return c.Add(e).Entity;
        }

        public virtual IEnumerable<T> All()
        {
            return c.Set<T>().ToList();
        }

        public virtual T Get(Guid id)
        {
            return c.Find<T>(id);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> e)
        {
            return c.Set<T>().Where(e).ToList();
        }

        public virtual void SaveChanges()
        {
            c.SaveChanges();
        }

        public virtual T Update(T entity) 
        {
            return c.Update(entity).Entity;
        }

    }
}
