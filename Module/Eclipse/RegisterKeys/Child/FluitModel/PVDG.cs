﻿#region <版 本 注 释>
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
    /// <summary> 气相PVT </summary>
    public class PVDG : RegionKey<PVDG.Item>
    {
        public PVDG(string _name)
            : base(_name)
        {

        }

        public int RegionCount = 0;

        public class Item: HeBianGu.Product.SimalorManager.ItemNormal
        {
           /// <summary> 压力 </summary>
           public string yl="0";
            /// <summary> 体积系数 </summary>
           public string tjxs = "0";
           /// <summary> 粘度 </summary>
           public string nd = "0";
           /// <summary> 使用气体压缩因子</summary>
           public string isUse;

           string formatStr = "{0}{1}{2}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, yl.ToSaveLockDD().ToSaveLockDD(), tjxs.ToSaveLockDD().ToSaveLockDD(), nd.ToSaveLockDD().ToSaveLockDD());//, isUse.ToDD()
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.yl = newStr[0];
                            break;
                        case 1:
                            this.tjxs = newStr[1];
                            break;
                        case 2:
                            this.nd = newStr[2];
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
