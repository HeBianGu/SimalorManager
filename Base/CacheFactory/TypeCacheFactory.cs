using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 类型缓存工厂 </summary>
    public abstract class TypeCacheFactory
    {
        private static TypeCacheFactory _instance = new ObjectFactoryImpl();

        protected Dictionary<string, Type> _objectCache = new Dictionary<string, Type>();

        /// <summary> 获取对象实例 </summary>
        public static TypeCacheFactory GetInstance()
        {
            return _instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Type GetType(string typePath, bool isCreate,bool isErr=true)
        {
            if (isCreate)
            {
                Type objType = Type.GetType(typePath, isErr);
                return objType;
            }

            if (!_objectCache.ContainsKey(typePath))
            {
                Type objType = Type.GetType(typePath, true);
                _objectCache.Add(typePath, objType);
            }
            return _objectCache[typePath];
        }


    }

    internal class ObjectFactoryImpl : TypeCacheFactory
    {

    }
}
