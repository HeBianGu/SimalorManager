#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/28 13:46:28
 * 文件名：EDIT
 * 说明：EDIT(选择)       对计算的孔隙体积，网格中心深度和传导率进行修改；
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
    /// <summary> 对计算的孔隙体积，网格中心深度和传导率进行修改 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Parent)]
    public class EDIT : ParentKey
    {
        public EDIT(string _name)
            : base(_name)
        {

        }
    }
}
