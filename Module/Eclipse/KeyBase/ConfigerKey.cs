#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 15:58:43
 * 说明：
 * 
 * 此方法和Key读取方法区别在于屏蔽掉 读取NormalKey 
   TITLE
   LN3T3
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
//using HeBianGu.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary>配置信息解析抽象类型 </summary>
    public abstract class ConfigerKey : BaseKey
    {
        public ConfigerKey(string _name)
            : base(_name)
        {
            this.BuilderHandler += (l, k) =>
            {

                CmdToItems();

                return this;
            };
        }

        bool isReadAllLine = false;
        /// <summary> 是否解析所有行 </summary>
        protected bool IsReadAllLine
        {
            get { return isReadAllLine; }
            set { isReadAllLine = value; }
        }

        protected virtual void CmdToItems()
        {
            string str = null;

            this.Lines.RemoveAll(l => !l.IsWorkLine());

            for (int i = 0; i < Lines.Count; i++)
            {
                if (!isReadAllLine)
                {
                    if (i == 0)
                    {
                        str = Lines[i];

                        List<string> newStr = str.EclExtendToArray();

                        Build(newStr);

                    }
                }
                else
                {
                    str = Lines[i];

                    List<string> newStr = str.EclExtendToArray();

                    Build(newStr);
                }


            }
        }

        /// <summary> 只调用ToString()方法 </summary>
        public override void WriteKey(StreamWriter writer)
        {
            WriteKeyMethod(writer);
            this.Lines.Clear();
            base.WriteKey(writer);
        }

        /// <summary> 提供子类扩展的写当前关键字方法 </summary>
        public virtual void WriteKeyMethod(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.GetType().Name);
            writer.WriteLine(this.ToString());
        }


        public abstract void Build(List<string> newStr);

    }


    /// <summary> 将多行合并一行的配置信息解析抽象类型 </summary>
    public abstract class MergeConfiger : ConfigerKey
    {
        public MergeConfiger(string _name)
            : base(_name)
        {
        }

        protected override void CmdToItems()
        {
            StringBuilder str = new StringBuilder();

            this.Lines.RemoveAll(l => !l.IsWorkLine());

            for (int i = 0; i < Lines.Count; i++)
            {
                str.Append(Lines[i].ClearLine() + " ");
            }

            List<string> newStr = str.ToString().EclExtendToArray();

            Build(newStr);
        }
    }
}
