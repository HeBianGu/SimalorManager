using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    /// <summary> 用于解析文件时将当前解析的生产时间记录在主文件中(为了兼容TSTEP增加)</summary>
    public interface IProductTime
    {
        /// <summary> 时间 </summary>
        DateTime DateTime
        {
            get;set;
        }

    }
}
