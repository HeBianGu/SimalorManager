using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 含气饱和度 </summary>
     [KeyAttribute(EclKeyType = EclKeyType.Solution, SimKeyType = SimKeyType.Eclipse, IsBigDataKey = true)]
    public class SGAS : TableKey
    {
        public SGAS(string name)
            : base(name)
        {

        }


    }
}
