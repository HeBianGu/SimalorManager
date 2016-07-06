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
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Parent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> Eclipse文件扩展方法 </summary>
    public static class EclipseExtend
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


        /// <summary> 添加井底流压限制和 </summary>
        //public static bool AddBtmWellPresslimit(this EclipseData eclData, DateTime pTime, Dictionary<string, double> wellWCONPROD, Dictionary<string, double> wellWECON)
        //{
        //    SCHEDULE schDate = eclData.Key.Find<SCHEDULE>();

        //    List<WCONHIST> wconHists = schDate.FindAll<WCONHIST>();

        //    List<WECON> wecons = schDate.FindAll<WECON>();

        //    DATES dates = eclData.AddSchDates(pTime);

        //    #region - 添加井底流压限制 -

        //    WCONPROD findWconProd = new WCONPROD("WCONPROD");
        //    foreach (var well in wellWCONPROD)
        //    {
        //        WCONHIST wconHist = null;
        //        if (wconHists != null)
        //        {
        //            //  每口井都找上一个存在的井数据
        //            wconHist = wconHists.FindLast(l => l.Items.Exists(k => k.wellName == well.Key));
        //        }

        //        //  没有找到
        //        if (wconHist == null)
        //        {
        //            throw new Exception("增加井底流压限制条件出错！没有找到井" + well + "对应的WCONHIST参考项");
        //        }
        //        else
        //        {
        //            ////  找到对应井项 + 转换预测项
        //            //WCONPROD.Item wconProdItem = wconHist.Items.Find(l => l.wellName == well.Key).ToProdItem();
        //            ////  设置井底流压限制
        //            //wconProdItem.BHP8 = well.Value.ToString();
        //            ////  增加到预测集合
        //            //findWconProd.Items.Add(wconProdItem);
        //        }
        //    }

        //    //  增加井底流压
        //    dates.InsertAfter(findWconProd);
        //    #endregion


        //    #region - 添加气水比限制 -

        //    WECON findWecon = new WECON("WECON");

        //    foreach (var well in wellWECON)
        //    {
        //        WECON param = null;
        //        if (wecons != null)
        //        {
        //            param = wecons.FindLast(l => l.Items.Exists(k => k.wellName == well.Key));
        //        }

        //        WECON.Item weconItem = default(WECON.Item);

        //        if (param != null)
        //        {
        //            weconItem = param.Items.Find(l => l.wellName == well.Key);
        //        }

        //        if (weconItem.Equals(default(WECON.Item)))
        //        {
        //            weconItem = new WECON.Item() { wellName = well.Key, maxGWPercent = well.Value.ToString() };
        //        }

        //        findWecon.Items.Add(weconItem);

        //    }


        //    //  增加最大气水比
        //    dates.InsertAfter(findWecon);

        //    #endregion


        //    return true;
        //}

        /// <summary> 查找指定重启时间 </summary>
        public static DATES FindDates(this EclipseData eclData, DateTime pTime)
        {
            List<DATES> Dates = eclData.Key.FindAll<DATES>();

            DATES findDate = Dates.Find(l => l.DateTime.IsEqulByDay(pTime));

            return findDate;
        }
        /*
        /// <summary> 增加指定时间的井产量 </summary>
        public static bool AddDatesWconHist(this EclipseData eclData, DateTime eTime, List<WCONHIST.Item> Items)
        {

            SCHEDULE schDate = eclData.Key.Find<SCHEDULE>();

            START startDate = eclData.Key.Find<START>();

            DateTime nowTime = startDate.StartTime;

            return schDate.AddDatesWconHist(nowTime, eTime, Items);
        }

        /// <summary> 增加指定时间的井产量 </summary>
        public static bool AddDatesWconHist(this SCHEDULE schDate, DateTime sTime, DateTime eTime, List<WCONHIST.Item> Items)
        {

            BaseKey findKey = null;

            DateTime nowTime = sTime;

            if (nowTime > eTime)
            {
                throw new ArgumentException("插入的时间不能小于案例的起始时间！");
            }

            schDate.Foreach(l =>
                {
                    if (l is DATES)
                    {
                        DATES date = l as DATES;
                        nowTime = date.DateTime;

                        //  记录比当前时间小的
                        if (nowTime < eTime)
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
                        if (nowTime <= eTime)
                        {
                            findKey = l;
                        }
                    }
                });

            DATES insertDate = new DATES("DATES");
            insertDate.SetDateTime(eTime);

            //  没有找到 = 插入END前面
            if (findKey == null)
            {
                END endKey = schDate.Find<END>();
                schDate.InsertBefore(endKey, insertDate);
            }

            //  找到了 = 插入指定关键字前面
            else
            {
                int index = schDate.FindIndex(findKey);

                //  删除日期和历史数据
                schDate.RemoveRange<DATES>(index);

                schDate.RemoveRange<WCONHIST>(index);

                //  插入当前时间和历史数据
                schDate.InsertAfter(findKey, insertDate);

                WCONHIST wconhist = new WCONHIST("WCONHIST");
                wconhist.Items = Items;

                schDate.InsertAfter(insertDate, wconhist);
            }

            return true;
        }

        /// <summary> 批量井产量 </summary>
        public static bool AddDatesWconHists(this EclipseData eclData, Dictionary<DATES, WCONHIST> datesValue)
        {

            SCHEDULE schDate = eclData.Key.Find<SCHEDULE>();

            START startDate = eclData.Key.Find<START>();

            DateTime nowTime = startDate.StartTime;

            return schDate.AddDatesWconHists(nowTime, datesValue);

        }

        /// <summary> 批量井产量 </summary>
        public static bool AddDatesWconHists(this SCHEDULE schDate, DateTime sTime, Dictionary<DATES, WCONHIST> datesValue)
        {
            if (datesValue == null || datesValue.Count == 0)
            {
                return false;
            }

            DATES first = datesValue.First().Key;

            DateTime eTime = first.DateTime;
            
            BaseKey findKey = null;

            DateTime nowTime = sTime;

            if (nowTime > first.DateTime)
            {
                throw new ArgumentException("插入的时间不能小于案例的起始时间！");
            }

            #region - 查找时间步 -

            schDate.Foreach(l =>
            {
                //  找到第一个大于时间点的
                if (findKey == null)
                {
                    if (l is DATES)
                    {
                        DATES date = l as DATES;
                        nowTime = date.DateTime;

                        //  记录比当前时间小的
                        if (nowTime >= eTime)
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
                        if (nowTime >= eTime)
                        {
                            findKey = l;
                        }
                    }
                }
            });

            #endregion

            DATES insertDate = null;

            //  没有找到 = 插入END前面
            if (findKey == null)
            {
                #region - 没有找到对应的日期数据 -
                //  清理数据
                var ds= schDate.FindAll<DATES>();

                var ws= schDate.FindAll<WCONHIST>();

                schDate.DeleteAll(ds);

                schDate.DeleteAll(ws);

                END endKey = schDate.Find<END>();

                BaseKey parent = endKey.ParentKey;

                findKey = endKey;

                foreach (var v in datesValue)
                {
                    //  插入当前时间和历史数据
                    if(findKey==endKey)
                    {
                        parent.InsertBefore(findKey, v.Key);
                    }
                    else
                    {
                        parent.InsertAfter(findKey, v.Key);
                    }

                    if (v.Value != null)
                    {
                        parent.InsertAfter(v.Key, v.Value);

                        findKey = v.Value;
                    }
                }

                #endregion
            }
            //  找到了 = 插入指定关键字前面
            else
            {
                #region - 找到对应的日期 -
                BaseKey parent = findKey.ParentKey;

                int index = parent.FindIndex(findKey);

                //  删除日期和历史数据
                //index = parent.FindIndex(findKey);

                parent.RemoveRange<BaseKey>(index);

                parent.RemoveRange<DATES>(index);

                if(parent.Keys.Count>0)
                {
                    //  找到前一个KEY
                    findKey = parent.Keys[index - 1];
                }
                else
                {
                    findKey = null;
                }

                foreach (var v in datesValue)
                {
                    if(findKey!=null)
                    {
                        //  插入当前时间和历史数据
                        parent.InsertAfter(findKey, v.Key);
                    }
                    else
                    {
                        parent.InsertKey(v.Key);
                    }

                    if (v.Value != null)
                    {
                        parent.InsertAfter(v.Key, v.Value);

                        findKey = v.Value;
                    }
                }
                #endregion
            }
            return true;
        }

        /// <summary> 获取所有井名 </summary>
        public static List<string> GetWellNames(this EclipseData eclData)
        {
            List<string> strs = new List<string>();
            SCHEDULE schDate = eclData.Key.Find<SCHEDULE>();

            //  历史数据
            //var lists = schDate.FindAll<WCONHIST>();

            var lists = schDate.FindAll<WELSPECS>();

            if (lists != null)
            {
                bool e = false;
                foreach (var v in lists)
                {
                    foreach (var item in v.Items)
                    {
                        e = strs.Contains(item.jm0);

                        if (!e)
                        {
                            strs.Add(item.jm0);
                        }
                    }
                }
            }

            //  预测数据
            var lists1 = schDate.FindAll<WCONPROD>();

            if (lists1 != null)
            {
                bool e = false;
                foreach (var v in lists1)
                {
                    foreach (var item in v.Items)
                    {
                        e = strs.Contains(item.jm0);

                        if (!e)
                        {
                            strs.Add(item.jm0);
                        }
                    }
                }
            }

            return strs;
        }


        //public static BaseKey FindLastDate(this  EclipseData eclData, DateTime pTime)
        //{


        //    DateTime eTime = first.DateTime;
            
        //    BaseKey findKey = null;

        //    DateTime nowTime = sTime;

        //    if (nowTime > first.DateTime)
        //    {
        //        throw new ArgumentException("插入的时间不能小于案例的起始时间！");
        //    }

        //    schDate.Foreach(l =>
        //    {
        //        //  找到第一个大于时间点的
        //        if (findKey == null)
        //        {
        //            if (l is DATES)
        //            {
        //                DATES date = l as DATES;
        //                nowTime = date.DateTime;

        //                //  记录比当前时间小的
        //                if (nowTime >= eTime)
        //                {
        //                    findKey = l;
        //                }
        //            }
        //            else if (l is TSTEP)
        //            {
        //                TSTEP step = l as TSTEP;
        //                int dayCount = step.DataCount;
        //                nowTime.AddDays(dayCount);

        //                //  记录比当前时间小的
        //                if (nowTime >= eTime)
        //                {
        //                    findKey = l;
        //                }
        //            }
        //        }
        //    });

        //}


        //public static BaseKey FindNextDate(this EclipseData eclData, DateTime pTime)

        */


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

        /// <summary> 获取所有井名 </summary>
        public static List<WELSPECS> GetAllWellModel(this EclipseData eclData)
        {
            SCHEDULE sch = eclData.Key.Find<SCHEDULE>();
            List<WELSPECS> ws = sch.FindAll<WELSPECS>();

            return ws;
        }
    }
}
