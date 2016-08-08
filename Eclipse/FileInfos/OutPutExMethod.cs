using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    /// <summary> 有关输出控制的扩展方法 </summary>
    public static class OutPutExMethod
    {
        /// <summary> 获取所有注册的输出关键字 </summary>
        public static List<OutPutBindKey> GetAllOutPutKey()
        {
            List<OutPutBindKey> keys = new List<OutPutBindKey>();

            BaseKey baseKey;

            foreach (var keyName in EclipseKeyFactory.Instance.keyOutPutConfiger)
            {
                baseKey = KeyConfigerFactroy.Instance.CreateKey<BaseKey>(keyName);

                if (baseKey is CheckListKey)
                {
                    CheckListKey c = baseKey as CheckListKey;
                    //  设置初始值
                    c.IsCheck = string.Empty;
                    keys.Add(c);
                }
                else if (baseKey is SingleKey)
                {
                    SingleKey s = baseKey as SingleKey;
                    //  设置初始值
                    s.IsCheck = "0";
                    keys.Add(s);
                }
            }

            return keys;
        }

        /// <summary> 过滤SingleKey </summary>
        public static List<OutPutBindKey> RemveSingleKey(this List<OutPutBindKey> outPutKey)
        {
            List<OutPutBindKey> outs = outPutKey.FindAll(l => !(l is SingleKey && !bool.Parse(l.IsCheck.ToString())));

            return outs;
        }

        /// <summary> 过滤CheckListKey</summary>
        public static List<OutPutBindKey> RemveCheckListKey(this List<OutPutBindKey> outPutKey)
        {
            List<OutPutBindKey> outs = outPutKey.FindAll(l => !(l is CheckListKey && string.IsNullOrEmpty(l.IsCheck.ToString())));

            return outs;
        }

        /// <summary> 转换成Key </summary>
        public static List<BaseKey> ConvertToKey(this List<OutPutBindKey> outPutKey)
        {
            List<BaseKey> bs = new List<BaseKey>();

            outPutKey.ForEach(
                l =>
                {
                    if (l is SingleKey)
                    {
                        bs.Add(l as SingleKey);
                    }
                    else if (l is CheckListKey)
                    {
                        bs.Add(l as CheckListKey);
                    }
                });

            return bs;
        }
    }
}
