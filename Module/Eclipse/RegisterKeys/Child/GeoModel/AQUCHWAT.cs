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
    /// <summary> 定压头水体水体属性 </summary>
     
    public class AQUCHWAT : ItemsKey<AQUCHWAT.Item>
    {
        public AQUCHWAT(string _name)
            : base(_name)
        {

        }



        /// <summary> 数值水体实体 </summary>
        public class Item : HeBianGu.Product.SimalorManager.Item
        {

            /// <summary> 水体编号 </summary>
            public string stbh0;
            /// <summary> 参考深度</summary>
            public string cksd1 ;
            /// <summary> 参数选择 </summary>
            public string csxz2 = "PRESSURE";
            /// <summary> 压力/压头</summary>
            public string cksdccsyl3;
            ///// <summary> 水侵指数 </summary>
            //public string cssttj3;
            ///// <summary> 水体压缩系数</summary>
            //public string stysxs4;
            /// <summary> 水侵指数 </summary>
            public string sqzs4;
            /// <summary> 水相PVT表编号 </summary>
            public string sxpvtbbh5;
            /// <summary> 水体初始含盐浓度 </summary>
            public string cell7;
            /// <summary> tNavi忽略 </summary>
            public string cell8;
            /// <summary> tNavi忽略 </summary>
            public string cell9;
            /// <summary> tNavi忽略 </summary>
            public string cell10;
            /// <summary> tNavi忽略 </summary>
            public string cell11;
            /// <summary> tNavi忽略 </summary>
            public string cell12;
             /// <summary> tNavi忽略 </summary>
            public string cell13;
            /// <summary> tNavi忽略 </summary>
            public string cell14;
            /// <summary> 水体温度 </summary>
            public string stwd14;

            /// <summary> 参数选择值变索引 </summary>
            public int ToChooseParamIndex()
            {
                switch(this.csxz2.Trim())
                {
                    case "PRESSURE":
                        return 0;
                    case "HEAD":
                        return 1;
                    default:
                        return 0;
                }
            }

            /// <summary> 参数选择索引变值 </summary>
            public string ToChooseParamIndex(string index)
            {
                switch (index)
                {
                    case "0":
                        this.csxz2 = "PRESSURE";
                        break;
                    case "1":
                        this.csxz2 = "HEAD";
                        break;
                    default:
                        this.csxz2 = "PRESSURE";
                        break;
                }

                return this.csxz2;
            }


            private AQUANCON.ProertyGroup connectGroup = new ConnectKey<AQUANCON.Item>.ProertyGroup();
            /// <summary> 对应的水体连接 </summary>
            public AQUANCON.ProertyGroup ConnectGroup
            {
                get { return connectGroup; }
                set { connectGroup = value; }
            }


            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()//cssttj3.ToDD(), stysxs4.ToDD(),
            {
                return string.Format(formatStr, stbh0.ToDD(), cksd1.ToDD(),this.csxz2.ToDD(), cksdccsyl3.ToDD(), sqzs4.ToDD(),
                    sxpvtbbh5.ToDD(), this.cell7.ToDD(), this.cell8.ToDD(),this.cell9.ToDD(),this.cell10.ToDD(),this.cell11.ToDD(),this.cell12.ToDD()
                    ,this.cell13.ToDD(),this.cell14.ToDD(),this.stwd14.ToDD());
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
                            this.csxz2 = newStr[2];
                            break;
                        case 3:
                            this.cksdccsyl3 = newStr[3];
                            break;
                        case 4:
                            this.sqzs4 = newStr[4];
                            break;
                        case 5:
                            this.sxpvtbbh5 = newStr[5];
                            break;
                        case 6:
                            this.cell7 = newStr[6];
                            break;
                        case 7:
                            this.cell8 = newStr[7];
                            break;
                          case 8:
                            this.cell9 = newStr[8];
                            break;
                        case 9:
                            this.cell10 = newStr[9];
                            break;
                        case 10:
                            this.cell11 = newStr[10];
                            break;
                        case 11:
                            this.cell12 = newStr[11];
                            break;
                        case 12:
                            this.cell13 = newStr[12];
                            break;
                        case 13:
                            this.cell14 = newStr[13];
                            break;
                        case 14:
                            this.stwd14 = newStr[14];
                            break;
                        default:
                            break;
                    }
                }
            }

        }



    }



}
