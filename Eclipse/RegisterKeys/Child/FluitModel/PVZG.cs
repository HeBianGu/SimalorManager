﻿#region <版 本 注 释>
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
    /// <summary> 气相PVT </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
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
           public double ckwd0=0;
            /// <summary> 压力 </summary>
           public double yl1 = 0;
           /// <summary> 压缩因子 </summary>
           public double ysyz2 = 0;
           /// <summary> 粘度</summary>
           public string nd3;

           string formatStr = "{0}{1}{2}{3}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, ckwd0.ToString().ToDD(), yl1.ToString().ToDD(), ysyz2.ToString().ToDD(), nd3.ToString().ToDD());//, isUse.ToDD()
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
                            this.ckwd0 = newStr[0].ToDouble();
                            break;
                        case 1:
                            this.yl1 = newStr[1].ToDouble();
                            break;
                        case 2:
                            this.ysyz2 = newStr[2].ToDouble();
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
