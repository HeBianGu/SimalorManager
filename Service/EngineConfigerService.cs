using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HeBianGu.Product.SimalorManager.Service
{

    /// <summary> 读取写入文件时用的锁 </summary>
    class EngineConfigerService:ServiceFactory<EngineConfigerService>
    {

        /// <summary> 初始化一个只需一个访问的信号量 </summary>
         Semaphore semaphore = new Semaphore(1, 1);

        private SimKeyType keyType= SimKeyType.Eclipse;

        /// <summary> 保存时当模拟器类型不同时加锁，目前应用到关键字保存默认数据格式问题 </summary> 
        public SimKeyType SaveKeyTypeLock
        {
            get
            {
                return keyType;
            }

            set
            {
                if(keyType!=value)
                {
                    // Todo ：设置的时候设置占用，保存后释放信号 
                    semaphore.WaitOne();
                    count++;
                    keyType = value;
                }
            }
        }

        private UnitType _unittype = UnitType.METRIC;

        /// <summary> 单位类型 </summary>
        public UnitType Unittype
        {
            get
            {
                return _unittype;
            }

            set
            {
                _unittype = value;
            }
        }

        // Todo ：用于计数去释放 
        int count=0;

        /// <summary> 释放占用的资源锁 </summary>
        public void SaveLockRelease()
        {
            if(count==1)
            {
               semaphore.Release();
                count--;
            }
        }
    }
}
