using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 最大水相相对渗透率 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Props, SimKeyType = SimKeyType.Eclipse, IsBigDataKey = true)]
    public class KRW: TableKey
    {
        public KRW(string name)
            : base(name)
        {

        }
    }
}
