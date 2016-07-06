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

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 恒量水体水体属性 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class AQUFLUX : ItemsKey<AQUFLUX.Item>
    {
        public AQUFLUX(string _name)
            : base(_name)
        {

        }



        /// <summary> 数值水体实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 水体编号 </summary>
            public string stbh0;
            /// <summary> 单位面积水体流量 </summary>
            public string dwmjstll1;
            /// <summary> 水体初始含盐浓度</summary>
            public string stcshynd2;
            /// <summary> 水体温度 </summary>
            public string stwd3;
            /// <summary> 参考深度处水体压力</summary>
            public string cksdcstyl4;


            private AQUANCON.ProertyGroup connectGroup = new ConnectKey<AQUANCON.Item>.ProertyGroup();
            /// <summary> 对应的水体连接 </summary>
            public AQUANCON.ProertyGroup ConnectGroup
            {
                get { return connectGroup; }
                set { connectGroup = value; }
            }


            string formatStr = "{0}{1}{2}{3}{4}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format
                    (
                    formatStr,
                    stbh0.ToDD(),
                    dwmjstll1.ToDD(),
                    stcshynd2.ToDD(),
                    stwd3.ToDD(),
                    cksdcstyl4.ToDD()
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
                            this.stbh0 = newStr[0];
                            break;
                        case 1:
                            this.dwmjstll1 = newStr[1];
                            break;
                        case 2:
                            this.stcshynd2 = newStr[2];
                            break;
                        case 3:
                            this.stwd3 = newStr[3];
                            break;
                        case 4:
                            this.cksdcstyl4 = newStr[4];
                            break;
                        default:
                            break;
                    }
                }
            }

        }



    }



}
