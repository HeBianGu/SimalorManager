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
    /// <summary> 油相溶解气油比 不用分区 </summary>
    public class RVCONST : ItemsKey<RVCONST.Item>
    {
        public RVCONST(string _name)
            : base(_name)
        {

        }

        public class Item : OPT.Product.SimalorManager.ItemNormal
        {

            /// <summary> 挥发油气比 </summary>
            public string hfyqb0;

            /// <summary> 露点压力 </summary>
            public string ldyl1;


            string formatStr = "{0}{1}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, hfyqb0.ToSaveLockDD(), ldyl1.ToSaveLockDD());
            }




            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.hfyqb0 = newStr[0];
                            break;
                        case 1:
                            this.ldyl1 = newStr[1];
                            break;
                        default:
                            break;
                    }
                }
            }



            public override object Clone()
            {
                Item item = new Item()
                {
                    ldyl1 = this.ldyl1,
                    hfyqb0 = this.hfyqb0,
                };

                return item;
            }
        }
    }
}
