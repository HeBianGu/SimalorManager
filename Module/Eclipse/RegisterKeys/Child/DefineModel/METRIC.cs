#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53

 * 说明：
 * 
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

    /// <summary> 公制单位 </summary>
    public class METRIC : Key
    {
        public METRIC(string _name)
            : base(_name)
        {

        }

        //private MetricType metrictype;
        ///// <summary> 流体类型 </summary>
        //public MetricType Metrictype
        //{
        //    get { return metrictype; }
        //    set { metrictype = value; }
        //}

        public override void WriteKey(System.IO.StreamWriter writer)
        {

            //CmdToStr();

            base.WriteKey(writer);
        }

        //void CmdToStr()
        //{
        //    this.Lines.Clear();

        //    switch (Metrictype)
        //    {
        //        case MetricType.BLACKOIL:
        //            this.Lines.Add("GAS");
        //            this.Lines.Add("OIL");
        //            this.Lines.Add("WATER");
        //            this.Lines.Add("DISGAS");
        //            break;
        //        case MetricType.OILWATER:
        //            this.Lines.Add("OIL");
        //            this.Lines.Add("WATER");
        //            break;
        //        case MetricType.GASWATER:
        //            this.Lines.Add("GAS");
        //            this.Lines.Add("WATER");
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }


}
