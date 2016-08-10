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
using OPT.Product.SimalorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPT.Product.SimalorManager.Base.AttributeEx;

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
                    else if (l is TSTEP)
                    {
                        TSTEP step = l as TSTEP;
                        int dayCount = step.DataCount;
                        nowTime.AddDays(dayCount);

                        //  记录比当前时间小的
                        if (nowTime <= pTime)
                        {
                            findKey = l;
                        }
                    }
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

            DATES findDate = Dates.Find(l => l.DateTime.IsEqulByDay(pTime));

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
                        funKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(md.KeyName) as TableKey;

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
                            copyKey = KeyConfigerFactroy.Instance.CreateKey<TableKey>(copy.Value, ecl.SimKeyType) as TableKey;

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
    }
}
