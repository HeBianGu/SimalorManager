﻿//#region <版 本 注 释>
// * ========================================================================
// * Copyright(c) ********************, All Rights Reserved.
// * ========================================================================
// *    
// * 作者：[HeBianGu]   时间：2015/11/28 13:46:47

// * 说明：
// * 
// * 
// * 修改者：           时间：               
// * 修改说明：
// * ========================================================================
//*/
//#endregion
//using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
///*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.SimON
{
    [KeyAttribute(EclKeyType = EclKeyType.Parent)]
    public class TUNESET : ParentKey
    {
        public TUNESET(string _name)
            : base(_name)
        {

        }

    }
}
