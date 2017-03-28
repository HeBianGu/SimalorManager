#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53

 * 说明：
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 支撑剂数量 </summary>
    public class NPROPANTS : ConfigerKey
    {
        public NPROPANTS(string _name)
            : base(_name)
        {

        }

        private int count = 1;

        /// <summary> 支撑剂个数 </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        string formatStr = "{0}" + KeyConfiger.NewLineEndFlag;

        public override string ToString()
        {
            return string.Format(formatStr, Count.ToD());
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
                        this.count = newStr[0].ToInt();
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
