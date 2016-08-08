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
    /// <summary> 油相PVT </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class PVCDO : RegionKey<PVCDO.Item>
    {
        public PVCDO(string _name)
            : base(_name)
        {

        }


        public class Item : OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 参考压力 </summary>
            public string ckyl0;
            
            /// <summary> 体积系数 </summary>
            public string tjxs1;

                        /// <summary> 压缩系数 </summary>
            public string ysxs2;

            /// <summary> 粘度 </summary>
            public string nd3;

            /// <summary> 粘度压缩系数 </summary>
            public string ndysxs4;

            string formatStr = "{0}{1}{2}{3}{4}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, ckyl0.ToDD(), tjxs1.ToD(), ysxs2.ToDD(), nd3.ToDD(), ndysxs4.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                this.ID = Guid.NewGuid().ToString();

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.ckyl0 = newStr[0];
                            break;
                        case 1:
                            this.tjxs1 = newStr[1];
                            break;
                        case 2:
                            this.ysxs2 = newStr[2];
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
                    ysxs2 = this.ysxs2,
                    ckyl0 = this.ckyl0,
                    tjxs1 = this.tjxs1,
                    nd3 = this.nd3,
                    ndysxs4 = this.ndysxs4,
                };

                return item;
            }
        }
    }
}
