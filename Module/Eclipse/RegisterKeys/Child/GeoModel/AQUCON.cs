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
    /// <summary> 数值水体连接 </summary>
     
    public class AQUCON : ConnectKey<AQUCON.Item>
    {
        public AQUCON(string _name)
            : base(_name)
        {

        }



        /// <summary> 数值水体实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 水体编号 </summary>
            private string stbh0;

            public string Stbh0
            {
                get { return stbh0; }
                set { stbh0 = value; }
            }
            /// <summary> 水体连接网格的X坐标（开始） </summary>
            private string xzbks1;

            public string Xzbks1
            {
                get { return xzbks1; }
                set { xzbks1 = value; }
            }
            /// <summary> 水体连接网格的X坐标（结束）</summary>
            private string xzbjs2;

            public string Xzbjs2
            {
                get { return xzbjs2; }
                set { xzbjs2 = value; }
            }
            /// <summary> 水体连接网格的Y坐标（开始） </summary>
            private string yks3;

            public string Yks3
            {
                get { return yks3; }
                set { yks3 = value; }
            }
            /// <summary> 水体连接网格的Y坐标（结束 </summary>
            private string yjs4;

            public string Yjs4
            {
                get { return yjs4; }
                set { yjs4 = value; }
            }
            /// <summary> 水体连接网格的Z坐标（开始） </summary>
            private string zks5;

            public string Zks5
            {
                get { return zks5; }
                set { zks5 = value; }
            }
            /// <summary> 水体连接网格的Z坐标（结束） </summary>
            private string zjs6;

            public string Zjs6
            {
                get { return zjs6; }
                set { zjs6 = value; }
            }
            /// <summary> 水体连接网格面朝向 </summary>
            private string wgmcx7;

            public string Wgmcx7
            {
                get { return wgmcx7; }
                set { wgmcx7 = value; }
            }
            /// <summary> 传导率乘子 </summary>
            private string cdlcz8 = "1";

            public string Cdlcz8
            {
                get { return cdlcz8; }
                set { cdlcz8 = value; }
            }
            /// <summary> 传导率计算选项 </summary>
            private string cdljsxx9 = "0";

            public string Cdljsxx9
            {
                get
                {
                    if (cdljsxx9 == "0")
                    {
                        return "设置横截面积";
                    }
                    else
                    {
                        return "通过公式计算横截面积";
                    }
                }
                set
                {
                    if (value == "设置横截面积")
                    {
                        cdljsxx9 = "0";
                    }
                    else
                    {
                        cdljsxx9 = "1";
                    }
                }
            }
            /// <summary> 允许连接到相邻网格面 </summary>
            private string yxljdxlwgm10 = "NO";

            public string Yxljdxlwgm10
            {
                get { return yxljdxlwgm10; }
                set { yxljdxlwgm10 = value; }
            }


            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, stbh0.ToDD(), xzbks1.ToDD(), xzbjs2.ToDD(), yks3.ToDD(), yjs4.ToDD(), zks5.ToDD(),
                    zjs6.ToDD(), wgmcx7.ToDD(), cdlcz8.ToDD(), cdljsxx9.ToDD(), yxljdxlwgm10.ToDD()); //, scsl11.ToDD()
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
                            this.xzbks1 = newStr[1];
                            break;
                        case 2:
                            this.xzbjs2 = newStr[2];
                            break;
                        case 3:
                            this.yks3 = newStr[3];
                            break;
                        case 4:
                            this.yjs4 = newStr[4];
                            break;
                        case 5:
                            this.zks5 = newStr[5];
                            break;
                        case 6:
                            this.zjs6 = newStr[6];
                            break;
                        case 7:
                            this.wgmcx7 = newStr[7];
                            break;
                        case 8:
                            this.cdlcz8 = newStr[8];
                            break;
                        case 9:
                            this.cdljsxx9 = newStr[9];
                            break;
                        case 10:
                            this.yxljdxlwgm10 = newStr[10];
                            break;
                        default:
                            break;
                    }
                }
            }
 
        }
    }



}
