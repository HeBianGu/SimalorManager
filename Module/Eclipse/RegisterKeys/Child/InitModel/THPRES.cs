#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 门槛压力 </summary>
    public class THPRES : ItemsKey<THPRES.Item>
    {
        public THPRES(string _name)
            : base(_name)
        {

        }


        public class Item : OPT.Product.SimalorManager.ItemNormal
        {

            /// <summary> 第一平衡区 </summary>
            public string diphq0;
            /// <summary> 第二平衡区 </summary>
            public string dephq1;
            /// <summary> 门限压力 </summary>
            public string mxyl2;
         

            string formatStr = "{0}{1}{2} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format
                    (
                    formatStr, 
                    diphq0.ToDD(), 
                    dephq1.ToDD(), 
                    mxyl2.ToDD()
                    );
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.diphq0 = newStr[0];
                            break;
                        case 1:
                            this.dephq1 = newStr[1];
                            break;
                        case 2:
                            this.mxyl2 = newStr[2];
                            break;
                        default:
                            break;
                    }
                }
            }
            public override object Clone()
            {

                return new Item()
                {
                    diphq0 = this.diphq0,
                    dephq1 = this.dephq1,
                    mxyl2 = this.mxyl2

                };
            }

        }
    }
}
