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
    /// <summary> 气相PVT </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class PVDG : RegionKey<PVDG.Item>
    {
        public PVDG(string _name)
            : base(_name)
        {

        }

        public int RegionCount = 0;

        public class Item: OPT.Product.SimalorManager.ItemNormal
        {
           /// <summary> 压力 </summary>
           public double yl=0;
            /// <summary> 体积系数 </summary>
           public double tjxs = 0;
           /// <summary> 粘度 </summary>
           public double nd = 0;
           /// <summary> 使用气体压缩因子</summary>
           public string isUse;

           string formatStr = "{0}{1}{2}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, yl.ToString().ToDD(), tjxs.ToString().ToDD(), nd.ToString().ToDD());//, isUse.ToDD()
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
                            this.yl = newStr[0].ToDouble();
                            break;
                        case 1:
                            this.tjxs = newStr[1].ToDouble();
                            break;
                        case 2:
                            this.nd = newStr[2].ToDouble();
                            break;
                        case 3:
                            this.isUse = newStr[3];
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
                    yl = this.yl,
                    tjxs = this.tjxs,
                    nd = this.nd,
                    isUse = this.isUse
                };

                return item;
            }
        }
    }
}
