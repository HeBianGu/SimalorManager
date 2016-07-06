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

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 井组经济限制GECON /GRUPLIM </summary>
    [KeyAttribute(AnatherName = "GRUPLIM")]
    public class GECON  : ItemsKey<GECON .Item>
    {
        public GECON(string _name)
            : base(_name)
        {

        }

        /// <summary> 黑油项实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 井组名 </summary>
            public string jzm0;
            /// <summary> 最低日产油量 </summary>
            public string zdrcyl1;
            /// <summary> 最低日产气量 </summary>
            public string zdrcql2;
            /// <summary> 最大含水率 </summary>
            public string zdhsl3;
            /// <summary> 最大气油比 </summary>
            public string zdqyb4;
            /// <summary> 最大水气比 </summary>
            public string zdsqb5;
            /// <summary> 修井措施 </summary>
            public string xjcs6="NONE";
            /// <summary> 结束模拟 </summary>
            public string jsmn7="NO";
         

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jzm0.ToEclStr(), zdrcyl1.ToDD(), zdrcql2.ToDD(), zdhsl3.ToDD(), zdqyb4.ToDD(),
                    zdsqb5.ToDD(), xjcs6.ToEclStr(), jsmn7.ToEclStr());
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
                            this.zdrcyl1 = newStr[1];
                            break;
                        case 2:
                            this.zdrcql2 = newStr[2];
                            break;
                        case 3:
                            this.zdhsl3 = newStr[3];
                            break;
                        case 4:
                            this.zdqyb4 = newStr[4];
                            break;
                        case 5:
                            this.zdsqb5 = newStr[5];
                            break;
                        case 6:
                            this.xjcs6 = newStr[6];
                            break;
                        case 7:
                            this.jsmn7 = newStr[7];
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }



}
