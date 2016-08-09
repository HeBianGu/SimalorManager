#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/28 13:46:15
 * 文件名：GRID
 * 说明：
 * 
 * 
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
    /// <summary> 计算网格的图形规定（网格角点位置）和每个网格岩石物性的规定（孔隙度、绝对渗透率等) </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Parent)]
    public class GRID
        : ParentKey
    {
        public GRID(string _name)
            : base(_name)
        {

        }
    }
}
