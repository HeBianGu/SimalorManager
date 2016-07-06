using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 缓存工厂 </summary>
    public abstract class CatcheFileFactroy
    {

        static CatcheFileFactroy _instance = null;
        /// <summary> 单例 </summary>
        public static CatcheFileFactroy CreateInstance()
        {
            if (_instance == null)
            {
                _instance = new EclFileFactroy_();
            }

            return _instance;
        }
        /// <summary> 文件缓存 </summary>
        Dictionary<string, Object> cache = new Dictionary<string, Object>();

        /// <summary> 通过缓存创建文件类 </summary>
        /// <typeparam name="T"> 文件类型 </typeparam>
        /// <param name="fileName"> 文件名称 </param>
        /// <param name="args"> 参数 </param>
        /// <returns> 文件类型 </returns>
        public T CreateFileByName<T>(string fileName, object[] args, bool isCreate)
        {
            if (!cache.ContainsKey(fileName))
            {
                cache.Add(fileName, CreateObject<T>(args));
            }
            else
            {
                if (isCreate)
                {
                    //  替换
                    cache.Remove(fileName);
                    cache.Add(fileName, CreateObject<T>(args));
                }
            }

            return (T)cache[fileName];
        }

        /// <summary> 通过缓存创建文件类 </summary>
        /// <typeparam name="T"> 文件类型 </typeparam>
        /// <param name="fileName"> 文件名称 </param>
        /// <param name="args"> 参数 </param>
        /// <returns> 文件类型 </returns>
        public T CreateFileByName<T>(string fileName, object[] args) where T : BaseFile
        {
            return CreateFileByName<T>(fileName, args, true);
        }

        private Object CreateObject<T>(object[] args)
        {
            return Activator.CreateInstance(typeof(T), args);
        }

    }
    /// <summary> 内部类用于不允许实例化 </summary>
    internal class EclFileFactroy_ : CatcheFileFactroy
    {

    }
}
