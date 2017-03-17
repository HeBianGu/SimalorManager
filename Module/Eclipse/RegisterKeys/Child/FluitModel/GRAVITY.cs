#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01
 * 文件名：START
 * 说明：
 * ROCK
             0            11
/
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
    /// <summary> 相对密度 </summary>
     
    public class GRAVITY : RegionKey<GRAVITY.Item>
    {
        public GRAVITY(string _name)
            : base(_name)
        {

        }

        public class Item: OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 原油API重度 </summary>
            public string yyapicd0 ;
           /// <summary> 水的相对密度 </summary>
            public string sdxdmd1 ;
           /// <summary> 气的相对密度 </summary>
            public string qdxdmd2;

            string formatStr = "{0}{1}{2}  ";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, yyapicd0.ToDD(), sdxdmd1.ToDD(), qdxdmd2.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
             

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.yyapicd0 = newStr[0];
                            break;
                        case 1:
                            this.sdxdmd1 = newStr[1];
                            break;
                        case 2:
                            this.qdxdmd2 = newStr[2];
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
                    yyapicd0 = this.yyapicd0,
                    sdxdmd1 = this.sdxdmd1,
                    qdxdmd2=this.qdxdmd2
                };
            }
        }
    }
}
