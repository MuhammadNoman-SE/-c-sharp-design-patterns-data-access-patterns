using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public interface IRepository<T>
    {

        T Get(Guid id);

        IEnumerable<T> All();

        T Add(T id);

        IEnumerable<T> Find(Expression<Func<T, bool>> e);

        void SaveChanges();

        T Update(T id);

    }
}
