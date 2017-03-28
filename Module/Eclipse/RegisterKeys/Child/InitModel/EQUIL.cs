#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：
EQUIL
-- 
-- Equilibration Data Specification
-- 第1个平衡分区
    3810   516.9    3814     1*     1*     1*     1*     1*    1*    1*      1*
/
----第2个平衡分区
    3810   516.9    3816     1*      1*    1*   1*      1*    1*    1*      1*
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
    /// <summary> 平衡法初始化</summary>
    public class EQUIL : RegionKey<EQUIL.Item>
    {
        public EQUIL(string _name)
            : base(_name)
        {

        }


        public class Item : OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 参考深度 </summary>
            public string cksd0;
            /// <summary> 参考压力 </summary>
            public string ckyl1;
            /// <summary> 油水界面深度 </summary>
            public string ysjmsd2;
            /// <summary> 油水界面处毛管压力 </summary>
            public string ysjmcmgyl3;
            /// <summary> 油气界面深度 </summary>
            public string yqjmsd4;
            /// <summary> 油气界面处毛管压力 </summary>
            public string yqjmcmgyl5;
            /// <summary> 泡点压力参数表 </summary>
            public string pdylcsb6;
            /// <summary> 露点压力参数表 </summary>
            public string ldylcsb7;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7} ";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format
                    (
                    formatStr, 
                    cksd0.ToDD(), 
                    ckyl1.ToDD(), 
                    ysjmsd2.ToDD(), 
                    ysjmcmgyl3.ToDD(), 
                    yqjmsd4.ToDD(), 
                    yqjmcmgyl5.ToDD(), 
                    pdylcsb6.ToDD(), 
                    ldylcsb7.ToDD()
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
                            this.cksd0 = newStr[0];
                            break;
                        case 1:
                            this.ckyl1 = newStr[1];
                            break;
                        case 2:
                            this.ysjmsd2 = newStr[2];
                            break;
                        case 3:
                            this.ysjmcmgyl3 = newStr[3];
                            break;
                        case 4:
                            this.yqjmsd4 = newStr[4];
                            break;
                        case 5:
                            this.yqjmcmgyl5 = newStr[5];
                            break;
                        case 6:
                            this.pdylcsb6 = newStr[6];
                            break;
                        case 7:
                            this.ldylcsb7 = newStr[7];
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
                    cksd0 = this.cksd0,
                    ckyl1 = this.ckyl1,
                    ysjmsd2 = this.ysjmsd2,
                    ysjmcmgyl3 = this.ysjmcmgyl3,
                    yqjmsd4 = this.yqjmsd4,
                    yqjmcmgyl5 = this.yqjmcmgyl5

                };
            }

        }
    }
}
