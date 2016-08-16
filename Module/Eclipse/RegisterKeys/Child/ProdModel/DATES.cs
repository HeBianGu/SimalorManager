#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:49
 * 文件名：DATES
 * 说明：
 * 对应格式如下
DATES
 1 'APR' 2011 /
/

WCONHIST 
       'C1'      'OPEN'      'GRAT'     10.041      2.186 121981.793  2*    201.500  2* /
       'C3'      'OPEN'      'GRAT'     31.083      2.344 163815.680  2*    210.700  2* /
/

-- 119.000000 days from start of simulation ( 2 'JAN' 2011 )
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class DATES : BaseKey, IComparable, IRootNode
    {
        public DATES(string _name)
            : base(_name)
        {
            this.BuilderHandler = CmdToItems;
        }

        DateTime _dateTime = default(DateTime);

        /// <summary> 时间步时间 </summary>
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
            }
        }

        public DATES(string _name, DateTime ptime)
            : base(_name)
        {
            _dateTime = ptime;

            SetDateTime(_dateTime);
        }



        #region - 基本操作 -


        /// <summary> 重启时间比较方法 </summary>
        public int CompareTo(object obj)
        {
            DATES pdate = obj as DATES;

            if (pdate == null)
            {
                return -1;
            }
            else
            {
                if (pdate.DateTime.Date > this.DateTime.Date)
                {
                    return 1;
                }
                else if (pdate.DateTime.Date.Equals(this.DateTime.Date))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary> 是否包含指定日期 </summary>
        public bool IsEquls(DateTime time)
        {
            return this.DateTime.Date.Equals(time);
        }

        /// <summary> 设置重启的时间 </summary>
        public void SetDateTime(DateTime pTime)
        {
            this._dateTime = pTime;
        }

        public override string ToString()
        {
            return this.DateTime.ToString("yyyy-MM-dd");
        }


        /// <summary> 复制时间信息 (不包括子节点事件) </summary>
        public DATES Clone()
        {
            return new DATES("DATES", this.DateTime);
        }

        public override void WriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.Name);

            this.Lines.Clear();
            this.Lines.Add(this.ToFileStr());
            this.Lines.Add(KeyConfiger.EndFlag);

            base.WriteKey(writer);
        }

        #endregion


        /// <summary> 转换成数模文件格式 </summary>
        string ToFileStr()
        {
            string formatStr = this._dateTime.ToString("dd  'MMM'  yyyy  /",
                CultureInfo.InvariantCulture).Replace("MMM",
                "'" + this._dateTime.ToString("MMM", CultureInfo.InvariantCulture) + "'").ToUpper();

            return formatStr;
        }

        /// <summary> 对子关键字中排序 </summary>
        public void Sort()
        {
            #region - VFP表 -

            var vfpprod = this.FindAll<VFPPROD>();

            this.Keys.RemoveAll(l => l is VFPPROD);

            if (vfpprod != null && vfpprod.Count > 0)
            {
                this.Keys.AddRange(vfpprod);
            }

            var vfpinj = this.FindAll<VFPINJ>();
            this.Keys.RemoveAll(l => l is VFPINJ);
            if (vfpinj != null && vfpinj.Count > 0)
            {
                this.Keys.AddRange(vfpinj);
            }

            #endregion

            #region - 井定义 -

            var welspecs = this.FindAll<WELSPECS>();
            this.Keys.RemoveAll(l => l is WELSPECS);
            if (welspecs != null && welspecs.Count > 0)
            {
                IEnumerable<ItemsKey<WELSPECS.Item>> items = welspecs;

                WELSPECS p = new WELSPECS("WELSPECS");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var welspecl = this.FindAll<WELSPECL>();
            this.Keys.RemoveAll(l => l is WELSPECL);
            if (welspecl != null && welspecl.Count > 0)
            {
                IEnumerable<ItemsKey<WELSPECL.Item>> items = welspecl;

                WELSPECL p = new WELSPECL("WELSPECL");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            #endregion

            #region - 井组定义 -

            var grop = this.FindAll<GRUPTREE>();
            this.Keys.RemoveAll(l => l is GRUPTREE);
            if (grop != null && grop.Count > 0)
            {

                IEnumerable<ItemsKey<GRUPTREE.Item>> items = grop;

                GRUPTREE p = new GRUPTREE("GRUPTREE");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            #endregion

            #region - 关井/重开井 -

            var welopen = this.FindAll<WELOPEN>();
            this.Keys.RemoveAll(l => l is WELOPEN);
            if (welopen != null && welopen.Count > 0)
            {
                IEnumerable<ItemsKey<WELOPEN.Item>> items = welopen;

                WELOPEN p = new WELOPEN("WELOPEN");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }
            #endregion

            #region - 生产数据 -

            var compdat = this.FindAll<COMPDAT>();
            this.Keys.RemoveAll(l => l is COMPDAT);
            if (compdat != null && compdat.Count > 0)
            {
                IEnumerable<ItemsKey<COMPDAT.Item>> items = compdat;

                COMPDAT p = new COMPDAT("COMPDAT");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var compdatl = this.FindAll<COMPDATL>();
            this.Keys.RemoveAll(l => l is COMPDATL);
            if (compdatl != null && compdatl.Count > 0)
            {
                IEnumerable<ItemsKey<COMPDATL.Item>> items = compdatl;

                COMPDATL p = new COMPDATL("COMPDATL");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var wconhist = this.FindAll<WCONHIST>();
            this.Keys.RemoveAll(l => l is WCONHIST);
            if (wconhist != null && wconhist.Count > 0)
            {
                IEnumerable<ItemsKey<WCONHIST.Item>> items = wconhist;


                WCONHIST p = new WCONHIST("WCONHIST");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }
            var wconprod = this.FindAll<WCONPROD>();
            this.Keys.RemoveAll(l => l is WCONPROD);
            if (wconprod != null && wconprod.Count > 0)
            {
                IEnumerable<ItemsKey<WCONPROD.ItemHY>> items = wconprod;

                WCONPROD p = new WCONPROD("WCONPROD");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }
            var wconinjh = this.FindAll<WCONINJH>();
            this.Keys.RemoveAll(l => l is WCONINJH);
            if (wconinjh != null && wconinjh.Count > 0)
            {
                IEnumerable<ItemsKey<WCONINJH.Item>> items = wconinjh;


                WCONINJH p = new WCONINJH("WCONINJH");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }
            var wconinje = this.FindAll<WCONINJE>();
            this.Keys.RemoveAll(l => l is WCONINJE);
            if (wconinje != null && wconinje.Count > 0)
            {
                IEnumerable<ItemsKey<WCONINJE.ItemHY>> items = wconinje;

                WCONINJE p = new WCONINJE("WCONINJE");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }
            var wecon = this.FindAll<WECON>();
            this.Keys.RemoveAll(l => l is WECON);

            if (wecon != null && wecon.Count > 0)
            {
                IEnumerable<ItemsKey<WECON.Item>> items = wecon;


                WECON p = new WECON("WECON");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            #endregion

            List<WPIMULT> wpimult = this.FindAll<WPIMULT>();
            this.Keys.RemoveAll(l => l is WPIMULT);
            if (wpimult != null && wpimult.Count > 0)
            {
                IEnumerable<ItemsKey<WPIMULT.Item>> items = wpimult;

                WPIMULT p = new WPIMULT("WPIMULT");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }
            var wdfac = this.FindAll<WDFAC>();
            this.Keys.RemoveAll(l => l is WDFAC);
            if (wdfac != null && wdfac.Count > 0)
            {
                IEnumerable<ItemsKey<WDFAC.Item>> items = wdfac;

                WDFAC p = new WDFAC("WDFAC");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }
            var weltarg = this.FindAll<WELTARG>();
            this.Keys.RemoveAll(l => l is WELTARG);
            if (weltarg != null && weltarg.Count > 0)
            {
                IEnumerable<ItemsKey<WELTARG.Item>> items = weltarg;

                WELTARG p = new WELTARG("WELTARG");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var wgrupcon = this.FindAll<WGRUPCON>();
            this.Keys.RemoveAll(l => l is WGRUPCON);
            if (wgrupcon != null && wgrupcon.Count > 0)
            {
                IEnumerable<ItemsKey<WGRUPCON.Item>> items = wgrupcon;

                WGRUPCON p = new WGRUPCON("WGRUPCON");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var gconprod = this.FindAll<GCONPROD>();
            this.Keys.RemoveAll(l => l is GCONPROD);
            if (gconprod != null && gconprod.Count > 0)
            {
                IEnumerable<ItemsKey<GCONPROD.Item>> items = gconprod;


                GCONPROD p = new GCONPROD("GCONPROD");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var gconinje = this.FindAll<GCONINJE>();
            this.Keys.RemoveAll(l => l is GCONINJE);
            if (gconinje != null && gconinje.Count > 0)
            {
                IEnumerable<ItemsKey<GCONINJE.Item>> items = gconinje;

                GCONINJE p = new GCONINJE("GCONINJE");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var gecon = this.FindAll<GECON>();
            this.Keys.RemoveAll(l => l is GECON);
            if (gecon != null && gecon.Count > 0)
            {
                IEnumerable<ItemsKey<GECON.Item>> items = gecon;


                GECON p = new GECON("GECON");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var wfracp = this.FindAll<WFRACP>();
            this.Keys.RemoveAll(l => l is WFRACP);
            if (wfracp != null && wfracp.Count > 0)
            {
                IEnumerable<ItemsKey<WFRACP.Item>> items = wfracp;

                WFRACP p = new WFRACP("WFRACP");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var wefac = this.FindAll<WEFAC>();
            this.Keys.RemoveAll(l => l is WEFAC);
            if (wefac != null && wefac.Count > 0)
            {
                IEnumerable<ItemsKey<WEFAC.Item>> items = wefac;

                WEFAC p = new WEFAC("WEFAC");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var gefac = this.FindAll<GEFAC>();
            this.Keys.RemoveAll(l => l is GEFAC);
            if (gefac != null && gefac.Count > 0)
            {
                IEnumerable<ItemsKey<GEFAC.Item>> items = gefac;

                GEFAC p = new GEFAC("GEFAC");

                p.Items.AddRange(items.CombineItem());

                this.Add(p);
            }

            var end = this.FindAll<END>();
            this.Keys.RemoveAll(l => l is END);
            if (end != null && end.Count > 0)
            {
                this.Keys.AddRange(end);
            }
        }

        /// <summary> 改变井名 </summary>
        public void ChangeAllWellName(string newWellName)
        {

            var events = this.FindAll<IProductEvent>();

            if (events == null || events.Count == 0) return;

            events.ForEach(l => l.SetWellName(newWellName));

        }


        bool isContain;

        /// <summary> 是否包含End关键字 </summary>
        public bool IsContainEnd
        {
            get
            {
                //return isContain = this.Find<END>() != null;
                isContain = this.Find<END>() != null;
                return isContain;
            }
            set
            {

                if (value)
                {
                    //  是则没有增加
                    if (this.Find<END>() == null)
                    {
                        END end = new END("END");

                        this.Add(end);
                    }
                }
                else
                {
                    //  否则删除节点下所有End

                    END end = this.Find<END>();

                    if (end != null)
                    {
                        this.Keys.Remove(end);
                    }
                }
                //isContain = value;

            }
        }

        BaseKey CmdToItems(BaseKey last, BaseKey per)
        {
            string str = null;

            this.Lines.RemoveAll(l => !l.IsWorkLine());

            BaseKey outKey = null;

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i];

                List<string> newStr = str.EclExtendToArray();

                outKey = Build(newStr, last, per);

            }

            return outKey;
        }

        public BaseKey Build(List<string> newStr, BaseKey last, BaseKey per)
        {

            if (this.DateTime.Equals(default(DateTime)))
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < newStr.Count; i++)
                {
                    sb.Append(newStr[i] + " ");
                }

                this.DateTime = DatesKeyService.Instance.GetDateTime(sb.ToString());

                return this;
            }
            else
            {
                if (this.ParentKey == null) return null;

                //   兼容格式 
                //   DATES
                //   31 'DEC' 2015/
                //   31 'DEC' 2016/

                DATES newDates = new DATES("DATES");

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < newStr.Count; i++)
                {
                    sb.Append(newStr[i] + " ");
                }

                newDates.DateTime = DatesKeyService.Instance.GetDateTime(sb.ToString());

                this.ParentKey.Add(newDates);

                //  上一节点指向新建的时间节点
                return newDates;

            }
        }


    }
}
