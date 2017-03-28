#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：


/
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
    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.EclipseAndSimON )]
    public class TOPS : TableKey
    {
        public TOPS(string _name)
            : base(_name)
        {

            // Todo ：只有 一层数据 
            this.TransValueZ = l => 1;
        }


        /// <summary> 转换为TOPS  TOPS=第一层DEPTH-第一层DZ/2 </summary>
        public static TOPS ToTOPS(DEPTH depth, DZ dz)
        {
            TOPS tops = new TOPS("TOPS");

            tops.BuildTableEntity(1, depth.X, depth.Y, depth.MmfPath);

            // Todo ：复制数据 
            for (int ix = 0; ix < depth.X; ix++)
            {
                for (int iy = 0; iy < depth.Y; iy++)
                {
                    //for (int iz = 0; iz < depth.Z; iz++)
                    //{

                    double v = depth.Tables[0].Get(iy, ix).ToString().ToDouble() - dz.Tables[0].Get(iy, ix).ToString().ToDouble() / 2;
                    //  对值执行func操作
                    tops.Tables[0].Set(iy, ix, depth.Tables[0].Get(iy, ix).ToString().ToDouble() - dz.Tables[0].Get(iy, ix).ToString().ToDouble() / 2);
                    //}
                }
            }

            return tops;
        }


    }
}
