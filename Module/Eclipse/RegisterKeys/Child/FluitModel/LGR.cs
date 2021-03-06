﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 15:35:04
 * 文件名：END
 * 说明：
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 双重介质模型 </summary>
    public class LGR : ConfigerKey
    {
        public LGR(string _name)
            : base(_name)
        {

        }

        private string p0;

        string formatStr = "{0} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, p0.ToDD());
        }

        /// <summary> 解析字符串 </summary>
        public override void Build(List<string> newStr)
        {
            //this.ID = Guid.NewGuid().ToString();

            //for (int i = 0; i < newStr.Count; i++)
            //{
            //    switch (i)
            //    {
            //        case 0:
            //            this.p0 = newStr[0];
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }
    }
}
