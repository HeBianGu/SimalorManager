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
    /// <summary> 岩石特性 </summary>
    public class ROCKTAB : RegionKey<ROCKTAB.Item>
    {
        public ROCKTAB(string _name)
            : base(_name)
        {

        }

        public class Item: OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 压力 </summary>
            public string y10;

            /// <summary> 孔隙度乘子 </summary>
            public string kxdcz1;

              /// <summary> X方向传导率乘子 </summary>
            public string xfxcdlcz2;

            /// <summary> Y方向传导率乘子 </summary>
            public string yfxcdlcz3;

              /// <summary> Z方向传导率乘子 </summary>
            public string zfxcdlcz4;

            string formatStr = "{0}{1}{2}{3}{4}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, y10.ToDD(), kxdcz1.ToDD(), xfxcdlcz2.ToDD(), yfxcdlcz3.ToD(), zfxcdlcz4.ToD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.y10 = newStr[0];
                            break;
                        case 1:
                            this.kxdcz1 = newStr[1];
                            break;
                        case 2:
                            this.xfxcdlcz2 = newStr[2];
                            break;
                        case 3:
                            this.yfxcdlcz3 = newStr[3];
                            break;
                        case 4:
                            this.zfxcdlcz4 = newStr[4];
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
                    y10 = this.y10,
                    kxdcz1 = this.kxdcz1,
                    xfxcdlcz2 = this.xfxcdlcz2,
                    yfxcdlcz3 = this.yfxcdlcz3,
                    zfxcdlcz4 = this.zfxcdlcz4
                };
                return item;
            }
        }
    }
}
