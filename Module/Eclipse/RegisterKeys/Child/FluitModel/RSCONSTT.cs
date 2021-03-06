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
    /// <summary> 油相溶解气油比 需要PVT分区 </summary>
    public class RSCONSTT : RegionKey<RSCONSTT.Item>
    {
        public RSCONSTT(string _name)
            : base(_name)
        {

        }

        public class Item : HeBianGu.Product.SimalorManager.ItemNormal
        {

            /// <summary> 溶解汽油比 </summary>
            public string tjxs0;

            /// <summary> 泡点压力 </summary>
            public string pdyl1;


            string formatStr = "{0}{1}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, tjxs0.ToSaveLockDD(), pdyl1.ToSaveLockDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.tjxs0 = newStr[0];
                            break;
                        case 1:
                            this.pdyl1 = newStr[1];
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
                    pdyl1 = this.pdyl1,
                    tjxs0 = this.tjxs0
                };

                return item;
            }
        }
    }
}
