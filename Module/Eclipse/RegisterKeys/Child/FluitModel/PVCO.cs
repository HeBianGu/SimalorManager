#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：

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
    /// <summary> 油相PVT </summary>
    public class PVCO : RegionKey<PVCO.Item>
    {
        public PVCO(string _name)
            : base(_name)
        {

        }

        public class Item : HeBianGu.Product.SimalorManager.ItemNormal
        {

            /// <summary> 泡点压力 </summary>
            public string pdyl0;

            /// <summary> 溶解气油比 </summary>
            public string rjqyb1;

            /// <summary> 体积系数 </summary>
            public string tjxs2;

            /// <summary> 粘度 </summary>
            public string nd3;
            /// <summary> 压缩系数 </summary>
            public string ysxs4;
            /// <summary> 粘度压缩系数 </summary>
            public string ndysxs5;

            string formatStr = "{0}{1}{2}{3}{4}{5} ";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, pdyl0.ToSaveLockDD(), rjqyb1.ToD(), tjxs2.ToSaveLockDD(), nd3.ToSaveLockDD(), ysxs4.ToSaveLockDD(), ndysxs5.ToSaveLockDD());
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
                            this.pdyl0 = newStr[0];
                            break;
                        case 1:
                            this.rjqyb1 = newStr[1];
                            break;
                        case 2:
                            this.tjxs2 = newStr[2];
                            break;
                        case 3:
                            this.nd3 = newStr[3];
                            break;
                        case 4:
                            this.ysxs4 = newStr[4];
                            break;
                        case 5:
                            this.ndysxs5 = newStr[5];
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
                    rjqyb1 = this.rjqyb1,
                    pdyl0 = this.pdyl0,
                    tjxs2 = this.tjxs2,
                    nd3 = this.nd3,
                    ysxs4 = this.ysxs4,
                    ndysxs5 = this.ndysxs5,
                };

                return item;
            }
        }
    }
}
