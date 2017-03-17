using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 含水饱和度 </summary>
     [KeyAttribute(EclKeyType = EclKeyType.Solution, SimKeyType = SimKeyType.EclipseAndSimON )]
    public class SWAT : TableKey
    {
        public SWAT(string name)
            : base(name)
        {

        }

    }
}
