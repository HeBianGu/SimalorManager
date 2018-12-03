#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:33

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
     
    public class MULTFLT : ItemsKey<MULTFLT.Item>
    {
        public MULTFLT(string _name)
            : base(_name)
        {

        }

        /// <summary> 项实体 </summary>
        public class Item : HeBianGu.Product.SimalorManager.Item
        {
            /// <summary> 断层名 </summary>
            public string dcm0;

            /// <summary> 断层传导率乘子 </summary>
            public string cdlcz1;

            string formatStr = "{0}{1} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, dcm0.ToEclStr(), cdlcz1.ToDD()); ;
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.dcm0 = newStr[0];
                            break;
                        case 1:
                            this.cdlcz1 = newStr[1];
                            break;
                        default:
                            break;
                    }
                }

            }

        }

    }
}
