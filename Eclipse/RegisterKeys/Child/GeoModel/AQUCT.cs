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
    /// <summary> Carter-Tracy水体 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class AQUCT : ItemsKey<AQUCT.Item>
    {
        public AQUCT(string _name)
            : base(_name)
        {

        }



        /// <summary> Carter-Tracy水体实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 水体编号 </summary>
            public string stbh0;
            /// <summary> 参考深度 </summary>
            public string cksd1;
            /// <summary> 参考深度处初始压力</summary>
            public string cksdccsyl2;
            /// <summary> 水体渗透率 </summary>
            public string ststl3;
            /// <summary> 水体孔隙度</summary>
            public string stkxd4="1";
            /// <summary> 水体压缩系数 </summary>
            public string stysxs5;
            /// <summary> 水体内疚 </summary>
            public string stnj6;
            /// <summary> 水体厚度 </summary>
            public string sthd7;
            /// <summary> 影响角 </summary>
            public string yxj8 = "360";
            /// <summary> 水相PVT表编号 </summary>
            public string sxpvtbbh9="1";
            /// <summary> 影响函数表编号 </summary>
            public string yxhsbbh10="1";
            /// <summary> 水体初始含盐浓度 </summary>
            public string stcshynd11="0";
            /// <summary> 水体温度 </summary>
            public string stwd12;


            private AQUANCON.ProertyGroup connectGroup = new ConnectKey<AQUANCON.Item>.ProertyGroup();
            /// <summary> 对应的水体连接 </summary>
            public AQUANCON.ProertyGroup ConnectGroup
            {
                get { return connectGroup; }
                set { connectGroup = value; }
            }


            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, stbh0.ToDD(), cksd1.ToDD(), cksdccsyl2.ToDD(), ststl3.ToDD(), stkxd4.ToDD(), stysxs5.ToDD(),
                    stnj6.ToDD(), sthd7.ToDD(), yxj8.ToDD(), this.sxpvtbbh9.ToDD(), this.yxhsbbh10.ToDD(), this.stcshynd11.ToDD(), this.stwd12.ToDD());
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
                            this.cksd1 = newStr[1];
                            break;
                        case 2:
                            this.cksdccsyl2 = newStr[2];
                            break;
                        case 3:
                            this.ststl3 = newStr[3];
                            break;
                        case 4:
                            this.stkxd4 = newStr[4];
                            break;
                        case 5:
                            this.stysxs5 = newStr[5];
                            break;
                        case 6:
                            this.stnj6 = newStr[6];
                            break;
                        case 7:
                            this.sthd7 = newStr[7];
                            break;
                        case 8:
                            this.yxj8 = newStr[8];
                            break;
                        case 9:
                            this.sxpvtbbh9 = newStr[9];
                            break;
                        case 10:
                            this.yxhsbbh10 = newStr[10];
                            break;
                        case 11:
                            this.stcshynd11 = newStr[11];
                            break;
                        case 12:
                            this.stwd12 = newStr[12];
                            break;
                        default:
                            break;
                    }
                }
            }

        }



    }



}
