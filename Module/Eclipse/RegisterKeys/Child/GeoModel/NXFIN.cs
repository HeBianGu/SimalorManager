﻿#region <版 本 注 释>
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
    /// <summary> NXFIN中参数个数=X2-X1+1</summary>
    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.Eclipse)]
    public class NXFIN : DynamicSingleLineKey
    {
        public NXFIN(string _name)
            : base(_name)
        {

        }



    }
}
