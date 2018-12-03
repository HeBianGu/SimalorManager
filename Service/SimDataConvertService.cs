using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
using HeBianGu.Product.SimalorManager.RegisterKeys.SimON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> 数模文件相互转换服务 </summary>
    public class SimDataConvertService : ServiceFactory<SimDataConvertService>
    {

        /// <summary> 将Eclipse数模文件转换成SimON数模文件 </summary>
        public SimONData ConvertToSimON(EclipseData ecl)
        {

            // Todo ：Eclipse里面的修改参数没有解析成SimON中修改参数 
            ecl.RunModify();

            RUNSPEC runspec = ecl.Key.Find<RUNSPEC>();
            GRID grid = ecl.Key.Find<GRID>();
            SOLUTION solution = ecl.Key.Find<SOLUTION>();
            SUMMARY summary = ecl.Key.Find<SUMMARY>();
            SCHEDULE schedule = ecl.Key.Find<SCHEDULE>();
            REGIONS regions = ecl.Key.Find<REGIONS>();
            PROPS props = ecl.Key.Find<PROPS>();

            SimONData simon = new SimONData();

            simon.FileName = ecl.FileName;
            simon.FilePath = ecl.FilePath;
            simon.MmfDirPath = ecl.MmfDirPath;
            simon.InitConstruct();

            simon.X = ecl.X;
            simon.Y = ecl.Y;
            simon.Z = ecl.Z;

            //  模型定义

            #region - 起始时间 -

            SOLVECTRL tuning = new SOLVECTRL("TUNING");

            tuning.Date = ecl.Key.Find<START>().StartTime;

            simon.Key.Add(tuning);

            #endregion

            #region - 维数定义 -

            RSVSIZE rsvsize = new RSVSIZE("RSVSIZE");

            DIMENS dimens = ecl.Key.Find<DIMENS>();

            rsvsize.X = dimens.X;
            rsvsize.Y = dimens.Y;
            rsvsize.Z = dimens.Z;



            simon.Key.Add(rsvsize);

            #endregion

            #region - 单位类型 -

            UnitType unitType = UnitType.METRIC;

            //  读到METRIC公制单位
            METRIC metric = ecl.Key.Find<METRIC>();

            if (metric != null)
            {
                simon.Key.Add(metric);
                unitType = UnitType.METRIC;
            }

            //  单位类型
            FIELD field = ecl.Key.Find<FIELD>();

            if (field != null)
            {
                simon.Key.Add(field);
                unitType = UnitType.FIELD;
            }

            #endregion

            #region - 流体类型 -

            MODELTYPE modeltype = new MODELTYPE("MODELTYPE");

            //  流体类型
            OIL oil = runspec.Find<OIL>();
            WATER water = runspec.Find<WATER>();
            GAS gas = runspec.Find<GAS>();
            DISGAS disgas = runspec.Find<DISGAS>();
            VAPOIL vapoil = runspec.Find<VAPOIL>();

            //  黑油
            if (oil != null && water != null && gas != null && disgas != null && vapoil == null)
            {
                modeltype.MetricType = MetricType.BLACKOIL;
            }
            //  油水
            else if (oil != null && water != null && gas == null && disgas == null && vapoil == null)
            {
                modeltype.MetricType = MetricType.OILWATER;
            }
            //  气水
            else if (oil == null && water != null && gas != null && disgas == null && vapoil == null)
            {
                modeltype.MetricType = MetricType.GASWATER;
            }
            //  挥发油
            else if (oil != null && water != null && gas != null && disgas != null && vapoil != null)
            {
                modeltype.MetricType = MetricType.HFOIL;
            }
            else
            {
                modeltype.MetricType = MetricType.BLACKOIL;
            }
            simon.Key.Add(modeltype);
            #endregion

            #region - 分区维数 -

            EQUILREG equilreg = new EQUILREG("EQUILREG");
            FIPREG fipreg = new FIPREG("FIPREG");
            ROCKREG rockreg = new ROCKREG("ROCKREG");
            SATREG satreg = new SATREG("SATREG");
            PVTREG pvtreg = new PVTREG("PVTREG");

            simon.Key.Add(equilreg);
            simon.Key.Add(fipreg);
            simon.Key.Add(rockreg);
            simon.Key.Add(satreg);
            simon.Key.Add(pvtreg);

            TABDIMS tabdims = runspec.Find<TABDIMS>();

            if (tabdims != null)
            {
                fipreg.X = tabdims.Fipfqzds4.ToString();
                rockreg.X = tabdims.Yslxgs12.ToString();
                satreg.X = tabdims.Bhdbs0.ToString();
                pvtreg.X = tabdims.Pvtbs1.ToString();

                //fipreg.X = "1";
                //rockreg.X = "1";
                //satreg.X = "1";
                //pvtreg.X = "1";
            }

            EQLDIMS eqldims = runspec.Find<EQLDIMS>();

            if (eqldims != null)
            {
                //equilreg.X = "1";
                equilreg.X = eqldims.Phfqs0.ToString();
            }

            OVERBURD overburd = props.Find<OVERBURD>();
            if (overburd != null)
            {
                //rockreg.X = overburd.Regions.Count.ToString();
            }

            EQUILMAP equilmap = new EQUILMAP("EQUILMAP");
            FIPMAP fipmap = new FIPMAP("FIPMAP");
            ROCKMAP rockmap = new ROCKMAP("ROCKMAP");
            SATMAP satmap = new SATMAP("SATMAP");
            PVTMAP pvtmap = new PVTMAP("PVTMAP");

            if (regions != null)
            {
                EQLNUM eqlnum = regions.Find<EQLNUM>();

                if (eqlnum != null)
                {
                    equilmap = eqlnum.ToTableKey<EQUILMAP>();
                    solution.Add(equilmap);
                    eqlnum.Delete();
                    eqlnum.Dispose();

                }

                // Todo ：非平衡初始化压力需要转换 
                var pressure = solution.Find<PRESSURE>();
                if (pressure != null)
                {
                    POIL poil = pressure.ToTableKey<POIL>();
                    solution.Add(poil);
                    pressure.Delete();
                    pressure.Dispose();
                }

                if (regions != null)
                {
                    FIPNUM fipnum = regions.Find<FIPNUM>();

                    if (fipnum != null)
                    {
                        fipmap = fipnum.ToTableKey<FIPMAP>();
                        grid.Add(fipmap);
                        fipnum.Delete();
                        fipnum.Dispose();
                    }
                    ROCKNUM rocknum = regions.Find<ROCKNUM>();

                    if (rocknum != null)
                    {
                        rockmap = rocknum.TransToTableKeyByName("ROCKMAP", true) as ROCKMAP;
                        grid.Add(rockmap);
                        rocknum.Delete();
                        rocknum.Dispose();
                    }

                    SATNUM satnum = regions.Find<SATNUM>();

                    if (satnum != null)
                    {
                        satmap = satnum.ToTableKey<SATMAP>();
                        grid.Add(satmap);
                        satnum.Delete();
                        satnum.Dispose();
                    }


                    PVTNUM pvtnum = regions.Find<PVTNUM>();

                    if (pvtnum != null)
                    {
                        pvtmap = pvtnum.ToTableKey<PVTMAP>();
                        grid.Add(pvtmap);
                        pvtnum.Delete();
                        pvtnum.Dispose();
                    }
                }

            }






            #endregion

            #region - 地质模型 -

            simon.Key.Add(grid);

            #endregion

            #region - 断层 -
            //var eclFaults = grid.FindAll<HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse.FAULTS>();

            //foreach (var v in eclFaults)
            //{
            //grid.AddRange(this.ConvertToSimON(v));

            //v.Delete();
            //}
            #endregion

            #region - 水体 -

            //AQUFETP AQUFETP=

            // Todo ：Fetkovich水体数据转换
            var ct = solution.Find<HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse.AQUCT>();

            if (ct != null)
            {
                var newFetp = this.ConvertToSimON(ct);

                solution.Add(newFetp);

                ct.Delete();
            }

            // Todo ：Fetkovich水体数据转换
            var fetp = solution.Find<HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse.AQUFETP>();

            if (fetp != null)
            {
                var newFetp = this.ConvertToSimON(fetp);

                solution.Add(newFetp);

                fetp.Delete();
            }

            // Todo ：水体连接数据转换
            var aquancon = solution.Find<HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse.AQUANCON>();

            if (aquancon != null)
            {
                var newFetp = this.ConvertToSimON(aquancon);

                solution.Add(newFetp);

                aquancon.Delete();
            }

            #endregion

            #region - 流体模型 岩石模型-

            GRAVITY gravity = ecl.Key.Find<GRAVITY>();

            if (gravity != null)
            {
                // Todo ：SimON只解析绝对密度 
                DENSITY density = this.ConvertTo(gravity, unitType);

                gravity.ParentKey.Add(density);

                gravity.Delete();
            }

            List<IRegionInterface> regSoltionKeys = solution.FindAll<IRegionInterface>();

            regSoltionKeys.ForEach(l => l.TransToSimONRegion());
            simon.Key.Add(solution);
            //  
            List<IRegionInterface> regPropsKeys = props.FindAll<IRegionInterface>();
            regPropsKeys.ForEach(l => l.TransToSimONRegion());

            //// Todo ：SGWFN 需要特殊转换为 SWGF
            //SGWFN sgwfn = props.Find<SGWFN>();
            //if (sgwfn != null)
            //{
            //    //props.AddRange(sgwfn.ConvertTo());

            //    simon.Key.AddRange<SWGF>(sgwfn.ConvertTo());
            //}

            simon.Key.Add(props);




            #endregion

            #region - 初始化模型 -

            List<EQUIL> equil = solution.FindAll<EQUIL>();
            foreach (var item in equil)
            {
                EQUILPAR equilpar = new EQUILPAR("EQUILPAR");
                EQUIL.Item it = item.GetSingleRegion().GetSingleItem();

                equilpar.Szstzdhs0 = it.cksd0;
                equilpar.Szstljs1 = it.ckyl1;
                equilpar.Ctstyxhs2 = it.ysjmsd2;
                equilpar.Ctstyxzdhs3 = it.ysjmcmgyl3.ToDefalt("0");
                //equilpar.Jxstzds4 = it.yqjmsd4;
                equilpar.E100wgzds5 = it.yqjmsd4;
                equilpar.E300jxstzds6 = it.yqjmcmgyl5;

                item.ParentKey.Add(equilpar);
                item.Delete();

            }

            #endregion

            #region - 生产模型 -

            WELL well = new WELL("WELL");


            // Todo ：添加完井数据 （注意要放到生产模型前面）
            simon.Key.Add(well);

            //  生产模型
            simon.Key.Add(this.ConvertToSimON(schedule, well, ecl.Key.Find<START>().StartTime, simon.HistoryData));


            #endregion

            // Todo ：转换修正关键字 
            List<ModifyKey> modifys = ecl.Key.FindAll<ModifyKey>();

            grid.AddRange(modifys);

            return simon;
        }

        /// <summary> 将Eclipse生产数据转换成SimON生产数据 </summary>
        public SCHEDULE ConvertToSimON(SCHEDULE sch, WELL location, DateTime startTime, BaseFile history)
        {

            // Todo ：保存SCH 

            SCHEDULE schedule = new SCHEDULE("SCHEDULE");

            List<string> wellNames = new List<string>();

            List<WELSPECS> ws = sch.FindAll<WELSPECS>();

            // Todo ：查找所有井名 
            ws.ForEach(l => wellNames.AddRange(l.Items.Select(k => k.jm0)));

            List<NAME> histNames = new List<NAME>();

            // Todo ：初始化名称 生产_historyproduction.dat
            wellNames.ForEach(l => histNames.Add(new NAME("NAME") { WellName = l }));

            histNames.ForEach(l => history.Key.Add(l));


            // Todo ：初始化完井WELL数据 
            List<NAME> names = new List<NAME>();

            wellNames.ForEach(l => names.Add(new NAME("NAME") { WellName = l }));

            names.ForEach(l => location.Add(l));

            List<DATES> ds = sch.FindAll<DATES>();

            string format = "井名：{0} ({1},{2})";

            // Todo ：添加起始信息到时间步 
            DATES start = new DATES("DATES", startTime);

            sch.DeleteAll<DATES>();

            start.AddRange<BaseKey>(sch.Keys);

            ds.Insert(0, start);

            List<PERF> comAllTemp = new List<PERF>();

            foreach (DATES d in ds)
            {
                // Todo ：对缓存中完井井名去重复取最后一条 
                var distincts = comAllTemp.GroupBy(l => l.WellName + l.I0 + l.J1 + l.K12).ToList();
                comAllTemp.Clear();
                foreach (var item in distincts)
                {
                    comAllTemp.Add(item.Last());
                }

                //  创建SimON日期
                TIME time = new TIME("TIME");
                time.Date = d.DateTime;
                schedule.Add(time);

                var wconprod = d.FindAll<WCONPROD>();
                var wconhist = d.FindAll<WCONHIST>();
                var wconinje = d.FindAll<WCONINJE>();
                var wconinjh = d.FindAll<WCONINJH>();

                //  完井数据(考虑到排序)
                List<BaseKey> compdats = d.FindAll<BaseKey>(l => l is COMPDAT || l is WELOPEN);

                List<WPIMULT> wpimult = d.FindAll<WPIMULT>();

                List<WELOPEN> welopen = d.FindAll<WELOPEN>();

                #region - 添加没有生产信息的完井 -
                //  添加完井数据 
                foreach (BaseKey c in compdats)
                {

                    if (c is COMPDAT)
                    {
                        COMPDAT com = c as COMPDAT;

                        foreach (COMPDAT.Item citem in com.Items)
                        {

                            // Todo ：过滤有生产数据的，用后面方法处理 
                            if (wconprod.Exists(l => l.Items.Exists(k => k.jm0 == citem.jm0))) continue;
                            if (wconhist.Exists(l => l.Items.Exists(k => k.wellName0 == citem.jm0))) continue;
                            if (wconinje.Exists(l => l.Items.Exists(k => k.jm0 == citem.jm0))) continue;
                            if (wconinjh.Exists(l => l.Items.Exists(k => k.jm0 == citem.jm0))) continue;

                            WELLCTRL well = time.Find<WELLCTRL>(l => l.WellName0 == citem.jm0);

                            if (well == null)
                            {
                                // Todo ：创建一个空的生产信息 
                                well = new WELLCTRL("WELLCTRL");
                                well.ProType = SimONProductType.NA;
                                well.WellName0 = citem.jm0;
                                time.Add(well);
                            }

                            NAME name = names.Find(l => l.WellName == well.WellName0);

                            #region - SCh数据 -

                            PERF perf = new PERF("PERF");
                            perf.WellName = well.WellName0;
                            perf.I0 = citem.i1;
                            perf.J1 = citem.j2;
                            perf.K12 = citem.swg3;
                            perf.K23 = citem.xwg4;
                            perf.Kgbs4 = citem.kgbz5;
                            perf.Jzs6 = citem.ljyz7;
                            perf.WjfxX7 = citem.skfx12 == "X" ? "DX" : "0";
                            perf.WjfxY8 = citem.skfx12 == "Y" ? "DY" : "0";
                            perf.WjfxZ9 = citem.skfx12 == "Z" ? "DZ" : "0";
                            perf.Bp10 = citem.bpxs10;

                            // Todo ：查找井指数乘子 
                            foreach (WPIMULT wp in wpimult)
                            {
                                var v = wp.Items.Find(l => l.jm0 == well.WellName0);

                                if (v != null)
                                {
                                    perf.Jzscz5 = v.jzscz1;
                                    break;
                                }
                            }

                            // Todo ：增加前先删除存在的重复数据 
                            well.DeleteAll<PERF>(l => l.I0 == perf.I0 && l.J1 == perf.J1 && l.K12 == perf.K12);
                            well.Add(perf);

                            #endregion

                            #region - WELL数据 -

                            NAME.Item nameItem = new NAME.Item();
                            nameItem.i0 = citem.i1;
                            nameItem.j1 = citem.j2;
                            nameItem.k12 = citem.swg3;
                            nameItem.k23 = citem.xwg4;
                            nameItem.kgbz4 = citem.kgbz5;
                            //nameItem.wi5 = "NA";// v.Value.skin.Value.Value.ToString();
                            //nameItem.dx6 = v.Value.wellIndex.Value.GetValue(v.Value.wellIndex.GetUnitValue(_ecl)).ToString();
                            //nameItem.dy7 = v.Value.wellDirection.Value.Value == "X" ? "0" : v.Value.wellDirection.Value.Value == "Y" ? "1" : "2";
                            nameItem.bpxs9 = citem.bpxs10;
                            nameItem.jj10 = (citem.jtnj8.ToDouble() / 2).ToString();
                            name.Items.Add(nameItem);
                            #endregion

                            comAllTemp.Add(perf);

                        }
                    }
                    else if (c is WELOPEN)
                    {
                        WELOPEN wp = c as WELOPEN;

                        foreach (var v in wp.Items)
                        {
                            // Todo ：过滤有生产数据的，用后面方法处理 
                            if (wconprod.Exists(l => l.Items.Exists(k => k.jm0 == v.jm0))) continue;
                            if (wconhist.Exists(l => l.Items.Exists(k => k.wellName0 == v.jm0))) continue;
                            if (wconinje.Exists(l => l.Items.Exists(k => k.jm0 == v.jm0))) continue;
                            if (wconinjh.Exists(l => l.Items.Exists(k => k.jm0 == v.jm0))) continue;
                            // WELOPEN
                            //'G13' 'SHUT' 0 0 0 2 * /
                            // /

                            // Todo ：查找之前所有完井
                            var coms = comAllTemp.FindAll(l => l.WellName == v.jm0);

                            Predicate<PERF> match = l => true;

                            // Todo ：0 或 *表示默认值全都取
                            if (v.i2 != KeyConfiger.EclipseDefalt && v.i2 != "0")
                            {
                                match += l => l.I0 == v.i2;
                            }

                            if (v.j3 != KeyConfiger.EclipseDefalt && v.j3 != "0")
                            {
                                match += l => l.J1 == v.j3;
                            }

                            if (v.k4 != KeyConfiger.EclipseDefalt && v.k4 != "0")
                            {
                                match += l => l.K12 == v.k4;
                            }

                            var findComs = coms.FindAll(match);

                            WELLCTRL well = time.Find<WELLCTRL>(l => l.WellName0 == v.jm0);
                            if (well == null)
                            {
                                // Todo ：创建一个空的生产信息 
                                well = new WELLCTRL("WELLCTRL");
                                well.ProType = SimONProductType.NA;
                                well.WellName0 = v.jm0;
                                time.Add(well);
                            }

                            // Todo ：增加WELOPEN控制的完井 
                            foreach (var fitem in findComs)
                            {
                                PERF perf = fitem.Copy();
                                perf.Kgbs4 = v.jz1;
                                // Todo ：增加前先删除存在的重复数据 
                                well.DeleteAll<PERF>(l => l.I0 == fitem.I0 && l.J1 == fitem.J1 && l.K12 == fitem.K12);
                                well.Add(perf);
                            }
                        }
                    }

                    //this.ConvertCompadat(well, names, compdats, wpimult, comAllTemp);
                }

                #endregion


                foreach (var item in wconprod)
                {
                    foreach (WCONPROD.ItemHY it in item.Items)
                    {
                        //  生产数据
                        WELLCTRL well = new WELLCTRL("WELLCTRL");

                        well.WellName0 = it.jm0;

                        well = this.ConvertToSimON(it, d, histNames);

                        if (well != null)
                        {
                            this.ConvertCompadat(well, names, compdats, wpimult, comAllTemp);

                            time.Add(well);
                        }

                    }
                }


                foreach (var item in wconhist)
                {
                    foreach (WCONHIST.Item it in item.Items)
                    {
                        //  生产数据
                        WELLCTRL well = new WELLCTRL("WELLCTRL");

                        well.WellName0 = it.wellName0;

                        well = this.ConvertToSimON(it, d, histNames);

                        this.ConvertCompadat(well, names, compdats, wpimult, comAllTemp);

                        time.Add(well);
                    }
                }


                foreach (var item in wconinje)
                {
                    foreach (WCONINJE.ItemHY it in item.Items)
                    {

                        //  生产数据
                        WELLCTRL well = new WELLCTRL("WELLCTRL");

                        well.WellName0 = it.jm0;

                        well = this.ConvertToSimON(it, d, histNames);

                        this.ConvertCompadat(well, names, compdats, wpimult, comAllTemp);

                        time.Add(well);

                    }
                }

                foreach (var item in wconinjh)
                {
                    foreach (WCONINJH.Item it in item.Items)
                    {
                        //  生产数据
                        WELLCTRL well = new WELLCTRL("WELLCTRL");

                        well.WellName0 = it.jm0;

                        well = this.ConvertToSimON(it, d, histNames);

                        this.ConvertCompadat(well, names, compdats, wpimult, comAllTemp);

                        time.Add(well);

                    }
                }

                //// Todo ：将之前的完井信息都加入到缓存中 
                //foreach (var item in compdats)
                //{
                //    comAllTemp.AddRange(item.Items);
                //}

            }

            return schedule;
        }

        /// <summary> 转换成SimON格式的项 </summary>
        public WELLCTRL ConvertToSimON(HeBianGu.Product.SimalorManager.Item item, DATES date, List<NAME> histNames)
        {

            WELLCTRL well = new WELLCTRL("WELLCTRL");

            if (item is WCONPROD.ItemHY)
            {
                WCONPROD.ItemHY nIt = item as WCONPROD.ItemHY;

                #region - 转换历史数据 -

                NAME name = histNames.Find(l => l.WellName == nIt.jm0);

                if (name == null)
                {
                    LogProviderHandler.Instance.OnRunLog("当前日期：" + date.DateTime + "井" + nIt.jm0 + "未找到对应的历史信息！");
                    return null;
                }


                DAYS days = name.Find<DAYS>();

                if (days == null)
                {
                    days = new DAYS("DAYS");
                    name.Add(days);
                }
                DAYS.Item dayitem = new DAYS.Item();
                dayitem.Time0 = date.DateTime;
                dayitem.Csl1 = nIt.rcsl4;
                dayitem.Cql2 = nIt.rcql5;
                dayitem.Cyl3 = nIt.rcyl3;
                dayitem.Cyl7 = nIt.liqutPro6;
                days.Items.Add(dayitem);

                #endregion

                #region - 转换数模数据 -

                well.WellName0 = nIt.jm0;
                well.Jcyblxz2 = nIt.rcsl4;

                //  产水
                if (nIt.kzms2 == "WATER")
                {
                    well.Jcyblxz2 = nIt.rcsl4;
                    well.ProType = SimONProductType.WRAT;
                }
                if (nIt.kzms2 == "GRAT")
                {
                    //  产气
                    well.Jcyblxz2 = nIt.rcql5;
                    well.ProType = SimONProductType.GRAT;
                }
                if (nIt.kzms2 == "ORAT")
                {
                    //  产油
                    well.Jcyblxz2 = nIt.rcyl3;
                    well.ProType = SimONProductType.ORAT;
                }

                if (nIt.kzms2 == "LRAT")
                {
                    //  产液
                    well.Jcyblxz2 = (nIt.rcyl3.ToDouble() + nIt.rcsl4.ToDouble()).ToString();
                    well.ProType = SimONProductType.LRAT;
                }
                #endregion

            }
            else if (item is WCONHIST.Item)
            {
                WCONHIST.Item nIt = item as WCONHIST.Item;

                #region - 转换历史数据 -

                NAME name = histNames.Find(l => l.WellName == nIt.wellName0);

                if (name == null)
                {
                    LogProviderHandler.Instance.OnRunLog("当前日期：" + date.DateTime + "井" + nIt.wellName0 + "未找到对应的历史信息！");
                    return well;
                }
                DAYS days = name.Find<DAYS>();

                if (days == null)
                {
                    days = new DAYS("DAYS");
                    name.Add(days);
                }
                DAYS.Item dayitem = new DAYS.Item();
                dayitem.Time0 = date.DateTime;
                dayitem.Csl1 = nIt.waterPro4;
                dayitem.Cql2 = nIt.gasPro5;
                dayitem.Cyl3 = nIt.oilPro3;
                dayitem.Cyl7 = (nIt.waterPro4.ToDouble() + nIt.oilPro3.ToDouble()).ToString();
                days.Items.Add(dayitem);

                #endregion

                #region - 转换数模数据 -

                well.WellName0 = nIt.wellName0;
                well.Jcyblxz2 = nIt.waterPro4;


                //  产水
                if (nIt.ctrlModel2 == "WATER")
                {
                    well.Jcyblxz2 = nIt.waterPro4;
                    well.ProType = SimONProductType.WRAT;
                }
                if (nIt.ctrlModel2 == "GRAT")
                {
                    //  产气
                    well.Jcyblxz2 = nIt.gasPro5;
                    well.ProType = SimONProductType.GRAT;
                }
                if (nIt.ctrlModel2 == "ORAT")
                {
                    //  产油
                    well.Jcyblxz2 = nIt.oilPro3;
                    well.ProType = SimONProductType.ORAT;
                }

                if (nIt.ctrlModel2 == "LRAT")
                {
                    //  产液
                    well.Jcyblxz2 = (nIt.waterPro4.ToDouble() + nIt.oilPro3.ToDouble()).ToString();
                    well.ProType = SimONProductType.LRAT;
                }
                #endregion
            }

            else if (item is WCONINJE.ItemHY)
            {
                WCONINJE.ItemHY nIt = item as WCONINJE.ItemHY;

                #region - 转换历史数据 -

                NAME name = histNames.Find(l => l.WellName == nIt.jm0);

                if (name == null)
                {
                    LogProviderHandler.Instance.OnRunLog("当前日期：" + date.DateTime + "井" + nIt.jm0 + "未找到对应的历史信息！");
                    return well;
                }


                DAYS days = name.Find<DAYS>();

                if (days == null)
                {
                    days = new DAYS("DAYS");
                    name.Add(days);
                }
                DAYS.Item dayitem = new DAYS.Item();
                dayitem.Time0 = date.DateTime;

                if (nIt.zrltlx1 == "WATER")
                {
                    dayitem.Zsl4 = nIt.rzrl4;
                }
                else
                {
                    dayitem.Zql5 = nIt.rzrl4;
                }

                days.Items.Add(dayitem);

                #endregion

                #region - 转换数模数据 -

                well.WellName0 = nIt.jm0;


                if (nIt.zrltlx1 == "WATER")
                {
                    //well.Jcyblxz2 = nIt.rzrl4;
                    well.Jcyblxz2 = nIt.rzrl4;
                    well.ProType = SimONProductType.WIR;

                }
                else
                {
                    //well.Jcyblxz2 = nIt.rzrl4;
                    //well.Jkkz1 = "6";

                    well.Jcyblxz2 = nIt.rzrl4;
                    well.ProType = SimONProductType.GIR;
                }

                #endregion
            }
            else if (item is WCONINJH.Item)
            {
                WCONINJH.Item nIt = item as WCONINJH.Item;

                #region - 转换历史数据 -

                NAME name = histNames.Find(l => l.WellName == nIt.jm0);

                if (name == null)
                {
                    LogProviderHandler.Instance.OnRunLog("当前日期：" + date.DateTime + "井" + nIt.jm0 + "未找到对应的历史信息！");
                    return well;
                }


                DAYS days = name.Find<DAYS>();

                if (days == null)
                {
                    days = new DAYS("DAYS");
                    name.Add(days);
                }
                DAYS.Item dayitem = new DAYS.Item();
                dayitem.Time0 = date.DateTime;

                if (nIt.zrltlx1 == "WATER")
                {
                    dayitem.Zsl4 = nIt.rzrl3;
                }
                else
                {
                    dayitem.Zql5 = nIt.rzrl3;
                }

                days.Items.Add(dayitem);

                #endregion

                #region - 转换数模数据 -

                well.WellName0 = nIt.jm0;

                if (nIt.zrltlx1 == "WATER")
                {
                    well.Jcyblxz2 = nIt.rzrl3;
                    //well.Jkkz1 = "7";
                    well.ProType = SimONProductType.WIBHP;
                }
                else
                {
                    well.Jcyblxz2 = nIt.rzrl3;
                    //well.Jkkz1 = "8";
                    well.ProType = SimONProductType.GIBHP;
                }

                #endregion
            }

            return well;

        }

        /// <summary> 将Eclipse完井数据转换成SimON完井数据 </summary>
        void ConvertCompadat(WELLCTRL well, List<NAME> names, List<BaseKey> basekey, List<WPIMULT> wpimult, List<PERF> compTemps)
        {
            List<BaseKey> compdats = basekey.FindAll(l => l is COMPDAT).ToList();
            //List<WELOPEN> welopen

            if (string.IsNullOrEmpty(well.WellName0) && compdats != null && compdats.Count > 0)
            {
                COMPDAT c = compdats.FirstOrDefault() as COMPDAT;
                well.WellName0 = c.Items.FirstOrDefault().jm0;
            }

            NAME name = names.Find(l => l.WellName == well.WellName0);

            //  添加完井数据 
            foreach (BaseKey c in basekey)
            {
                if (c is COMPDAT)
                {
                    COMPDAT com = c as COMPDAT;

                    foreach (COMPDAT.Item citem in com.Items)
                    {
                        if (citem.jm0 != well.WellName0) continue;

                        #region - SCh数据 -

                        PERF perf = new PERF("PERF");
                        perf.WellName = well.WellName0;
                        perf.I0 = citem.i1;
                        perf.J1 = citem.j2;
                        perf.K12 = citem.swg3;
                        perf.K23 = citem.xwg4;
                        perf.Kgbs4 = citem.kgbz5;
                        perf.Jzs6 = citem.ljyz7;
                        perf.WjfxX7 = citem.skfx12 == "X" ? "DX" : "0";
                        perf.WjfxY8 = citem.skfx12 == "Y" ? "DY" : "0";
                        perf.WjfxZ9 = citem.skfx12 == "Z" ? "DZ" : "0";
                        perf.Bp10 = citem.bpxs10;

                        // Todo ：查找井指数乘子 
                        foreach (WPIMULT wp in wpimult)
                        {
                            var v = wp.Items.Find(l => l.jm0 == well.WellName0);

                            if (v != null)
                            {
                                perf.Jzscz5 = v.jzscz1;
                                break;
                            }
                        }


                        // Todo ：增加前先删除存在的重复数据 
                        well.DeleteAll<PERF>(l => l.I0 == perf.I0 && l.J1 == perf.J1 && l.K12 == perf.K12);

                        well.Add(perf);

                        #endregion

                        #region - WELL数据 -

                        NAME.Item nameItem = new NAME.Item();
                        nameItem.i0 = citem.i1;
                        nameItem.j1 = citem.j2;
                        nameItem.k12 = citem.swg3;
                        nameItem.k23 = citem.xwg4;
                        nameItem.kgbz4 = citem.kgbz5;
                        //nameItem.wi5 = "NA";// v.Value.skin.Value.Value.ToString();
                        //nameItem.dx6 = v.Value.wellIndex.Value.GetValue(v.Value.wellIndex.GetUnitValue(_ecl)).ToString();
                        //nameItem.dy7 = v.Value.wellDirection.Value.Value == "X" ? "0" : v.Value.wellDirection.Value.Value == "Y" ? "1" : "2";
                        nameItem.bpxs9 = citem.bpxs10;
                        nameItem.jj10 = (citem.jtnj8.ToDouble() / 2).ToString();
                        name.Items.Add(nameItem);
                        #endregion


                        // Todo ：将当前时间点下 WELOPEN前增加到数据中
                        compTemps.Add(perf);
                    }
                }
                else if (c is WELOPEN)
                {
                    WELOPEN wp = c as WELOPEN;
                    if (wp.Items == null || wp.Items.Count == 0) continue;
                    var vs = wp.Items.FindAll(l => l.jm0 == well.WellName0 || l.jm0 == KeyConfiger.EclipseDefalt);
                    if (vs == null || vs.Count == 0) continue;
                    WELOPEN.Item v = vs.Last();
                    if (v == null) continue;

                    // WELOPEN
                    //'G13' 'SHUT' 0 0 0 2 * /
                    // /

                    // Todo ：查找之前所有完井
                    var coms = compTemps.FindAll(l => l.WellName == well.WellName0);

                    Predicate<PERF> match = l => true;

                    // Todo ：0 或 *表示默认值全都取
                    if (v.i2 != KeyConfiger.EclipseDefalt && v.i2 != "0")
                    {
                        match += l => l.I0 == v.i2;
                    }

                    if (v.j3 != KeyConfiger.EclipseDefalt && v.j3 != "0")
                    {
                        match += l => l.J1 == v.j3;
                    }

                    if (v.k4 != KeyConfiger.EclipseDefalt && v.k4 != "0")
                    {
                        match += l => l.K12 == v.k4;
                    }

                    var findComs = coms.FindAll(match);

                    // Todo ：增加WELOPEN控制的完井 
                    foreach (var item in findComs)
                    {
                        PERF perf = item.Copy();
                        perf.Kgbs4 = v.jz1;
                        // Todo ：增加前先删除存在的重复数据 
                        well.DeleteAll<PERF>(l => l.I0 == item.I0 && l.J1 == item.J1 && l.K12 == item.K12);
                        well.Add(perf);
                    }
                }
            }
        }

        ///// <summary> 将Eclipse断层转换为SimON断层 </summary>
        //public List<HeBianGu.Product.SimalorManager.RegisterKeys.SimON.FAULTS> ConvertToSimON(HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse.FAULTS faults)
        //{

        //    List<RegisterKeys.SimON.FAULTS> fs = new List<RegisterKeys.SimON.FAULTS>();
        //    var names = faults.Items.Select(l => l.dcm0).Distinct();

        //    foreach (var name in names)
        //    {
        //        HeBianGu.Product.SimalorManager.RegisterKeys.SimON.FAULTS f = new RegisterKeys.SimON.FAULTS("FAULTS");
        //        fs.Add(f);

        //        var items = faults.Items.FindAll(l => l.dcm0 == name);

        //        items.ForEach(l =>
        //            {
        //                RegisterKeys.SimON.FAULTS.Item item = new RegisterKeys.SimON.FAULTS.Item();
        //                item.X11 = l.X11;
        //                item.X22 = l.X22;
        //                item.Y13 = l.Y13;
        //                item.Y24 = l.Y24;
        //                item.Z15 = l.Z15;
        //                item.Z26 = l.Z26;
        //                item.Dcm7 = l.Dcm7;

        //                f.Items.Add(item);
        //            });
        //    }

        //    return fs;
        //}


        /// <summary> 将Eclipse水体数据转换成SimON水体数据 </summary>
        public HeBianGu.Product.SimalorManager.RegisterKeys.SimON.AQUFETP ConvertToSimON(HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse.AQUFETP eclaqu)
        {
            HeBianGu.Product.SimalorManager.RegisterKeys.SimON.AQUFETP simon = new RegisterKeys.SimON.AQUFETP("AQUFETP");

            foreach (var item in eclaqu.Items)
            {
                RegisterKeys.SimON.AQUFETP.Item nItem = new RegisterKeys.SimON.AQUFETP.Item();

                nItem.stbh0 = item.stbh0;
                nItem.cksd1 = item.cksd1;
                nItem.cksdccsyl2 = item.cksdccsyl2;
                nItem.cssttj3 = item.cssttj3;
                nItem.stysxs4 = item.stysxs4;
                nItem.productionindex5 = item.sqzs5;
                nItem.sxpvtbbh6 = item.sxpvtbbh6;
                simon.Items.Add(nItem);
            }

            return simon;

        }

        /// <summary> 将Eclipse水体数据转换成SimON水体数据 </summary>
        public HeBianGu.Product.SimalorManager.RegisterKeys.SimON.AQUCT ConvertToSimON(HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse.AQUCT eclaqu)
        {
            HeBianGu.Product.SimalorManager.RegisterKeys.SimON.AQUCT simon = new RegisterKeys.SimON.AQUCT("AQUCT");

            foreach (var item in eclaqu.Items)
            {
                RegisterKeys.SimON.AQUCT.Item nItem = new RegisterKeys.SimON.AQUCT.Item();

                nItem.stbh0 = item.stbh0;
                nItem.cksd1 = item.cksd1;
                nItem.cksdccsyl2 = item.cksdccsyl2;
                nItem.ststl3 = item.ststl3;
                nItem.stkxd4 = item.stkxd4;
                nItem.stysxs5 = item.stysxs5;
                nItem.stnj6 = item.stnj6;
                nItem.sthd7 = item.sthd7;
                nItem.yxj8 = item.yxj8;
                nItem.sxpvtbbh9 = item.sxpvtbbh9;
                nItem.yxhsbbh10 = item.yxhsbbh10;

                //nItem.stcshynd11 = item.stysxs4;
                //nItem.stwd12 = item.sqzs5;
                //nItem.yxhsbbh10 = item.sxpvtbbh6;
                simon.Items.Add(nItem);
            }

            return simon;

        }

        /// <summary> 将Eclipse水体数据转换成SimON水体数据 </summary>
        public HeBianGu.Product.SimalorManager.RegisterKeys.SimON.AQUANCON ConvertToSimON(HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse.AQUANCON eclaqu)
        {
            HeBianGu.Product.SimalorManager.RegisterKeys.SimON.AQUANCON simon = new RegisterKeys.SimON.AQUANCON("AQUANCON");

            foreach (var item in eclaqu.Items)
            {
                RegisterKeys.SimON.AQUANCON.Item nItem = new RegisterKeys.SimON.AQUANCON.Item();

                nItem.Stbh0 = item.Stbh0;
                nItem.Xzbks1 = item.Xzbks1;
                nItem.Xzbjs2 = item.Xzbjs2;
                nItem.Yks3 = item.Yks3;
                nItem.Yjs4 = item.Yjs4;
                nItem.Zks5 = item.Zks5;
                nItem.Zjs6 = item.Zjs6;
                nItem.Wgmcx7 = item.Wgmcx7;
                nItem.Cdlcz8 = item.Cdlcz8;
                nItem.Cdljsxx9 = item.Cdljsxx9;
                simon.Items.Add(nItem);
            }

            return simon;

        }

        ///// <summary> 转换存储格式 </summary>
        //public void TransToSimONFaults(List<RegisterKeys.SimON.FAULTS> fs)
        //{
        //    //FAULTS -- 示例
        //    //FAULTS1
        //    //30 30 8 8 75 76  Y + 1 0
        //    //FAULTS2
        //    //30 30 8 8 75 76  Y + 1 0

        //    for (int i = 1; i <= fs.Count; i++)
        //    {
        //        RegisterKeys.SimON.FAULTS f = fs[i - 1];
        //        f.Name = "FAULTS" + i.ToString();
        //    }

        //    RegisterKeys.SimON.FAULTS faults = new RegisterKeys.SimON.FAULTS("FAULTS");
        //    fs.Insert(0, faults);
        //}

        /// <summary> 相对密度转换成绝对密度 </summary>
        public DENSITY ConvertTo(GRAVITY gravity, UnitType utype)
        {
            DENSITY density = new DENSITY("DENSITY");

            foreach (var item in gravity.Regions)
            {
                DENSITY.Region region = new DENSITY.Region(item.RegionIndex);

                foreach (var it in item)
                {
                    DENSITY.Item inew = new DENSITY.Item();

                    if (utype == UnitType.FIELD)
                    {
                        // Todo ： 141500/(131.5+A)*0.0624279605761466 B*1000*0.0624279605761466 C*1.293*0.0624279605761466 

                        inew.ymd = (141500 / (131.5 + it.yyapicd0.ToDouble()) * 0.0624279605761466).ToString();

                        inew.smd = (it.sdxdmd1.ToDouble() * 1000 * 0.0624279605761466).ToString();

                        inew.qmd = (it.qdxdmd2.ToDouble() * 1.293 * 0.0624279605761466).ToString();

                    }
                    else if (utype == UnitType.METRIC)
                    {

                        // Todo ：141500/(131.5+A) B*1000 C*1.293 

                        inew.ymd = (141500 / (131.5 + it.yyapicd0.ToDouble())).ToString();

                        inew.smd = (it.sdxdmd1.ToDouble() * 1000).ToString();

                        inew.qmd = (it.qdxdmd2.ToDouble() * 1.293).ToString();
                    }

                    region.Add(inew);
                }

                density.Regions.Add(region);
            }

            return density;



        }



        /// <summary> 更换案例路径 P1= D:\WorkArea\3106  P2 = D:\WorkArea\数值模拟</summary>
        public void ChangePath(SimONData _simONData, string newFullPath, string tempOld)
        {
            string caseName = Path.GetFileNameWithoutExtension(newFullPath);
            

            string tempPath = Path.Combine(newFullPath, caseName);//  E:\\aaaa\\aaaa

            _simONData.FilePath = tempOld + KeyConfiger.SimONExtend;

            string oldFile = Path.Combine(newFullPath, Path.GetFileName(_simONData.FilePath));//  E:\\aaaa\\数值模拟.dat

            string newStr = Path.Combine(newFullPath, caseName + Path.GetExtension(oldFile));//  E:\\aaaa\\aaaa.dat

            // Todo ：更改文件路径  E:\\数值模拟\\数值模拟_prd.dat To:E:\\aaaa\\aaaa_prd.dat
            Func<string, string> replace = old => old.Replace(tempOld, tempPath);

            string oldCaseName = Path.GetFileNameWithoutExtension(_simONData.FilePath);//    数值模拟

            // Todo ：重新保存一份主文件 不加载INCLUDE 只处理主文件
            SimONData tempSimon = FileFactoryService.Instance.ThreadLoadFunc<SimONData>(() => new SimONData(newStr, null, k => false));
            var allInc = tempSimon.Key.FindAll<INCLUDE>();

            foreach (var item in allInc)
            {
                // Todo ：不保存 防止覆盖原INCLUDE文件 
                item.IsCreateFile = false;
                item.FileName = item.FileName.Replace(oldCaseName, caseName);
                item.FilePath = replace(item.FilePath);
            }
            tempSimon.Save();

            // Todo ：修改所有INCLUDE文件名 
            List<INCLUDE> includes = _simONData.Key.FindAll<INCLUDE>();

            foreach (var item in includes)
            {
                string includePath = Path.Combine(newFullPath, item.FileName);
                File.Move(includePath, replace(item.FilePath));
                item.FileName = item.FileName.Replace(oldCaseName, caseName);
                item.FilePath = replace(item.FilePath);
            }

            _simONData.FilePath = newStr;


            string hist = tempPath + KeyConfiger.HistroyFileName;

            string oldhist= Path.Combine(newFullPath, oldCaseName) + KeyConfiger.HistroyFileName;

            File.Move(oldhist, hist);
        }

    }
}
