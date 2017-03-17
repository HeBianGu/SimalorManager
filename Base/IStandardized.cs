using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    /// <summary> 用于数模文件处理 （需要格式化的关键字都需要实现接口，目前没有用到）</summary>
    public interface IStandardized
    {
        /// <summary> 标准化数模模型拟合的格式 </summary>
        void Standardized();

    }
}
