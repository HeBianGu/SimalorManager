#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/18 11:28:21
 * 文件名：SingleKey
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 只有名称没有内容的关键字 </summary>
    public abstract class CheckListKey : ItemsKey<CheckListKey.Item>, OutPutBindKey
    {
        public CheckListKey(string _name)
            : base(_name)
        {

        }


        string isCheck;

        public object IsCheck
        {
            get
            {
                return isCheck;
            }
            set
            {
                if (value == null)
                {
                    isCheck = string.Empty;
                }
                else
                {
                    isCheck = value.ToString();
                }

            }
        }


        public string OutName
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        protected override void CmdGetWellItems()
        {

            ClearItem();

            string str = string.Empty;

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i];
                //  读到结束符不继续读取
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    break;
                }

                ////  不以"/"开头 以 "/" 结束的行
                //if (!string.IsNullOrEmpty(str) && str.EndsWith("/"))
                //{
                //    List<string> newStr = str.EclExtendToArray();

                //    if (newStr.Count > 0)
                //    {
                //        T pitem = new T();
                //        pitem.Build(newStr);
                //        //  标记行的ID位置
                //        Lines[i] = pitem.ID;
                //        if (pitem != null)
                //        {
                //            Items.Add(pitem);
                //        }
                //    }
                //}

                ////  不为空的行
                //if (!string.IsNullOrEmpty(str) && !str.StartsWith(KeyConfiger.ExcepFlag))
                //{
                if (str.IsWorkLine())
                {
                    List<string> newStr = str.EclExtendToArray();

                    if (newStr.Count > 0)
                    {
                        Item pitem = new Item();
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

            this.isCheck = ConvertToArrStr();
        }

        /// <summary> 转换成绑定字符串e </summary>
        string ConvertToArrStr()
        {
            StringBuilder sb = new StringBuilder();
            this.Items.ForEach(l => sb.Append(l.WellName.Trim() + ","));
            return sb.ToString();
        }

        /// <summary> 转换成项</summary>
        void ConvertToItem()
        {
            string[] strTemp = this.isCheck.Trim(',').Split(',');

            this.Items.Clear();
            foreach (var item in strTemp)
            {
                Item it = new Item();
                it.WellName = item;
                this.Items.Add(it);

            }
        }

        /// <summary> 获取项字符串集合 </summary>
        protected override void CmdGetWellLines(List<Item> pItems)
        {
            ConvertToItem();

            this.Lines.Clear();

            string s = string.Empty;

            this.ClearItemLine();

            for (int i = 0; i < pItems.Count; i++)
            {
                int index = this.Lines.FindIndex(l => l == pItems[i].ID);

                if (index >= 0)
                {
                    //  找到直接插入
                    this.Lines[index] = pItems[i].ToString();
                }
                else
                {
                    //   没找到直接插入 有可能是新增
                    this.Lines.Add(pItems[i].ToString());
                }
            }

            this.Lines.Add(KeyConfiger.EndFlag);

        }


        public class Item : OPT.Product.SimalorManager.Item
        {

            public string WellName;


            string formatStr = "{0}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, WellName.ToEclStr()); ;
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {
                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.WellName = newStr[0];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public string OutType
        {
            get
            {
                return OutPutType.GetAttribute<DescriptionAttribute>().Description;
            }
        }

        /// <summary> 输出指标类型 </summary>
        public OutPutGroupType OutPutType
        {
            get
            {
                if (this.GetType().Name.StartsWith("W") && this.GetType().Name.EndsWith("H"))
                {
                    return OutPutGroupType.WellHist;
                }
                else if (this.GetType().Name.StartsWith("W") && !this.GetType().Name.EndsWith("H"))
                {
                    return OutPutGroupType.WellSingle;
                }
                else if (this.GetType().Name.StartsWith("G") && this.GetType().Name.EndsWith("H"))
                {
                    return OutPutGroupType.GroupHist;
                }
                else if (this.GetType().Name.StartsWith("G") && !this.GetType().Name.EndsWith("H"))
                {
                    return OutPutGroupType.GroupSingle;
                }
                else
                {
                    return OutPutGroupType.Others;
                }
            }
        }


        public string Description
        {
            get { return this.TitleStr; }
        }
    }

    /// <summary> 输出指标分组 </summary>
    public enum OutPutGroupType
    {
        [Description("单井历史输出项")]
        WellHist = 0,
        [Description("单井指标输出项")]
        WellSingle,
        [Description("井组历史输出项")]
        GroupHist,
        [Description("井组指标输出项")]
        GroupSingle,
        [Description("其他指标项")]
        Others
    }
}
