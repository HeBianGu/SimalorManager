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
    public class EclipseFileFactory 
    {

        /// <summary> 单例模式 </summary>
        private static EclipseFileFactory t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static EclipseFileFactory Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new EclipseFileFactory();
                    }
                }
                return t;
            }
        }

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
