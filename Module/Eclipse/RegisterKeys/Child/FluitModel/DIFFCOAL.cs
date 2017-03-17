#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01
 * 文件名：START
 * 说明：
 * ROCK
             0            11
/
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
    /// <summary> 扩散系数（黑油模拟） </summary>
    public class DIFFCOAL : RegionKey<DIFFCOAL.Item>
    {
        public DIFFCOAL(string _name)
            : base(_name)
        {

        }

        public class Item : OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 气体扩散系数 </summary>
            public string qtksxs0;
            /// <summary> 重吸附因子 </summary>
            public string cxfyz1 = "1";
            /// <summary> 溶剂扩散系数 </summary>
            public string rjkzxs2;

            string formatStr = "{0}{1}{2}  ";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, qtksxs0.ToDD(), cxfyz1.ToDD(), rjkzxs2.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                //this.ID = Guid.NewGuid().ToString();

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.qtksxs0 = newStr[0];
                            break;
                        case 1:
                            this.cxfyz1 = newStr[1];
                            break;
                        case 2:
                            this.rjkzxs2 = newStr[2];
                            break;
                        default:
                            break;
                    }
                }
            }

            public override object Clone()
            {
                return new Item()
                {
                    qtksxs0 = this.qtksxs0,
                    cxfyz1 = this.cxfyz1,
                    rjkzxs2 = this.rjkzxs2
                };
            }
        }
    }
}
