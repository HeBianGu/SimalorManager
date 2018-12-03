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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 重启时间标志 RESTART </summary>
    [KeyAttribute(SimKeyType = SimKeyType.SimON, AnatherName = "RESTART")]
    public class STEPRST : SingleKey
    {

        public STEPRST(string _name)
            : base(_name)
        {

            this.EachLineCmdHandler = l =>
            {
                //  截取前后空格判断是否为关键字
                return l.Trim();

            };

        }

    }
}
