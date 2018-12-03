#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:17

 * 说明：

 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 支撑剂名称表 </summary>
     
    public class PROPANTNAMES : DynamicKey
    {
        public PROPANTNAMES(string _name)
            : base(_name)
        {

        }


        PROPANTTABLE _propantTable;
        /// <summary> 支撑剂对应的数据表 </summary>
        public PROPANTTABLE PropantTable
        {
            get { return _propantTable; }
            set { _propantTable = value; }
        }

        /// <summary> 绑定数据表 </summary>
        public void BindTable(PROPANTTABLE table)
        {
            if (this.Items.Count != table.GridTable.Columns.Count - 1)
                return;

            _propantTable = table;

            table.GridTable.Columns[0].Caption = "压力";

            table.GridTable.Columns[0].ColumnName = "压力";


            for (int i = 0; i < this.Items.Count; i++)
            {

                table.GridTable.Columns[i + 1].Caption = this.Items[i].Value;

                table.GridTable.Columns[i + 1].ColumnName = this.Items[i].Value;

            }
        }

    }
}
