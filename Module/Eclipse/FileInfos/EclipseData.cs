#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/27 13:24:46
 * 说明：使用方法 = 传递ECLIPSE主文件名，将关键字读写到树形类型内存中操作
 *       调用Save()方法存储文件
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
using HeBianGu.Product.SimalorManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace HeBianGu.Product.SimalorManager.Eclipse.FileInfos
{
    /// <summary> ECLIPSE主文件操作类 </summary>
    public class EclipseData : BaseFile
    {
        public EclipseData(): base()
        {
            InitConstruct();
        }


        /// <summary> P1=主文件地址 P2=内存镜像文件地址 </summary>
        public EclipseData(string _filePath, string mmfDirPath = null)
            : base(_filePath, mmfDirPath)
        {

        }


        /// <summary> P1=主文件地址 P2=遇到没解析关键字触发的事件 P3=内存镜像文件地址 </summary>
        public EclipseData(string _filePath, WhenUnkownKey UnkownEvent, string mmfDirPath = null)
            : base(_filePath, UnkownEvent, l => true, mmfDirPath)
        {

        }

        /// <summary> P1=主文件地址 P2=遇到没解析关键字触发的事件 P3=是否读取匹配的INCLUDE文件 P4=内存镜像文件地址 </summary>
        public EclipseData(string _filePath, WhenUnkownKey UnkownEvent, Predicate<INCLUDE> isReadInclud, string mmfDirPath = null)
            : base(_filePath, UnkownEvent, isReadInclud, mmfDirPath)
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
            this.Key.Add(summary);

            SCHEDULE schedule = new SCHEDULE("SCHEDULE");
            this.Key.Add(schedule);
        }

        /// <summary> 清理父节点 </summary>
        public void ClearParentKey()
        {
            var fs = this.Key.FindAll<ParentKey>();

            if (fs != null)
            {
                fs.ForEach(l => l.Clear());
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
                //using (StreamReader streamRead = new StreamReader(fileStream, System.Text.Encoding.GetEncoding("GB2312")))
                using (StreamReader streamRead = new StreamReader(fileStream, System.Text.Encoding.Default))
                {
                    while (!streamRead.EndOfStream)
                    {
                        BaseKey endKey = this.Key.ReadKeyLine(streamRead);

                        if (endKey.BuilderHandler != null)
                        {
                            //  读到最后触发一次创建方法
                            endKey.BuilderHandler.Invoke(endKey, endKey);
                        }
                    }
                }
            }
        }

        /// <summary> 保存 </summary>
        public override void Save()
        {
            this.SaveAs(this.FilePath);
        }

        /// <summary> 写入文件 文件全路径 </summary>
        public override void SaveAsExtend(string pathName)
        {
            using (FileStream fileStream = new FileStream(pathName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWrite = new StreamWriter(fileStream, System.Text.Encoding.Default))
                {
                    streamWrite.WriteLine(this.FielDetail);

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
}
