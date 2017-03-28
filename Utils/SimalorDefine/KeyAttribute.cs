#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 16:01:43

 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Base.AttributeEx
{
    /// <summary> key特性 用于描述Key的特征 </summary>
    public class KeyAttribute : Attribute
    {
        /// <summary> 模型类型 </summary>
        //public ModelType ModelType { get; set; }

        /// <summary> 关键字所属模拟器 </summary>
        public SimKeyType SimKeyType { get; set; }

        /// <summary> 关键字类型（用于对不同关键字分组） </summary>
        public EclKeyType EclKeyType { get; set; }

        ///// <summary> 标识关键字是否包含大数据 </summary>
        //public bool IsBigDataKey { get; set; }

        /// <summary> 关键字的别名（用于兼容老版本关键字） </summary>
        public string AnatherName { get; set; }

    }

    public enum EclKeyType
    {
        /// <summary> 父关键字类型 暂时没用 </summary>
        Parent = 0,
        /// <summary> 子关键字类型 暂时没用 </summary>
        Child,
        ///// <summary> INCLUDE关键字类型 暂时没用 </summary>
        //Include,
        /// <summary> 用于标识输出类型的关键字类型 初始化是注册到内存中 </summary>
        OutPut,
        /// <summary> 用于标识保存在GRID主关键字下面的类型  </summary>
        Grid,
        /// <summary> 用于标识保存在Props主关键字下面的类型  </summary>
        Props,
        /// <summary> 用于标识保存在Solution主关键字下面的类型  </summary>
        Solution,
        /// <summary> 用于标识保存在Regions主关键字下面的类型  </summary>
        Regions,
        /// <summary> 用于标识保存在Edit主关键字下面的类型  </summary>
        Edit
    }

    /// <summary> 关键字所属模拟器(用来区分关键字都属于哪种模拟器) </summary>
    public enum SimKeyType
    {
        Eclipse = 0,
        SimON,
        tNavigator,
        Petrel,
        Fault,
        All,
        EclipseAndSimON
       
    }


}
