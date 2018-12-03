#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 模型起始时间 </summary>
    public class START : ConfigerKey,IProductTime
    {
        public START(string _name)
            : base(_name)
        {

        }
        public START(string _name, DateTime dt)
            : base(_name)
        {
            _startTime = dt;
        }

        DateTime _startTime = default(DateTime);
        /// <summary> 模型起始时间 </summary>
        [CategoryAttribute("模型参数"), DescriptionAttribute("开始时间"), DisplayName("开始时间")]
        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
            }
        }

        /// <summary> 模型起始时间 </summary>
        public DateTime DateTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
            }
        }

        /// <summary> 设置重启的时间 </summary>
        void SetDateTime(DateTime pTime)
        {
            string formatStr = pTime.ToString("dd  'MMM'  yyyy  /", CultureInfo.InvariantCulture).Replace("MMM", "'" + pTime.ToString("MMM", CultureInfo.InvariantCulture) + "'").ToUpper();
            this.Lines.Insert(0, formatStr);
        }

        public override string ToString()
        {
            string formatStr = _startTime.ToString("dd  'MMM'  yyyy  /", CultureInfo.InvariantCulture).Replace("MMM", "'" + _startTime.ToString("MMM", CultureInfo.InvariantCulture) + "'").ToUpper();
            return formatStr;
        }


        public override void Build(List<string> newStr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < newStr.Count; i++)
            {
                sb.Append(newStr[i] + " ");
            }

           this._startTime= DatesKeyService.Instance.GetDateTime(sb.ToString());
        }
    }
}
