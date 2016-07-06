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

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 油相PVT </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class PVDO : RegionKey<PVDO.Item>
    {
        public PVDO(string _name)
            : base(_name)
        {

        }

        public class Item : OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 泡点压力 </summary>
            public string pdyl0;

            /// <summary> 体积系数 </summary>
            public string tjxs1;

            /// <summary> 粘度 </summary>
            public string nd2;

            string formatStr = "{0}{1}{2} ";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, pdyl0.ToDD(), tjxs1.ToDD(), nd2.ToDD());
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
                            this.pdyl0 = newStr[0];
                            break;
                        case 1:
                            this.tjxs1 = newStr[1];
                            break;
                        case 2:
                            this.nd2 = newStr[2];
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
                    pdyl0 = this.pdyl0,
                    tjxs1 = this.tjxs1,
                    nd2 = this.nd2,
                };

                return item;
            }
        }
    }
}
