using OPT.Product.SimalorManager.Eclipse.FileInfos;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace OPT.Product.SimalorManager
{
    /// <summary> EclipseFile类型构造工厂 </summary>
    public class EclipseFileFactory : BaseFactory<EclipseFileFactory>
    {
        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public EclipseData ThreadLoadResize(string fileFullPath, int stactSize = 4194304)
        {
            EclipseData eclData = null;

            Thread thread = new Thread(() => eclData = new EclipseData(fileFullPath), stactSize);// 4mb栈

            thread.Start();

            while (true)
            {
                if (thread.ThreadState == ThreadState.Stopped)
                {
                    break;
                }
            }

            return eclData;
        }

    }
}
