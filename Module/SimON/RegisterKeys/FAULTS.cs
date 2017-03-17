#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:33
 * 文件名：WECON
 * 说明：
 * WECON
' O1'  4*  0.0005  'WELL' 'NO' 1* /
--井名 1* 最小产气量  2*  最大水气比  其他参数固定即可 /
/

 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager;
using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
     
    public class FAULTS : ItemsKey<FAULTS.Item>
    {
        public FAULTS(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler = l =>
            {
                // Todo ：FAULTS关键字的专有读法 FAULTS*等都做等于FAULTS处理 
                return l.Trim().StartsWith(this.GetType().Name) ? this.GetType().Name : l.Trim();

            };
        }

        protected override void ItemWriteKey(System.IO.StreamWriter writer)
        {
            this.Lines.Clear();

            writer.WriteLine();
            writer.WriteLine(this.Name);

            for (int i = 0; i < this.Items.Count; i++)
            {
                writer.WriteLine(this.Items[i].ToString());
            }
        }


        private string falutName;
        /// <summary> 断层名 </summary>
        public string FalutName
        {
            get { return falutName; }
            set { falutName = value; }
        }

        /// <summary> 项实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {

            private string x11;
            /// <summary> X1 </summary>
            public string X11
            {
                get { return x11; }
                set { x11 = value; }
            }

            private string x22;
            /// <summary> X2 </summary>
            public string X22
            {
                get { return x22; }
                set { x22 = value; }
            }

            private string y13;
            /// <summary> Y1 </summary>
            public string Y13
            {
                get { return y13; }
                set { y13 = value; }
            }

            private string y24;
            /// <summary> Y2 </summary>
            public string Y24
            {
                get { return y24; }
                set { y24 = value; }
            }

            private string z15;
            /// <summary> Z1 </summary>
            public string Z15
            {
                get { return z15; }
                set { z15 = value; }
            }

            private string z26;
            /// <summary> Z2 </summary>
            public string Z26
            {
                get { return z26; }
                set { z26 = value; }
            }

            private string dcm7 = "X";
            /// <summary> 断层面 </summary>
            public string Dcm7
            {
                get { return dcm7; }
                set { dcm7 = value; }
            }

            private string cdxsyz8 = "0";
            /// <summary> 常数 </summary>
            public string Cdxsyz8
            {
                get { return cdxsyz8; }
                set { cdxsyz8 = value; }
            }


            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, x11.ToSDD(), x22.ToSDD(), y13.ToSDD(), y24.ToSDD(), z15.ToSDD(), z26.ToSDD(), dcm7.ToSDD(), this.cdxsyz8.ToSDD(), "0".ToSDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.x11 = newStr[0];
                            break;
                        case 1:
                            this.x22 = newStr[1];
                            break;
                        case 2:
                            this.y13 = newStr[2];
                            break;
                        case 3:
                            this.y24 = newStr[3];
                            break;
                        case 4:
                            this.z15 = newStr[4];
                            break;
                        case 5:
                            this.z26 = newStr[5];
                            break;
                        case 6:
                            this.dcm7 = newStr[6];
                            break;
                        case 7:
                            this.cdxsyz8 = newStr[7];
                            break;
                        default:
                            break;
                    }
                }

            }


            string TransToDcm(string str)
            {
                str = str.Trim();

                switch (str)
                {
                    case "I":
                        return "X";

                    case "J":
                        return "Y";

                    case "K":
                        return "Z";

                    default:
                        return str;
                }
            }

        }

    }
}
