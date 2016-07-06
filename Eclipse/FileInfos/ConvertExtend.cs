using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    public static class ConvertExtend
    {
        /// <summary> 递归将一系列关键字合并到一个INCLUDE中 </summary>
        public static List<BaseKey> ConvertToInclue(this List<BaseKey> keys)
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
