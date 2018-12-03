#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:33

 * 说明：
 * WECON
' O1'  4*  0.0005  'WELL' 'NO' 1* /
--井名 1* 最小产气量  2*  最大水气比  其他参数固定即可 /
/

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
     
    public class WECON : ItemsKey<WECON.Item>,IProductEvent
    {
        public WECON(string _name)
            : base(_name)
        {

        }


        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }

        /// <summary> 设置井底流压限制值 </summary>
        /// <param name="wellName"> 井名 </param>
        /// <param name="v"> 限制值 </param>
        /// <returns> 是否成功 </returns>
        public bool SetMaxGWpercent(string wellName, double v)
        {
            bool isExist = Items.Exists(l => l.jm0 == wellName);
            if (!isExist)
            {
                return false;
            }
            else
            {
                var fndItem = Items.Find(l => l.jm0 == wellName);
                fndItem.zdsqb5 = v.ToString();
                return true;
            }
        }

        /// <summary> 项实体 </summary>
        public class Item : HeBianGu.Product.SimalorManager.Item, IProductItem
        {

            /// <summary> 井名 </summary>
            public string jm0;
            /// <summary> 最低日产油量 </summary>
            public string zdrcyl1;
            /// <summary> 最低日产气量 </summary>
            public string zdrcql2;
            /// <summary> 最大含水率 </summary>
            public string zdhsl3;
            /// <summary> 最大气油比 </summary>
            public string zdqsb4;
            /// <summary> 最大水气比 </summary>
            public string zdsqb5;//   最大气水比
            /// <summary> 修井措施 </summary>
            public string xjcs6 = "WELL";
            /// <summary> 结束模拟 </summary>
            public string jsmn7 = "NO";
            /// <summary> 最低日产油量 </summary>
            public string zdrcyl8;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), zdrcyl1.ToDD(), zdrcql2.ToDD(), zdhsl3.ToDD(), zdqsb4.ToDD(), zdsqb5.ToDD(), xjcs6.ToDD(), jsmn7.ToDD(), zdrcyl8.ToDD()); ;
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
                            this.zdrcyl1 = newStr[1];
                            break;
                        case 2:
                            this.zdrcql2 = newStr[2];
                            break;
                        case 3:
                            this.zdhsl3 = newStr[3];
                            break;
                        case 4:
                            this.zdqsb4 = newStr[4];
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
                        case 8:
                            this.zdrcyl8 = newStr[8];
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

    }
}
