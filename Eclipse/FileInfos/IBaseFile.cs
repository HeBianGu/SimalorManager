using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    interface IBaseFileInterface : IFileInterface
    {
        void Delete(BaseKey key);

        void Delete(string key);

        void Delete(List<BaseKey> keys);

        void Delete(List<string> keys);

        void Delete(Predicate<BaseKey> match);

        void InsertKey(BaseKey key);

        bool InsertAfter(BaseKey key, BaseKey inKey);

        bool InsertBefore(BaseKey key, BaseKey inKey);

        bool Exist(string key);

        bool Exist(Predicate<BaseKey> match);

        BaseKey Find(BaseKey key);

        BaseKey Find(Predicate<BaseKey> match);

        List<BaseKey> FindAll(Predicate<BaseKey> match);

        BaseKey Find(string key);

    }
}
