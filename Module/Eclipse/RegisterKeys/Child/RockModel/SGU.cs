using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
     [KeyAttribute(EclKeyType = EclKeyType.Props, SimKeyType = SimKeyType.Eclipse, IsBigDataKey = true)]
    public class SGU: TableKey
    {
        public SGU(string name)
            : base(name)
        {

        }
    }
}
