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
    /// <summary> 岩石密度  （如果要增加和Cosim一样带枚举的 请参考Cosim关键字） </summary>
    public class ROCKDEN : ConfigerKey
    {
        public ROCKDEN(string _name)
            : base(_name)
        {

        }

        private string _value = "1";

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        string formatStr = "{0} ";

        public override string ToString()
        {
            return string.Format(formatStr, Value.ToSDD());
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
                        this._value = newStr[0];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
