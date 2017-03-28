#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:33

 * 说明：

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
     
    public class FLOWFUNC : ItemsKey<FLOWFUNC.Item>
    {
        public FLOWFUNC(string _name)
            : base(_name)
        {

        }

        /// <summary> 项实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {


            private string hsm0;
            /// <summary> 函数名 </summary>
            public string Hsm0
            {
                get { return hsm0; }
                set { hsm0 = value; }
            }

            
            private string hslx1;
            /// <summary> 函数类型 </summary>
            public string Hslx1
            {
                get { return hslx1; }
                set { hslx1 = value; }
            }

          
            private string xsk2;
            /// <summary> 系数K </summary>
            public string Xsk2
            {
                get { return xsk2; }
                set { xsk2 = value; }
            }
            
            private string xsa3;
            /// <summary> 系数a </summary>
            public string Xsa3
            {
                get { return xsa3; }
                set { xsa3 = value; }
            }

            string formatStr = "{0}{1}{2}{3} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, hsm0.ToEclStr(), hslx1.ToDD(), xsk2.ToDD(), xsa3.ToDD()); ;
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.hsm0 = newStr[0];
                            break;
                        case 1:
                            this.hslx1 = newStr[1];
                            break;
                        case 2:
                            this.xsk2 = newStr[2];
                            break;
                        case 3:
                            this.xsa3 = newStr[3];
                            break;
                        default:
                            break;
                    }
                }

            }
        }

    }
}
