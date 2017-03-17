#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 9:25:19
 * 文件名：EclipseExtend
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using OPT.Product.SimalorManager.RegisterKeys.SimON;
using OPT.Product.SimalorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPT.Product.SimalorManager.Base.AttributeEx;
using System.Threading;
using System.IO;

namespace OPT.Product.SimalorManager
{
    /// <summary> Eclipse文件服务 </summary>
    public static class EclipseDataService
    {
        /// <summary> 增加重启时间 </summary> 
        public static DATES AddSchDates(this EclipseData eclData, DateTime pTime)
        {

            SCHEDULE schDate = eclData.Key.Find<SCHEDULE>();

            START startDate = eclData.Key.Find<START>();

            DateTime nowTime = startDate.StartTime;

            return schDate.AddSchDates(nowTime, pTime);

        }

        /// <summary> 增加重启时间 </summary> 
        public static DATES AddSchDates(this SCHEDULE schDate, DateTime startTime, DateTime pTime)
        {

            BaseKey findKey = null;

            DateTime nowTime = startTime;
            if (nowTime > pTime)
            {
                throw new ArgumentException("插入的时间不能小于案例的起始时间！");
            }

            schDate.Foreach(
                l =>
                {
                    if (l is DATES)
                    {
                        DATES date = l as DATES;
                        nowTime = date.DateTime;

                        //  记录比当前时间小的
                        if (nowTime < pTime)
                        {
                            findKey = l;
                        }
                    }
                    //else if (l is TSTEP)
                    //{
                    //    TSTEP step = l as TSTEP;
                    //    int dayCount = step.DataCount;
                    //    nowTime.AddDays(dayCount);

                    //    //  记录比当前时间小的
                    //    if (nowTime <= pTime)
                    //    {
                    //        findKey = l;
                    //    }
                    //}
                }
                );

            DATES insertDate = new DATES("DATES");
            insertDate.SetDateTime(pTime);

            //  没有找到 = 插入END前面
            if (findKey == null)
            {
                END endKey = schDate.Find<END>();
                schDate.InsertBefore(endKey, insertDate);
            }

            //  找到了 = 插入指定关键字前面
            else
            {
                schDate.InsertAfter(findKey, insertDate);
            }


            return insertDate;


        }

        /// <summary> 查找指定重启时间 </summary>
        public static DATES FindDates(this EclipseData eclData, DateTime pTime)
        {
            List<DATES> Dates = eclData.Key.FindAll<DATES>();

            DATES findDate = Dates.Find(l => l.DateTime.Date==pTime.Date);

            return findDate;
        }

        /// <summary> 获取所有井名 </summary>
        public static List<string> GetAllWell(this EclipseData eclData)
        {
            SCHEDULE sch = eclData.Key.Find<SCHEDULE>();
            List<WELSPECS> ws = sch.FindAll<WELSPECS>();

            List<string> strs = new List<string>();

            ws.ForEach(l =>
                {
                    l.Items.ForEach(k =>
                        {
                            strs.Add(k.jm0);
                        });

                });

            return strs.Distinct().ToList();
        }

        /// <summary> 快速获取所有井名(只读取schedule部分的Include关键字) </summary>
        public static List<string> GetAllWell(string dataFile)
        {
            List<string> strs = new List<string>();

            EclipseData eclData = null;

            Thread thread = new Thread(() => eclData = new EclipseData(dataFile, null, l => l.IsMatchParent<SCHEDULE>()), 4194304);// 4mb栈

            thread.Start();

            while (true)
            {
                if (thread.ThreadState == ThreadState.Stopped)
                {
                    break;
                }
            }

            SCHEDULE sch = eclData.Key.Find<SCHEDULE>();
            List<WELSPECS> ws = sch.FindAll<WELSPECS>();

            ws.ForEach(l =>
            {
                l.Items.ForEach(k =>
                {
                    strs.Add(k.jm0);
                });

            });

            eclData.Dispose();

            return strs.Distinct().ToList();
        }

        /// <summary> 获取单位制 </summary>
        public static UnitType GetUnitType(string dataFile)
        {
            //  不读取INCLUDE部分数据
            EclipseData data = new EclipseData(dataFile, null, l => false);

            //var incs = data.Key.FindAll<INCLUDE>();

            ////  设置所有INCLUDE都不生成文件
            //incs.ForEach(l => l.IsCreateFile = false);

            // Todo ：释放表格缓存文件
            data.Key.SetAllMmfDispose();


            //  读到METRIC英制单位
            METRIC metric = data.Key.Find<METRIC>();

            if (metric != null)
            {
                return UnitType.METRIC;
            }

            //  单位类型
            FIELD field = data.Key.Find<FIELD>();

            if (field != null)
            {
                return UnitType.FIELD;
            }

            data.Dispose();

            return UnitType.METRIC;
        }

        /// <summary> 获取所有井组名 </summary>
        public static List<string> GetAllWellGroup(this EclipseData eclData)
        {

            SCHEDULE sch = eclData.Key.Find<SCHEDULE>();

            List<GRUPTREE> ws = sch.FindAll<GRUPTREE>();

            List<string> strs = new List<string>();

            ws.ForEach(l =>
            {
                l.Items.ForEach(k =>
                {
                    strs.Add(k.zjzm0);
                    strs.Add(k.fjzm1);
                });

            });

            return strs.Distinct().ToList();
        }

        /// <summary> 获取所有井名 </summary>
        public static List<WELSPECS> GetAllWellModel(this EclipseData eclData)
        {
            SCHEDULE sch = eclData.Key.Find<SCHEDULE>();
            List<WELSPECS> ws = sch.FindAll<WELSPECS>();

            return ws;
        }

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
                //else
                //{
                //    //  不是空赋值临时范围
                //    tempRegion = m.DefautRegion;
                //}

                foreach (IModifyModel md in m.ObsoverModel)
                {

                    //  是空则用临时范围
                    if (md.Region == null)
                    {
                        md.Region = m.DefautRegion;
                    }
                    else
                    {
                        //  不是空赋值临时范围
                        m.DefautRegion = md.Region;
                    }


                    if (md is ModifyApplyModel)
                    {
                        TableKey funKey = ecl.Key.Find<TableKey>(l => l.Name == md.KeyName);

                        if (funKey == null)
                        {
                            //  没有则创建关键字
                            funKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(md.KeyName) as TableKey;

                            m.ParentKey.Add(funKey);

                        }

                        funKey.Build(d.Z, d.X, d.Y);

                        ModifyApplyModel app = md as ModifyApplyModel;

                        app.RunModify(funKey);
                    }
                    else if (md is ModifyCopyModel)
                    {
                        ModifyCopyModel copy = md as ModifyCopyModel;

                        TableKey copyKey = ecl.Key.Find<TableKey>(l => l.Name == copy.Key);

                        if (copyKey == null)
                        {
                            //  没有则创建关键字
                            copyKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(copy.Key, ecl.SimKeyType) as TableKey;

                            m.ParentKey.Add(copyKey);

                        }

                        TableKey funKey = ecl.Key.Find<TableKey>(l => l.Name == copy.Value);

                        if (funKey == null)
                        {
                            //  没有则创建关键字
                            funKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(copy.Value) as TableKey;

                            m.ParentKey.Add(funKey);

                        }

                        funKey.Build(d.Z, d.X, d.Y);

                        copyKey.Build(d.Z, d.X, d.Y);

                        copy.RunModify(copyKey, funKey);

                    }
                    else if (md is ModifyBoxModel)
                    {
                        TableKey funKey = ecl.Key.Find<TableKey>(l => l.Name == md.KeyName);

                        if (funKey == null)
                        {
                            //  没有则创建关键字
                            funKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(md.KeyName) as TableKey;

                            m.ParentKey.Add(funKey);

                        }

                        funKey.Build(d.Z, d.X, d.Y);

                        ModifyBoxModel app = md as ModifyBoxModel;

                        app.RunModify(funKey);
                    }
                }
            }
        }

        /// <summary> 对文件执行修改关键字修改 跟DataImportEcl对接方法 </summary>
        public static void RunModify_bak(this EclipseData ecl)
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




                    if (md is ModifyApplyModel)
                    {
                        TableKey funKey = ecl.Key.Find<TableKey>(l => l.Name == md.KeyName);

                        if (funKey == null)
                        {
                            //  没有则创建关键字
                            funKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(md.KeyName) as TableKey;

                            m.ParentKey.Add(funKey);

                        }

                        funKey.Build(d.Z, d.X, d.Y);

                        ModifyApplyModel app = md as ModifyApplyModel;

                        app.RunModify(funKey);
                    }
                    else if (md is ModifyCopyModel)
                    {
                        ModifyCopyModel copy = md as ModifyCopyModel;

                        TableKey copyKey = ecl.Key.Find<TableKey>(l => l.Name == copy.Key);

                        if (copyKey == null)
                        {
                            //  没有则创建关键字
                            copyKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(copy.Key, ecl.SimKeyType) as TableKey;

                            m.ParentKey.Add(copyKey);

                        }

                        TableKey funKey = ecl.Key.Find<TableKey>(l => l.Name == copy.Value);

                        if (funKey == null)
                        {
                            //  没有则创建关键字
                            funKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(copy.Value) as TableKey;

                            m.ParentKey.Add(funKey);

                        }

                        funKey.Build(d.Z, d.X, d.Y);

                        copyKey.Build(d.Z, d.X, d.Y);

                        copy.RunModify(copyKey, funKey);

                    }
                    else if (md is ModifyBoxModel)
                    {
                        TableKey funKey = ecl.Key.Find<TableKey>(l => l.Name == md.KeyName);

                        if (funKey == null)
                        {
                            //  没有则创建关键字
                            funKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(md.KeyName) as TableKey;

                            m.ParentKey.Add(funKey);

                        }

                        funKey.Build(d.Z, d.X, d.Y);

                        ModifyBoxModel app = md as ModifyBoxModel;

                        app.RunModify(funKey);
                    }
                }
            }
        }

        /// <summary> 对文件格式化 </summary>
        public static void Standardized(this EclipseData ecl)
        {
            var ps = ecl.Key.FindAll<ParentKey>();

            ps.ForEach(l => l.Standardized());
        }

        /// <summary> 获取指定分组的修正关键字 </summary>
        public static List<ModifyKey> FilterByGroup(this EclKeyType group, List<ModifyKey> modifys)
        {
            List<string> findKeys = group.GetGroupKeyNames();

            if (modifys == null) return null;

            //  默认取第一个修改参数做判断
            return modifys.FindAll(l => l.ObsoverModel.Count > 0).FindAll(l => findKeys.Contains(l.ObsoverModel[0].KeyName));
        }

        /// <summary> 获取指定分组的修正关键字 </summary>
        public static List<OPT.Product.SimalorManager.RegisterKeys.SimON.BOX> FilterByGroup(this EclKeyType group, List<OPT.Product.SimalorManager.RegisterKeys.SimON.BOX> modifys)
        {
            List<string> findKeys = group.GetGroupKeyNames();

            if (modifys == null) return null;

            //  默认取第一个修改参数做判断
            return modifys.FindAll(l => findKeys.Contains(l.KeyName.Trim()));
        }

        /// <summary> 获取指定分区的所有关键字 </summary>
        public static List<string> GetGroupKeyNames(this EclKeyType group)
        {

            if (group == EclKeyType.Grid)
            {
                return KeyConfigerFactroy.Instance.EclipseKeyFactory.GridPartConfiger;

            }
            else if (group == EclKeyType.Props)
            {
                return KeyConfigerFactroy.Instance.EclipseKeyFactory.PropsPartConfiger;
            }

            else if (group == EclKeyType.Solution)
            {
                return KeyConfigerFactroy.Instance.EclipseKeyFactory.SolutionPartConfiger;
            }

            else if (group == EclKeyType.Regions)
            {
                return KeyConfigerFactroy.Instance.EclipseKeyFactory.RegionsPartConfiger;
            }

            return null;
        }

        /// <summary> 获取所有块中心网格数据 </summary> 
        public static List<TableKey> GetTopsxyz(this BaseFile eclData)
        {
            return eclData.Key.FindAll<TableKey>(l => l.Name == "TOPS" || l.Name == "DX" || l.Name == "DY" || l.Name == "DZ");
        }

        /// <summary> 获取所有属性网格数据 </summary> 
        public static List<TableKey> GetAllProperty(this BaseFile eclData)
        {
            // Todo ：过滤地质模型部分关键字 
            string[] ss = { "TOPS", "DX", "DY", "DZ", "ZCORN", "COORD" };

            List<TableKey> finds = eclData.Key.FindAll<TableKey>(l => !ss.Contains(l.Name));

            // Todo ：查找不是Region部分属性 
            Predicate<TableKey> match = l =>
                {
                    object[] attr = l.GetType().GetCustomAttributes(typeof(KeyAttribute), false);

                    return attr != null && attr.Length > 0 && ((KeyAttribute)attr[0]).EclKeyType != EclKeyType.Regions;
                };

            return finds.FindAll(match);
        }

        /// <summary> 获取所有属性网格数据 </summary> 
        public static List<TableKey> GetAllPropertyWithRegions(this BaseFile eclData)
        {
            // Todo ：过滤地质模型部分关键字 
            string[] ss = { "TOPS", "DX", "DY", "DZ", "ZCORN", "COORD" };

            List<TableKey> finds = eclData.Key.FindAll<TableKey>(l => !ss.Contains(l.Name));

            return finds;
        }

        /// <summary> 关闭所有内存镜像资源流(不删除文件) </summary>
        public static void SetAllMmfClose(this BaseFile eclData)
        {
            List<TableKey> finds = eclData.Key.FindAll<TableKey>();

            finds.ForEach(l => l.Close());
        }

        /// <summary> 关闭所有内存镜像资源流(删除文件) </summary>
        public static void SetAllMmfDispose(this BaseKey key)
        {
            List<TableKey> finds = key.FindAll<TableKey>();

            finds.ForEach(l => l.Close());


        }

        /// <summary> 更改路径镜像文件路径 </summary>
        public static void ChangeMmfPath(this BaseFile eclData, string path, bool isCopy)
        {
            List<TableKey> finds = eclData.Key.FindAll<TableKey>();

            finds.ForEach(l => l.ChangePath(path, isCopy));


        }

        /// <summary> 重命名（以拷贝的方式重命名）正常用SAVEAS()会引起文件重复   </summary>
        [Obsolete("未测试")]
        public static void ReName(this SimONData ecl, string oldFullPath, string newFullPath)
        {
            string oldCaseName = Path.GetFileName(oldFullPath);

            string caseName = Path.GetFileName(newFullPath);

            string oldPath = Path.GetDirectoryName(oldFullPath);

            string newPath = Path.GetDirectoryName(newFullPath);

            // Todo ：更改主文件名称 
            if (File.Exists(oldFullPath))
            {
                string newStr = newFullPath;//  E:\\aaaa\\aaaa.dat

                File.Move(oldFullPath, newFullPath);

                // Todo ：重新保存一份主文件 不加载INCLUDE 只处理主文件
                SimONData tempSimon = FileFactoryService.Instance.ThreadLoadFunc<SimONData>(() => new SimONData(newStr, null, k => false));
                var allInc = tempSimon.Key.FindAll<INCLUDE>();

                foreach (var item in allInc)
                {
                    // Todo ：不保存 防止覆盖原INCLUDE文件 
                    item.IsCreateFile = false;
                    item.FileName = item.FileName.Replace(oldCaseName, caseName);
                    //item.FilePath = item.FilePath.Replace(oldPath, newPath);
                }
                tempSimon.Save();

                // Todo ：修改所有INCLUDE文件名 
                List<INCLUDE> includes = ecl.Key.FindAll<INCLUDE>();

                foreach (var item in includes)
                {
                    string includePath = Path.Combine(oldPath, item.FileName);
                    string includePathNew = Path.Combine(newPath, item.FileName);
                    File.Move(includePath, includePathNew);
                }

                ecl.FilePath = newStr;
            }
        }


        /// <summary> 复制文件 </summary>
        [Obsolete("未测试,用到时参考逻辑调试")]
        public static void CopyTo(this BaseFile file, string toFileFullPath)
        {
            var incs = file.Key.FindAll<INCLUDE>();


            string folder = Path.GetDirectoryName(toFileFullPath);

            if (Directory.Exists(Path.GetDirectoryName(toFileFullPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(toFileFullPath));
            }

            // Todo ：复制主文件 
            File.Copy(file.FilePath, toFileFullPath);

            // Todo ：复制包含文件 
            incs.ForEach(l =>
            {
                string d = Path.Combine(folder, l.FileName);

                // Todo ：相同路径不复制 
                if (l.FilePath != d)
                {
                    // Todo ：存在该文件覆盖文件 
                    if (File.Exists(d))
                    {
                        File.Delete(d);
                    }

                    File.Copy(l.FilePath, d);
                }
            });
        }


        /// <summary> 标准化复制文件 </summary>
        [Obsolete("未测试,用到时参考逻辑调试")]
        public static void CopyToStandardized(this BaseFile file, string toFileFullPath)
        {
            // Todo ：获取所有需要标准化的关键字 
            file.Standardized();

            file.CopyTo(toFileFullPath);
        }

        /// <summary> 标准化文件 </summary>
        [Obsolete("未测试,用到时参考逻辑调试")]
        public static void Standardized(this BaseFile file)
        {
            // Todo ：获取所有需要标准化的关键字 
            var standards = file.Key.FindAll<IStandardized>();

            standards.ForEach(l => l.Standardized());
        }

    }
}
