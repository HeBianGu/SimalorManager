#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：


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
    /// <summary> 油气相渗 </summary>
    public class SLGOF : RegionKey<SLGOF.Item>
    {
        public SLGOF(string _name)
            : base(_name)
        {

        }

        /// <summary> 分区数量 </summary>
        public int RegionCount=0;


        public class Item: OPT.Product.SimalorManager.ItemNormal
        {

            /// <summary> 含液饱和度</summary>
            public string hybhd0;
           /// <summary> 气相相对渗透率 </summary>
            public string qxxdstl1;
           /// <summary> 油相相对渗透率 </summary>
            public string yxxdstl2;
           /// <summary> 油气毛管压力 </summary>
            public string yqmgyl3;


           string formatStr = "{0}{1}{2}{3}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, hybhd0.ToSaveLockDD(), qxxdstl1.ToSaveLockDD(), yxxdstl2.ToSaveLockDD(), yqmgyl3.ToSaveLockDD());
            }



            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.hybhd0 = newStr[0];
                            break;
                        case 1:
                            this.qxxdstl1 = newStr[1];
                            break;
                        case 2:
                            this.yxxdstl2 = newStr[2];
                            break;
                        case 3:
                            this.yqmgyl3 = newStr[3];
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
                    hybhd0 = this.hybhd0,
                    qxxdstl1 = this.qxxdstl1,
                    yxxdstl2 = this.yxxdstl2,
                    yqmgyl3 = this.yqmgyl3
                };
                return item;
            }
        }
    }
}
