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
using OPT.Product.SimalorManager.RegisterKeys.SimON;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 油水相渗 </summary>
     
    public class SGWFN : RegionKey<SGWFN.Item>
    {
        public SGWFN(string _name)
            : base(_name)
        {

        }

        /// <summary> 分区数量 </summary>
        public int RegionCount = 0;

        
        /// <summary> 转换成SWGF </summary>
        public List<SWGF> ConvertTo()
        {
            List<SWGF> swgfs = new List<SWGF>();
            foreach (var items in this.Regions)
            {
                SWGF swgf = new SWGF("SWGF");
                SWGF.Region r = new RegionKey<SWGF.Item>.Region(items.RegionIndex);
                swgf.Regions.Add(r);

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[items.Count - i-1];

                    SWGF.Item it = new SWGF.Item();
                    it.sbhd0 = (1 - item.hqbhd0.ToDouble()).ToString();
                    it.sdxdstl1 = item.sxxdstl2;
                    it.qxdxdstl2 = item.qxxdstl1;
                    it.mgyl3 = item.mgyl;
                    r.Add(it);
                }
              
                   

                swgfs.Add(swgf);
            }

            return swgfs;
        }


        public class Item : OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 含气饱和度 </summary>
            public string hqbhd0;
            /// <summary> 气相相对渗透率 </summary>
            public string qxxdstl1;
            /// <summary> 水相相对渗透率 </summary>
            public string sxxdstl2;
            /// <summary> 气水毛管压力 </summary>
            public string mgyl;


            string formatStr = "{0}{1}{2}{3}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, hqbhd0.ToSaveLockDD(), qxxdstl1.ToSaveLockDD(), sxxdstl2.ToSaveLockDD(), mgyl.ToSaveLockDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.hqbhd0 = newStr[0];
                            break;
                        case 1:
                            this.qxxdstl1 = newStr[1];
                            break;
                        case 2:
                            this.sxxdstl2 = newStr[2];
                            break;
                        case 3:
                            this.mgyl = newStr[3];
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
                    hqbhd0 = this.hqbhd0,
                    qxxdstl1 = this.qxxdstl1,
                    sxxdstl2 = this.sxxdstl2,
                    mgyl = this.mgyl
                };
                return item;
            }
        }
    }
}
