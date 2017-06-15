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
using System.Reflection;
using OPT.Product.SimalorManager.RegisterKeys.SimON;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 输出控制 </summary>
    public class RPTSUM : ItemsKey<RPTSUM.Item>
    {
        public RPTSUM(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler = l =>
            {
                return l.TrimEnd(); ;
            };
        }

        /// <summary> 说明 </summary>
        public bool FWPR
        {

            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FGPR
        {

            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FLPR
        {

            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FGIR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }
        /// <summary> 说明 </summary>
        public bool FWIR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FWCT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FWPT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FGPT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FLPT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FGIT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FWIT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool PGAS
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1] + " WAVG";

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }
        /// <summary> 说明 </summary>
        public bool FGIP
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        Action<string, bool> Act
        {
            get
            {
                return (n, k) =>
                {
                    string v = n.Split('_')[1];

                    if (k)
                    {
                        if (!this.Items.Exists(l => l.Value == v))
                        {
                            this.Items.Add((Item)v);
                        }
                    }
                    else
                    {
                        this.Items.RemoveAll(l => l.Value == v);
                    }
                };
            }
        } 
        /// <summary> 说明 </summary>
        public bool WWPR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WGPR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WLPR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WGIR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WWIR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WWPT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WGPT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WLPT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WGIT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WWIT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
            }

        /// <summary> 说明 </summary>
        public bool WBHP
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FOPR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FOPT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool POIL
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1] + " WAVG";

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool FOIP
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WOPR
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }


        /// <summary> 说明 </summary>
        public bool WWCT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WOPT
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WBP
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WBP4
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }


        /// <summary> 说明 </summary>
        public bool WBP5
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;
                Act(name, value);
            }
        }

        /// <summary> 说明 </summary>
        public bool WBP9
        {
            get
            {
                string name = MethodBase.GetCurrentMethod().Name;

                string v = name.Split('_')[1];

                return this.Items.Exists(l => l.Value == v);
            }
            set
            {
                string name = MethodBase.GetCurrentMethod().Name;

                Act(name, value);
            }
        }



        protected override void ItemWriteKey(System.IO.StreamWriter writer)
        {

            MODELTYPE modeltype = this.BaseFile.Key.Find<MODELTYPE>();

            if (modeltype.MetricType == MetricType.BLACKOIL || modeltype.MetricType == MetricType.HFOIL)
            {
                #region - 说明 -

                this.FOPR = true;
                this.FWPR = true;
                this.FGPR = true;
                this.FLPR = true;
                this.FGIP = true;
                this.FWIR = true;
                this.FWCT = true;
                this.FOPT = true;
                this.FWPT = true;
                this.FGPT = true;
                this.FLPT = true;
                this.FGIT = true;
                this.FWIT = true;
                this.POIL = true;
                this.FOIP = true;
                this.FGIP = true;
                this.WOPR = true;
                this.WWPR = true;
                this.WGPR = true;
                this.WLPR = true;
                this.WGIR = true;
                this.WWIR = true;
                this.WWCT = true;
                this.WOPT = true;
                this.WWPT = true;
                this.WGPT = true;
                this.WLPT = true;
                this.WGIT = true;
                this.WWIT = true;
                this.WBHP = true;
                this.WBP = true;
                this.WBP4 = true;
                this.WBP5 = true;
                this.WBP9 = true;

                #endregion
            }

            else if (modeltype.MetricType == MetricType.GASWATER)
            {
                #region - 说明 -

                this.FWPR = true;
                this.FGPR = true;
                this.FLPR = true;
                this.FGIR = true;
                this.FWIR = true;
                this.FWCT = true;
                this.FWPT = true;
                this.FGPT = true;
                this.FLPT = true;
                this.FGIT = true;
                this.FWIT = true;
                this.PGAS = true;
                this.FGIP = true;
                this.WWPR = true;
                this.WGPR = true;
                this.WLPR = true;
                this.WGIR = true;
                this.WWIR = true;
                this.WWPT = true;
                this.WGPT = true;
                this.WLPT = true;
                this.WGIT = true;
                this.WWIT = true;
                this.WBHP = true;

                this.WBP = true;
                this.WBP4 = true;
                this.WBP5 = true;
                this.WBP9 = true;

                #endregion
            }

            else if (modeltype.MetricType == MetricType.OILWATER)
            {
                #region - 说明 -
                this.FOPR = true;
                this.FWPR = true;
                this.FLPR = true;
                this.FWIR = true;
                this.FWCT = true;
                this.FOPT = true;
                this.FWPT = true;
                this.FLPT = true;
                this.FWIT = true;
                this.POIL = true;
                this.FOIP = true;
                this.WOPR = true;
                this.WWPR = true;
                this.WLPR = true;
                this.WWIR = true;
                this.WWCT = true;
                this.WOPT = true;
                this.WWPT = true;
                this.WLPT = true;
                this.WWIT = true;
                this.WBHP = true;

                this.WBP = true;
                this.WBP4 = true;
                this.WBP5 = true;
                this.WBP9 = true;

                #endregion
            }


            this.Lines.Clear();

            writer.WriteLine();
            writer.WriteLine(this.Name);

            for (int i = 0; i < this.Items.Count; i++)
            {
                writer.WriteLine(this.Items[i].ToString());
            }

            writer.WriteLine(KeyConfiger.EndFlag);
        }

        /// <summary> 项实体 </summary>
        public class Item : OPT.Product.SimalorManager.Item
        {

            /// <summary> 隐私转换 </summary>
            public static explicit operator Item(string f)
            {
                Item it = new Item();
                it.Value = f;
                return it;
            }

            private string v;
            /// <summary> X1 </summary>
            public string Value
            {
                get { return v; }
                set { v = value; }
            }

            string formatStr = @"    {0}  /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, v.ToSDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.v = newStr[0];
                            break;
                        default:
                            break;
                    }
                }

            }




        }

    }
}
