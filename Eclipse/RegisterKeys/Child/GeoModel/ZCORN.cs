#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53
 * 文件名：COORD
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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.Eclipse, IsBigDataKey = true)]
    public class ZCORN : TableKey
    {
        public ZCORN(string _name)
            : base(_name)
        {

        }



        /// <summary> 创建数据 z x y </summary>
        public override void Build(int tableCount, int xCount, int yCount)
        {
            bool isbuild = this.X == xCount && this.Y == yCount && this.Z == tableCount;

            if (isbuild)
                return;

            this.Tables.Clear();

            Z = tableCount;
            Y = yCount;
            X = xCount;


            xCount = xCount * 2;
            yCount = yCount * 2;
            tableCount = tableCount * 2;

            for (int i = 1; i <= tableCount; i++)
            {
                if (this.Tables.Exists(l => l.IndexNum == i))
                {
                    //  存在该层不处理
                    continue;
                }
                GridTable t = new GridTable();
                t.Parent = this;
                t.IndexNum = i;
                t.XCount = xCount;
                t.YCount = yCount;
                t.Build(this.DefaultValue);

                //this.BuildTable(t, t.XCount, t.YCount);

                this.Tables.Add(t);
            }

            int vIndex = 0;

            for (int i = 0; i < this.Lines.Count; i++)
            {
                //  过滤无效行
                if (!this.Lines[i].Trim().IsWorkLine())
                {
                    continue;
                }
                List<string> pl = this.Lines[i].EclExtendToPetrelArray();

                for (int j = vIndex; j < vIndex + pl.Count; j++)
                {
                    //  表格索引
                    int tableIndex = j / (xCount * yCount);

                    //  行索引
                    int rowIndex = (j % (xCount * yCount)) / xCount;

                    //  列索引
                    int colIndex = (j % (xCount * yCount)) % xCount;

                    //  插入数据
                    this.Tables[tableIndex].Matrix.Mat[rowIndex,colIndex] = pl[j - vIndex].ToDouble();

                }

                vIndex += pl.Count;
            }
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
