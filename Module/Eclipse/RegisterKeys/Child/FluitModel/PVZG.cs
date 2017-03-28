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
    /// <summary> 气相PVT </summary>
    public class PVZG : RegionKey<PVZG.Item>
    {
        public PVZG(string _name)
            : base(_name)
        {

        }

        public int RegionCount = 0;

        public class Item: OPT.Product.SimalorManager.ItemNormal
        {
           /// <summary> 参考温度 </summary>
           public string ckwd0="0";
            /// <summary> 压力 </summary>
           public string yl1 = "0";
           /// <summary> 压缩因子 </summary>
           public string ysyz2 = "0";
           /// <summary> 粘度</summary>
           public string nd3;

           string formatStr = "{0}{1}{2}{3}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, ckwd0.ToSaveLockDD(), yl1.ToSaveLockDD(), ysyz2.ToSaveLockDD(), nd3.ToSaveLockDD());//, isUse.ToDD()
            }



            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.ckwd0 = newStr[0];
                            break;
                        case 1:
                            this.yl1 = newStr[1];
                            break;
                        case 2:
                            this.ysyz2 = newStr[2];
                            break;
                        case 3:
                            this.nd3 = newStr[3];
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
                    ckwd0 = this.ckwd0,
                    yl1 = this.yl1,
                    ysyz2 = this.ysyz2,
                    nd3 = this.nd3
                };

                return item;
            }
        }
    }
}
