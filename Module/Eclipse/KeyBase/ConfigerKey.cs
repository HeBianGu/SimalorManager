#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 15:58:43
 * 文件名：NormalKey
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
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
//using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 写入方法是直接调用this.ToString()方法 </summary>
    public abstract class ConfigerKey : Key
    {
        public ConfigerKey(string _name)
            : base(_name)
        {
            this.BuilderHandler = (l, k) =>
            {
                CmdToItems();
            };
        }

        void CmdToItems()
        {
            string str = null;

            this.Lines.RemoveAll(l => l.StartsWith(KeyConfiger.ExcepFlag));

            for (int i = 0; i < Lines.Count; i++)
            {
                if (i == 0)
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
            writer.WriteLine();
            writer.WriteLine(this.Name);
            writer.WriteLine(this.ToString());
            writer.WriteLine();
        }


        public abstract void Build(List<string> newStr);
    }
}
