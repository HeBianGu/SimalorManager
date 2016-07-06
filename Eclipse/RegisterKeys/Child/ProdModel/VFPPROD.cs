#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：WCONPROD
 * 说明：
 * WCONPROD
' O1' 'OPEN' 'GRAT' 2* 80000 1* 1* 80 3* /
--井名 开井 定产气 2* 产气量 1* 1* 井底流压限制 3*
 /

 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class VFPPROD : Key
    {
        public VFPPROD(string _name)
            : base(_name)
        {

        }
    }



}
