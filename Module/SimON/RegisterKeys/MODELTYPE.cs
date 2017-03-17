#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：GCONPROD
 * 说明：
 * 

BOX
1 40 1 20 5 8 /
PERMX
3200*0.03 /
ENDBOX


 * 
 * 读取规则：找到ENDBOX退出读取，读到要修改的关键字放到ObsoverKey观察对象中
 * 

 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 流体类型 </summary>
    public class MODELTYPE : BaseKey
    {
        public MODELTYPE(string _name)
            : base(_name)
        {
            this.BuilderHandler += (l, k) =>
                {
                    this._typeString = this.Lines[0];

                    switch (this._typeString.ToUpper().Trim())
                    {
                        case "BLACKOIL":
                            _metricType = MetricType.BLACKOIL;
                            break;
                        case "OILWATER":
                            _metricType = MetricType.OILWATER;
                            break;
                        case "GASWATER":
                            _metricType = MetricType.GASWATER;
                            break;
                        default:
                            _metricType = MetricType.HFOIL;
                            break;
                    }

                    return this;
                };
        }

        private MetricType _metricType;
        /// <summary> 流体类型 </summary>
        public MetricType MetricType
        {
            get { return _metricType; }
            set { _metricType = value; }
        }

        private string _typeString;
        ///// <summary> 类型文本 </summary>
        //public string TypeString
        //{
        //    get { return _typeString; }
        //    set { _typeString = value; }
        //}


        public override void WriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            if(this._metricType== MetricType.HFOIL)
            {
                // Todo ：挥发油保存黑油关键字写法
                writer.WriteLine("MODELTYPE".ToDWithOutSpace() + MetricType.BLACKOIL.ToString().ToSDD());
            }
            else
            {
                writer.WriteLine("MODELTYPE".ToDWithOutSpace() + this._metricType.ToString().ToSDD());
            }
           
            this.Lines.Clear();
            base.WriteKey(writer);

        }

    }
}
