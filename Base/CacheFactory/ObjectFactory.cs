using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 类型工厂 </summary>
    public abstract class ObjectFactory
    {
        private static ObjectFactory _instance = new ObjectFactoryImp2();

        protected Dictionary<Type, Object> _objectCache = new Dictionary<Type, Object>();

        /// <summary> 获取对象实例 </summary>
        public static ObjectFactory GetInstance()
        {
            return _instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public T GetObject<T>(Type t, object[] args, bool isCreate)
        {
            if (isCreate)
            {
                return (T)CreateObject(t, args);
            }

            if (!_objectCache.ContainsKey(t))
            {
                _objectCache.Add(t, CreateObject(t, args));
            }
            return (T)_objectCache[t];
        }

        public T GetObject<T>(object[] args, bool isCreate)
        {
            return GetObject<T>(typeof(T), args, isCreate);
        }

        public T GetObject<T>(object[] args)
        {
            return GetObject<T>(typeof(T), args, false);
        }

        public T GetObject<T>(bool isCreate)
        {
            return GetObject<T>(typeof(T), null, isCreate);
        }

        public T GetObject<T>()
        {
            return GetObject<T>(typeof(T), null, false);
        }

        private Object CreateObject(Type t, object[] args)
        {
            return Activator.CreateInstance(t, args);
        }
    }

    internal class ObjectFactoryImp2 : ObjectFactory
    {

    }
}
