using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPT.Product.SimalorManager;

namespace OPT.Product.SimalorManager
{
    /// <summary> 结果输出绑定接口 </summary>
    public interface OutPutBindKey
    {
        string OutName
        {
            get;
            set;
        }
        object IsCheck
        {
            get;
            set;
        }
        string OutType
        {
            get;
        }

        string Description
        {
            get;
        }
    }
}
