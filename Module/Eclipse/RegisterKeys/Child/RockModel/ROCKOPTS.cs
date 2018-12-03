#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：


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
    /// <summary> 岩石选项设置 </summary>
    public class ROCKOPTS : ConfigerKey
    {
        public ROCKOPTS(string _name)
            : base(_name)
        {

        }

        /// <summary> 压力选择 </summary>
        public string ylxz0 = "PRESSURE";

        /// <summary> 参考压力选项 </summary>
        public string ckylxx1 = "NOSTORE";

        /// <summary> 岩石分区表 </summary>
        public string ysfqb2 = "PVTNUM";


        string formatStr = "{0}{1}{2}" + Environment.NewLine + KeyConfiger.EndFlag;

        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, ylxz0.ToDD(), ckylxx1.ToDD(), ysfqb2.ToDD());
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
                        this.ylxz0 = newStr[0];
                        break;
                    case 1:
                        this.ckylxx1 = newStr[1];
                        break;
                    case 2:
                        this.ysfqb2 = newStr[2];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
