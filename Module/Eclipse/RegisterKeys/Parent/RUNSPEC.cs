#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/11/27 14:53:34

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
    /// <summary> RUNSPEC(必须)  题目、作业维数、运算键、目前的相态等； </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Parent)]
    public class RUNSPEC : ParentKey
    {
        public RUNSPEC(string _name)
            : base(_name)
        {

        }
    }
}
