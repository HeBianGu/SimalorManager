#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53

 * 说明：

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> Langmuir等温吸附模型的参数 </summary>
    [KeyAttribute(SimKeyType = SimKeyType.SimON)]
    public class LANGPROP : RegionKey<LANGPROP.Item>
    {
        public LANGPROP(string _name)
            : base(_name)
        {

        }


        /// <summary> 说明 </summary>
        public class Item : ItemNormal
        {
            public string jxxfnd0 = "0";
            /// <summary> 1.	极限吸附浓度VL，单位质量固体吸附的气体质量，单位：无量纲 </summary>
            public string Jxxfnd0
            {
                get { return jxxfnd0; }
                set { jxxfnd0 = value; }
            }

            private string yl1 = "0";
            /// <summary> 2.	Langmuir压力PL，单位：bar(米制)，psi(英制)，atm(lab)，Pa(MESO) </summary>
            public string Yl1
            {
                get { return yl1; }
                set { yl1 = value; }
            }

            private string ljjxfyl2 = "0";
            /// <summary> 3.	临界解吸附压力PCR，当油藏压力低于此时才开始解吸附，如果定义大于油藏初始压力的PCR，则解吸附压力被模拟器调整为等于油藏初始压力，单位：bar(米制)，psi(英制)，atm(lab)，Pa(MESO) </summary>
            public string Ljjxfyl2
            {
                get { return ljjxfyl2; }
                set { ljjxfyl2 = value; }
            }

            private string tzjxfsj3 = "0";
            /// <summary> 4.	特征解吸附时间τ，单位：day(米制)，day(英制)，hour(lab)，ms(MESO) </summary>
            public string Tzjxfsj3

            {
                get { return tzjxfsj3; }
                set { tzjxfsj3 = value; }
            }

            private string xfsztybqd4 = "0";
            /// <summary> 5.	吸附所致体应变强度EL，单位：无量纲，基质由吸附造成的体应变由公式 计算 </summary>
            public string Xfsztybqd4
            {
                get { return xfsztybqd4; }
                set { xfsztybqd4 = value; }
            }

            private string zxfslcs5 = "0";
            /// <summary> 6.	重吸附速率乘数res，如果设置为零，则无重吸附 </summary>
            public string Zxfslcs5
            {
                get { return zxfslcs5; }
                set { zxfslcs5 = value; }
            }


            string formatStr = @"# VL    PL     Pcr     ao     El   Resorb  
    {0}     {1}    {2}    {3}    {4}    {5} ";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, this.jxxfnd0.ToSDD(), this.yl1.ToSDD(), this.ljjxfyl2.ToSDD(), this.tzjxfsj3.ToSDD(), this.xfsztybqd4.ToSDD(), this.zxfslcs5.ToSDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.jxxfnd0 = newStr[0];
                            break;
                        case 1:
                            this.yl1 = newStr[1];
                            break;
                        case 2:
                            this.ljjxfyl2 = newStr[2];
                            break;
                        case 3:
                            this.tzjxfsj3 = newStr[3];
                            break;
                        case 4:
                            this.xfsztybqd4 = newStr[4];
                            break;
                        case 5:
                            this.zxfslcs5 = newStr[5];
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
                    jxxfnd0 = this.jxxfnd0,
                    yl1 = this.yl1,
                    ljjxfyl2 = this.ljjxfyl2,
                    tzjxfsj3 = this.tzjxfsj3,
                    xfsztybqd4 = this.xfsztybqd4,
                    zxfslcs5 = this.zxfslcs5

                };
            }
        }
    }
}
