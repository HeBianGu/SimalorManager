#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 17:43:17

 * 说明：
 * 
 *CARFIN 'FIN2' 
   RANGE  6  8  4  6  6  6
   NXFIN  3*3
   NYFIN  2  3  2
   NZFIN  3
   FINBOX PERMX  1 9   1  7   1  3  '='  100
   FINBOX PERMY  1 9   1  7   1  3  '='  100
   FINBOX PERMZ  1 9   1  7   1  3  '='  10 

 * 

 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 局部网格加密 </summary>
    public class GLOBAL : ItemsKey<NAME.Item>, IRootNode
    {
        public GLOBAL(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler = l =>
              {
                  // HTodo  ：截取前面空格来判断是否为关键字 
                  return l.TrimStart();
              };

         
        }

        public List<string> GetChildKeys()
        {
            return new List<string>() { "PERF" };
        }
        protected override void ItemWriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(string.Empty.ToD() + string.Empty.ToD()+this.GetType().Name.ToDWithOutSpace());
            for (int i = 0; i < this.Items.Count; i++)
            {
                writer.WriteLine(this.Items[i].ToString());
            }
        }
    }
}
