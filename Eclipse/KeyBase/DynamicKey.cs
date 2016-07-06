#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：GCONPROD
 * 说明：
 * 
WEFAC
‘P25’  0.89  NO/
‘P12’  0.7  /
/


 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 动态列表抽象类 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public abstract class DynamicKey : BaseKey
    {
        public DynamicKey(string _name)
            : base(_name)
        {

        }

        List<StringModel> items = new List<StringModel>();

        public List<StringModel> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary> 重建项 </summary>
        public void BuildItem(int count,string defaultValue=null )
        {
            items.Clear();

            for(int i=0;i<count;i++)
            {
                StringModel sm = new StringModel();
                sm.Index = (i + 1).ToString();
                sm.Value = defaultValue;
                this.items.Add(sm);
            }
        }

        protected virtual void CmdGetWellItems()
        {
            this.items.Clear();

            string str = string.Empty;

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i];
                //  读到结束符不继续读取
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    break;
                }

                //  不为空的行
                if (str.IsWorkLine())
                {
                    List<string> newStr = str.EclExceptSpaceToArray();

                    if (newStr.Count > 0)
                    {
                        int index = 1;
                        newStr.ForEach(l => items.Add(StringModel.Build(l, (index++).ToString())));
                    }
                }

            }
        }

        /// <summary> 获取项字符串集合 </summary>
        protected virtual void CmdGetWellLines(List<StringModel> pItems)
        {
            this.Lines.Clear();

            string s = string.Empty;

            for (int i = 0; i < pItems.Count; i++)
            {
                //   没找到直接插入 有可能是新增
                this.Lines.Add(pItems[i].Value.ToEclStr());
            }

            this.Lines.Add(KeyConfiger.EndFlag);

        }


        public override BaseKey ReadKeyLine(System.IO.StreamReader reader)
        {

            string tempStr = null;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().TrimEnd();

                //  读到结束标记退出
                if (tempStr.EndsWith(KeyConfiger.EndFlag))
                {
                    this.Lines.Add(tempStr);
                    break;
                }
                   

                if (tempStr.IsWorkLine())
                {
                    this.Lines.Add(tempStr);
                }
            }

            CmdGetWellItems();

            return this;
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            writer.WriteLine(this.Name);

            CmdGetWellLines(Items);

            base.WriteKey(writer);
        }

    }

    public class StringModel
    {
        public static StringModel Build(string stringTemp, string index = null)
        {
            StringModel m = new StringModel();
            m.Value = stringTemp;
            m.index = index;
            return m;
        }

        string index;

        public string Index
        {
            get { return index; }
            set { index = value; }
        }

        string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }


    }



}
