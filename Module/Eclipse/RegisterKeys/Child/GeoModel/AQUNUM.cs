#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:17

 * 说明：

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

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 数值水体 </summary>
     
    public class AQUNUM : ConnectKey<AQUNUM.Item>
    {
        public AQUNUM(string _name)
            : base(_name)
        {

        }


        protected override void CmdGetWellItems()
        {
            this.Items.Clear();

            string str = string.Empty;

            List<string> transLine = Lines.ReadNewLineArray();

            for (int i = 0; i < transLine.Count; i++)
            {
                str = transLine[i];

                //  读到结束符不继续读取
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    break;
                }

                //  不为空的行
                if (str.IsWorkLine())
                {

                    if (!str.EndsWith(KeyConfiger.EndFlag))
                    {

                    }

                    List<string> newStr = str.EclExtendToArray();

                    if (newStr.Count > 0)
                    {
                        Item pitem = new Item();
                        pitem.Build(newStr);
                        //  标记行的ID位置
                        //Lines[i] = pitem.ID;
                        if (pitem != null)
                        {
                            Items.Add(pitem);
                        }
                    }
                }

            }
        }

        /// <summary> 数值水体实体 </summary>
        public class Item : HeBianGu.Product.SimalorManager.Item
        {
            /// <summary> 水体编号 </summary>
            public string stbh0;
            /// <summary> 水体连接网格的X坐标（开始） </summary>
            public string xzbks1;
            /// <summary> 水体连接网格的Y坐标（开始）</summary>
            public string yzbks2;
            /// <summary> 水体连接网格的Z坐标（开始） </summary>
            public string zzbks3;
            /// <summary> 横截面积 </summary>
            public string hjmj4;
            /// <summary> 长度 </summary>
            public string cd5;
            /// <summary> 孔隙度 </summary>
            public string kxd6;
            /// <summary> 渗透率 </summary>
            public string stl7;
            /// <summary> 水体深度 </summary>
            public string stsd8;
            /// <summary> 初始压力 </summary>
            public string qsyl9;
            /// <summary> 水相PVT表编号 </summary>
            public string sxpvtbbh10;
            /// <summary> 水体饱和度函数表编号 </summary>
            public string stbhdhsbbh11;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, stbh0.ToDD(), xzbks1.ToDD(), yzbks2.ToDD(), zzbks3.ToDD(), hjmj4.ToDD(), cd5.ToDD(),
                    kxd6.ToDD(), stl7.ToDD(), stsd8.ToDD(), qsyl9.ToDD(), sxpvtbbh10.ToDD(), stbhdhsbbh11.ToDD()); //, scsl11.ToDD()
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
                            this.xzbks1 = newStr[1];
                            break;
                        case 2:
                            this.yzbks2 = newStr[2];
                            break;
                        case 3:
                            this.zzbks3 = newStr[3];
                            break;
                        case 4:
                            this.hjmj4 = newStr[4];
                            break;
                        case 5:
                            this.cd5 = newStr[5];
                            break;
                        case 6:
                            this.kxd6 = newStr[6];
                            break;
                        case 7:
                            this.stl7 = newStr[7];
                            break;
                        case 8:
                            this.stsd8 = newStr[8];
                            break;
                        case 9:
                            this.qsyl9 = newStr[9];
                            break;
                        case 10:
                            this.sxpvtbbh10 = newStr[10];
                            break;
                        case 11:
                            this.stbhdhsbbh11 = newStr[11];
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }



}
