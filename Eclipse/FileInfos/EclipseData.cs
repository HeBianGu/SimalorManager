#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/27 13:24:46
 * 文件名：EclipseData
 * 说明：使用方法 = 传递ECLIPSE主文件名，将关键字读写到树形类型内存中操作
 *       调用Save()方法存储文件
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Parent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace OPT.Product.SimalorManager.Eclipse.FileInfos
{
    /// <summary> ECLIPSE主文件操作类 </summary>
    public class EclipseData : BaseFile
    {
        public EclipseData()
        {
            InitConstruct();
        }

        public EclipseData(string _filePath)
            : base(_filePath)
        {

        }

        public EclipseData(string _filePath, WhenUnkownKey UnkownEvent, bool isReadInclud = true)
            : base(_filePath, UnkownEvent, isReadInclud)
        {

        }

        void InitConstruct()
        {
            RUNSPEC runspec = new RUNSPEC("RUNSPEC");
            this.Key.Add(runspec);
            GRID grid = new GRID("GRID");
            this.Key.Add(grid);
            EDIT edit = new EDIT("EDIT");
            this.Key.Add(edit);
            PROPS props = new PROPS("PROPS");
            this.Key.Add(props);
            REGIONS regions = new REGIONS("REGIONS");
            this.Key.Add(regions);
            SOLUTION solution = new SOLUTION("SOLUTION");
            this.Key.Add(solution);
            SUMMARY summary = new SUMMARY("SUMMARY");
            //summary.Lines.Add(this.outControlStr);
            this.Key.Add(summary);
            SCHEDULE schedule = new SCHEDULE("SCHEDULE");
            //schedule.Lines.Add(this.calculateControlStrOne);
            //schedule.Lines.Add(this.calculateControlStrTwo);
            this.Key.Add(schedule);
        }


        /// <summary> 清理父节点 </summary>
        public void ClearParentKey()
        {
            var fs = this.Key.FindAll<ParentKey>();
            if (fs != null)
            {
                fs.ForEach(l =>
                    {
                        //if (l is SUMMARY)
                        //{

                        //}
                        //else
                        //{
                        l.Clear();
                        //}
                    });
            }
        }

        /// <summary> 清理父节点 </summary>
        public void InitParentKey()
        {

            RUNSPEC runspec = this.Key.CreateSingle<RUNSPEC>("RUNSPEC");

            GRID grid = this.Key.CreateSingle<GRID>("GRID");
            if (grid != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_grid.inc";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                grid.Add(include);

                INCLUDE include1 = new INCLUDE("INCLUDE");
                include1.FileName = this.FileName.GetFileNameWithoutExtension() + "_faults.inc";
                include1.FilePath = this.FilePath.GetDirectoryName() + "\\" + include1.FileName;
                grid.Add(include1);

                INCLUDE include2 = new INCLUDE("INCLUDE");
                include2.FileName = this.FileName.GetFileNameWithoutExtension() + "_aquifer.inc";
                include2.FilePath = this.FilePath.GetDirectoryName() + "\\" + include1.FileName;
                grid.Add(include2);
            }

            EDIT edit = this.Key.Find<EDIT>();
            if (edit != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_edit.inc";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                edit.Add(include);
            }



            PROPS props = this.Key.CreateSingle<PROPS>("PROPS");

            if (props != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_pvt.inc";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                props.Add(include);

                include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_rp.inc";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                props.Add(include);
            }


            SOLUTION solution = this.Key.CreateSingle<SOLUTION>("SOLUTION");

            if (solution != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_init.inc";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                solution.Add(include);
            }



            REGIONS region = this.Key.CreateSingle<REGIONS>("REGIONS");
            if (region != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_regs.inc";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                region.Add(include);
            }

            SUMMARY summary = this.Key.CreateSingle<SUMMARY>("SUMMARY");
            if (summary != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_sum.inc";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                summary.Add(include);

                //if (include.Keys.Count == 0)
                //{
                //    ALL all = new ALL("ALL");
                //    include.Add(all);
                //}
            }

            SCHEDULE schedule = this.Key.CreateSingle<SCHEDULE>("SCHEDULE");

            if (schedule != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_sch.inc";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                schedule.Add(include);
            }


            END end = this.Key.CreateSingle<END>("END");

        }

        /// <summary> 初始化类(树形结构) </summary>
        protected override void InitializeComponent()
        {
            string strTemp = string.Empty;
            //  打开子文件并读取子文件关键字内容
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamRead = new StreamReader(fileStream, Encoding.Default))
                {
                    while (!streamRead.EndOfStream)
                    {
                        strTemp = streamRead.ReadLine().TrimEnd();

                        //  注释内容 以 -- 开头
                        //if (strTemp.StartsWith(base.ExceptString))
                        //{
                        //    this.Lines.Add(strTemp);
                        //}

                        bool isRegister = KeyConfigerFactroy.Instance.IsParentRegisterKey(strTemp);

                        if (isRegister)
                        {
                            //  注册的关键字
                            ParentKey pKey = KeyConfigerFactroy.Instance.CreateParentKey<ParentKey>(strTemp);
                            pKey.BaseFile = this;
                            this.Key.Keys.Add(pKey);
                            pKey.ParentKey = this.Key;
                            pKey.ReadKeyLine(streamRead);
                        }
                        else
                        {
                            isRegister = KeyConfigerFactroy.Instance.IsParentRegisterKey(strTemp);

                            if (strTemp.IsKeyFormat())
                            {
                                //  添加普通关键字
                                Key normalKey = new Key(KeyChecker.FormatKey(strTemp));
                                normalKey.BaseFile = this;
                                normalKey.ParentKey = this.Key;
                                this.Lines.Add(strTemp);
                                normalKey.ReadKeyLine(streamRead);
                            }
                            else
                            {
                                this.Lines.Add(strTemp);
                            }

                        }
                    }
                }
            }

            //  执行参数修改
            //this.RunModify();
        }

        /// <summary> 保存 </summary>
        public override void Save()
        {
            this.SaveAs(this.FilePath);
        }

        /// <summary> 写入文件 文件全路径 </summary>
        public override void SaveAs(string pathName)
        {
            using (FileStream fileStream = new FileStream(pathName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWrite = new StreamWriter(fileStream, Encoding.Default))
                {
                    //  读写备注
                    foreach (var str in Lines)
                    {
                        streamWrite.WriteLine(str);
                    }

                    //  写关键字
                    this.Key.Keys.ForEach(l => l.WriteKey(streamWrite));

                }
            }
        }

        /// <summary> 递归写关键字 </summary>
        [Obsolete("已过时")]
        void SaveKey(StreamWriter streamWrite, BaseKey pKey)
        {
            if (pKey.Keys.Count > 0)
            {
                streamWrite.WriteLine();
                streamWrite.WriteLine(pKey.Name);
                for (int j = 0; j < pKey.Keys.Count; j++)
                {
                    //BaseKey cKey = pKey.Keys[j];
                    ////  递归处
                    //SaveKey(streamWrite, cKey);
                }
            }
            else
            {
                streamWrite.WriteLine();
                streamWrite.WriteLine(pKey.Name);
                pKey.Lines.ForEach(l => streamWrite.WriteLine(l));
                streamWrite.WriteLine();
            }
        }

        /// <summary> 格式化文件 </summary>
        public void Format()
        {
            RUNSPEC runspec = this.Key.CreateSingle<RUNSPEC>("RUNSPEC");

            REGIONS region = this.Key.CreateSingle<REGIONS>("REGIONS");
            if (region != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_REG.INC";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                region.Add(include);
            }

            GRID grid = this.Key.CreateSingle<GRID>("GRID");

            var includesOld = grid.FindAll<INCLUDE>();

            if (grid != null)
            {
                INCLUDE include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_GOPP.INC";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                grid.Add(include);

                include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_GGO.INC";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                grid.Add(include);

                #region - ggo -
                var echo = this.Key.FindAll<ECHO>();
                if (echo != null)
                {
                    grid.DeleteAll<ECHO>();
                    include.AddRange(echo);
                }

                var mapaxes = this.Key.FindAll<MAPAXES>();
                if (mapaxes != null)
                {
                    grid.DeleteAll<MAPAXES>();
                    include.AddRange(mapaxes);
                }


                var gridunit = this.Key.FindAll<GRIDUNIT>();
                if (gridunit != null)
                {
                    grid.DeleteAll<GRIDUNIT>();
                    include.AddRange(gridunit);
                }

                var coordsys = this.Key.FindAll<COORDSYS>();
                if (coordsys != null)
                {
                    grid.DeleteAll<COORDSYS>();
                    include.AddRange(coordsys);
                }

                var mapunits = this.Key.FindAll<MAPUNITS>();
                if (mapunits != null)
                {
                    grid.DeleteAll<MAPUNITS>();
                    include.AddRange(mapunits);
                }


                var noecho = this.Key.FindAll<NOECHO>();
                if (noecho != null)
                {
                    grid.DeleteAll<NOECHO>();
                    include.AddRange(noecho);
                }



                var coord = this.Key.FindAll<COORD>();
                if (coord != null)
                {
                    grid.DeleteAll<COORD>();
                    include.AddRange(coord);
                }



                var zcorn = this.Key.FindAll<ZCORN>();
                if (zcorn != null)
                {
                    grid.DeleteAll<ZCORN>();
                    include.AddRange(zcorn);
                }
                //  清空原有INCLUDE
                foreach (var v in includesOld)
                {
                    grid.Delete(v);
                }
                #endregion

                include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_GPRO.INC";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                grid.Add(include);

                include = new INCLUDE("INCLUDE");
                include.FileName = this.FileName.GetFileNameWithoutExtension() + "_GOTH.INC";
                include.FilePath = this.FilePath.GetDirectoryName() + "\\" + include.FileName;
                grid.Add(include);

                ECHO echo1 = grid.Find<ECHO>();
                if (echo1 != null)
                {
                    grid.DeleteAll<ECHO>();
                    include.Add(echo1);
                }

                List<FAULTS> faults = grid.FindAll<FAULTS>();
                if (faults != null)
                {
                    grid.DeleteAll<FAULTS>();
                    foreach (var v in faults)
                    {
                        include.Add(v);
                    }

                }

                MULTFLT multflt = grid.Find<MULTFLT>();
                if (multflt != null)
                {
                    grid.DeleteAll<MULTFLT>();
                    include.Add(multflt);
                }

            }


            END end = this.Key.CreateSingle<END>("END");
        }
    }


    public static class EclipseDataExtention
    {

        /// <summary> 对文件执行修改关键字修改 跟DataImportEcl对接方法 </summary>
        public static void RunModify(this EclipseData ecl)
        {
            //  查找所有修改关键字
            List<ModifyKey> modify = ecl.Key.FindAll<ModifyKey>();

            DIMENS d = ecl.Key.Find<DIMENS>();

            if (d == null) return;

            //  构造全网格范围
            RegionParam tempRegion = new RegionParam();
            tempRegion.XFrom = 1;
            tempRegion.XTo = d.X;
            tempRegion.YFrom = 1;
            tempRegion.YTo = d.Y;
            tempRegion.ZFrom = 1;
            tempRegion.ZTo = d.Z;

            foreach (ModifyKey m in modify)
            {
                ParentKey p = m.GetParentKey();

                if (p != null && p.Name == "EDIT")
                {
                    continue;
                }

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


                    TableKey funKey = ecl.Key.Find<TableKey>(l => l.Name == md.KeyName);

                    if (funKey == null)
                    {
                        //  没有则创建关键字
                        funKey = KeyConfigerFactroy.Instance.CreateChildKey<TableKey>(md.KeyName);

                        m.ParentKey.Add(funKey);

                    }

                    funKey.Build(d.Z, d.X, d.Y);

                    if (md is ModifyApplyModel)
                    {
                        ModifyApplyModel app = md as ModifyApplyModel;

                        app.RunModify(funKey);
                    }
                    else if (md is ModifyCopyModel)
                    {
                        ModifyCopyModel copy = md as ModifyCopyModel;

                        TableKey copyKey = ecl.Key.Find<TableKey>(l => l.Name == copy.Value);

                        if (copyKey == null)
                        {
                            //  没有则创建关键字
                            copyKey = KeyConfigerFactroy.Instance.CreateChildKey<TableKey>(copy.Value);

                            m.ParentKey.Add(copyKey);

                        }

                        copyKey.Build(d.Z, d.X, d.Y);

                        copy.RunModify(copyKey, funKey);

                    }
                    else if (md is ModifyBoxModel)
                    {
                        ModifyBoxModel app = md as ModifyBoxModel;

                        app.RunModify(funKey);
                    }
                }
            }
        }

        /// <summary> 获取指定分组的修正关键字 </summary>
        public static List<ModifyKey> FilterByGroup(this EclKeyType group, List<ModifyKey> modifys)
        {
            List<string> findKeys = group.GetGroupKeyNames();

            if (modifys == null) return null;

            //  默认取第一个修改参数做判断
            return modifys.FindAll(l => l.ObsoverModel.Count > 0).FindAll(l => findKeys.Contains(l.ObsoverModel[0].KeyName));
        }

        /// <summary> 获取指定分区的所有关键字 </summary>
        public static List<string> GetGroupKeyNames(this EclKeyType group)
        {

            if (group == EclKeyType.Grid)
            {
                return KeyConfigerFactroy.GridPartConfiger;

            }
            else if (group == EclKeyType.Props)
            {
                return KeyConfigerFactroy.PropsPartConfiger;
            }

            else if (group == EclKeyType.Solution)
            {
                return KeyConfigerFactroy.SolutionPartConfiger;
            }

            else if (group == EclKeyType.Regions)
            {
                return KeyConfigerFactroy.RegionsPartConfiger;
            }

            return null;
        }

    }

}
