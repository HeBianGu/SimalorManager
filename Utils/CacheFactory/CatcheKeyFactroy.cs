using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> 关键字工厂 </summary>
    public abstract class CatcheKeyFactroy
    {

        static CatcheKeyFactroy _instance = new CatcheKeyFactroy_();
        /// <summary> 获取实例 </summary>
        public static CatcheKeyFactroy Instance
        {
            get { return CatcheKeyFactroy._instance; }
            set { CatcheKeyFactroy._instance = value; }
        }
        /// <summary> 文件缓存 </summary>
        List<BaseKey> cache = new List<BaseKey>();

        /// <summary> 创建或获取缓存中文件 </summary>
        public BaseKey GetKey(string keyName, bool isCreate)
        {

            if (!cache.Exists(l=>l.Name==keyName))
            {
                cache.Add(new Key(keyName));
            }
            else
            {
                if(isCreate)
                {
                    cache.RemoveAll(l => l.Name == keyName);
                    cache.Add(new Key(keyName));
                }
            }

            return cache.Find(l=>l.Name==keyName);
        }


        /// <summary> 内部类用于不允许实例化 </summary>
        private class CatcheKeyFactroy_ : CatcheKeyFactroy
        {

        }
    }

}
