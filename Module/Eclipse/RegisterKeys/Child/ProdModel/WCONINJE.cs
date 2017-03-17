#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：WCONPROD
 * 说明：
WCONINJE
'INIJ1' 'WATER' 'OPEN' 'BHP' 2* 3700 7* /
 /

 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 注入井 </summary>
     
    public class WCONINJE : ItemsKey<WCONINJE.ItemHY>,IProductEvent
    {
        public WCONINJE(string _name)
            : base(_name)
        {

        }


        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }

        /// <summary> 项实体 </summary>
        public class ItemZF : OPT.Product.SimalorManager.Item, IProductItem
        {
            /// <summary> 井名 </summary>
            public string jm0;
            /// <summary> 注入流体 </summary>
            public string zrlt1 = "WATER";
            /// <summary> 开关标志 </summary>
            public string kgbz2 = "OPEN";
            /// <summary> 控制模式 </summary>
            public string kzms3 = "RATE";
            /// <summary> 地面日产液量 </summary>
            public string dmrcyl4;
            /// <summary> 地面日产气量 </summary>
            public string dmrcql5;
            /// <summary> 油藏体积流量 </summary>
            public string yctjll6;
            /// <summary> 井底流压限制 </summary>
            public string jdlyxz7;
            /// <summary> 井口压力限制 </summary>
            public string jkylxz8;
            /// <summary> VFP表编号 </summary>
            public string VFP9;
            /// <summary> 挥发油浓度 </summary>
            public string hfynd10;
            /// <summary> 溶解气浓度 </summary>
            public string rjqnd11;


            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}  /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), zrlt1.ToDD(), kgbz2.ToEclStr(), kzms3.ToDD(),
                    dmrcyl4.ToDD(), dmrcql5.ToDD(), yctjll6.ToDD(), jdlyxz7.ToDD(), jkylxz8.ToDD(), VFP9.ToDD(), hfynd10.ToDD(), rjqnd11.ToDD()); ;
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.jm0 = newStr[0];
                            break;
                        case 1:
                            this.zrlt1 = newStr[1];
                            break;
                        case 2:
                            this.kgbz2 = newStr[2];
                            break;
                        case 3:
                            this.kzms3 = newStr[3];
                            break;
                        case 4:
                            this.dmrcyl4 = newStr[4];
                            break;
                        case 5:
                            this.dmrcql5 = newStr[5];
                            break;
                        case 6:
                            this.yctjll6 = newStr[6];
                            break;
                        case 7:
                            this.jdlyxz7 = newStr[7];
                            break;
                        case 8:
                            this.jkylxz8 = newStr[8];
                            break;
                        case 9:
                            this.VFP9 = newStr[9];
                            break;
                        case 10:
                            this.hfynd10 = newStr[10];
                            break;
                        default:
                            break;
                    }
                }
            }

            public string Name
            {
                get
                {
                    return jm0;
                }
                set
                {
                    jm0 = value;
                }
            }
        }

        /// <summary> 项实体 </summary>
        public class ItemHY : OPT.Product.SimalorManager.Item,IProductItem
        {
            /// <summary> 井名 </summary>
            public string jm0;
            /// <summary> 注入流体类型 </summary>
            public string zrltlx1 = "WATER";
            /// <summary> 井开关标志 </summary>
            public string jzkgz2 = "OPEN";
            /// <summary> 生产控制方式 </summary>
            public string kzms3 = "RATE";
            /// <summary> 日注入量 </summary>
            public string rzrl4;
            /// <summary> 油藏体积产量 </summary>
            public string yctjcl5;
            /// <summary> 井底流压限制 </summary>
            public string jdlyxz6;
            /// <summary> 井口压力 </summary>
            public string jkyl7;
            /// <summary> VFP表号 </summary>
            public string VFP8;
            /// <summary> 挥发油浓度/溶解气浓度 </summary>
            public string hfynd9;
            ///// <summary> 溶解气浓度 </summary>
            //public string rjqnd10;
            ///// <summary> 生产时率 </summary>
            //public string scsl11;


            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), zrltlx1.ToEclStr(), jzkgz2.ToEclStr(), kzms3.ToEclStr(), rzrl4.ToDD(), yctjcl5.ToDD(), jdlyxz6.ToDD(), jkyl7.ToDD(), VFP8.ToDD(), hfynd9.ToDD()); ;
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.jm0 = newStr[0];
                            break;
                        case 1:
                            this.zrltlx1 = newStr[1];
                            break;
                        case 2:
                            this.jzkgz2 = newStr[2];
                            break;
                        case 3:
                            this.kzms3 = newStr[3];
                            break;
                        case 4:
                            this.rzrl4 = newStr[4];
                            break;
                        case 5:
                            this.yctjcl5 = newStr[5];
                            break;
                        case 6:
                            this.jdlyxz6 = newStr[6];
                            break;
                        case 7:
                            this.jkyl7 = newStr[7];
                            break;
                        case 8:
                            this.VFP8 = newStr[8];
                            break;
                        case 9:
                            this.hfynd9 = newStr[9];
                            break;
                        //case 10:
                        //    this.rjqnd10 = newStr[10];
                        //    break;
                        //case 11:
                        //    this.scsl11 = newStr[10];
                        //    break;
                        default:
                            break;
                    }
                }
            }

            public string Name
            {
                get
                {
                    return jm0;
                }
                set
                {
                    jm0 = value;
                }
            }
        }
    }
}
