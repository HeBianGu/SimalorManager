#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:17

 * 说明：
 * 
 * 读取规则：找到ENDBOX退出读取，读到要修改的关键字放到ObsoverKey观察对象中
 * 

 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 复值 </summary>
   public  class COPY : ModifyKey
    {
        public COPY(string _name)
            : base(_name)
        {
           
        }


        /// <summary> 抽象方法 子类用来扩展 </summary>
        protected override void ConvertToModel()
        {
            foreach (ModifyItem it in this.Items)
            {
                ModifyCopyModel model = it.ToModel(this);
                model.ParentKey = this;
               this.ObsoverModel.Add(model);
            }
        }

    }
}
