#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/11/28 13:46:56

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
    /// <summary> SOLUTION(必须) 对油藏原始条件的确定：a.用规定的流体接触面深度到可能的流体高度（平衡）计算；b .从上一次运算建立的重启文件中读出；或c.用户自己确定每一个网格区的原始条件（一般不用此项）；</summary>
    [KeyAttribute(EclKeyType = EclKeyType.Parent)]
    public class SOLUTION : ParentKey
    {
        public SOLUTION(string _name)
            : base(_name)
        {

        }

    }
}
