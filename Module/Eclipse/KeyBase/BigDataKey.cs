#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:46:54
 * 文件名：BigDataKey
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
//using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 大数据用于标识是否读取 </summary>
    public class BigDataKey : Key
    {
        public BigDataKey(string _name)
            : base(_name)
        {

        }
        /// <summary> 大数据写入方法 = 拷贝大数据文件 </summary>
        public override void WriteKey(System.IO.StreamWriter writer)
        {
            base.WriteKey(writer);
        }
    }
}
