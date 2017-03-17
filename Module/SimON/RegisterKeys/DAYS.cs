#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01
 * 文件名：START
 * 说明：
 * ROCK
             0            11
/
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 用于读取history.dat文件中生产详细信息 Add by lhj </summary>
    public class DAYS : ItemsKey<DAYS.Item>
    {
        public DAYS(string _name)
            : base(_name)
        {
            //  注销掉读取行名称信息
            this.BuilderHandler -= BaseKeyHandleFactory.Instance.InitLineHandler;
        }

        public Item GetSingleItem
        {
            get
            {
                if (this.Items.Count == 0)
                {
                    Item item = new Item();
                    this.Items.Add(item);
                    return item;

                }
                else
                {
                    return this.Items[0];
                }
            }
        }


        /// <summary> 排序</summary>
        public void OrderBy()
        {
            this.Items = this.Items.OrderBy(l => l.Time0).ToList();
        }


        /// <summary> 去重复 </summary>
        public void Distinct()
        {
            var its = this.Items.Distinct(new DAYS.ItemCompare()).ToList();

            this.Items.Clear();

            this.Items.AddRange(its);
        }

        private UnitType _unitType;
        /// <summary> 单位类型 </summary>
        public UnitType UnitType
        {
            get { return _unitType; }
            set { _unitType = value; }
        }



        string temp = "        SM3/DAY        SM3/DAY        SM3/DAY        SM3/DAY        SM3/DAY         BARSA            SM3/DAY";

        string tempField = "       STB/DAY       Mscf/DAY         STB/DAY        STB/DAY       Mscf/DAY           PISA       STB/DAY";

        protected override void ItemWriteKey(System.IO.StreamWriter writer)
        {
            writer.WriteLine();

            if (this.UnitType == UnitType.FIELD)
            {
                writer.WriteLine(this.Name + tempField);
            }
            else
            {
                writer.WriteLine(this.Name + temp);
            }


            for (int i = 0; i < this.Items.Count; i++)
            {
                writer.WriteLine(this.Items[i].ToString());
            }

            //writer.WriteLine(KeyConfiger.EndFlag);
        }



        public class Item : OPT.Product.SimalorManager.Item
        {

            private DateTime time0;
            /// <summary> 时间 </summary>
            public DateTime Time0
            {
                get { return time0; }
                set { time0 = value; }
            }


            private string csl1 = "0";
            /// <summary> HWatProdRate </summary>
            public string Csl1
            {
                get { return csl1; }
                set { csl1 = value; }
            }


            private string cql2 = "0";
            /// <summary> HGasProdRate </summary>
            public string Cql2
            {
                get { return cql2; }
                set { cql2 = value; }
            }


            private string cyl3 = "0";
            /// <summary> HOilProdRate </summary>
            public string Cyl3
            {
                get { return cyl3; }
                set { cyl3 = value; }
            }


            private string zsl4 = "0";
            /// <summary> HWatInjRate </summary>
            public string Zsl4
            {
                get { return zsl4; }
                set { zsl4 = value; }
            }


            private string zql5 = "0";
            /// <summary> HGasInjRate </summary>
            public string Zql5
            {
                get { return zql5; }
                set { zql5 = value; }
            }


            private string hbhp6 = "0";
            /// <summary> HBHP </summary>
            public string Hbhp6
            {
                get { return hbhp6; }
                set { hbhp6 = value; }
            }


            private string cyl7 = "0";
            /// <summary> HLiqProdRate </summary>
            public string Cyl7
            {
                get { return cyl7; }
                set { cyl7 = value; }
            }



            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, this.time0.ToString(KeyConfiger.TimeFormat), this.csl1.ToSDD(), this.cql2.ToSDD(), this.cyl3.ToSDD(), this.zsl4.ToSDD(), this.zql5.ToSDD(), this.hbhp6.ToSDD(), this.cyl7.ToSDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.time0 = DateTime.ParseExact(newStr[0], KeyConfiger.TimeFormat, CultureInfo.InvariantCulture);
                            break;
                        case 1:
                            this.csl1 = newStr[1];
                            break;
                        case 2:
                            this.cql2 = newStr[2];
                            break;
                        case 3:
                            this.cyl3 = newStr[3];
                            break;
                        case 4:
                            this.zsl4 = newStr[4];
                            break;
                        case 5:
                            this.zql5 = newStr[5];
                            break;
                        case 6:
                            this.hbhp6 = newStr[6];
                            break;
                        case 7:
                            this.cyl7 = newStr[7];
                            break;
                        default:
                            break;
                    }
                }
            }


            public bool Equals(object x, object y)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<DAYS.Item> GetAllItems()
        {
            return this.Items;
        }


        /// <summary> 自定义Item比较方法 </summary>
        public class ItemCompare : IEqualityComparer<Item>
        {
            public bool Equals(Item x, Item y)
            {
                //Check whether the compared objects reference the same data. 
                if (Object.ReferenceEquals(x, y)) return true;

                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                return x.Time0 == y.Time0;

            }

            public int GetHashCode(Item obj)
            {
                //Check whether the object is null  
                if (Object.ReferenceEquals(obj, null)) return 0;

                return (obj.Time0).GetHashCode();
            }
        }
    }
}
