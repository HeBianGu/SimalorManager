#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/28 13:47:06
 * 文件名：SUMMARY
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
