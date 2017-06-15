#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/4/12 14:36:24
 * 文件名：FinConfigerKey
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{

    /// <summary> 加密区格式解析规则 示例：NXFIN  4   3   2 </summary>
    public abstract class FinConfigerKey : ConfigerKey
    {
        public FinConfigerKey(string _name)
            : base(_name)
        {

        }

        List<StringModel> items = new List<StringModel>();

        public List<StringModel> Items
        {
            get { return items; }
            set { items = value; }
        }

        public override void Build(List<string> newStr)
        {
            for (int i = 0; i < newStr.Count; i++)
            {
                items.Add(StringModel.Build(newStr[i], (i + 1).ToString()));
            }
        }

        /// <summary> 重建项 </summary>
        public void BuildItem(int count, string defaultValue = null)
        {
            items.Clear();

            for (int i = 0; i < count; i++)
            {
                StringModel sm = new StringModel();
                sm.Index = (i + 1).ToString();
                sm.Value = defaultValue;
                this.items.Add(sm);
            }
        }
    }
}
