#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17

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
using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 局部网格加密 </summary>
    public class CARFNAME : ItemsKey<NAME.Item>, IRootNode
    {
        public CARFNAME(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler = l =>
              {

                  // HTodo  ：截取前面空格来判断是否为关键字 
                  return l.TrimStart();
              };

            this.BuilderHandler += (l, k) =>
            {
                this.carName = this.Lines[0].Trim('\'');

                this.Lines.RemoveAt(0);
                return this;
            };
        }


        /// <summary> 网格加密名称 </summary>
        private string carName;

        public string CarName
        {
            get { return carName; }
            set { carName = value; }
        }

        public List<string> GetChildKeys()
        {
            return new List<string>() { "PERF" };
        }

        protected override void ItemWriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(string.Empty.ToD() + string.Empty.ToD()+this.GetType().Name.ToDWithOutSpace() + this.carName.ToEclStr());
            for (int i = 0; i < this.Items.Count; i++)
            {
                writer.WriteLine(this.Items[i].ToString());
            }
        }
    }
}
