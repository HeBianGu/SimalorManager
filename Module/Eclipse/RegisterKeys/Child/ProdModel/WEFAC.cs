#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17

 * 说明：
 * 
WEFAC
‘P25’  0.89  NO/
‘P12’  0.7  /
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
    /// <summary> 单井生产时率 该关键字主要用于设置修井时间（downtime）对于模拟的影响。 </summary>
     
    public class WEFAC  : ItemsKey<WEFAC .Item>,IProductEvent
    {
        public WEFAC(string _name)
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

            /// <summary> 生产时率 </summary>
            public string scsl1;

            /// <summary> 是否用于扩展井网模型计算 </summary>
            public string sfyykzjwmxjs2;

            string formatStr = "{0}{1}{2} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), scsl1.ToDD(), sfyykzjwmxjs2.ToDD());
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
                            this.scsl1 = newStr[1];
                            break;
                        case 2:
                            this.sfyykzjwmxjs2 = newStr[2];
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
