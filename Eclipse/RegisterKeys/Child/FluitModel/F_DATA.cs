using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{

    public class GAS : Key
    {
        public GAS(string _name)
            : base(_name)
        {

        }
    }

    public class WATER : Key
    {
        public WATER(string _name)
            : base(_name)
        {

        }
    }

    public class OIL : Key
    {
        public OIL(string _name)
            : base(_name)
        {

        }
    }
    public class VAPOIL : Key
    {
        public VAPOIL(string _name)
            : base(_name)
        {

        }
    }


    public class MSGFILE : BaseKey
    {
        public MSGFILE(string _name)
            : base(_name)
        {

        }
    }

    public class FAULTDIM : BaseKey
    {
        public FAULTDIM(string _name)
            : base(_name)
        {

        }
    }

    public class INIT : BaseKey
    {
        public INIT(string _name)
            : base(_name)
        {

        }
    }
    public class GRIDFILE : BaseKey
    {
        public GRIDFILE(string _name)
            : base(_name)
        {

        }
    }


}
