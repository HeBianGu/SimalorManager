#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:17

 * 说明：
范例	AQUFETP # Fetkovich aquifer parameters
# ID   dep    p0     Vol      Ct     PI   NotUse
   1   425    NA     1E5    6.0E-6    1    0.0
   2   425    NA     1E5    6.0E-6    1    0.0

 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> Fetkovich水体属性 </summary>
     
    public class AQUFETP : ItemsKey<AQUFETP.Item>
    {
        public AQUFETP(string _name)
            : base(_name)
        {

        }

        public class Item : HeBianGu.Product.SimalorManager.Item
        {

            /// <summary> 第一列是水体的编号，必须连续编号 </summary>
            public string stbh0;
            /// <summary> 第二列是水体的深度，单位：m(米制)，feet(英制) </summary>
            public string cksd1;
            /// <summary> 第三列是水体的初始压力，单位：bar(米制)，psi(英制)，如果此数据是负值或者“NA”，则模拟器自动计算初始压力使得初始时刻水流量为零；</summary>
            public string cksdccsyl2;
            /// <summary> 第四列是水体的体积，单位：m3(米制)，stb(英制)； </summary>
            public string cssttj3;
            /// <summary> 第五列是水体的总压缩系数(水+岩石)，单位：bar-1(米制)，psi-1(英制)；</summary>
            public string stysxs4;
            /// <summary> 第六列是水体的production index，单位：m3·bar-1·day-1(米制)， stb·psi-1·day-1(英制)； </summary>
            public string productionindex5;
            /// <summary> 第七列数据暂不使用，仅用于占位 </summary>
            public string sxpvtbbh6;


            private AQUANCON.ProertyGroup connectGroup = new ConnectKey<AQUANCON.Item>.ProertyGroup();
            /// <summary> 对应的水体连接 </summary>
            public AQUANCON.ProertyGroup ConnectGroup
            {
                get { return connectGroup; }
                set { connectGroup = value; }
            }


            string formatStr = "{0}{1}{2}{3}{4}{5}{6} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, stbh0.ToSDD(), cksd1.ToSDD(), cksdccsyl2.ToSDD(), cssttj3.ToSDD(), stysxs4.ToSDD(), productionindex5.ToSDD(),
                    sxpvtbbh6.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.stbh0 = newStr[0];
                            break;
                        case 1:
                            this.cksd1 = newStr[1];
                            break;
                        case 2:
                            this.cksdccsyl2 = newStr[2];
                            break;
                        case 3:
                            this.cssttj3 = newStr[3];
                            break;
                        case 4:
                            this.stysxs4 = newStr[4];
                            break;
                        case 5:
                            this.productionindex5 = newStr[5];
                            break;
                        case 6:
                            this.sxpvtbbh6 = newStr[6];
                            break;
                        default:
                            break;
                    }
                }
            }

        }



    }



}
