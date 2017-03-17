#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：WCONPROD
 * 说明：
 * WCONPROD
' O1' 'OPEN' 'GRAT' 2* 80000 1* 1* 80 3* /
--井名 开井 定产气 2* 产气量 1* 1* 井底流压限制 3*
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
    /// <summary> Carter-Tracy水体表 </summary>
     
    public class AQUTAB : RegionKey<AQUTAB.Item>
    {
        public AQUTAB(string _name)
            : base(_name)
        {

        }

        /// <summary> Carter-Tracy水体表实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 无因次时间 </summary>
            public string wycsj0;
            /// <summary> 无因次压力 </summary>
            public string wycyl1;

            string formatStr = "{0}{1}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, wycsj0.ToDD(), wycyl1.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.wycsj0 = newStr[0].ToString();
                            break;
                        case 1:
                            this.wycyl1 = newStr[1];
                            break;
                        default:
                            break;
                    }
                }
            }

        }



    }



}
