using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{
    class RandNumber
    {
        Random r = new Random();

        public int Rand01()
        {
          return  r.Next(10);
        }

        public int  RandInt(int low, int high)
        {
            return r.Next(low, high);
        }
    }
}
