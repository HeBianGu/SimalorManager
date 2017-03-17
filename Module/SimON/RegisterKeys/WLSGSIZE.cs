﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53
 * 文件名：COORD
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

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary>  </summary>
    public class WLSGSIZE : ConfigerKey
    {
        public WLSGSIZE(string _name)
            : base(_name)
        {

        }

        private string _size;

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        string formatStr = "{0} ";

        public override string ToString()
        {
            return string.Format(formatStr, Size.ToSDD());
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
                        this._size = newStr[0];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}