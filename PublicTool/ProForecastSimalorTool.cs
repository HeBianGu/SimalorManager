#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 14:18:22
 * 文件名：ProForecastSimalorTool
 * 说明：方案预测完善工具
 *       主要包含独立的完善Eclipse文件的方法
 *  
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.PublicTool
{
    /// <summary> 方案预测完善工具 </summary>
    public class SimToolForProForecast : BaseFactory<SimToolForProForecast>
    {

        /// <summary> 增加井底流压限制  </summary>
        /// <param name="pValue"> 值 </param>
        /// <returns> 是否成功 </returns>
        //public bool AddBtmWellPresslimi(string filePath, we
        //{
        //    return true;
        //}


        /// <summary> 增加最大汽油比  </summary>
        /// <param name="pValue"> 值 </param>
        /// <returns> 是否成功 </returns>
        public bool AddMaxRatioOfAirToWater(double pValue)
        {
            return true;
        }




        /// <summary> 增加时间间隔 </summary>
        /// <param name="startTime"> 起始时间 </param>
        /// <param name="endTime"> 终止时间 </param>
        /// <param name="span"> 时间间隔(天) </param>
        /// <returns> 是否成功 </returns>
        public bool AddTimeSpan(DateTime startTime, DateTime endTime, double span)
        {
            return true;
        }

    }
}
