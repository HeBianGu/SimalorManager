#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:17

 * 说明：
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> Carter-Tracy水体表 </summary>
     
    public class AQUTAB : RegionKey<AQUTAB.Item>
    {
        public AQUTAB(string _name)
            : base(_name)
        {

        }

        /// <summary> Carter-Tracy水体表实体 </summary>
        public class Item : HeBianGu.Product.SimalorManager.Item
        {
            /// <summary> 无因次时间 </summary>
            public string wycsj0;
            /// <summary> 无因次压力 </summary>
            public string wycyl1;

            string formatStr = "{0}{1}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, wycsj0.ToDD(), wycyl1.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.wycsj0 = newStr[0].ToString();
                            break;
                        case 1:
                            this.wycyl1 = newStr[1];
                            break;
                        default:
                            break;
                    }
                }
            }

        }



    }



}
