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

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> RVVD </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class RVVD : RegionKey<RVVD.Item>
    {
        public RVVD(string _name)
            : base(_name)
        {

        }


        public class Item : OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 深度 </summary>
            public string sd;
            /// <summary> 露点压力 </summary>
            public string ldyl;

           string formatStr = "{0}{1}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, sd.ToDD(), ldyl.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                this.ID = Guid.NewGuid().ToString();

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.sd = newStr[0];
                            break;
                        case 1:
                            this.ldyl = newStr[1];
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
                    ldyl = this.ldyl
                };
            }
           
        }
    }
}
