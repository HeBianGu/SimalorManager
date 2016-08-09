#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 15:58:43
 * 文件名：NormalKey
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 为解析的关键字 </summary>
    public class UnkownKey : Key
    {
        public UnkownKey(string _name)
            : base(_name)
        {

        }
    }
}
