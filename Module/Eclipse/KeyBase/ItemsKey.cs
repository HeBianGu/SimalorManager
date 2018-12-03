using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> 带有Item的关键字抽象类 </summary>
    public class ItemsKey<T> : BaseKey, ItemsKeyInterface where T : Item, new()
    {
        public ItemsKey(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler += l =>
              {
                  return l.Trim();
              };
        }

        protected virtual void CmdGetWellItems()
        {
            //  清理项
            Items.Clear();

            string str = string.Empty;

            this.Lines.RemoveAll(l => !l.IsWorkLine());

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i].ClearLine();

                //  读到结束符不继续读取
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    break;
                }

                List<string> newStr = str.EclExceptSpaceToArray();

                if (newStr.Count > 0)
                {
                    T pitem = new T();
                    pitem.Build(newStr);
                    //  标记行的ID位置
                    //Lines[i] = pitem.ID;
                    if (pitem != null)
                    {
                        Items.Add(pitem);
                    }
                }

            }
        }

        /// <summary> 获取项字符串集合 </summary>
        protected virtual void CmdGetWellLines(List<T> pItems)
        {
            this.Lines.Clear();

            string s = string.Empty;

            this.ClearItemLine();

            for (int i = 0; i < pItems.Count; i++)
            {
                this.Lines.Add(pItems[i].ToString());
            }

            this.Lines.Add(KeyConfiger.EndFlag);

        }

        public T GetSingleItem()
        {
            if (this.Items.Count == 0)
            {
                T item = new T();
                this.Items.Add(item);
                return item;

            }
            else
            {
                return this.Items[0];
            }
        }


        List<T> items = new List<T>();

        /// <summary> 包含项 </summary>
        public List<T> Items
        {
            get { return items; }
            set { items = value; }
        }

        public override BaseKey ReadKeyLine(System.IO.StreamReader reader)
        {
            BaseKey overKey = base.ReadKeyLine(reader);

            CmdGetWellItems();

            return overKey;
        }

        protected virtual void ItemWriteKey(System.IO.StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.Name);

            for (int i = 0; i < this.Items.Count; i++)
            {
                writer.WriteLine(this.Items[i].ToString());
            }

            writer.WriteLine(KeyConfiger.EndFlag);
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            this.Lines.Clear();

            this.ItemWriteKey(writer);

            base.WriteKey(writer);
        }

        /// <summary> 清理所有有效行  包含 “/” 读到 "/" 结束符结束 </summary>
        public void ClearItemLine()
        {
            string str = string.Empty;

            List<string> removeLine = new List<string>();

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i];
                //  读到结束符不继续读取
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    break;
                }
                //  不以"/"开头 以 "/" 结束的行
                if (!string.IsNullOrEmpty(str) && str.EndsWith("/"))
                {
                    removeLine.Add(str);
                }
            }
            foreach (var v in removeLine)
            {
                this.Lines.Remove(v);
            }
        }


        public IEnumerable<Item> GetItems()
        {
            return this.items;
        }
    }

    public interface ItemsKeyInterface
    {
        IEnumerable<Item> GetItems();
    }

    public static class ItemExtend
    {

        /// <summary> 合并数组为一个关键字 需要通过协变方式访问 IEnumerable  items = grop; </summary>
        public static List<T> CombineItem<T>(this IEnumerable<ItemsKey<T>> itemKey) where T : Item, new()
        {
            List<T> itemAll = new List<T>();

            foreach (var item in itemKey)
            {
                itemAll.AddRange(item.Items);
            }

            return itemAll;
        }

    }
    /// <summary> Item项 </summary>
    public abstract class Item
    {

        public bool ReadOnly = false;

        string formatStr;

        public string ID
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        public abstract void Build(List<string> strs);

        public bool IsVaild()
        {
            var fields = this.GetType().GetFields();

            foreach (var item in fields)
            {
                object v = item.GetValue(this);
                // Todo ：遍历所有字段有一个满足条件即为不可用
                if (v == null || v == string.Empty)
                {
                    return false;
                }

                // Todo ：是Eclipse和SimON默认值为不可用 
                if (v.ToString().EndsWith(KeyConfiger.EclipseDefalt) || v.ToString().EndsWith(KeyConfiger.SimONDefalt))
                {
                    return false;
                }
            }

            return true;
        }

    }

    /// <summary> Item项 </summary>
    public abstract class ItemNormal : Item, ICloneable
    {
        public abstract object Clone();
    }

    /// <summary> 生产模型数据项名称接口 </summary>
    public interface IProductItem
    {
        /// <summary> 生产模型数据项名称 </summary>
        string Name
        {
            get;
            set;
        }
    }

    /// <summary> 生产模型事件接口 </summary>
    public interface IProductEvent
    {
        /// <summary> 设置新井名 </summary> 
        void SetWellName(string wellName);

        ///// <summary> 设置新井名 </summary> 
        //void SetWellName(string oldName,string wellName);
    }
}
