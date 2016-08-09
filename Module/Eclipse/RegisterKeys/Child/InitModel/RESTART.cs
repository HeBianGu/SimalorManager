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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 重启信息 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class RESTART : ItemsKey<RESTART.Item>
    {
        public RESTART(string _name)
            : base(_name)
        {

        }


        public class Item : OPT.Product.SimalorManager.Item
        {

            /// <summary> 井名 </summary>
            public string fwjm0;
            /// <summary> 井组 </summary>
            public string cqss1;
            /// <summary> 网格i </summary>
            public string i3;
            /// <summary> 网格j </summary>
            public string j4;


            string formatStr = "{0}{1}{2}{3}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, fwjm0.ToEclStr(), cqss1.ToDD(), i3.ToDD(), j4.ToDD());
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
                            this.fwjm0 = newStr[0];
                            break;
                        case 1:
                            this.cqss1 = newStr[1];
                            break;
                        case 2:
                            this.i3 = newStr[2];
                            break;
                        case 3:
                            this.j4 = newStr[3];
                            break;
                        default:
                            break;
                    }
                }
            }
        }


    }


}
