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
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary>  </summary>
    [KeyAttribute(SimKeyType = SimKeyType.SimON)]
    public class WELL : ConfigerKey, IRootNode
    {
        public WELL(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler = l =>
            {
                //  截取前后空格判断是否为关键字
                return l.Trim();
            };
        }

        private string wellName0;
        /// <summary> 井名 </summary>
        public string WellName0
        {
            get { return wellName0; }
            set { wellName0 = value; }
        }

        private string jkkz1="WIR";
        ///// <summary> 井口控制 </summary>
        //public string Jkkz1
        //{
        //    get { return jkkz1; }
        //    set { jkkz1 = value; }
        //}

        private string jcyblxz2;
        /// <summary> 井产液比例限制 </summary>
        public string Jcyblxz2
        {
            get { return jcyblxz2; }
            set { jcyblxz2 = value; }
        }

        private string jskkz3;
        /// <summary> 井射孔控制 </summary>
        public string Jskkz3
        {
            get { return jskkz3; }
            set { jskkz3 = value; }
        }

        private string oilTemp;
        /// <summary> 说明 </summary>
        public string OilTemp
        {
            get { return oilTemp; }
            set { oilTemp = value; }
        }

        private string waterTemp;
        /// <summary> 说明 </summary>
        public string WaterTemp
        {
            get { return waterTemp; }
            set { waterTemp = value; }
        }

        private string gasTemp;
        /// <summary> 说明 </summary>
        public string GasTemp
        {
            get { return gasTemp; }
            set { gasTemp = value; }
        }

        private string liqTemp;
        /// <summary> 说明 </summary>
        public string LiqTemp
        {
            get { return liqTemp; }
            set { liqTemp = value; }
        }



        string formatStr = "{0}{1}{2}{3} ";

        public override string ToString()
        {
            return string.Format(formatStr, this.wellName0.ToEclStr(), this.ProType.ToString().ToSDD(), this.Jcyblxz2.ToSDD(), this.Jskkz3.ToSimDefaultStr());
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
                        this.wellName0 = newStr[0];
                        break;
                    case 1:
                        this.jkkz1 = newStr[1];
                        break;
                    case 2:
                        this.jcyblxz2 = newStr[2];
                        break;
                    case 3:
                        this.jskkz3 = newStr[3];
                        break;
                    default:
                        break;
                }
            }

            this.SetProType(this.jkkz1);
        }

        /// <summary> 设置生产类型 </summary>
        void SetProType(string value)
        {
            SimONProductType t = (SimONProductType)Enum.Parse(typeof(SimONProductType), value);

            ProType = t;
        }



        //private SimONProductType _protype;
        /// <summary> 生产类型 </summary>
        public SimONProductType ProType
        {
            get
            {
                return (SimONProductType)Enum.Parse(typeof(SimONProductType), this.jkkz1);
            }
            set
            {
                this.jkkz1 = (value).ToString();
            }
        }

        public override void WriteKeyMethod(System.IO.StreamWriter writer)
        {
            writer.WriteLine();
            if (string.IsNullOrEmpty(this.wellName0))
            {
                //    WELL
                writer.WriteLine(this.Name);
            }
            else
            {
                //    WELL  'MSW2'  4   4   101.35  
                writer.WriteLine(string.Empty.ToD() + "WELL".ToDWithOutSpace() + this.ToString());
            }

        }

    }

    /// <summary> 说明 </summary>
    public enum SimONProductType:int
    {
        /// <summary> 0.定井底压力：BHP </summary>
        BHP = 0,
        /// <summary> 1.定水产量：WRAT </summary>
        WRAT,
        /// <summary> 2.定气产量：GRAT </summary>
        GRAT,
        /// <summary> 3.定油产量：ORAT </summary>
        ORAT,
        /// <summary> 4.定总液量：LRAT </summary>
        LRAT,
        /// <summary> 5.定流量注水：WIR </summary>
        WIR,
        /// <summary> 6.定流量注气：GIR </summary>
        GIR,
        /// <summary> 7.定压力注水：WIBHP </summary>
        WIBHP,
        /// <summary> 8.定压力注气：GIBHP </summary>
        GIBHP,
        /// <summary> 9.关井：SHUT </summary>
        SHUT,
        NA

    }
}
