using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager.Service
{

    /// <summary> 配置文件 </summary>
    public class ConfigerResourceService : ServiceFactory<DatesKeyService>
    {
        IConfigerResource _configer;

        /// <summary> 初始化配置关系 </summary>
        public void Init(IConfigerResource configer)
        {
            _configer = configer;
        }

        /// <summary> 获取对应关系 </summary>
        public string GetString(string id)
        {
            return _configer.GetString(id);
        }
    }


    /// <summary> 文本对应关系 </summary>
    public interface IConfigerResource
    {
        /// <summary> 获取对应关系 </summary>
        string GetString(string id);
    }
}
