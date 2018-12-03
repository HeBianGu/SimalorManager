using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> 带有Item的关键字抽象类 创建的表资源要记得释放Dispose() </summary>
    public partial class TableKey : Key
    {
        ReadTableKeyMode _readMode = ReadTableKeyMode.ReadFromLines;

        public TableKey(string _name)
            : base(_name)
        {

            if (_readMode == ReadTableKeyMode.ReadFromLines)
            {
                this.BuilderHandler = (l, k) =>
                {
                    int xt = this.BaseFile.X;
                    int yt = this.BaseFile.Y;
                    int zt = this.BaseFile.Z;

                    this.Build(zt, xt, yt);
                    return this;
                };
            }

            if (_readMode == ReadTableKeyMode.ReadRightNow)
            {

                // Todo ：初始化关键字是要做的事情 
                this.CreaterHandler += (l, k) =>
                    {
                        int xt = this.BaseFile.X;
                        int yt = this.BaseFile.Y;
                        int zt = this.BaseFile.Z;

                        // Todo ：创建表结构 
                        //this.BuildTableEntity(zt, xt, yt);

                        this.Build(zt, xt, yt);
                    };

                // Todo ：注册实时读取方法 
                this.ReadNewLineHandler = this.ReadNewLine;


                // Todo ：读取结束构建表格要做的事情 
                this.BuilderHandler = (l, k) =>
                {
                    return this;
                };
            }
        }


        /// <summary> 索引器 X Y Z </summary>
        public double this[int inx, int iny, int inz]
        {
            get { return this.tables[inz].Get(iny, inx); }
            set { this.tables[inz].Set(iny, inx, value); }
        }

        List<GridTable> tables = new List<GridTable>();
        /// <summary> 包含的表格数据 </summary>
        public List<GridTable> Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        int x;
        /// <summary> 维数 </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        int y;
        /// <summary> 维数 </summary>
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        int z;
        /// <summary> 维数 </summary>
        public int Z
        {
            get { return z; }
            set { z = value; }
        }

        private TableKey cacheTable;

        /// <summary> 缓存数据表格  </summary>
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

        /// <summary> 复制表格数据 </summary>
        [Obsolete("修改废弃")]
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

        /// <summary> 初始化缓存表格数据 </summary>
        [Obsolete("修改废弃")]
        void InitCacheTables()
        {
            this.CacheTable.tables.Clear();
            //  刷新表格
            for (int i = 0; i < this.Tables.Count; i++)
            {
                this.CacheTable.Tables.Add(this.Tables[i].Clone());
            }
        }

        public GridTable GetSingleTable()
        {

            if (this.tables.Count == 0)
            {
                GridTable item = new GridTable(this, 1);
                item.IndexNum = 1;
                this.Tables.Add(item);
                return item;

            }
            else
            {
                return this.Tables[0];
            }
        }

        private double defaultValue = 0;
        /// <summary> 默认数据 </summary>
        protected double DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        protected Func<int, int> TransValueX = l => l;

        protected Func<int, int> TransValueY = l => l;

        protected Func<int, int> TransValueZ = l => l;


        // Todo ：解析数据时匹配索引执行Func操作 
        protected Dictionary<int, Func<double, double>> MatchFunc;

        /// <summary> 创建数据 z x y mmf表示内存镜像文件夹的路径 C:\Users\navy\Desktop\</summary>
        public virtual void Build(int tableCount, int xCount, int yCount, string mmf = null)
        {

            tableCount = TransValueZ(tableCount);
            xCount = TransValueX(xCount);
            yCount = TransValueY(yCount);

            bool isbuild = this.x == xCount && this.y == yCount && this.z == tableCount;

            if (isbuild) return;

            this.x = xCount;
            this.y = yCount;
            this.z = tableCount;

            //  创建结构
            this.BuildTableEntity(this.z, this.x, this.y, mmf);

            vIndex = 0;

            if (this.MatchFunc == null)
            {
                // Todo ：不使用相对坐标 
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
                        int tableIndex = j / (this.x * this.y);

                        //  行索引
                        int rowIndex = (j % (this.x * this.y)) / this.x;

                        //  列索引
                        int colIndex = (j % (this.x * this.y)) % this.x;

                        //  插入数据
                        this._matrix.Set(colIndex, rowIndex, tableIndex, pl[j - vIndex].ToDouble());

                    }

                    vIndex += pl.Count;
                }
            }
            else
            {
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
                        int tableIndex = j / (this.x * this.y);

                        //  行索引
                        int rowIndex = (j % (this.x * this.y)) / this.x;

                        //  列索引
                        int colIndex = (j % (this.x * this.y)) % this.x;

                        //  插入数据
                        this._matrix.Set(colIndex, rowIndex, tableIndex, this.MatchFunc[j % 3](pl[j - vIndex].ToDouble()));

                    }

                    vIndex += pl.Count;
                }
            }


            //  清理内存中数据
            this.Lines.Clear();
        }

        string mmfPath;
        /// <summary> 构建表结构 </summary>
        protected virtual void BuildTableEntity(int tableCount, int xCount, int yCount, string mmf = null)
        {
            #region - 创建内存镜像文件 -

            MmfPath = mmf;

            if (string.IsNullOrEmpty(mmf))
            {
                //  创建内存镜像文件
                string mp = this.BaseFile.MmfDirPath;

                if (string.IsNullOrEmpty(this.BaseFile.MmfDirPath))
                {
                    return;
                }

                MmfPath = mp;

                mmf = Path.Combine(mp, KeyConfiger.tableMapCache, this.Name + Guid.NewGuid().ToString() + ".mp");
            }
            else
            {
                mmf = Path.Combine(mmf, KeyConfiger.tableMapCache, this.Name + Guid.NewGuid().ToString() + ".mp");
            }

            this.Matrix = new DxyzMmfEntity<double>(mmf, xCount, yCount, tableCount);

            #endregion

            this.BuildMapMatchFunc();

            //  ..End

            //  创建内存结构
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
                GridTable t = new GridTable(this, i);
                t.Parent = this;
                t.IndexNum = i;
                t.XCount = xCount;
                t.YCount = yCount;
                //t.Build(this.DefaultValue);
                this.tables.Add(t);
            }

            this.Matrix.SetAll(this.DefaultValue);
        }

        /// <summary> 修改相对坐标 </summary>
        void BuildMapMatchFunc()
        {
            if (this.BaseFile == null || this.BaseFile.Key == null) return;

            // Todo ：初始化设置是否使用相对坐标 
            GRIDUNIT mapunits = this.BaseFile.Key.Find<GRIDUNIT>();

            if (mapunits == null || !mapunits.IsUseMap) return;

            MAPAXES mapaxes = this.BaseFile.Key.Find<MAPAXES>();

            if (mapaxes == null) return;

            var tkAttr = this.GetType().GetCustomAttributes(typeof(TableKeyAttribute), true);

            if (tkAttr == null || tkAttr.Length == 0) return;

            TableKeyAttribute at = tkAttr.First() as TableKeyAttribute;

            if (at.KeyType != TableKeyType.GGO) return;

            MatchFunc = new Dictionary<int, Func<double, double>>();

            double xl = mapaxes.XLocation.ToDouble();
            double yl = mapaxes.YLocation.ToDouble();

            // Todo ：索引余数为0视为X 对X位置执行相对坐标 
            MatchFunc.Add(0, i => i + xl);
            // Todo ：索引余数为1视为Y 对Y位置执行相对坐标 
            MatchFunc.Add(1, i => yl - i);
            MatchFunc.Add(2, i => i);
        }

        int vIndex = 0;
        /// <summary> 当读取到新的一行信息向表格中添加 </summary>
        void ReadNewLine(string newLine)
        {
            //  过滤无效行
            if (!newLine.Trim().IsWorkLine())
            {
                this.Lines.Add(newLine); return;
            }

            List<string> pl = newLine.EclExtendToPetrelArray();

            if (this.MatchFunc == null)
            {
                // Todo ：不使用相对坐标 
                for (int j = vIndex; j < vIndex + pl.Count; j++)
                {
                    //  表格索引
                    int tableIndex = j / (this.x * this.y);

                    //  行索引
                    int rowIndex = (j % (this.x * this.y)) / this.x;

                    //  列索引
                    int colIndex = (j % (this.x * this.y)) % this.x;

                    //  插入数据
                    this._matrix.Set(colIndex, rowIndex, tableIndex, pl[j - vIndex].ToDouble());


                }
            }
            else
            {

                for (int j = vIndex; j < vIndex + pl.Count; j++)
                {
                    //  表格索引
                    int tableIndex = j / (this.x * this.y);

                    //  行索引
                    int rowIndex = (j % (this.x * this.y)) / this.x;

                    //  列索引
                    int colIndex = (j % (this.x * this.y)) % this.x;

                    //  插入数据
                    this._matrix.Set(colIndex, rowIndex, tableIndex, this.MatchFunc[j % 3](pl[j - vIndex].ToDouble()));


                }
            }

            vIndex += pl.Count;
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
            //int xt=this.BaseFile.TempRegion.XTo;
            //int yt=this.BaseFile.TempRegion.YTo;
            //int zt=this.BaseFile.TempRegion.ZTo;
            //this.Build(xt, yt, zt);

            writer.WriteLine();
            writer.WriteLine(this.Name);

            StringBuilder sb = new StringBuilder();

            foreach (GridTable v in tables)
            {
                for (int i = 0; i < v.YCount; i++)
                {
                    sb.Clear();

                    for (int j = 0; j < v.XCount; j++)
                    {
                        sb.Append(v.Get(i, j).ToString().ToD().PadLeft(KeyConfiger.TableLenght));

                        //  超出最大换行
                        if ((j + 1) % KeyConfiger.MaxColCount == 0)
                        {
                            //this.Lines.Add(sb.ToString());
                            writer.WriteLine(sb.ToString());
                            sb.Clear();
                        }
                    }

                    if (sb.Length > 0)
                    {
                        //this.Lines.Add(sb.ToString());
                        writer.WriteLine(sb.ToString());
                    }
                }
            }



            //foreach (GridTable v in tables)
            //{
            //    for (int i = 0; i < v.Matrix.Row; i++)
            //    {
            //        sb.Clear();

            //        for (int j = 0; j < v.Matrix.Col; j++)
            //        {
            //            sb.Append(v.Matrix[i, j].ToString().ToD().PadLeft(KeyConfiger.TableLenght));

            //            //  超出最大换行
            //            if ((j + 1) % KeyConfiger.MaxColCount == 0)
            //            {
            //                //this.Lines.Add(sb.ToString());
            //                writer.WriteLine(sb.ToString());
            //                sb.Clear();
            //            }
            //        }

            //        if (sb.Length > 0)
            //        {
            //            //this.Lines.Add(sb.ToString());
            //            writer.WriteLine(sb.ToString());
            //        }
            //    }
            //}

            writer.WriteLine(KeyConfiger.EndFlag);

            //base.WriteKey(writer);
        }

        /// <summary> 深复制成指定名称的关键字 isDisposeOld 是否清楚原有镜像文件 </summary>
        public TableKey TransToTableKeyByName(string name, bool isDisposeOld = false)
        {
            TableKey tb = KeyConfigerFactroy.Instance.CreateKey<TableKey>(name, this.BaseFile.SimKeyType) as TableKey;
            ////  复制行数据
            //this.Lines.ForEach(l => tb.Lines.Add(l));

            //tb.Build(this.z, this.x, this.y,  mmfPath);

            tb.BuildTableEntity(this.z, this.x, this.y, MmfPath);

            // Todo ：复制数据 
            //tb.RunModify();
            for (int ix = 0; ix < this.x; ix++)
            {
                for (int iy = 0; iy < this.y; iy++)
                {
                    for (int iz = 0; iz < this.z; iz++)
                    {
                        //  对值执行func操作
                        tb.Tables[iz].Set(iy, ix, this.Tables[iz].Get(iy, ix).ToString().ToDouble());
                    }
                }
            }

            if (isDisposeOld)
            {
                this.Close();

                this.Dispose();
            }

            return tb;
        }

        /// <summary> 深复制成指定名称的关键字 isDisposeOld 是否清楚原有镜像文件 </summary>
        public T ToTableKey<T>(SimKeyType simType = SimKeyType.EclipseAndSimON, bool isDisposeOld = true) where T : TableKey
        {
            T tb = KeyConfigerFactroy.Instance.CreateKey<T>(typeof(T).Name, simType) as T;

            tb.BuildTableEntity(this.z, this.x, this.y, MmfPath);

            // Todo ：复制数据 
            for (int ix = 0; ix < this.x; ix++)
            {
                for (int iy = 0; iy < this.y; iy++)
                {
                    for (int iz = 0; iz < this.z; iz++)
                    {
                        //  对值执行func操作
                        tb.Tables[iz].Set(iy, ix, this.Tables[iz].Get(iy, ix).ToString().ToDouble());
                    }
                }
            }

            if (isDisposeOld)
            {
                this.Close();

                this.Dispose();
            }

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
                            ////  对值执行func操作
                            //this.Tables[z].Matrix.Mat[y, x] = func(this.Tables[z].Matrix.Mat[y, x].ToString().ToDouble());


                            //  对值执行func操作
                            this.Tables[z].Set(y, x, func(this.Tables[z].Get(y, x).ToString().ToDouble()));
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
                            copyKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(copy.Value, this.BaseFile.SimKeyType) as TableKey;

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
                            copyKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(copy.Value, this.BaseFile.SimKeyType) as TableKey;

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
                foreach (var v in ms)
                {
                    if (v.ObsoverModel.Count == 0)
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

        /// <summary> 转换成double数组 </summary>
        public double[] ToDoubleList()
        {
            List<double> temp = new List<double>();

            foreach (GridTable v in tables)
            {
                for (int i = 0; i < v.XCount; i++)
                {
                    for (int j = 0; j < v.YCount; j++)
                    {

                        temp.Add(v.Get(j, i));
                    }
                }

                //for (int i = 0; i < v.Matrix.Row; i++)
                //{
                //    for (int j = 0; j < v.Matrix.Col; j++)
                //    {
                //        //v.Matrix[i, j];

                //        temp.Add(v.Matrix[i, j]);
                //    }
                //}  
            }

            return temp.ToArray();
        }


        /// <summary> 转换成double数组 Y先排序 </summary>
        public double[] ToDoubleListFx()
        {
            List<double> temp = new List<double>();

            foreach (GridTable v in tables)
            {
                for (int i = 0; i < v.YCount; i++)
                {
                    for (int j = 0; j < v.XCount; j++)
                    {

                        temp.Add(v.Get(i, j));
                    }
                }

                //for (int i = 0; i < v.Matrix.Row; i++)
                //{
                //    for (int j = 0; j < v.Matrix.Col; j++)
                //    {
                //        //v.Matrix[i, j];

                //        temp.Add(v.Matrix[i, j]);
                //    }
                //}
            }

            return temp.ToArray();
        }

        private DxyzMmfEntity<double> _matrix;
        /// <summary> 内存镜像文件 </summary>
        public DxyzMmfEntity<double> Matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }

        public string MmfPath
        {
            get
            {
                return mmfPath;
            }

            set
            {
                mmfPath = value;
            }
        }

        /// <summary> 转换成指定关键字类型 </summary>
        public T ConvertTo<T>() where T : TableKey
        {
            T tnew = default(T);

            var atts = typeof(T).GetCustomAttributes(typeof(KeyAttribute), false);

            if (atts != null && atts.Length > 0)
            {
                KeyAttribute at = atts[0] as KeyAttribute;
                tnew = KeyConfigerFactroy.Instance.CreateKey<T>(typeof(T).Name, at.SimKeyType) as T;
            }
            else
            {
                tnew = KeyConfigerFactroy.Instance.CreateKey<T>(typeof(T).Name) as T;
            }

            // Todo ：传递内存镜像引用 
            tnew.Tables = this.Tables;
            tnew.X = this.X;
            tnew.Y = this.Y;
            tnew.Z = this.Z;

            return tnew;
        }

        /// <summary> 释放资源 删除内存镜像文件</summary>
        protected override void Dispose(bool disposing)
        {
            if (!base._isDisposed)
            {
                if (this._matrix != null)
                {
                    this._matrix.Dispose();
                }

                this._isDisposed = true;
            }
        }

        /// <summary> 释放内存资源 不删除内存镜像文件 </summary>
        public void Close()
        {
            if (this._matrix != null)
            {
                this._matrix.Close();
            }
        }

        /// <summary> 更换镜像文件路径 </summary>
        public void ChangePath(string path,bool isCopy)
        {
            if (this._matrix == null) return;
            this._matrix.ChangePath(path, isCopy);
        }


        /// <summary> 获取不重复数据 </summary>
        public List<double> DistinctValue()
        {
            List<double> l = new List<double>();

            double temp;
            this._matrix.Count.DoCountWhile(k =>
                {
                    // Todo ：遍历所有数据找出不重复的数据 
                    temp = this._matrix.GetIndex(k);

                    if (!l.Contains(temp))
                    {
                        l.Add(temp);
                    }
                }
           );

            return l;
        }


    }

    /// <summary> 表格数据结构 </summary>
    public class GridTable
    {
        //DxyMmfEntity<double> _matrix = null;
        ///// <summary> 硬盘内存镜像 </summary>
        //public DxyMmfEntity<double> Matrix
        //{
        //    get { return _matrix; }
        //    set { _matrix = value; }
        //}
        public GridTable(TableKey parent, int indexNum)
        {
            _parent = parent;

            _indexNum = indexNum;

            //string dir = Path.GetDirectoryName(parent.BaseFile.FilePath);

            //string filepath = Path.Combine(dir, parent.Name + "-" + Guid.NewGuid().ToString() + ".mmf"); ;

            //_matrix = new DxyMmfEntity<double>(filepath, parent.X * parent.Y * parent.Z);
        }

        public double this[int inx, int iny]
        {
            get { return this.Get(inx, iny); }
            set { this.Set(inx, iny, value); }
        }

        private int _indexNum;
        /// <summary> 层号 </summary>
        public int IndexNum
        {
            get { return _indexNum; }
            set { _indexNum = value; }
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

        TableKey _parent = null;
        /// <summary> 父表格关键字 </summary>
        public TableKey Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }


        /// <summary> 设置值 </summary>
        public void Set(int x, int y, double value)
        {
            //this._matrix.Set(x, y, value);

            this.Parent.Matrix.Set(y, x, this._indexNum - 1, value);
        }

        public double Get(int x, int y)
        {
            //return this._matrix.Get(x, y);

            return this.Parent.Matrix.Get(y, x, _indexNum - 1);
        }


        ///// <summary> 创建表格数据 </summary>
        //public void Build(double defaultValue = 0)
        //{
        //    _matrix.ReSetSize(yCount * xCount);

        //    _matrix.SetAll(defaultValue);

        //}

        /// <summary> 转换成DataTable数据结构 </summary>
        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();

            for (int x = 1; x <= XCount; x++)
            {
                dt.Columns.Add(x.ToString());
            }
            for (int y = 0; y < YCount; y++)
            {
                DataRow dr = dt.NewRow();

                for (int index = 0; index < XCount; index++)
                {
                    dr[index] = this.Get(y, index);
                }
                dt.Rows.Add(dr);

            }

            //for (int x = 1; x <= _matrix.Col; x++)
            //{
            //    dt.Columns.Add(x.ToString());
            //}
            //for (int y = 0; y < _matrix.Row; y++)
            //{
            //    DataRow dr = dt.NewRow();
            //    for (int index = 0; index < _matrix.Col; index++)
            //    {
            //        dr[index] = _matrix.Mat[y, index];
            //    }
            //    dt.Rows.Add(dr);

            //}

            return dt;
        }

        /// <summary> 复制表格格式 </summary>
        [Obsolete("修改废弃")]
        public GridTable Clone()
        {
            GridTable grid = new GridTable(this._parent, this._indexNum);
            grid.xCount = this.xCount;
            grid.yCount = this.yCount;
            grid._indexNum = this._indexNum;
            //grid.Matrix = this._matrix.Clone();
            grid._parent = this._parent;
            return grid;
        }

    }

    //public class GridTable
    //{
    //    Matrix _matrix = new Matrix();
    //    /// <summary> 矩阵数据 </summary>
    //    public Matrix Matrix
    //    {
    //        get { return _matrix; }
    //        set { _matrix = value; }
    //    }
    //    public GridTable()
    //    {

    //    }
    //    private int indexNum;
    //    /// <summary> 层号 </summary>
    //    public int IndexNum
    //    {
    //        get { return indexNum; }
    //        set { indexNum = value; }
    //    }

    //    int xCount;
    //    /// <summary> ｘ方向 </summary>
    //    public int XCount
    //    {
    //        get { return xCount; }
    //        set { xCount = value; }
    //    }

    //    int yCount;
    //    /// <summary> ｙ方向 </summary>
    //    public int YCount
    //    {
    //        get { return yCount; }
    //        set { yCount = value; }
    //    }

    //    TableKey parent = null;
    //    /// <summary> 父表格关键字 </summary>
    //    public TableKey Parent
    //    {
    //        get { return parent; }
    //        set { parent = value; }
    //    }

    //    /// <summary> 创建表格数据 </summary>
    //    public void Build(double defaultValue = 0)
    //    {

    //        _matrix.ReSetRowAndCol(yCount, xCount);


    //        for (int r = 0; r < _matrix.Row; r++)
    //        {
    //            for (int c = 0; c < _matrix.Col; c++)
    //            {
    //                _matrix.Mat[r, c] = defaultValue;
    //            }
    //        }

    //    }

    //    /// <summary> 转换成DataTable数据结构 </summary>
    //    public DataTable ToDataTable()
    //    {
    //        DataTable dt = new DataTable();

    //        for (int x = 1; x <= _matrix.Col; x++)
    //        {
    //            dt.Columns.Add(x.ToString());
    //        }
    //        for (int y = 0; y < _matrix.Row; y++)
    //        {
    //            DataRow dr = dt.NewRow();
    //            for (int index = 0; index < _matrix.Col; index++)
    //            {
    //                dr[index] = _matrix.Mat[y, index];
    //            }
    //            dt.Rows.Add(dr);

    //        }

    //        return dt;
    //    }

    //    /// <summary> 复制表格数据 </summary>
    //    public GridTable Clone()
    //    {
    //        GridTable grid = new GridTable();
    //        grid.xCount = this.xCount;
    //        grid.yCount = this.yCount;
    //        grid.indexNum = this.indexNum;
    //        grid.Matrix = this._matrix.Clone();
    //        grid.parent = this.parent;
    //        return grid;
    //    }

    //}


    /// <summary> 加载表格数据方式 </summary>
    enum ReadTableKeyMode
    {
        /// <summary> 读取一行信息实时加载表格 </summary>
        ReadRightNow = 0,
        /// <summary> 加载表格数据在加载完Lines后 </summary>
        ReadFromLines,
        /// <summary> 加载表格数据在手动调用Build后 </summary>
        ReadOnBuild
    }


    /// <summary> 表格数据的特性，用来区分地层和属性关键字(目前修改为标识是否计算相对坐标) </summary>
    public class TableKeyAttribute : Attribute
    {
        private TableKeyType _keyType;

        public TableKeyType KeyType
        {
            get { return _keyType; }
            set { _keyType = value; }
        }
    }


    /// <summary> 表格关键字类型 </summary>
    public enum TableKeyType
    {

        /// <summary> 地层 </summary>
        GGO = 0,

        /// <summary> 属性 </summary>
        GPRO
    }
}
