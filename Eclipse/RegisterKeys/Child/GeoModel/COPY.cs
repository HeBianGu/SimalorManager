#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：GCONPROD
 * 说明：
 * 

BOX
1 40 1 20 5 8 /
PERMX
3200*0.03 /
ENDBOX


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

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
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
