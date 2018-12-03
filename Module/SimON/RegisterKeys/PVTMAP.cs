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
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.SimON
{
   [KeyAttribute(EclKeyType = EclKeyType.Regions, SimKeyType = SimKeyType.SimON)]
    public class PVTMAP : TableKey
    {
        public PVTMAP(string _name)
            : base(_name)
        {
            this.DefaultValue = 1;
        }
    }
}
