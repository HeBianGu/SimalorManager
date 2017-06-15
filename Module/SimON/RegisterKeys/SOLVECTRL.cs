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
    /// <summary> 时间步控制，求解器选择，收敛判据 </summary>
    [KeyAttribute(SimKeyType = SimKeyType.SimON,AnatherName = "TUNING")]
    public class SOLVECTRL : MergeConfiger
    {
        public SOLVECTRL(string _name)
            : base(_name)
        {
            this.BuilderHandler += (l, k) =>
                {
                    this.Date = DatesKeyService.Instance.GetSimONDateTime(this.Lines[0].Trim().Substring(0, 8));
                    return this;
                };
        }

        private DateTime _date = DateTime.Now;
        /// <summary> 说明 </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }



        public string szstzdhs0;
        //[Desc("# Ref_dep")]
        ///// <summary> 1.	模拟开始时间t0，可以是实数时间或YYYY-MM-DD格式的日期，使用实数时间还是日期，在脚本文件中必须统一，实数时间的单位：day(米制)，day(英制)，hour(lab)，ms(MESO) </summary>
        //public string Szstzdhs0
        //{
        //    get { return szstzdhs0; }
        //    set { szstzdhs0 = value; }
        //}

        private string szstljs1 = "10";
        [Desc("# Ref_dep")]
        /// <summary> 2.	一个时间步内的最大牛顿迭代次数，多于此次数，模拟软件会减少时间步长，重新开始迭代。推荐值：10 </summary>
        public string Szstljs1
        {
            get { return szstljs1; }
            set { szstljs1 = value; }
        }

        private string ctstyxhs2 = "0.1";
        [Desc("# Ref_dep")]
        /// <summary> 3.	最短时间步长，也是第一个时间步的步长，单位：day(米制)，day(英制)，hour(lab)，ms(MESO). 推荐值：0.1至0.001 </summary>
        public string Ctstyxhs2
        {
            get { return ctstyxhs2; }
            set { ctstyxhs2 = value; }
        }

        private string ctstyxzdhs3 = "366";
        [Desc("# Ref_dep")]
        /// <summary> 4.	最大时间步长，单位：day(米制)，day(英制)，hour(lab)，ms(MESO) </summary>
        public string Ctstyxzdhs3
        {
            get { return ctstyxzdhs3; }
            set { ctstyxzdhs3 = value; }
        }

        private string jxstzds4 = "1054";
        [Desc("# Ref_dep")]
        /// <summary> 5.	求解器编号。推荐值：1034 (CPR+GMRES，restart number=10，相对误差10-4). 求解器编号的规则见表 1。 </summary>
        public string Jxstzds4
        {
            get { return jxstzds4; }
            set { jxstzds4 = value; }
        }

        private string e100wgzds5 = "0";
        [Desc("# Ref_dep")]
        /// <summary> 6.	时间步目标最大压力变化量，单位：bar(米制)，psi(英制)，atm(lab)，Pa(MESO). 推荐值：0(不控制) </summary>
        public string E100wgzds5
        {
            get { return e100wgzds5; }
            set { e100wgzds5 = value; }
        }

        private string e300jxstzds6 = "0.2";
        /// <summary> 7.	时间步目标最大饱和度变化量，推荐值：0.2至0.5 </summary>
        public string E300jxstzds6
        {
            get { return e300jxstzds6; }
            set { e300jxstzds6 = value; }
        }

        private string e300jxstzds8 = "0";
        /// <summary> 8.	时间步目标最大溶解质量比(或吸附质量比)变化。推荐值：0(不控制)</summary>
        public string E300jxstzds8
        {
            get { return e300jxstzds8; }
            set { e300jxstzds8 = value; }
        }

        private string e300jxstzds9 = "1E-3";
        /// <summary> 9.	收敛时允许的“单个网格最大相对质量误差”。推荐值：1E-3或1E-4 </summary>
        public string E300jxstzds9
        {
            get { return e300jxstzds9; }
            set { e300jxstzds9 = value; }
        }

        private string e300jxstzds10 = "1E-6";
        /// <summary> 10.	收敛时允许的“总质量误差/全油藏总物质质量”。推荐值：1E-6或1E-7 </summary>
        public string E300jxstzds10
        {
            get { return e300jxstzds10; }
            set { e300jxstzds10 = value; }
        }

        private string e300jxstzds11 = "3";
        /// <summary> 11.	时间步增长乘数。推荐值：2至3 </summary>
        public string E300jxstzds11
        {
            get { return e300jxstzds11; }
            set { e300jxstzds11 = value; }
        }

        private string e300jxstzds12 = "0.3";
        /// <summary> 12.	时间步缩减乘数。推荐值：0.3至0.5 </summary>
        public string E300jxstzds12
        {
            get { return e300jxstzds12; }
            set { e300jxstzds12 = value; }
        }

        private string e300jxstzds13 = "1";
        /// <summary> 13.	收敛判据是否包含“每个变量变化量足够小”，0—不包含，1—包含。推荐值：1 </summary>
        public string E300jxstzds13
        {
            get { return e300jxstzds13; }
            set { e300jxstzds13 = value; }
        }


        public bool IsE300jxstzds13
        {
            get
            {
                return e300jxstzds13 == "1";
            }
            set
            {
                e300jxstzds13 = value ? "1" : "0";
            }
        }

        private string e300jxstzds14 = "1";
        /// <summary> 14.	AMG solver参数设置。只推荐使用默认值：1。 </summary>
        public string E300jxstzds14
        {
            get { return e300jxstzds14; }
            set { e300jxstzds14 = value; }
        }

        private string e300jxstzds15 = "0";
        /// <summary> 15.	是否用直接法求解井矩阵，作为预调件的一步。 0—自动判断，1—是。推荐值：0 </summary>
        public string E300jxstzds15
        {
            get { return e300jxstzds15; }
            set { e300jxstzds15 = value; }
        }

        public bool IsE300jxstzds15
        {
            get
            {
                return e300jxstzds15 == "1";
            }
            set
            {
                e300jxstzds15 = value ? "1" : "0";
            }
        }


        string formatStr = @"{0}
# max_itr   min_dt   max_dt  Solver   MaxDP   MaxDS   MaxDC 
    {1}       {2}      {3}    {4}      {5}     {6}     {7}
# MBEPerCell MBEAvg  DtIncr   DtCut  CheckDetx  AMG_SET  WWJS
    {8}       {9}    {10}     {11}     {12}      {13}    {14}";

        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, this.Date.ToString("yyyyMMdd") + "D", szstljs1, ctstyxhs2, ctstyxzdhs3, jxstzds4,
                e100wgzds5, e300jxstzds6, e300jxstzds8, e300jxstzds9, e300jxstzds10, e300jxstzds11, e300jxstzds12,
                e300jxstzds13, e300jxstzds14, e300jxstzds15);
        }

        /// <summary> 解析字符串 </summary>
        public override void Build(List<string> newStr)
        {
            this.ID = Guid.NewGuid().ToString();

            for (int i = 0; i < newStr.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.szstzdhs0 = newStr[0];
                        break;
                    case 1:
                        this.szstljs1 = newStr[1];
                        break;
                    case 2:
                        this.ctstyxhs2 = newStr[2];
                        break;
                    case 3:
                        this.ctstyxzdhs3 = newStr[3];
                        break;
                    case 4:
                        this.jxstzds4 = newStr[4];
                        break;
                    case 5:
                        this.e100wgzds5 = newStr[5];
                        break;
                    case 6:
                        this.e300jxstzds6 = newStr[6];
                        break;
                    case 7:
                        this.e300jxstzds8 = newStr[7];
                        break;
                    case 8:
                        this.e300jxstzds9 = newStr[8];
                        break;
                    case 9:
                        this.e300jxstzds10 = newStr[9];
                        break;
                    case 10:
                        this.e300jxstzds11 = newStr[10];
                        break;
                    case 11:
                        this.e300jxstzds12 = newStr[11];
                        break;
                    case 12:
                        this.e300jxstzds13 = newStr[12];
                        break;
                    case 13:
                        this.e300jxstzds14 = newStr[13];
                        break;
                    case 14:
                        this.e300jxstzds15 = newStr[14];
                        break;
                    case 15:
                        this.e300jxstzds6 = newStr[15];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
