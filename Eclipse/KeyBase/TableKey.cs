using OPT.Product.SimalorManager.Eclipse.FileInfos;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 带有Item的关键字抽象类 </summary>
    public class TableKey : Key
    {
        public TableKey(string _name)
            : base(_name)
        {
        }

        List<GridTable> tables = new List<GridTable>();

        public List<GridTable> Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        int z;

        public int Z
        {
            get { return z; }
            set { z = value; }
        }

        private TableKey cacheTable;

        /// <summary> huancunshujubiaoge  </summary>
        public TableKey CacheTable
        {
            get
            {
                if (cacheTable == null)
                    cacheTable = this.Clone();

                //  chongxin fuzhi 
                if (this.cacheTable.Tables.Count != this.Tables.Count
                    || this.cacheTable.Tables.Count != this.Tables.Count
                    || this.cacheTable.Tables.Count != this.Tables.Count
                    )
                {
                    this.cacheTable = this.Clone();
                }

                return cacheTable;
            }
        }

        public bool IsModifyChanged = true;

        /// <summary> fuzhibiaoge shuju </summary>
        public TableKey Clone()
        {
            TableKey clone = new TableKey(this.Name);
            clone.X = this.x;
            clone.Y = this.y;
            clone.Z = this.z;

            foreach (GridTable t in this.Tables)
            {
                clone.Tables.Add(t.Clone());
            }

            return clone;
        }

        /// <summary> chushihua huancun biaoge shuju </summary>
        void InitCacheTables()
        {
            this.CacheTable.tables.Clear();
            //  shuaxin biaoge 
            for (int i = 0; i < this.Tables.Count; i++)
            {
                this.CacheTable.Tables.Add(this.Tables[i].Clone());
            }
        }

        public GridTable GetSingleTable()
        {

            if (this.tables.Count == 0)
            {
                GridTable item = new GridTable();
                item.IndexNum = 1;
                this.Tables.Add(item);
                return item;

            }
            else
            {
                return this.Tables[0];
            }
        }

        /// <summary> 默认数据 </summary>
        private double defaultValue = 0;

        protected double DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }


        /// <summary> 创建数据 z x y </summary>
        public virtual void Build(int tableCount, int xCount, int yCount)
        {
            bool isbuild = this.x == xCount && this.y == yCount && this.z == tableCount;

            if (isbuild)
                return;

            this.x = xCount;
            this.y = yCount;
            this.z = tableCount;

            this.Tables.Clear();

            z = tableCount;
            y = yCount;
            x = xCount;

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
                this.tables.Add(t);
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
                    ////  表格索引
                    //int tableIndex = j / (xCount * yCount);

                    ////  行索引
                    //int rowIndex = (j % (xCount * yCount)) / yCount;

                    ////  列索引
                    //int colIndex = (j % (xCount * yCount)) % yCount;

                    ////  插入数据
                    //this.tables[tableIndex].Rows[colIndex][rowIndex] = pl[j - vIndex];

                    //  表格索引
                    int tableIndex = j / (xCount * yCount);

                    //  行索引
                    int rowIndex = (j % (xCount * yCount)) / xCount;

                    //  列索引
                    int colIndex = (j % (xCount * yCount)) % xCount;

                    //  插入数据
                    this.tables[tableIndex].Matrix.Mat[rowIndex, colIndex] = pl[j - vIndex].ToDouble();

                }

                vIndex += pl.Count;
            }

            //  清理内存中数据
            this.Lines.Clear();
        }

        public override BaseKey ReadKeyLine(System.IO.StreamReader reader)
        {
            BaseKey overKey = base.ReadKeyLine(reader);

            return overKey;
        }

        /// <summary> 读取BOX下的表格数据 </summary>
        public string ReadBoxKeyLine(System.IO.StreamReader reader)
        {
            string tempStr;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().TrimEnd();

                //  遇到结束符退出
                if (tempStr == KeyConfiger.EndFlag || tempStr == BOX.BoxEndFlag)
                {
                    return null;
                }
                if (tempStr.IsKeyFormat())
                {
                    return tempStr;
                }

                //  有效行插入到集合
                if (tempStr.IsWorkLine())
                {
                    this.Lines.Add(tempStr);
                }
            }
            return null;
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            this.Lines.Clear();

            StringBuilder sb = new StringBuilder();

            foreach (GridTable v in tables)
            {
                for (int i = 0; i < v.Matrix.Row; i++)
                {
                    sb.Clear();

                    for (int j = 0; j < v.Matrix.Col; j++)
                    {
                        sb.Append(v.Matrix[i, j].ToString().ToD().PadLeft(KeyConfiger.TableLenght));
                    }
                    this.Lines.Add(sb.ToString());
                }
            }
            this.Lines.Add(KeyConfiger.EndFlag);

            base.WriteKey(writer);
        }

        /// <summary> 深复制成指定名称的关键字 </summary>
        public TableKey TransToTableKeyByName(string name)
        {
            TableKey tb = KeyConfigerFactroy.Instance.CreateChildKey<TableKey>(name);
            //  复制行数据
            this.Lines.ForEach(l => tb.Lines.Add(l));

            //  复制维数
            tb.X = this.x;

            tb.y = this.y;

            tb.z = this.z;

            //  复制表格
            this.Tables.ForEach(l => tb.Tables.Add(l.Clone()));

            return tb;
        }

        /// <summary> 修正关键字 对关键字的指定范围值执行Func操作 </summary>
        public void RunModify(Func<double, double> func, RegionParam region)
        {

            if (CheckRegion(region))
            {
                double temp;

                //  遍历更改区域 
                for (int x = region.XFrom; x < region.XTo; x++)
                {
                    for (int y = region.YFrom; y < region.YTo; y++)
                    {
                        for (int z = region.ZFrom; z < region.ZTo; z++)
                        {
                            //  对值执行func操作
                            this.Tables[z].Matrix.Mat[y, x] = func(this.Tables[z].Matrix.Mat[y, x].ToString().ToDouble());
                        }
                    }
                }
            }
        }

        /// <summary> 修正关键字 对关键字的指定范围值执行Func操作 </summary>
        public void RunModify(Func<double, double> func)
        {
            RegionParam region = new RegionParam();
            region.XFrom = 1;
            region.XTo = this.x;
            region.YFrom = 1;
            region.YTo = this.y;
            region.ZFrom = 1;
            region.ZTo = this.z;

            RunModify(func, region);
        }

        /// <summary> 用修正参数获取临时数据 endPosition 碰到此修正参数退出 </summary>
        public void RefreshCacheTable(EclipseData _eclData, List<ModifyKey> modifys, IModifyModel endPosition = null)
        {
            if (!IsModifyChanged) return;

            this.InitCacheTables();

            //  查找所有修改关键字
            //List<ModifyKey> modifys = _eclData.Key.FindAll<ModifyKey>();

            ////  获取当前关键字关联的修改关键字
            //List<ModifyKey> thisModify = modifys.FindAll(l => l.ObsoverModel.Exists(k => k.KeyName == this.Name));

            DIMENS d = _eclData.Key.Find<DIMENS>();

            if (d == null) return;

            IsModifyChanged = false;

            //  构造全网格范围
            RegionParam tempRegion = new RegionParam();
            tempRegion.XFrom = 1;
            tempRegion.XTo = d.X;
            tempRegion.YFrom = 1;
            tempRegion.YTo = d.Y;
            tempRegion.ZFrom = 1;
            tempRegion.ZTo = d.Z;

            foreach (ModifyKey m in modifys)
            {
                //  是空则用临时范围
                if (m.DefautRegion == null)
                {
                    m.DefautRegion = tempRegion;
                }
                else
                {
                    //  不是空赋值临时范围
                    tempRegion = m.DefautRegion;
                }




                foreach (IModifyModel md in m.ObsoverModel)
                {

                    //  读取到结束标记处退出
                    if (endPosition != null && md == endPosition)
                        return;

                    //  是空则用临时范围
                    if (md.Region == null)
                    {
                        md.Region = tempRegion;
                    }
                    else
                    {
                        //  不是空赋值临时范围
                        tempRegion = md.Region;
                    }

                    if (md.KeyName != this.Name) continue;

                    if (md is ModifyApplyModel)
                    {
                        ModifyApplyModel app = md as ModifyApplyModel;

                        app.RunModify(this.CacheTable);
                    }
                    else if (md is ModifyCopyModel)
                    {
                        ModifyCopyModel copy = md as ModifyCopyModel;

                        TableKey copyKey = _eclData.Key.Find<TableKey>(l => l.Name == copy.Value);

                        ////  取到当前位置
                        //List<ModifyKey> modifysCopyPosition = modifys.TakeWhile(l => l == m).ToList();

                        ////  取当前model位置
                        //List<IModifyModel> modelCopyPosition = m.ObsoverModel.TakeWhile(l => l == md).ToList();

                        //modifysCopyPosition.rem

                        if (copyKey == null)
                        {
                            //  没有则创建关键字
                            copyKey = KeyConfigerFactroy.Instance.CreateChildKey<TableKey>(copy.Value);

                            m.ParentKey.Add(copyKey);

                        }

                        copyKey.Build(this.Z, this.X, this.Y);

                        copyKey.RefreshCacheTable(_eclData, modifys, copy);

                        copyKey.IsModifyChanged = true;

                        copy.RunModify(this.CacheTable, copyKey.CacheTable);


                    }
                    else if (md is ModifyBoxModel)
                    {
                        ModifyBoxModel app = md as ModifyBoxModel;

                        app.RunModify(this.CacheTable);
                    }
                }
            }

        }

        /// <summary> 用修正参数获取临时数据 endPosition 碰到此修正参数退出 </summary>
        public void RefreshCacheTable(CARFIN carfin, List<ModifyKey> modifys, IModifyModel endPosition = null)
        {
            if (!IsModifyChanged) return;

            this.InitCacheTables();

            IsModifyChanged = false;


            //  构造全网格范围
            RegionParam tempRegion = new RegionParam();
            tempRegion.XFrom = 1;
            tempRegion.XTo = carfin.X;
            tempRegion.YFrom = 1;
            tempRegion.YTo = carfin.Y;
            tempRegion.ZFrom = 1;
            tempRegion.ZTo = carfin.Z;

            foreach (ModifyKey m in modifys)
            {
                //  是空则用临时范围
                if (m.DefautRegion == null)
                {
                    m.DefautRegion = tempRegion;
                }
                else
                {
                    //  不是空赋值临时范围
                    tempRegion = m.DefautRegion;
                }

                List<IModifyModel> models = new List<IModifyModel>();
                //  加载本修改模型中关键字
                models.AddRange(m.ObsoverModel);

                //  加载子修改模型中关键字 如BOX
                List<ModifyKey> ms = m.FindAll<ModifyKey>();

                ms.Remove(m);

                ms.ForEach(l => models.AddRange(l.ObsoverModel));

                foreach (IModifyModel md in models)
                {

                    //  读取到结束标记处退出
                    if (endPosition != null && md == endPosition)

                        return;

                    //  是空则用临时范围
                    if (md.Region == null)
                    {
                        md.Region = tempRegion;
                    }
                    else
                    {
                        //  不是空赋值临时范围
                        tempRegion = md.Region;
                    }

                    if (md.KeyName != this.Name) continue;

                    if (md is ModifyApplyModel)
                    {
                        ModifyApplyModel app = md as ModifyApplyModel;

                        app.RunModify(this.CacheTable);
                    }
                    else if (md is ModifyCopyModel)
                    {
                        ModifyCopyModel copy = md as ModifyCopyModel;

                        //  查找关键字模型
                        ModifyBoxModel boxModel = carfin.ObsoverModel.Find(l => l is ModifyBoxModel && l.KeyName == copy.Value) as ModifyBoxModel;

                        TableKey copyKey = null;

                        if (boxModel != null) copyKey = boxModel.Key;

                        if (copyKey == null)
                        {
                            //  没有则创建关键字
                            copyKey = KeyConfigerFactroy.Instance.CreateChildKey<TableKey>(copy.Value);

                            m.ParentKey.Add(copyKey);

                        }

                        copyKey.Build(this.Z, this.X, this.Y);

                        copyKey.RefreshCacheTable(carfin, modifys, copy);

                        copyKey.IsModifyChanged = true;

                        copy.RunModify(this.CacheTable, copyKey.CacheTable);

                        copyKey.IsModifyChanged = true;
                    }
                    else if (md is ModifyBoxModel)
                    {
                        ModifyBoxModel app = md as ModifyBoxModel;

                        app.RunModify(this.CacheTable);
                    }
                }
            }

        }

        /// <summary> 删除当前关键的所有修改参数 </summary>
        public void RemoveAllModify(List<ModifyKey> modifys)
        {
            IsModifyChanged = true;

            foreach (ModifyKey m in modifys)
            {
                //  加载子修改模型中关键字 如BOX
                List<ModifyKey> ms = m.FindAll<ModifyKey>();

                //  删除所有指定名称的修改参数
                ms.ForEach(l => l.RemoveKey(this.Name));

                ms.Remove(m);

                //  如果不存在修正参数删除关键字
                foreach(var v  in ms)
                {
                    if(v.ObsoverModel.Count==0)
                    {
                        v.Delete();
                    }
                }
            }

            //  如果不存在修正参数删除关键字
            modifys.RemoveAll(l => l.ObsoverModel.Count == 0);
        }


        /// <summary> 删除当前关键的所有修改参数 </summary>
        public void RemoveAllModify(CARFIN carfin)
        {
            carfin.ObsoverModel.RemoveAll(l => l.KeyName == this.Name);

            this.RemoveAllModify(carfin.ModifyKeyCar);
        }

        /// <summary> 检查修改分区是否合法 </summary>
        bool CheckRegion(RegionParam region)
        {
            if (region.ZTo > this.Z ||
                region.XTo > this.X ||
                region.YTo > this.Y
                )
            {
                throw new Exception("CheckRegion() err Dimens Count");
            }
            else
            {
                return true;
            }
        }



    }
    /// <summary> 表格数据结构 </summary>
    public class GridTable
    {
        Matrix _matrix = new Matrix();
        /// <summary> 矩阵数据 </summary>
        public Matrix Matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }
        public GridTable()
        {

        }
        private int indexNum;
        /// <summary> 层号 </summary>
        public int IndexNum
        {
            get { return indexNum; }
            set { indexNum = value; }
        }

        int xCount;
        /// <summary> ｘ方向 </summary>
        public int XCount
        {
            get { return xCount; }
            set { xCount = value; }
        }

        int yCount;
        /// <summary> ｙ方向 </summary>
        public int YCount
        {
            get { return yCount; }
            set { yCount = value; }
        }

        TableKey parent = null;
        /// <summary> 父表格关键字 </summary>
        public TableKey Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        /// <summary> 创建表格数据 </summary>
        public void Build(double defaultValue = 0)
        {
            _matrix.ReSetRowAndCol(yCount, xCount);

            for (int r = 0; r < _matrix.Row; r++)
            {
                for (int c = 0; c < _matrix.Col; c++)
                {
                    _matrix.Mat[r, c] = defaultValue;
                }
            }

        }

        /// <summary> 转换成DataTable数据结构 </summary>
        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();

            for (int x = 1; x <= _matrix.Col; x++)
            {
                dt.Columns.Add(x.ToString());
            }
            for (int y = 0; y < _matrix.Row; y++)
            {
                DataRow dr = dt.NewRow();
                for (int index = 0; index < _matrix.Col; index++)
                {
                    dr[index] = _matrix.Mat[y, index];
                }
                dt.Rows.Add(dr);

            }

            return dt;
        }

        /// <summary> 复制表格数据 </summary>
        public GridTable Clone()
        {
            GridTable grid = new GridTable();
            grid.xCount = this.xCount;
            grid.yCount = this.yCount;
            grid.indexNum = this.indexNum;
            grid.Matrix = this._matrix.Clone();
            grid.parent = this.parent;
            return grid;
        }
    }


    public class String2D
    {
        string[,] strs = new string[0, 0];

        public string[,] Strs
        {
            get { return strs; }
            set { strs = value; }
        }
    }
}
