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
    /// <summary> 单井指导产量 </summary>
     
    public class WGRUPCON  : ItemsKey<WGRUPCON .Item>,IProductEvent
    {
        public WGRUPCON(string _name)
            : base(_name)
        {

        }


        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }

        /// <summary> 黑油项实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item,IProductItem
        {

            /// <summary> 井名 </summary>
            public string jm0;
            /// <summary> 井组控制 </summary>
            public string jzkz1 = "YES";
            /// <summary> 指导产量 </summary>
            public string zdcl2;
            /// <summary> 流体相态 </summary>
            public string ltxt3;
            /// <summary> 产量乘子 </summary>
            public string clcz4="1";
         

            string formatStr = "{0}{1}{2}{3}{4}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), jzkz1.ToEclStr(), zdcl2.ToDD(), ltxt3.ToEclStr(), clcz4.ToDD());
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
                            this.jzkz1 = newStr[1];
                            break;
                        case 2:
                            this.zdcl2 = newStr[2];
                            break;
                        case 3:
                            this.ltxt3 = newStr[3];
                            break;
                        case 4:
                            this.clcz4 = newStr[4];
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
                    return this.jm0;
                }
                set
                {
                    this.jm0=value;
                }
            }
        }
    }



}
