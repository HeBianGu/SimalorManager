#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:33
 * 文件名：WECON
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
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class WCYCLE : ItemsKey<WCYCLE.Item>
    {
        public WCYCLE(string _name)
            : base(_name)
        {

        }

        /// <summary> 项实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 井名 </summary>
            public string jm0;
            /// <summary> 开井天数 </summary>
            public string zdrcyl1;
            /// <summary> 关井天数 </summary>
            public string zdrcql2;
            /// <summary> 4 </summary>
            public string zdhsl3;
            /// <summary> 5 </summary>
            public string zdqsb4;
            /// <summary> 6 </summary>
            public string zdsqb5;


            string formatStr = "{0}{1}{2}{3}{4}{5}{6} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), zdrcyl1.ToDD(), zdrcql2.ToDD(), zdhsl3.ToDD(), zdqsb4.ToDD(), zdsqb5.ToDD());
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
                        default:
                            break;
                    }
                }

            }

        }

    }
}
