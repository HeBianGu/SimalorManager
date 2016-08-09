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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 井组 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class GRUPTREE : ItemsKey<GRUPTREE.Item>
    {
        public GRUPTREE(string _name)
            : base(_name)
        {

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

        /// <summary> 查找所有子节点 </summary>
        public List<Item> GetChilds(Item item)
        {
            return this.Items.FindAll(l => l.fjzm1 == item.zjzm0);
        }

        /// <summary> 查找所有子节点 </summary>
        public List<Item> GetChilds(string parentName)
        {
            return this.Items.FindAll(l => l.fjzm1 == parentName);
        }

        /// <summary>  查找指定集合中的子节点项 </summary>
        public static List<Item> GetChild(List<Item> its, Item it)
        {
            return its.FindAll(l => l.fjzm1 == it.zjzm0);
        }

        /// <summary> 查找Item所属时间步 </summary>
        public static DATES FindItemDates(List<DATES> grupDates, GRUPTREE.Item item)
        {
            foreach (DATES d in grupDates)
            {
                var gs = d.FindAll<GRUPTREE>();

                foreach (var g in gs)
                {
                    var fitem = g.Items.Find(l => l.zjzm0 == item.zjzm0);

                    if (fitem != null)
                    {
                        return d;

                    }
                }
            }

            return null;
        }

        public class Item : OPT.Product.SimalorManager.Item
        {
            /// <summary> 子井组名 </summary>
            public string zjzm0;

            /// <summary> 父井组名 </summary>
            public string fjzm1;

            string formatStr = "{0}{1}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr,
                    zjzm0.ToEclStr(),
                    fjzm1.ToEclStr());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                this.ID = Guid.NewGuid().ToString();

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.zjzm0 = newStr[0];
                            break;
                        case 1:
                            this.fjzm1 = newStr[1];
                            break;
                        default:
                            break;
                    }
                }
            }

            Item parentItem;
            /// <summary> 父节点 </summary>
            public Item ParentItem
            {
                get { return parentItem; }
                set { parentItem = value; }
            }

            List<Item> childItems;
            /// <summary> 子节点 </summary>
            public List<Item> ChildItems
            {
                get { return childItems; }
                set { childItems = value; }
            }
        }


    }


}
