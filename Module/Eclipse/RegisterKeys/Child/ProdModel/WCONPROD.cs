#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17

 * 说明：
 * WCONPROD
' O1' 'OPEN' 'GRAT' 2* 80000 1* 1* 80 3* /
--井名 开井 定产气 2* 产气量 1* 1* 井底流压限制 3*
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
     
    public class WCONPROD : ItemsKey<WCONPROD.ItemHY>,IProductEvent
    {
        public WCONPROD(string _name)
            : base(_name)
        {

        }


        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }

        /// <summary> 黑油项实体 </summary>
        public class ItemHY : OPT.Product.SimalorManager.Item, IProductItem
        {
            /// <summary> 井名 </summary>
            public string jm0;
            /// <summary> 开关标志 </summary>
            public string kgbz1="OPEN";
            /// <summary> 控制模式 </summary>
            public string kzms2="ORAT";
            /// <summary> 日产油量 </summary>
            public string rcyl3;
            /// <summary> 日产水量 </summary>
            public string rcsl4;
            /// <summary> 日产气量 </summary>
            public string rcql5;
            /// <summary> 日产液量 </summary>
            public string liqutPro6;
            /// <summary> 油藏体积流量 </summary>
            public string yctjll61;
            /// <summary> 井底流压限制 </summary>
            public string jdlyxz7;
            /// <summary> 井口压力 </summary>
            public string jkyl8;
            /// <summary> VFP表号 </summary>
            public string VFP9;
            /// <summary> 人工举升参数 </summary>
            public string rgjscs10;
            /// <summary> 生产时率 </summary>
            public string scsl11;


            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), kgbz1.ToEclStr(), kzms2.ToEclStr(), rcyl3.ToDD(), rcsl4.ToDD(), rcql5.ToDD(),
                    liqutPro6.ToDD(), yctjll61.ToDD(), jdlyxz7.ToDD(), jkyl8.ToDD(), VFP9.ToDD(), rgjscs10.ToDD()); //, scsl11.ToDD()
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
                            this.kgbz1 = newStr[1];
                            break;
                        case 2:
                            this.kzms2 = newStr[2];
                            break;
                        case 3:
                            this.rcyl3 = newStr[3];
                            break;
                        case 4:
                            this.rcsl4 = newStr[4];
                            break;
                        case 5:
                            this.rcql5 = newStr[5];
                            break;
                        case 6:
                            this.liqutPro6 = newStr[6];
                            break;
                        case 7:
                            this.yctjll61 = newStr[7];
                            break;
                        case 8:
                            this.jdlyxz7 = newStr[8];
                            break;
                        case 9:
                            this.jkyl8 = newStr[9];
                            break;
                        case 10:
                            this.VFP9 = newStr[10];
                            break;
                        case 11:
                            this.rgjscs10 = newStr[11];
                            break;
                        case 12:
                            this.scsl11 = newStr[12];
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

        /// <summary> 组分项实体 </summary>
        public class ItemZF : OPT.Product.SimalorManager.Item
        {
            /// <summary> 井名 </summary>
            public string wellName0;
            /// <summary> 开关标志 </summary>
            public string openFlag1;
            /// <summary> 控制模式 </summary>
            public string ctrlModel2;
            /// <summary> 日产油量 </summary>
            public string oilPro3;
            /// <summary> 日产水量 </summary>
            public string waterPro4;
            /// <summary> 日产气量 </summary>
            public string gasPro5;
            /// <summary> 日产液量 </summary>
            public string liqutPro6;
            /// <summary> 油藏体积流量 </summary>
            public string volume7;
            /// <summary> BHP目标 </summary>
            public string BHP8;
            /// <summary> THP目标 </summary>
            public string THP9;
            /// <summary> VFP表号 </summary>
            public string VFP10;
            /// <summary> 人工举升量 </summary>
            public string rgjsl11;
            /// <summary> 湿气日产量 </summary>
            public string sqrcl12;
            /// <summary> 总摩尔流量 </summary>
            public string zmell13;
            /// <summary> 蒸汽日产量 </summary>
            public string zqrcl14;


            string formatStr = "'{0}'      '{1}'      '{2}'      {3}      {4}      {5}      {6}       {7}      {8}      {9}      {10}      {11}      {12}      {13}      {14} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, wellName0, openFlag1, ctrlModel2, oilPro3, waterPro4, gasPro5, liqutPro6, volume7, BHP8, THP9, VFP10, rgjsl11, sqrcl12, zmell13, zqrcl14); ;
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.wellName0 = newStr[0];
                            break;
                        case 1:
                            this.openFlag1 = newStr[1];
                            break;
                        case 2:
                            this.ctrlModel2 = newStr[2];
                            break;
                        case 3:
                            this.oilPro3 = newStr[3];
                            break;
                        case 4:
                            this.waterPro4 = newStr[4];
                            break;
                        case 5:
                            this.gasPro5 = newStr[5];
                            break;
                        case 6:
                            this.liqutPro6 = newStr[6];
                            break;
                        case 7:
                            this.volume7 = newStr[7];
                            break;
                        case 8:
                            this.BHP8 = newStr[8];
                            break;
                        case 9:
                            this.THP9 = newStr[9];
                            break;
                        case 10:
                            this.VFP10 = newStr[10];
                            break;
                        case 11:
                            this.rgjsl11 = newStr[11];
                            break;
                        case 12:
                            this.sqrcl12 = newStr[12];
                            break;
                        case 13:
                            this.zmell13 = newStr[13];
                            break;
                        case 14:
                            this.zqrcl14 = newStr[14];
                            break;
                        default:
                            break;
                    }
                }
            }

        }

  
    }



}
