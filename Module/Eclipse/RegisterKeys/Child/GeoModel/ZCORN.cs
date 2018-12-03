#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 13:39:53

 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.EclipseAndSimON )]
    public class ZCORN : TableKey
    {
        public ZCORN(string _name)
            : base(_name)
        {
            this.TransValueX = l => 2 * l;

            this.TransValueY = l => 2 * l;

            this.TransValueZ = l => 2 * l;
        }



        /// <summary> 创建角点网格 </summary>
        public void BuildTable(GridTable table, int xCount, int yCount)
        {
            //for (int x = 1; x <= xCount / 2; x++)
            //{
            //    table.Columns.Add("Front" + (x).ToString());

            //    table.Columns.Add("Back" + (x).ToString());
            //}
            //for (int y = 0; y < yCount; y++)
            //{
            //    DataRow dr = table.NewRow();

            //    table.Rows.Add(dr);
            //}
        }
    }
}
