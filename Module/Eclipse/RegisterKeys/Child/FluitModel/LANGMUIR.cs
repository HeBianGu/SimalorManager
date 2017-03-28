#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：

 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> langmuir参数（黑油模拟）</summary>
     
    public class LANGMUIR : RegionKey<LANGMUIR.Item>
    {
        public LANGMUIR(string _name)
            : base(_name)
        {

        }

        public class Item : OPT.Product.SimalorManager.ItemNormal
        {

            /// <summary> 压力 </summary>
            public string yl0;

            /// <summary> 气体浓度 </summary>
            public string qtnd1;


            string formatStr = "{0}{1} ";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, yl0.ToDD(), qtnd1.ToD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.yl0 = newStr[0];
                            break;
                        case 1:
                            this.qtnd1 = newStr[1];
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
                    qtnd1 = this.qtnd1,
                    yl0 = this.yl0
                };

                return item;
            }
        }
    }
}
