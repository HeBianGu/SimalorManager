#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：
SIGMA
 1 /
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 形状因子 </summary>
    public class SIGMA : ConfigerKey
    {
        public SIGMA(string _name)
            : base(_name)
        {

        }

        private string xzyz = "1";

        /// <summary> 形状因子 </summary>
        public string Xzyz
        {
            get { return xzyz; }
            set { xzyz = value; }
        }

        string formatStr = "{0} /";

        public override string ToString()
        {
            return string.Format(formatStr, xzyz);
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            this.Lines.Insert(0, string.Format(formatStr, xzyz.ToD()));
            base.WriteKey(writer);
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
                        this.xzyz = newStr[0];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
