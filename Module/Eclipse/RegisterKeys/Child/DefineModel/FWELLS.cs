#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53
 * 文件名：COORD
 * 说明：
   压裂井完井	FWELLS	设置水平井完井选项开启	FWELLS
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 压裂井完井 </summary>
    public class FWELLS : Key
    {
        public FWELLS(string _name)
            : base(_name)
        {

        }
    }
}
