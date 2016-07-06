using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 压力 </summary>
     [KeyAttribute(EclKeyType = EclKeyType.Solution, SimKeyType = SimKeyType.Eclipse, IsBigDataKey = true)]
    public class PRESSURE : TableKey
    {
        public PRESSURE(string name)
            : base(name)
        {

        }
    }
}
