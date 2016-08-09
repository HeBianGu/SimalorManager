#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01
 * 文件名：START
 * 说明：
 * ROCK
             0            11
/
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
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 支撑剂关系表</summary>
    [KeyAttribute(EclKeyType = EclKeyType.Child, SimKeyType = SimKeyType.tNavigator)]
    public class PROPANTTABLE : Key
    {
        public PROPANTTABLE(string _name)
            : base(_name)
        {

        }

        DataTable gridTable;

        public DataTable GridTable
        {
            get { return gridTable; }
            set { gridTable = value; }
        }

        int propantCount = -1;

        /// <summary> 创建数据 z x y </summary>
        public void Build(int count)
        {
            bool isbuild = propantCount == count;

            if (isbuild)
                return;

            this.propantCount = count;

            gridTable = new DataTable();

            for (int i = 0; i < count + 1; i++)
            {
                DataColumn c = new DataColumn();
                c.ColumnName = i.ToString();
                gridTable.Columns.Add(c);
            }

            for (int i = 0; i < this.Lines.Count; i++)
            {
                //  过滤无效行
                if (!this.Lines[i].Trim().IsWorkLine())
                {
                    continue;
                }
                List<string> pl = this.Lines[i].EclExtendToArray();

                DataRow r = gridTable.NewRow();

                for (int j = 0; j < pl.Count; j++)
                {
                    if (pl.Count > j)
                    {
                        r[j] = pl[j];
                    }
                    else
                    {
                        r[j] = string.Empty;
                    }
                }

                gridTable.Rows.Add(r);
            }


            //  清理内存中数据
            this.Lines.Clear();
        }

        /// <summary> 增加列 </summary>
        public void AddColumns(string colName)
        {
            this.gridTable.Columns.Add(colName);
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            this.Lines.Clear();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.gridTable.Rows.Count; i++)
            {
                sb.Clear();

                for (int j = 0; j < this.gridTable.Columns.Count; j++)
                {
                    sb.Append(this.gridTable.Rows[i][j].ToString().ToDD().PadLeft(KeyConfiger.TableLenght));
                }
                this.Lines.Add(sb.ToString());
            }
            this.Lines.Add(KeyConfiger.EndFlag);

            base.WriteKey(writer);
        }

    }
}
