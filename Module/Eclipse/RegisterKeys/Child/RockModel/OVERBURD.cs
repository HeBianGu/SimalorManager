#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：


/
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 岩石特性 </summary>
    public class OVERBURD : RegionKey<OVERBURD.Item>
    {
        public OVERBURD(string _name)
            : base(_name)
        {

        }

        public class Item: HeBianGu.Product.SimalorManager.ItemNormal
        {

            /// <summary> 深度 </summary>
            public string sd0;
            /// <summary> 上覆岩层压力 </summary>
            public string sfycyl1;

           string formatStr = "{0}{1}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, sd0.ToDD(), sfycyl1.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.sd0 = newStr[0];
                            break;
                        case 1:
                            this.sfycyl1 = newStr[1];
                            break;
                        default:
                            break;
                    }
                }
            }



            public override object Clone()
            {
                Item item = new Item()
                {
                    sd0 = this.sd0,
                    sfycyl1 = this.sfycyl1
                };
                return item;
            }
        }
    }
}
