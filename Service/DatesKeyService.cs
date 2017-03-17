using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    /// <summary> 有关日期的操作服务 </summary>
    public class DatesKeyService : ServiceFactory<DatesKeyService>
    {
        BaseKeyHandleFactory ss = new BaseKeyHandleFactory();
        /// <summary> 查找指定井名的生产模型起始时间 </summary>
        public DATES GetWellProStartDate(List<DATES> ds, string wellName)
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
        public DATES GetWellInjStartDate(List<DATES> ds, string wellName)
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
        public DATES GetWellInjHistStartDate(List<DATES> ds, string wellName)
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
        public DATES GetWellproHistStartDate(List<DATES> ds, string wellName)
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
        public DATES GetWellProStartDate(BaseKey key, string wellName)
        {
            List<DATES> ds = key.FindAll<DATES>();

            if (ds != null)
            {
                return this.GetWellProStartDate(ds, wellName);
            }

            return null;
        }

        /// <summary> 查找指定井ming的注入模型起始时间 </summary>
        public DATES GetWellInjStartDate(BaseKey key, string wellName)
        {

            List<DATES> ds = key.FindAll<DATES>();

            if (ds != null)
            {
                return this.GetWellInjStartDate(ds, wellName);
            }

            return null;
        }


        /// <summary> 解析时间 01  'JAN'  2001  / </summary>
        public DateTime GetDateTime(string str)
        {
            string line = str.Replace("'", "").Replace("JLY", "JUL");
            string format = "ddMMMyyyy";
            string[] strList = line.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
            string str3 = strList[0].PadLeft(2, '0') + strList[1] + strList[2];
            return DateTime.ParseExact(str3, format, CultureInfo.InvariantCulture);
        }

        /// <summary> 解析时间 20100101D  </summary>
        public DateTime GetSimONDateTime(string str)
        {
            string format = "yyyyMMdd";
            DateTime outTime;
            DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture,DateTimeStyles.None, out outTime);

            return outTime;
        }

    }
}
