using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain.Lazy
{
    public interface IValueHolder<T>
    {
        T GetValue(object p);
    }
    public class ValueHolder<T>:IValueHolder<T>
    {
        private Func<object, T> e;
        T v;

        public ValueHolder(Func<object,T> _e) {
            e = _e;
        }
        public T GetValue(object p) {
            if (null == v)
                v = e(p);
            return v;
        }
    }
}
