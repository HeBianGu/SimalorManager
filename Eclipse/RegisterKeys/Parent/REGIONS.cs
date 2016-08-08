#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/28 13:46:47
 * 文件名：REGION
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
    /// <summary> REGION(选择)    为计算流体特性（PVT,即流体的密度和粘度）饱和度特性（相对渗透率和毛细管压力）原始条件（平衡压力和平衡饱和度）流体储量（流体储量和区内流动）所需对计算网格分区；如果这部分省略，所有的网格区块都放在第1区；</summary>
    [KeyAttribute(EclKeyType = EclKeyType.Parent)]
    public class REGIONS : ParentKey
    {
        public REGIONS(string _name)
            : base(_name)
        {

        }

    }
}
