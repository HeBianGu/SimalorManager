#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53

 * 说明：

 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 时间步控制，求解器选择，收敛判据 </summary>
    public class TSTART : ConfigerKey
    {
        public TSTART(string _name)
            : base(_name)
        {

        }

        private DateTime _date = DateTime.Now;
        /// <summary> 说明 </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }


        string formatStr = @"{0}";

        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, this.Date.ToString("yyyyMMdd") + "D");
        }

        /// <summary> 解析字符串 </summary>
        public override void Build(List<string> newStr)
        {
            this._date = DatesKeyService.Instance.GetDateTime(newStr[0]);
        }

    }
}
