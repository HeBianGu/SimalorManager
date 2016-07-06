#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:49:10
 * 文件名：TSTEP
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class TSTEP : Key
    {
        public TSTEP(string _name)
            : base(_name)
        {

        }

        int dataCount;
        /// <summary> 时间步中的天数 </summary>
        public int DataCount
        {
            get { return GetDataCount(); }
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            base.WriteKey(writer);
        }

        //  解析时间步中的天数
        int GetDataCount()
        {
            int count = 0;
            if (this.Lines.Count > 0)
            {
                string s = this.Lines[0];
                s.Split(' ').ToList().ForEach(l =>
                               {
                                   if (l.Contains('*'))
                                   {
                                       count += l.Split('*')[0].ToInt();
                                   }
                                   else
                                   {

                                       int temp;
                                       if (int.TryParse(l, out temp))
                                       {
                                           count++;
                                       }
                                   }
                               });
            }

            return count;
        }
    }
}
