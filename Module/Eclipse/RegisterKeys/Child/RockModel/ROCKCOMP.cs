#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：
ROCKCOMP
IRREVERS 5
/

 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 岩石压实参数 </summary>
    public class ROCKCOMP : ConfigerKey
    {
        public ROCKCOMP(string _name)
            : base(_name)
        {

        }

        /// <summary> 岩石压实 </summary>
        public string ysys0 = "IRREVERS";

        /// <summary> 岩石压实分区个数 </summary>
        public string ysysfqgs1 = "1";


        string formatStr = "{0}{1}" + Environment.NewLine + KeyConfiger.EndFlag;

        public override string ToString()
        {
            return string.Format(formatStr, ysys0.ToD(), ysysfqgs1.ToD());
        }

        public override void Build(List<string> newStr)
        {
            this.ID = Guid.NewGuid().ToString();

            for (int i = 0; i < newStr.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.ysys0 = newStr[0];
                        break;
                    case 1:
                        this.ysysfqgs1 = newStr[1];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
