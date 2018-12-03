using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> 文件类型构造工厂服务 </summary>
    public class FileFactoryService : ServiceFactory<FileFactoryService>
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

        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public SimONData ThreadLoadSimONResize(string fileFullPath, int stactSize = 4194304)
        {
            SimONData eclData = null;

            Thread thread = new Thread(() => eclData = new SimONData(fileFullPath), stactSize);// 4mb栈

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
        /// <summary> 利用数模文件异步创建指定大小栈的内存模型  SimONData data = FileFactoryService.Instance.ThreadLoadFunc<SimONData>(() => new SimONData(mainData.FilePath, null, l => false))</summary>
        public T ThreadLoadFunc<T>(Func<T> act, int stactSize = 4194304) where T : BaseFile
        {
            T eclData = default(T);

            Thread thread = new Thread(() => eclData = act.Invoke(), stactSize);// 4mb栈

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

        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public INCLUDE ThreadLoadFromFile(string pfilePath, SimKeyType keyType = SimKeyType.Eclipse, int stactSize = 4194304)
        {
            INCLUDE include = new INCLUDE("INCLUDE");

            return ThreadLoadFromFile(include, pfilePath, keyType, stactSize);
        }

        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public INCLUDE ThreadLoadFromFile(INCLUDE include, string pfilePath, SimKeyType keyType = SimKeyType.Eclipse, int stactSize = 4194304)
        {

            include.FileName = Path.GetFileName(pfilePath);

            include.FilePath = pfilePath;

            if (include.BaseFile == null)
            {
                if (keyType == SimKeyType.Eclipse)
                {
                    EclipseData ecl = new EclipseData();
                    include.BaseFile = ecl;
                }
                else if (keyType == SimKeyType.SimON)
                {
                    SimONData simon = new SimONData();
                    include.BaseFile = simon;
                }
            }


            Thread thread = new Thread(() => include.ReadFromStream(), stactSize);// 4mb栈

            thread.Start();

            while (true)
            {
                if (thread.ThreadState == ThreadState.Stopped)
                {
                    break;
                }
            }


            return include;
        }

    }
}
