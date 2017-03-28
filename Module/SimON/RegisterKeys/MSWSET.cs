#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：


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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 射孔数据 </summary>
    public class MSWSET : ItemsKey<MSWSET.Item>
    {
        public MSWSET(string _name)
            : base(_name)
        {
            this.BuilderHandler += (l, k) =>
                {
                    this.WellName = this.Lines[0].Trim('\'');

                    this.Lines.RemoveAt(0);

                    return this;
                };
        }

        private string _wellName;
        /// <summary> 井名 </summary>
        public string WellName
        {
            get { return _wellName; }
            set { _wellName = value; }
        }

        /// <summary> 排序</summary>
        public void OrderBy()
        {
            this.Items.OrderBy(l => l.I1 + l.J2);
        }


        public class Item : OPT.Product.SimalorManager.Item, IProductItem
        {

            private string jm0 = "新增";
            /// <summary> 井名 </summary>
            public string Jm0
            {
                get { return jm0; }
                set { jm0 = value; }
            }


            private string i1 = "HO";
            /// <summary> HF </summary>
            public string I1
            {
                get { return i1; }
                set { i1 = value; }
            }


            private string j2 = "HF-";
            /// <summary> HO </summary>
            public string J2
            {
                get { return j2; }
                set { j2 = value; }
            }


            string formatStr = "{0}{1}{2} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), i1.ToSDD(), j2.ToSDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.jm0 = newStr[0];
                            break;
                        case 1:
                            this.i1 = newStr[1];
                            break;
                        case 2:
                            this.j2 = newStr[2];
                            break;
                        default:
                            break;
                    }
                }
            }


            public string Name
            {
                get
                {
                    return this.jm0;
                }
                set
                {
                    this.jm0 = value;
                }
            }

            public bool Equals(object x, object y)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }
        }

        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }
    }
}
