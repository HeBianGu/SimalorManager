﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/11/28 13:47:06

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> SUMMARY(选择) 在每一个时间步后，确定的数据被编入汇总文件中； </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Parent)]
    public class SUMMARY : ParentKey
    {
        public SUMMARY(string _name)
            : base(_name)
        {

        }
    }
}
