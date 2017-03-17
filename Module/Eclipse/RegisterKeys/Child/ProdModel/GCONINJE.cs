#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：GCONPROD
 * 说明：
 * 
GCONPROD
'G' ORAT 11000 3* CON /
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
    /// <summary> 注入井组控制 </summary>
     
    public class GCONINJE : ItemsKey<GCONINJE.Item>
    {
        public GCONINJE(string _name)
            : base(_name)
        {

        }

        /// <summary> 黑油项实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 井组名 </summary>
            public string jzm0;
            /// <summary> 注入流体 </summary>
            public string kzms1="WATER";
            /// <summary> 控制方式 </summary>
            public string rcyl2 = "NONE";
            /// <summary> 日注入量 </summary>
            public string rcsl3;
            /// <summary> 油藏体积注入速度 </summary>
            public string rcql4;
            /// <summary> 回注百分数 </summary>
            public string rcyl5;
            /// <summary> 亏空补偿 </summary>
            public string xjcs6;
            /// <summary> 高级井组控制 </summary>
            public string gjjzkz7="YES";
            /// <summary> 指导注入量 </summary>
            public string zdcl8;
            /// <summary> 指导注入量设置 </summary>
            public string ltxt9;
            /// <summary> 回注井组名 </summary>
            public string cswfxj10;
            /// <summary> 亏空补偿井组名 </summary>
            public string cqwfxj11;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jzm0.ToEclStr(), kzms1.ToEclStr(), rcyl2.ToEclStr(), rcsl3.ToDD(), rcql4.ToDD(),
                    rcyl5.ToDD(), xjcs6.ToDD(), gjjzkz7.ToEclStr(), zdcl8.ToDD(), ltxt9.ToEclStr(), cswfxj10.ToDD(), this.cqwfxj11.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.jzm0 = newStr[0];
                            break;
                        case 1:
                            this.kzms1 = newStr[1];
                            break;
                        case 2:
                            this.rcyl2 = newStr[2];
                            break;
                        case 3:
                            this.rcsl3 = newStr[3];
                            break;
                        case 4:
                            this.rcql4 = newStr[4];
                            break;
                        case 5:
                            this.rcyl5 = newStr[5];
                            break;
                        case 6:
                            this.xjcs6 = newStr[6];
                            break;
                        case 7:
                            this.gjjzkz7 = newStr[7];
                            break;
                        case 8:
                            this.zdcl8 = newStr[8];
                            break;
                        case 9:
                            this.ltxt9 = newStr[9];
                            break;
                        case 10:
                            this.cswfxj10 = newStr[10];
                            break;
                        case 11:
                            this.cqwfxj11 = newStr[11];
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }



}
