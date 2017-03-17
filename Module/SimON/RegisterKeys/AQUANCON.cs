#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：WCONPROD
 * 说明：

AQUANCON # Connection data for analytic aquifers
# ID  X1  X2  Y1  Y2  Z1  Z2  Dir  Mult   No-use
  1   1   4   1   1   1   5   -2   1.0    0.0 
  2   1   4   4   4   1   5   +2   1.0    0.0 

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

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 数值水体连接 </summary>
     
    public class AQUANCON : ConnectKey<AQUANCON.Item>
    {
        public AQUANCON(string _name)
            : base(_name)
        {

        }



        /// <summary> 数值水体实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {
            
            private string stbh0;
            /// <summary>  第一列是水体的编号； </summary>
            public string Stbh0
            {
                get { return stbh0; }
                set { stbh0 = value; }
            }
            
            private string xzbks1;
            /// <summary> 第二，三列是水体与网格连接的x范围 </summary>
            public string Xzbks1
            {
                get { return xzbks1; }
                set { xzbks1 = value; }
            }
           
            private string xzbjs2;
            /// <summary> 第二，三列是水体与网格连接的x范围 </summary>
            public string Xzbjs2
            {
                get { return xzbjs2; }
                set { xzbjs2 = value; }
            }
            
            private string yks3;
            /// <summary> 第四，五列是水体与网格连接的y范围；</summary>
            public string Yks3
            {
                get { return yks3; }
                set { yks3 = value; }
            }
           
            private string yjs4;
            /// <summary> 第四，五列是水体与网格连接的y范围；</summary>
            public string Yjs4
            {
                get { return yjs4; }
                set { yjs4 = value; }
            }
           
            private string zks5;
            /// <summary> 第六，七列是水体与网格连接的z范围 </summary>
            public string Zks5
            {
                get { return zks5; }
                set { zks5 = value; }
            }
          
            private string zjs6;
            /// <summary> 第六，七列是水体与网格连接的z范围 </summary>
            public string Zjs6
            {
                get { return zjs6; }
                set { zjs6 = value; }
            }
            
            private string wgmcx7;
            /// <summary> 第八列是网格面的朝向，1代表x+，2代表y+，4代表z+，-1代表x-，-2代表y-，-4代表z-； </summary>
            public string Wgmcx7
            {
                get { return wgmcx7; }
                set { wgmcx7 = value; }
            }
           
            private string cdlcz8 = "1";
            /// <summary> 第九列是水体与网格接触面积的乘数，接触面积不用给定，由模拟器计算； </summary>
            public string Cdlcz8
            {
                get { return cdlcz8; }
                set { cdlcz8 = value; }
            }
            
            private string cdljsxx9 = "0";
            /// <summary> 第十列数据暂不使用，仅用于占位 </summary>
            public string Cdljsxx9
            {
                get
                {
                    return cdljsxx9;
                }
                set
                {
                    cdljsxx9 = value;
                }
            }

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, stbh0.ToSDD(), xzbks1.ToSDD(), xzbjs2.ToSDD(), yks3.ToSDD(), yjs4.ToSDD(), zks5.ToSDD(),
                    zjs6.ToSDD(), wgmcx7.ToSDD(), cdlcz8.ToSDD(), cdljsxx9.ToSDD()); //, scsl11.ToDD()
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
                        default:
                            break;
                    }
                }
            }

        }
    }



}
