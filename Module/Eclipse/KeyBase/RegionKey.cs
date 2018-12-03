using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> 带分区的关键字 </summary>
    public abstract class RegionKey<T> : Key, IRegionInterface where T : Item, new()
    {
        public RegionKey(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler += l =>
            {
                return l.Trim();
            };
        }

        public int RegionCount = 0;

        List<Region> regions = new List<Region>();

        /// <summary> 获取默认项 如果没有创建一个 </summary>
        public Item GetSingleItem()
        {
            return GetSingleRegion().GetSingleItem();
        }

        /// <summary> 获取默认分区 如果没有创建一个 </summary>
        public Region GetSingleRegion()
        {
            if (this.regions.Count == 0)
            {
                Region item = new Region(1);
                this.regions.Add(item);
                return item;

            }
            else
            {
                return this.regions[0];
            }
        }

        /// <summary> 创建分区 存在则不创建 </summary>
        public Region CreateRegion(int index)
        {
            if (this.regions.Count < index)
            {
                Region r = new Region(index);
                this.regions.Add(r);
            }

            return this.regions[index - 1];
        }

        public List<Region> Regions
        {
            get { return regions; }
            set { regions = value; }
        }

        /// <summary> 复制分区到指定分区号 如果存在删除重新复制 </summary>
        public Region CopyRegionTo(Region region, int index)
        {
            if (this.regions.Count < index)
            {
                Region r = region.Clone() as Region;
                this.regions.Add(r);
            }

            else
            {
                this.regions[index - 1] = region.Clone() as Region;
            }
            return this.regions[index - 1];

        }
        protected virtual void CmdGetWellItems()
        {
            this.Regions.Clear();

            string str = string.Empty;

            int regionNum = 1;

            Region pRegion = new Region(1);

            this.Regions.Add(pRegion);

            // Todo ：删除所有无效行 不包括分区行
            this.Lines.RemoveAll(l => !l.IsWorkLine() && !l.IsEndLine());

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i].ClearLine().Trim();

                //  读到结束符 增加分区
                if (str.StartsWith(KeyConfiger.EndFlag) && str.EndsWith(KeyConfiger.EndFlag))
                {
                    if (!this.Regions.Contains(pRegion) && pRegion.Count > 0)
                    {
                        //  只是结束符时 
                        this.Regions.Add(pRegion);
                    }

                    //  如果是最后一行的结束符 退出循环
                    if (i == Lines.Count - 1) break;

                    regionNum++;
                    pRegion = new Region(regionNum);
                    continue;
                }

                //  不为空的行不包含结束符
                if (str.IsWorkLine())
                {
                    List<string> newStr = str.Trim(KeyConfiger.ExcepFlag.ToCharArray()).EclExtendToArray();

                    if (newStr.Count > 0)
                    {
                        T pitem = new T();
                        pitem.Build(newStr);
                        //  标记行的ID位置
                        //Lines[i] = pitem.ID;

                        if (pitem != null)
                        {
                            pRegion.Add(pitem);
                        }
                    }

                    //  包含结束符创建分区
                    //if (str.EndsWith(KeyConfiger.EndFlag))
                    if (str.Contains(KeyConfiger.EndFlag))
                    {
                        if (!this.Regions.Contains(pRegion))
                        {
                            //  只是结束符时 
                            this.Regions.Add(pRegion);
                        }
                        regionNum++;
                        pRegion = new Region(regionNum);

                    }
                }

            }
        }

        /// <summary> 获取项字符串集合 </summary>
        protected virtual void CmdGetWellLines()
        {

            string s = string.Empty;

            this.Lines.Clear();

            for (int i = 0; i < Regions.Count; i++)
            {
                for (int j = 0; j < Regions[i].Count; j++)
                {
                    //int index = this.Lines.FindIndex(l => l == Regions[i][j].ID);

                    //if (index >= 0)
                    //{
                    //    //  找到直接插入
                    //    this.Lines[index] = Regions[i][j].ToString();
                    //}
                    //else
                    //{
                    //   没找到直接插入 有可能是新增
                    this.Lines.Add(Regions[i][j].ToString());
                    //}
                }
                //  增加分区标识
                this.Lines.Add(KeyConfiger.EndFlag);
            }
        }

        public override BaseKey ReadKeyLine(System.IO.StreamReader reader)
        {
            BaseKey overKey = base.ReadKeyLine(reader);
            CmdGetWellItems();
            return overKey;
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            CmdGetWellLines();
            base.WriteKey(writer);
        }


        public void Clear()
        {
            base.Clear();
            this.Regions.Clear();
        }

        /// <summary> 分区 </summary>
        public class Region : List<T>, ICloneable
        {
            public Region(int index)
            {
                RegionIndex = index;
            }
            public int RegionIndex;
            public T GetSingleItem()
            {

                if (this.Count == 0)
                {
                    T item = new T();
                    this.Add(item);
                    return item;

                }
                else
                {
                    return this[0] as T;
                }
            }
            public object Clone()
            {
                if (typeof(T) is ICloneable)
                {
                    Region newRegion = new Region(RegionIndex);
                    foreach (var v in this)
                    {
                        ICloneable c = v as ICloneable;
                        newRegion.Add(c.Clone() as T);
                    }
                    return newRegion;
                }
                else
                {
                    throw new Exception("当前关键字项没有实现ICloneable接口，无法实现复制，请实现该接口后重试！");

                }
            }

            /// <summary> 复制成指定分区 </summary>
            public object Clone(int regionIndex)
            {
                Region newReion = this.Clone() as Region;

                newReion.RegionIndex = regionIndex;

                return newReion;
            }
        }

        public void TransToSimONRegion()
        {
            foreach (Region r in this.regions)
            {
                var key = KeyConfigerFactroy.Instance.CreateKey<BaseKey>(this.Name, SimKeyType.Eclipse);
                //  拆分分区节点
                this.ParentKey.Add(key);

                RegionKey<T> rkey = key as RegionKey<T>;

                //  添加分区
                rkey.Regions.Add(r);

            }

            //  删除本分区
            this.ParentKey.Delete(this);
        }
    }


    /// <summary> 分区接口 </summary>
    interface IRegionInterface
    {
        /// <summary> 转换成SimON格式的分区 </summary>
        void TransToSimONRegion();
    }
}
