﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:17

 * 说明：


 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 解析水体水体连接 </summary>
     
    public class AQUANCON : ConnectKey<AQUANCON.Item>
    {
        public AQUANCON(string _name)
            : base(_name)
        {

        }

        /// <summary> Fetkovich水体实体 </summary>
        public class Item : HeBianGu.Product.SimalorManager.Item, ICloneable
        {
        
            private string stbh0;
            /// <summary> 水体编号 </summary>
            public string Stbh0
            {
                get { return stbh0; }
                set { stbh0 = value; }
            }
            
            private string xzbks1;
            /// <summary> 水体连接网格的X坐标（开始） </summary>
            public string Xzbks1
            {
                get { return xzbks1; }
                set { xzbks1 = value; }
            }
            
            private string xzbjs2;
            /// <summary> 水体连接网格的X坐标（结束）</summary>
            public string Xzbjs2
            {
                get { return xzbjs2; }
                set { xzbjs2 = value; }
            }
           
            private string yks3;
            /// <summary> 水体连接网格的Y坐标（开始） </summary>
            public string Yks3
            {
                get { return yks3; }
                set { yks3 = value; }
            }
            
            private string yjs4;
            /// <summary> 水体连接网格的Y坐标（结束 </summary>
            public string Yjs4
            {
                get { return yjs4; }
                set { yjs4 = value; }
            }
            
            private string zks5;
            /// <summary> 水体连接网格的Z坐标（开始） </summary>
            public string Zks5
            {
                get { return zks5; }
                set { zks5 = value; }
            }
           
            private string zjs6;
            /// <summary> 水体连接网格的Z坐标（结束） </summary>
            public string Zjs6
            {
                get { return zjs6; }
                set { zjs6 = value; }
            }
           
            private string wgmcx7 = "I+";
            /// <summary> 水体连接网格面朝向 </summary>
            public string Wgmcx7
            {
                get { return wgmcx7; }
                set { wgmcx7 = value; }
            }
           
            private string cdlcz8;
            /// <summary> 水体流量系数 </summary>
            public string Cdlcz8
            {
                get { return cdlcz8; }
                set { cdlcz8 = value; }
            }
            
            private string cdljsxx9;
            /// <summary> 水体流量系数乘子 </summary>
            public string Cdljsxx9
            {
                get { return cdljsxx9; }
                set { cdljsxx9 = value; }
            }
            
            private string yxljdxlwgm10 = "NO";
            /// <summary> 允许连接到相邻网格面 </summary>
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
                            this.cdlcz8 = newStr[8].ToEclDefaultStr();
                            break;
                        case 9:
                            this.cdljsxx9 = newStr[9].ToEclDefaultStr();
                            break;
                        case 10:
                            this.yxljdxlwgm10 = newStr[10].ToEclDefaultStr();
                            break;
                        default:
                            break;
                    }
                }
            }


            public object Clone()
            {
                Item item = new Item()
                {
                    stbh0 = this.stbh0
                };
                return item;
            }
        }
    }



}
