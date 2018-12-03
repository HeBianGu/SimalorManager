#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：

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
    /// <summary> 扩散系数乘子  可选 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.Eclipse)]
    public class DIFFMMF : TableKey
    {
        public DIFFMMF(string _name)
            : base(_name)
        {
            this.DefaultValue = 1;
        }

    }
}
