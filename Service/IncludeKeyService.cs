using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace OPT.Product.SimalorManager
{
    /// <summary> INCLUDE关键字的服务 </summary>
    public class IncludeKeyService : ServiceFactory<IncludeKeyService>
    {
        /// <summary> 递归将一系列关键字合并到一个INCLUDE中 </summary>
        public List<BaseKey> ConvertToInclue(List<BaseKey> keys)
        {
            List<BaseKey> inclue = new List<BaseKey>();

            foreach (BaseKey k in keys)
            {
                if (k is INCLUDE)
                {
                    //  递归处
                    List<BaseKey> inc = ConvertToInclue(k.Keys);
                    inclue.AddRange(inc);
                }
                else
                {
                    inclue.Add(k);
                }
            }

            return inclue;
        }
    }
}
