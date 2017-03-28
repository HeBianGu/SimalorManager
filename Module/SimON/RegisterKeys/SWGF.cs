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

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 气水相渗 </summary>
     
    public class SWGF : RegionKey<SWGF.Item>
    {
        public SWGF(string _name)
            : base(_name)
        {

        }

        /// <summary> 分区数量 </summary>
        public int RegionCount=0;


        public class Item: OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 水饱和度Sw </summary>
            public string sbhd0;
           /// <summary> 水的相对渗透率krw </summary>
            public string sdxdstl1;
           /// <summary> 气相的相对渗透率krg </summary>
            public string qxdxdstl2;
           /// <summary> 毛管力Pcgw(=Pg-Pw) </summary>
            public string mgyl3;


           string formatStr = "{0}{1}{2}{3}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, sbhd0.ToSDD(), sdxdstl1.ToSDD(), qxdxdstl2.ToSDD(), mgyl3.ToSDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.sbhd0 = newStr[0];
                            break;
                        case 1:
                            this.sdxdstl1 = newStr[1];
                            break;
                        case 2:
                            this.qxdxdstl2 = newStr[2];
                            break;
                        case 3:
                            this.mgyl3 = newStr[3];
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
                    sbhd0 = this.sbhd0,
                    sdxdstl1 = this.sdxdstl1,
                    qxdxdstl2=this.qxdxdstl2,
                    mgyl3=this.mgyl3
                };
                return item;
            }
        }
    }
}
