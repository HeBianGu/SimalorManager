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
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 完井射孔数据 </summary>
    public class PERF : ConfigerKey
    {
        public PERF(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler = l =>
            {
                // HTodo  ：兼容WELL关键字 2017-05-24 14:27:53 
                return BaseKeyHandleFactory.Instance.ForWellToWellCtrl(l).Trim();
            };
        }
        private string _wellName;
        /// <summary> 井名 </summary>
        public string WellName
        {
            get { return _wellName; }
            set { _wellName = value; }
        }


        private string i0;
        /// <summary> 网格i </summary>
        public string I0
        {
            get { return i0; }
            set { i0 = value; }
        }

        private string j1;
        /// <summary> 网格j </summary>
        public string J1
        {
            get { return j1; }
            set { j1 = value; }
        }

        private string k12;
        /// <summary> 上网格k1 </summary>
        public string K12
        {
            get { return k12; }
            set { k12 = value; }
        }

        private string k23;
        /// <summary> 下网格k2 </summary>
        public string K23
        {
            get { return k23; }
            set { k23 = value; }
        }

        private string kgbs4 = "OPEN";
        /// <summary> 开/关 </summary>
        public string Kgbs4
        {
            get { return kgbs4; }
            set { kgbs4 = value; }
        }

        private string jzscz5;
        /// <summary> 井指数乘子 </summary>
        public string Jzscz5
        {
            get { return jzscz5; }
            set { jzscz5 = value; }
        }

        private string jzs6;
        /// <summary> 井指数 </summary>
        public string Jzs6
        {
            get { return jzs6; }
            set { jzs6 = value; }
        }

        private string wjfxX7 = "0";
        /// <summary> 完井方向X/Y/Z </summary>
        public string WjfxX7
        {
            get { return wjfxX7; }
            set { wjfxX7 = value; }
        }

        private string wjfxY8 = "0";
        /// <summary> 完井方向X/Y/Z </summary>
        public string WjfxY8
        {
            get { return wjfxY8; }
            set { wjfxY8 = value; }
        }

        private string wjfxZ9 = "0";
        /// <summary> 完井方向DX/DY/DZ </summary>
        public string WjfxZ9
        {
            get { return wjfxZ9; }
            set { wjfxZ9 = value; }
        }

        private string bp10 = "0";
        /// <summary> 表皮 </summary>
        public string Bp10
        {
            get { return bp10; }
            set { bp10 = value; }
        }

        private string tempJJ;
        /// <summary> 临时存储井径的变量 </summary>
        public string TempJJ
        {
            get { return tempJJ; }
            set { tempJJ = value; }
        }


        string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10} ";

        public override string ToString()
        {
            if (this.kgbs4 == "CLOSE")
            {
                return string.Format(formatStr, this.i0.ToSDD(), this.j1.ToSDD(), this.k12.ToSDD(), this.k23.ToSDD(), this.kgbs4.ToSDD(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
            else
            {

                // Todo ：井指数乘子为空默认保存为1 
                this.jzscz5 = string.IsNullOrEmpty(this.jzscz5) ? "1" : this.jzscz5;

                return string.Format(formatStr, this.i0.ToSDD(), this.j1.ToSDD(), this.k12.ToSDD(), this.k23.ToSDD(), this.kgbs4.ToSDD(), this.jzscz5.ToSDD(), this.jzs6.ToSimDefaultStr(), this.wjfxX7.ToSDD(), this.wjfxY8.ToSDD(), this.wjfxZ9.ToSDD(), this.bp10.ToSDD());

            }

        }

        /// <summary> 解析字符串 </summary>
        public override void Build(List<string> newStr)
        {
            //this.ID = Guid.NewGuid().ToString();

            for (int i = 0; i < newStr.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.i0 = newStr[0];
                        break;
                    case 1:
                        this.j1 = newStr[1];
                        break;
                    case 2:
                        this.k12 = newStr[2];
                        break;
                    case 3:
                        this.k23 = newStr[3];
                        break;
                    case 4:
                        this.kgbs4 = newStr[4];
                        break;
                    case 5:
                        this.jzscz5 = newStr[5];
                        break;
                    case 6:
                        this.jzs6 = newStr[6];
                        break;
                    case 7:
                        this.wjfxX7 = newStr[7];
                        break;
                    case 8:
                        this.wjfxY8 = newStr[8];
                        break;
                    case 9:
                        this.wjfxZ9 = newStr[9];
                        break;
                    case 10:
                        this.bp10 = newStr[10];
                        break;
                    default:
                        break;
                }
            }

            WELLCTRL wellctrl = this.FindParentKey(l => l is WELLCTRL) as WELLCTRL;

            this._wellName = wellctrl.WellName0;

            //if (this.GetKeys is WELLCTRL)
            //{
            //    WELLCTRL w = this.ParentKey as WELLCTRL;

            //    this._wellName = w.WellName0;
            //}
        }


        public override void WriteKeyMethod(System.IO.StreamWriter writer)
        {
            writer.WriteLine();
            //   PERF 19   21   25   25   OPEN   1   0.048   0   0   DZ   0 
            writer.WriteLine(string.Empty.ToD() + string.Empty.ToD() + "PERF".ToD() + this.ToString());
        }


        /// <summary> 复制数据 </summary>
        public PERF Copy()
        {
            PERF perf = new PERF("PERF");
            perf.WellName = this.WellName;
            perf.I0 = this.I0;
            perf.J1 = this.J1;
            perf.K12 = this.K12;
            perf.K23 = this.K23;
            perf.Kgbs4 = this.Kgbs4;
            perf.Jzscz5 = this.Jzscz5;
            perf.Jzs6 = this.Jzs6;
            perf.WjfxX7 = this.WjfxX7;
            perf.WjfxY8 = this.WjfxY8;
            perf.WjfxZ9 = this.WjfxZ9;
            perf.Bp10 = this.Bp10;
            return perf;
        }
    }
}
