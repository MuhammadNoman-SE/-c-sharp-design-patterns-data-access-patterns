using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace MyShop.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Get(Guid g);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        IEnumerable<T> All();
        void SaveChanges();
    }
}
