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
     
    public class THPRESFT : ItemsKey<THPRESFT.Item>
    {
        public THPRESFT(string _name)
            : base(_name)
        {

        }

        /// <summary> 项实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 断层名 </summary>
            public string dcm0;

            /// <summary> 开启压力 </summary>
            public string kqyl1;

            string formatStr = "{0}{1} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, dcm0.ToEclStr(), kqyl1.ToDD()); ;
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.dcm0 = newStr[0];
                            break;
                        case 1:
                            this.kqyl1 = newStr[1];
                            break;
                        default:
                            break;
                    }
                }

            }

        }

    }
}
