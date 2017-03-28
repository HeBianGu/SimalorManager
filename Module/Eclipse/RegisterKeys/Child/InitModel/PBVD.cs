#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：


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
using OPT.Product.SimalorManager;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 泡点压力</summary>
    public class PBVD : RegionKey<PBVD.Item>
    {
        public PBVD(string _name)
            : base(_name)
        {

        }


        public class Item : OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 深度 </summary>
           public string sd;
            /// <summary> 泡点压力 </summary>
           public string pdyl;

           string formatStr = "{0}{1}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, sd.ToSaveLockDD(), pdyl.ToSaveLockDD());
            }



            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.sd = newStr[0];
                            break;
                        case 1:
                            this.pdyl = newStr[1];
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
                    sd = this.sd,
                    pdyl = this.pdyl
                };
            }
           
        }
    }
}
