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

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 输出控制 </summary>
    public class RPTSCHED : ConfigerKey
    {
        public RPTSCHED(string _name)
            : base(_name)
        {

        }

        private string type = @"
 FREQ {0}  PRECS {1}  PRINTW {2}
 {3} {4} {5} {6} {7}
 {8} {9} {10} {11}  {12} {13} {14} {15} {16}
 {17}
 {18}
 {19}
/";

        //        private string type = @"
        // FREQ 0  PRECS 7  PRINTW 11
        // BINOUT RSTBIN PLOT GEOM WLOC
        // WATIP GASIP OILIP POIL SGAS SOIL SWAT RS 
        // SEPARATE
        // RPTRESULT
        // RPTONLY
        ///

        //RPTSUM
        //  FWPR /
        //  FGPR /
        //  FLPR /
        //  FGIR /
        //  FWIR /
        //  FWCT /
        //  FWPT /
        //  FGPT /
        //  FLPT /
        //  FGIT /
        //  FWIT /
        //  PGAS WAVG /  
        //  FGIP /     
        //  WWPR /
        //  WGPR /
        //  WLPR /
        //  WGIR /
        //  WWIR /
        //  WWPT /
        //  WGPT /
        //  WLPT /
        //  WGIT /
        //  WWIT /
        //  WBHP /
        //  / ";
        private string freq0 = "0";
        /// <summary> 说明 </summary>
        public string FREQ
        {
            get { return freq0; }
            set { freq0 = value; }
        }

        private string precs1 = "7";
        /// <summary> 说明 </summary>
        public string PRECS
        {
            get { return precs1; }
            set { precs1 = value; }
        }

        private string printw2 = "11";
        /// <summary> 说明 </summary>
        public string PRINTW
        {
            get { return printw2; }
            set { printw2 = value; }
        }

        private string binout3 = "BINOUT";
        /// <summary> 说明 </summary>
        public bool BINOUT
        {
            get { return !string.IsNullOrEmpty(binout3); }
            set { binout3 = value ? "BINOUT" : null; }
        }

        private string rstbin4 = "RSTBIN";
        /// <summary> 说明 </summary>
        public bool RSTBIN
        {
            get { return !string.IsNullOrEmpty(rstbin4); }
            set { rstbin4 = value ? "RSTBIN" : null; }
        }

        private string plot5 = "PLOT";
        /// <summary> 说明 </summary>
        public bool PLOT
        {
            get { return !string.IsNullOrEmpty(plot5); }
            set { plot5 = value ? "PLOT" : null; }
        }

        private string geom6 = "GEOM";
        /// <summary> 说明 </summary>
        public bool GEOM
        {
            get { return !string.IsNullOrEmpty(geom6); }
            set { geom6 = value ? "GEOM" : null; }
        }


        private string watip7 = "WATIP";
        /// <summary> 说明 </summary>
        public bool WATIP
        {
            get { return !string.IsNullOrEmpty(watip7); }
            set { watip7 = value ? "WATIP" : null; }
        }

        private string gasip8 = "GASIP";
        /// <summary> 说明 </summary>
        public bool GASIP
        {
            get { return !string.IsNullOrEmpty(gasip8); }
            set { gasip8 = value ? "GASIP" : null; }
        }

        private string wloc9 = "WLOC";
        /// <summary> 说明 </summary>
        public bool WLOC
        {
            get { return !string.IsNullOrEmpty(wloc9); }
            set { wloc9 = value ? "WLOC" : null; }
        }

        private string oilip10 = "OILIP";
        /// <summary> 说明 </summary>
        public bool OILIP
        {
            get { return !string.IsNullOrEmpty(oilip10); }
            set { oilip10 = value ? "OILIP" : null; }
        }

        private string poil11 = "POIL";
        /// <summary> 说明 </summary>
        public bool POIL
        {
            get { return !string.IsNullOrEmpty(poil11); }
            set { poil11 = value ? "POIL" : null; }
        }



        private string pgas19 = "PGAS";
        /// <summary> 说明 </summary>
        public bool PGAS
        {
            get { return !string.IsNullOrEmpty(pgas19); }
            set { pgas19 = value ? "PGAS" : null; }
        }


        private string sgas12 = "SGAS";
        /// <summary> 说明 </summary>
        public bool SGAS
        {
            get { return !string.IsNullOrEmpty(sgas12); }
            set { sgas12 = value ? "SGAS" : null; }
        }



        private string soil13 = "SOIL";
        /// <summary> 说明 </summary>
        public bool SOIL
        {
            get { return !string.IsNullOrEmpty(soil13); }
            set { soil13 = value ? "SOIL" : null; }
        }


        private string swat14 = "SWAT";
        /// <summary> 说明 </summary>
        public bool SWAT
        {
            get { return !string.IsNullOrEmpty(swat14); }
            set { swat14 = value ? "SWAT" : null; }
        }


        private string rs15 = "RS";
        /// <summary> 说明 </summary>
        public bool RS
        {
            get { return !string.IsNullOrEmpty(rs15); }
            set { rs15 = value ? "RS" : null; }
        }

        private string separate16 = "SEPARATE";
        /// <summary> 说明 </summary>
        public bool SEPARATE
        {
            get { return !string.IsNullOrEmpty(separate16); }
            set { separate16 = value ? "SEPARATE" : null; }
        }

        private string rptresult17 = "RPTRESULT";
        /// <summary> 说明 </summary>
        public bool RPTRESULT
        {
            get { return !string.IsNullOrEmpty(rptresult17); }
            set { rptresult17 = value ? "RPTRESULT" : null; }
        }

        private string rptonly18 = "RPTONLY";
        /// <summary> 说明 </summary>
        public bool RPTONLY
        {
            get { return !string.IsNullOrEmpty(rptonly18); }
            set { rptonly18 = value ? "RPTONLY" : null; }
        }

        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(type, this.freq0, this.precs1, this.printw2, 
                this.binout3, this.rstbin4, this.plot5, this.geom6, this.wloc9,
                this.watip7, this.gasip8, this.oilip10, this.poil11, this.pgas19, this.sgas12, this.soil13, this.swat14, this.rs15, 
                this.separate16, 
                this.rptresult17, 
                this.rptonly18);
        }

        /// <summary> 解析字符串 </summary>
        public override void Build(List<string> newStr)
        {
            this.ID = Guid.NewGuid().ToString();

            //for (int i = 0; i < newStr.Count; i++)
            //{
            //    switch (i)
            //    {
            //        case 0:
            //            this.type = newStr[0];
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

    }
}
