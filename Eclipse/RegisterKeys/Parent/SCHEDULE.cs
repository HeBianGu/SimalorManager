#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/28 13:47:15
 * 文件名：SCHEDULE
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

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Parent
{
    /// <summary> SCHEDULE(必须) 确定模拟的作业（产量、注水量控制和限制）和给定需要输出模拟结果的时间。在SCHEDULE部分中同样还能确定垂向流动的动态曲线和油管模拟参数。 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Parent)]
    public class SCHEDULE : ParentKey
    {
        public SCHEDULE(string _name)
            : base(_name)
        {

        }
    }
}
