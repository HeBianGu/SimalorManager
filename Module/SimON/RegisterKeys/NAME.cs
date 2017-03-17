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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 射孔数据 </summary>
    public class NAME : ItemsKey<NAME.Item>, IRootNode
    {
        public NAME(string _name)
            : base(_name)
        {
            this.BuilderHandler += (l, k) =>
                {

                    // Todo ：创建时做解析井名的处理 
                    this.WellName = this.Lines[0].Trim('\'');

                    this.Lines.RemoveAt(0);

                    return this;
                };
        }

        private string _wellName;
        /// <summary> 井名 </summary>
        public string WellName
        {
            get { return _wellName; }
            set { _wellName = value; }
        }

        protected override void ItemWriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine("NAME".ToDWithOutSpace() + this.WellName.ToEclStr());

            if (this.ParentKey != null && this.ParentKey.Name == "BaseFile")
            {
                writer.WriteLine("Time   HWatProdRate   HGasProdRate   HOilProdRate    HWatInjRate    HGasInjRate          HBHP       HLiqProdRate");
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                writer.WriteLine(this.Items[i].ToString());
            }
        }

        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 第一列是井射孔所在网格的i编号 </summary>
            public string i0;

            /// <summary> 第二列是井射孔所在网格的j编号 </summary>
            public string j1;

            /// <summary> 第三列是井射孔所在网格的起始k编号K1 </summary>
            public string k12;

            /// <summary> 第四列是井射孔所在网格的结束k编号K2 </summary>
            public string k23;

            /// <summary> 第五列是射孔段开关标示，“OPEN”为开，“SHUT”为关，SHUT的射孔段永远没有机会与油藏连接 </summary>
            public string kgbz4 = "OPEN";

            /// <summary> 第六列数据等于WI，单位：m.mD (米制)，feet.mD (英制) </summary>
            public string wi5 = "NA";

            /// <summary> 第七至九列分别是射孔在x, y, z轴上的投影长度，单位：m(米制)，feet(英制)。对于完全射穿的垂直井，x方向水平井，或y方向水平井，可将这三个数据设置为“0  0  DZ”，“DX  0  0”，或“0  DY  0”，让模拟器自动决定射孔长度。如果第七至九列有任意数据不为0，则第六列数据将被模拟器计算的值覆盖 </summary>
            public string dx6 = "0";

            /// <summary> 同xsfqh6 </summary>
            public string dy7 = "0";

            /// <summary> 同xsfqh6 </summary>
            public string dz8 = "0";

            /// <summary> 第十列是表皮系数(skin factor) </summary>
            public string bpxs9 = "0";

            /// <summary> 第十一列是井径，单位：m(米制)，feet(英制) </summary>
            public string jj10 = "0";

            /// <summary> 第十二列和第十三列分别是井节点长度（单位：m(米制)，feet(英制)）和井节点与垂直方向的夹角（单位：degree）。也可以将这两个数据中的任意一个设置为“”，模拟器会自动计算井节点长度和倾角 </summary>
            public string jjdcd11 = "0";

            /// <summary> 井节点与垂直方向的夹角 </summary>
            public string jjdjj12 = "NA";

            /// <summary> 第十四列是井壁粗糙元特征长度，用于井壁摩擦力计算，单位：m(米制)，feet(英制) </summary>
            public string jbccytzcd13 = "0.01";

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}";

            /// <summary> 标识是否绑定到项中 </summary>
            public bool isBinding = false;
            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, i0.ToSDD(), j1.ToSDD(), this.k12.ToSDD(),
                    this.k23.ToSDD(), this.kgbz4.ToSDD(), this.wi5.ToSDD(), dx6.ToSDD(),
                    dy7.ToSDD(), dz8.ToSDD(), bpxs9.ToSDD(), jj10.ToSDD(),
                    jjdcd11.ToSDD(), jjdjj12.ToSDD(), jbccytzcd13.ToSDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.i0 = newStr[0];
                            break;
                        case 1:
                            this.j1 = newStr[1];
                            break;
                        case 2:
                            this.k12 = newStr[2];
                            break;
                        case 3:
                            this.k23 = newStr[3];
                            break;
                        case 4:
                            this.kgbz4 = newStr[4];
                            break;
                        case 5:
                            this.wi5 = newStr[5];
                            break;
                        case 6:
                            this.dx6 = newStr[6];
                            break;
                        case 7:
                            this.dy7 = newStr[7];
                            break;
                        case 8:
                            this.dz8 = newStr[8];
                            break;
                        case 9:
                            this.bpxs9 = newStr[9];
                            break;
                        case 10:
                            this.jj10 = newStr[10];
                            break;
                        case 11:
                            this.jjdcd11 = newStr[11];
                            break;
                        case 12:
                            this.jjdjj12 = newStr[12];
                            break;
                        case 13:
                            this.jbccytzcd13 = newStr[13];
                            break;
                        default:
                            break;
                    }
                }
            }
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

                return x.i0 == y.i0 && x.j1 == y.j1 && x.k12 == y.k12 && x.k23 == y.k23;

            }

            public int GetHashCode(Item obj)
            {
                //Check whether the object is null  
                if (Object.ReferenceEquals(obj, null)) return 0;

                return (obj.i0 + obj.j1 + obj.k12 + obj.k23).GetHashCode();
            }
        }

        /// <summary> 去重复 </summary>
        public void Distinct()
        {
            var its = this.Items.Distinct(new NAME.ItemCompare()).ToList();

            this.Items.Clear();

            this.Items.AddRange(its);
        }

        /// <summary> 排序</summary>
        public void OrderBy()
        {
            this.Items = this.Items.OrderBy(l => l.k12.ToInt()).ThenBy(l=>l.j1.ToInt()).ThenBy(l=>l.i0.ToInt()).ToList();
            //this.Items = this.Items.OrderBy(l => l.k12.ToInt() * 100000000 + l.i0.ToInt() * 10000 + l.j1.ToInt()).ToList();
        }
    }
}
