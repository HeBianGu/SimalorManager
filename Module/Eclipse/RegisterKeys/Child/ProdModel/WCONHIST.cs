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
     
    public class WCONHIST : ItemsKey<WCONHIST.Item>,IProductEvent
    {
        public WCONHIST(string _name)
            : base(_name)
        {

        }


        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }


        /// <summary> 黑油项实体 </summary>
        public class Item : HeBianGu.Product.SimalorManager.Item,IProductItem
        {
            /// <summary> 井名  </summary>
            public string wellName0;
            /// <summary> 开关标志 </summary>
            public string openFlag1="OPEN";
            /// <summary> 控制模式 </summary>
            public string ctrlModel2="ORAT";
            /// <summary> 观测日产油量 </summary>
            public string oilPro3;
            /// <summary> 观测日产水量</summary>
            public string waterPro4;
            /// <summary> 观测日产气量 </summary>
            public string gasPro5;
            /// <summary> VFP表号 </summary>
            public string VFP6;
            /// <summary> 人工举升量 </summary>
            public string perCount7;
            /// <summary> 观测THP </summary>
            public string THP8;
            /// <summary> 观测BHP </summary>
            public string BHP9;
            /// <summary> 观测湿气日产量 </summary>
            public string wGasPro10;


            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, wellName0.ToEclStr(), openFlag1.ToEclStr(), ctrlModel2.ToEclStr(), oilPro3.ToDD(), waterPro4.ToDD(), gasPro5.ToDD(), VFP6.ToDD(), perCount7.ToDD(), THP8.ToDD(), BHP9.ToDD(), wGasPro10.ToDD());
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
                            this.VFP6 = newStr[6];
                            break;
                        case 7:
                            this.perCount7 = newStr[7];
                            break;
                        case 8:
                            this.THP8 = newStr[8];
                            break;
                        case 9:
                            this.BHP9 = newStr[9];
                            break;
                        case 10:
                            this.wGasPro10 = newStr[10];
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
                    return this.wellName0;
                }
                set
                {
                    this.wellName0=value;
                }
            }
        }

    }



}
