#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/2 18:47:29  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����BaseFactory
 *
 * ˵����
 * 
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{

    /// <summary> ������������</summary>
    public class BaseFactory<T> where T : class, new()
    {

        #region - Start ����ģʽ -

        /// <summary> ����ģʽ </summary>
        private static T t = null;

        /// <summary> ���߳��� </summary>
        private static object localLock = new object();

        /// <summary> ����ָ������ĵ���ʵ�� </summary>
        public static T Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new T();
                    }
                }
                return t;
            }
        }

        #endregion - ����ģʽ End -


        #region - Start ����ģʽ -

        /// <summary> ����ģʽ </summary>
        static Dictionary<string, T> cache = null;

        /// <summary> ͨ�����Ƶõ�����ʵ�� </summary>
        public static T InstanceByName(string strKey)
        {
            lock (localLock)
            {
                if (cache == null)
                    cache = new Dictionary<string, T>();

                if (!cache.ContainsKey(strKey))
                    cache.Add(strKey, new T());

                return cache[strKey];
            }

        }

        #endregion - ����ģʽ End -



        /// <summary> �ⲿ�����Թ��� </summary>
        protected BaseFactory()
        { }
    }

}
