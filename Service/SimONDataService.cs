using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using OPT.Product.SimalorManager.RegisterKeys.SimON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.Service
{
    /// <summary> SimON服务类 </summary>
    public class SimONDataService : ServiceFactory<SimONDataService>
    {
        /// <summary> 转换成_historyproduction.dat </summary>
        public List<NAME> ConvertToHistoryProduction(List<string> wells, List<TIME> times)
        {
            List<NAME> names = new List<NAME>();

            //  初始化名称
            wells.ForEach(l => names.Add(new NAME("NAME") { WellName = l }));

            foreach (var n in names)
            {
                //  查找包含井名的时间步
                var findTimes = times.FindAll(l => l.Find<WELL>() != null && l.Find<WELL>().WellName0 == n.WellName);

                DAYS days = new DAYS("DAYS");
                n.Add(days);

                foreach (var item in findTimes)
                {
                    DAYS.Item it = new DAYS.Item();
                    it.Time0 = item.Date;
                    days.Items.Add(it);

                    WELL well = item.Find<WELL>();

                    switch (well.ProType)
                    {
                        case SimONProductType.BHP:
                            break;
                        case SimONProductType.WRAT:
                            it.Csl1 = well.Jcyblxz2;
                            break;
                        case SimONProductType.GRAT:
                            it.Cql2 = well.Jcyblxz2;
                            break;
                        case SimONProductType.ORAT:
                            it.Cyl3 = well.Jcyblxz2;
                            break;
                        case SimONProductType.LRAT:
                            it.Cyl7 = well.Jcyblxz2;
                            break;
                        case SimONProductType.WIR:
                            it.Zsl4 = well.Jcyblxz2;
                            break;
                        case SimONProductType.GIR:
                            it.Zql5 = well.Jcyblxz2;
                            break;
                        case SimONProductType.WIBHP:
                            it.Zsl4 = well.Jcyblxz2;
                            break;
                        case SimONProductType.GIBHP:
                            it.Zql5 = well.Jcyblxz2;
                            break;
                        case SimONProductType.SHUT:                       // 关井
                            break;
                        default:
                            break;
                    }
                }
            }

            return names;
        }

        /// <summary> 获取当前时间前的所有生产信息 </summary>
        public List<WELL> GetAllWellBeforeTime(BaseKey sch, DateTime time = default(DateTime))
        {
            if (time == default(DateTime))
                time = DateTime.MaxValue;


            // Todo ：比当前时间小的所有WELL关键字 
            var wells = sch.FindAll<WELL>(l =>
            {
                if (l.ParentKey != null && l.ParentKey is TIME)
                {
                    TIME t = l.ParentKey as TIME;

                    return time.Date.Date <= time.Date;
                }
                else
                {
                    return false;
                }
            });

            return wells;
        }

        /// <summary> 获取当前时间前的所有完井信息 包含最后一个时间的井类型 </summary>
        public List<WellLocation> GetAllLocationOfBeforeTime(BaseKey sch, DateTime time = default(DateTime))
        {
            if (time == default(DateTime))
                time = DateTime.MaxValue;

            // Todo ：比当前时间小的所有WELL关键字 
            List<WELL> wells = this.GetAllWellBeforeTime(sch, time);

            // Todo ：获取不重复井名 
            var wellNames = wells.Select(l => l.WellName0).Distinct();

            List<WellLocation> wellLocations = new List<WellLocation>();

            foreach (var item in wellNames)
            {
                WellLocation wl = new WellLocation();
                wl.WellName = item;

                List<Point<int>> ps = new List<Point<int>>();

                Tuple<List<PERF>, SimONProductType> perf = this.GetAllLoaction(wells, item);

                wl.WellType = perf.Item2;

                foreach (var it in perf.Item1)
                {
                    Point<int> p = new Point<int>();
                    p.X = it.I0.ToInt();
                    p.Y = it.J1.ToInt();
                    p.Z = it.K12.ToInt();
                    ps.Add(p);
                }
                wl.Location = ps;

                wellLocations.Add(wl);

            }

            return wellLocations;


        }

        /// <summary> 获取当前时间前的所有完井信息 包含最后一个时间的井类型 </summary>
        public List<WellLocation> GetAllLocationOfBeforeTime(string casePath, string caseName, DateTime time = default(DateTime))
        {
            string schFile = Path.Combine(casePath, caseName + "_SCH.DAT");
            INCLUDE sch = FileFactoryService.Instance.ThreadLoadFromFile(schFile, SimKeyType.SimON);
            return SimONDataService.Instance.GetAllLocationOfBeforeTime(sch, time);
        }

        /// <summary> 获取指定井名的所有完井信息和最后一个时间点的井别 </summary>
        public Tuple<List<PERF>, SimONProductType> GetAllLoaction(List<WELL> wells, string wellName)
        {
            var locations = wells.FindAll(l => l.WellName0 == wellName);

            if (locations == null || locations.Count == 0) return null;

            Tuple<List<PERF>, SimONProductType> perf = new Tuple<List<PERF>, SimONProductType>(new List<PERF>(), locations.Last().ProType);

            // Todo ：获取最后一个存在完井信息的生产数据 
            WELL lastWellContain = locations.FindLast(l => l.Find<PERF>() != null);

            if (lastWellContain != null)
            {
                perf.Item1.AddRange(lastWellContain.FindAll<PERF>());
            }

            //locations.ForEach(l => perf.Item1.AddRange(l.FindAll<PERF>()));

            return perf;
        }

    }


    /// <summary> 井的射孔信息 </summary>
    public class WellLocation
    {
        string wellName;

        /// <summary> 井名 </summary>
        public string WellName
        {
            get { return wellName; }
            set { wellName = value; }
        }

        SimONProductType wellType;

        /// <summary> 井类型 </summary>
        public SimONProductType WellType
        {
            get { return wellType; }
            set { wellType = value; }
        }

        List<Point<int>> location;

        /// <summary> 井的射孔位置信息 </summary>
        public List<Point<int>> Location
        {
            get { return location; }
            set { location = value; }
        }


        /// <summary> 转换成网格索引格式 </summary>
        public List<int> ToCellIndex(int I, int J, int K)
        {
            List<int> cellIndexList = new List<int>();

            var order = location.OrderBy(l=>l.Z).ThenBy(l=>l.Y).ThenBy(l=>l.X);

            foreach (var w in order)
            {
                //cellIndexList.Add(w.X*J + w.Y + w.Z * I * J);

                cellIndexList.Add((w.X - 1) + (w.Y - 1) * I + (w.Z - 1) * I * J);
            }

            return cellIndexList;
        }


        //// Todo ：按 Z  Y X 排序 
        //Func<Point<int>, int> orderHandle = l => l.X + l.Y * 10 + l.Z * 100;

    }



    /// <summary> 三维泛型结构 </summary>
    public class Point<T>
    {
        T _x;

        public T X
        {
            get { return _x; }
            set { _x = value; }
        }

        T _y;

        public T Y
        {
            get { return _y; }
            set { _y = value; }
        }

        T _z;

        public T Z
        {
            get { return _z; }
            set { _z = value; }
        }
    }
}
