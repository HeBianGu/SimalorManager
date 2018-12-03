#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 13:39:53

 * 说明：
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary>  </summary>
    public class SATREG : ConfigerKey
    {
        public SATREG(string _name)
            : base(_name)
        {

        }

        private string x = "1";

        public string X
        {
            get { return x; }
            set { x = value; }
        }
        string formatStr = "{0} ";

        public override string ToString()
        {
            return string.Format(formatStr, X.ToSDD());
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
                        this.x = newStr[0];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
