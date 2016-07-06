using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    /// <summary> 有关日期的操作扩展方法 </summary>
    public static class DatesExMethod
    {
        /// <summary> 查找指定井名的生产模型起始时间 </summary>
        public static DATES GetWellProStartDate(this List<DATES> ds, string wellName)
        {
            //  查找生产数据其实时间
            foreach (DATES d in ds)
            {
                List<WCONPROD> ps = d.FindAll<WCONPROD>();

                if (ps.Exists(l => l.Items.Exists(k => k.jm0 == wellName)))
                {
                    //  找到对应生产井井名
                    return d;

                }
            }
            return null;
        }

        /// <summary> 查找指定井ming的注入模型起始时间 </summary>
        public static DATES GetWellInjStartDate(this List<DATES> ds, string wellName)
        {
            //  查找生产数据其实时间
            foreach (DATES d in ds)
            {
                List<WCONINJE> ps = d.FindAll<WCONINJE>();

                if (ps.Exists(l => l.Items.Exists(k => k.jm0 == wellName)))
                {
                    //  找到对应生产井井名
                    return d;

                }
            }
            return null;
        }

        /// <summary> 查找指定井ming的注入模型起始时间 </summary>
        public static DATES GetWellInjHistStartDate(this List<DATES> ds, string wellName)
        {
            //  查找生产数据其实时间
            foreach (DATES d in ds)
            {
                List<WCONINJH> ps = d.FindAll<WCONINJH>();

                if (ps.Exists(l => l.Items.Exists(k => k.jm0 == wellName)))
                {
                    //  找到对应生产井井名
                    return d;

                }
            }
            return null;
        }

        /// <summary> 查找指定井ming的注入模型起始时间 </summary>
        public static DATES GetWellproHistStartDate(this List<DATES> ds, string wellName)
        {
            //  查找生产数据其实时间
            foreach (DATES d in ds)
            {
                List<WCONHIST> ps = d.FindAll<WCONHIST>();

                if (ps.Exists(l => l.Items.Exists(k => k.wellName0 == wellName)))
                {
                    //  找到对应生产井井名
                    return d;

                }
            }
            return null;
        }


        /// <summary> 查找指定井名的生产模型起始时间 </summary>
        public static DATES GetWellProStartDate(this BaseKey key, string wellName)
        {
            List<DATES> ds = key.FindAll<DATES>();

            if (ds != null)
            {
                return ds.GetWellProStartDate(wellName);
            }

            return null;
        }

        /// <summary> 查找指定井ming的注入模型起始时间 </summary>
        public static DATES GetWellInjStartDate(this BaseKey key, string wellName)
        {

            List<DATES> ds = key.FindAll<DATES>();

            if (ds != null)
            {
                return ds.GetWellInjStartDate(wellName);
            }

            return null;
        }

    }
}
