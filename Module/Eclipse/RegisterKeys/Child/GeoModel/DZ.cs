#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 流体分区</summary>
    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.EclipseAndSimON , AnatherName = "DZV")]
    public class DZ : TableKey
    {
        public DZ(string _name)
            : base(_name)
        {
      
        }

        public override void Build(int tableCount, int xCount, int yCount, string mmf = null)
        {
            if (this.BaseFile is SimalorManager.Eclipse.FileInfos.SimONData && this.BaseFile.DoubleType != DoubleType.DKJZMX)
            {
                // HTodo  ：SimON文件的双孔双渗只有一半数据 

                this.TransValueZ = l => l / 2;
            }
            else
            {
                this.TransValueZ = l => l;
            }

            base.Build(tableCount, xCount, yCount, mmf);
        }

    }
}
