using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 最大油气毛管压力 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Props, SimKeyType = SimKeyType.Eclipse, IsBigDataKey = true)]
    public class PCG: TableKey
    {
        public PCG(string name)
            : base(name)
        {

        }
    }
}
