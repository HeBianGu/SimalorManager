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
    /// <summary> 取最大值 </summary>
    class MAXVALUE : ModifyKey
    {
        public MAXVALUE(string _name)
            : base(_name)
        {
            //  相加运算方法
            base.func = (l, k) => l < k ? l : k;
        }

    }
}
