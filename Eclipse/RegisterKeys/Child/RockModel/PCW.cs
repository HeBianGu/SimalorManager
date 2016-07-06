using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 最大油水毛管压力 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Props, SimKeyType = SimKeyType.Eclipse, IsBigDataKey = true)]
    public class PCW: TableKey
    {
        public PCW(string name)
            : base(name)
        {

        }
    }
}
