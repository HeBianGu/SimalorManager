#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 16:55:59
 * 说明：读取本程序集中指定注册命名空间的所有类型添加到静态注册集合中用于解析ECLIPSE的类型
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;

using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 关键字工厂(一个关键字对应于一个关键字的实现类，没有实现用NormalKey) </summary>
    public class KeyConfigerFactroy : ServiceFactory<KeyConfigerFactroy>
    {
        #region - 初始化注册关键字 -

        /// <summary> 静态构造函数用于初始化注册关键字 </summary>
        public KeyConfigerFactroy()
        {
            InitCompenont();
        }

        PublicKeyFactory _publicKeyFactory = new PublicKeyFactory();

        public PublicKeyFactory PublicKeyFactory
        {
            get { return _publicKeyFactory; }
            set { _publicKeyFactory = value; }
        }

        EclipseKeyFactory _eclipseKeyFactory = new EclipseKeyFactory();

        public EclipseKeyFactory EclipseKeyFactory
        {
            get { return _eclipseKeyFactory; }
            set { _eclipseKeyFactory = value; }
        }

        SimONKeyFactory _simONKeyFactory = new SimONKeyFactory();

        public SimONKeyFactory SimONKeyFactory
        {
            get { return _simONKeyFactory; }
            set { _simONKeyFactory = value; }
        }


        /// <summary> 利用反射注册类关键字 抽象工厂 </summary>
        public void InitCompenont()
        {
            //  遍历所有Key注册关键字
            Type[] classes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var item in classes)
            {
                #region - 初始化公用关键字 -
                if (item.Namespace == _publicKeyFactory.Publickeynamespace)
                {
                    _publicKeyFactory.BuildRegister(item);
                }
                #endregion

                #region  - 初始化Eclipse关键字 -

                if (item.Namespace == _eclipseKeyFactory.Publickeynamespace)
                {
                    _eclipseKeyFactory.BuildRegister(item);

                    _simONKeyFactory.InitRegisterFromEclipse(item);
                }

                #endregion

                #region  - 初始化SimON关键字
                if (item.Namespace == _simONKeyFactory.Publickeynamespace)
                {
                    _simONKeyFactory.BuildRegister(item);
                }
                #endregion
            }
        }

        #endregion

        /// <summary> 创建关键字方法 </summary> 
        public BaseKey CreateKey<T>(string keyName, SimKeyType simType = SimKeyType.Eclipse) where T : BaseKey
        {
            if (simType == SimKeyType.Eclipse)
            {
                //  如果是Eclipse注册关键字 直接创建
                if (_eclipseKeyFactory.IsRegisterKey(keyName))
                {
                    return _eclipseKeyFactory.CreateKey<BaseKey>(keyName) as T;
                }

                //  不是Eclipse注册关键字 在公用关键字中找
                if (_publicKeyFactory.IsRegisterKey(keyName))
                {
                    return _publicKeyFactory.CreateKey<BaseKey>(keyName) as T;
                }

                //  如果都不是走后面UnkownKey

            }
            else if (simType == SimKeyType.SimON)
            {
                //  如果是SimON注册关键字 直接创建
                if (_simONKeyFactory.IsRegisterKey(keyName))
                {
                    return _simONKeyFactory.CreateKey<BaseKey>(keyName) as T;
                }

                //  不是Eclipse注册关键字 在公用关键字中找
                if (_publicKeyFactory.IsRegisterKey(keyName))
                {
                    return _publicKeyFactory.CreateKey<BaseKey>(keyName) as T;
                }

                //  如果是Eclipse注册关键字 直接创建
                if (_eclipseKeyFactory.IsRegisterKey(keyName))
                {
                    return _eclipseKeyFactory.CreateKey<BaseKey>(keyName) as T;
                }

                //  如果都不是走后面UnkownKey
            }
            else if (simType == SimKeyType.EclipseAndSimON)
            {
                //  如果是Eclipse注册关键字 直接创建
                if (_eclipseKeyFactory.IsRegisterKey(keyName))
                {
                    return _eclipseKeyFactory.CreateKey<BaseKey>(keyName) as T;
                }

                //  如果是SimON注册关键字 直接创建
                if (_simONKeyFactory.IsRegisterKey(keyName))
                {
                    return _simONKeyFactory.CreateKey<BaseKey>(keyName) as T;
                }

                //  不是Eclipse注册关键字 在公用关键字中找
                if (_publicKeyFactory.IsRegisterKey(keyName))
                {
                    return _publicKeyFactory.CreateKey<BaseKey>(keyName) as T;
                }
            }

            UnkownKey unkownKey = new UnkownKey(KeyChecker.FormatKey(keyName));

            return unkownKey;
        }

        /// <summary> 创建关键字方法 </summary> 
        public bool IsRegister(string keyName, SimKeyType simType = SimKeyType.Eclipse)
        {
            if (simType == SimKeyType.Eclipse)
            {
                return KeyConfigerFactroy.Instance.EclipseKeyFactory.IsRegisterKey(keyName) || KeyConfigerFactroy.Instance.EclipseKeyFactory.IsRegisterKey(keyName);

            }
            else if (simType == SimKeyType.SimON)
            {
                return KeyConfigerFactroy.Instance.EclipseKeyFactory.IsRegisterKey(keyName) || KeyConfigerFactroy.Instance.EclipseKeyFactory.IsRegisterKey(keyName);

            }

            return false;
        }


    }



    /// <summary> 关键字工厂基类 </summary>
    public class BaseKeyFactory
    {
        public BaseKeyFactory()
        {

        }
        string _publickeynamespace = "OPT.Product.SimalorManager.RegisterKeys";

        public string Publickeynamespace
        {
            get { return _publickeynamespace; }
            set { _publickeynamespace = value; }
        }

        //  关键字注册表
        List<string> _registerList = new List<string>();

        public List<string> RegisterList
        {
            get { return _registerList; }
            set { _registerList = value; }
        }

        /// <summary> 增加注册关键字 </summary> 
        public void AddRegionList(string registerName)
        {
            if (registerName.IsRegisterName())
            {
                this._registerList.Add(registerName);
            }
        }

        #region - 关键字工厂 -

        /// <summary> 通过关键字名称找到对应名称的类型 </summary>
        public T CreateKey<T>(string keyName) where T : BaseKey
        {
            //keyName = KeyChecker.FormatKey(keyName);

            Type objType = TypeCacheFactory.GetInstance().GetType(_publickeynamespace + "." + KeyChecker.FormatKey(keyName), false);

            object obj = Activator.CreateInstance(objType, new object[] { keyName });

            return obj as T;
        }

        /// <summary> 验证字符串是否为注册关键字 </summary>
        internal bool IsRegisterKey(string keyName)
        {
            string temp = KeyChecker.FormatKey(keyName);

            return _registerList.Contains(temp);
        }
        #endregion

        public virtual void BuildRegister(Type t)
        {

        }


    }

    /// <summary> 公用关键字工厂基类 </summary>
    public class PublicKeyFactory : BaseKeyFactory
    {
        string _publickeynamespace = "OPT.Product.SimalorManager.RegisterKeys";

        public PublicKeyFactory()
        {
            base.Publickeynamespace = _publickeynamespace;
        }
    }

    /// <summary> Eclipse关键字工厂基类 </summary>
    public class EclipseKeyFactory : BaseKeyFactory
    {
        string _publickeynamespace = "OPT.Product.SimalorManager.RegisterKeys.Eclipse";

        public EclipseKeyFactory()
        {
            base.Publickeynamespace = _publickeynamespace;
        }


        #region - 输出关键字工厂 -

        //  关键字注册表
        internal List<string> keyOutPutConfiger = new List<string>();

        #endregion

        #region - 别名关键字工厂 -

        //  关键字注册表
        internal Dictionary<string, string> AnatherNameConfiger = new Dictionary<string, string>();

        #endregion

        #region - 表格形式关键字工厂 -

        //  关键字注册表
        private List<string> gridPartConfiger = new List<string>();
        /// <summary> Grid部分关键字 </summary>
        public List<string> GridPartConfiger
        {
            get { return gridPartConfiger; }
            set { gridPartConfiger = value; }
        }

        //  关键字注册表
        private List<string> propsPartConfiger = new List<string>();
        /// <summary> Props部分关键字 </summary>
        public List<string> PropsPartConfiger
        {
            get { return propsPartConfiger; }
            set { propsPartConfiger = value; }
        }

        //  关键字注册表
        private List<string> solutionPartConfiger = new List<string>();
        /// <summary> Solution部分关键字 </summary>
        public List<string> SolutionPartConfiger
        {
            get { return solutionPartConfiger; }
            set { solutionPartConfiger = value; }
        }

        //  关键字注册表
        private List<string> regionsPartConfiger = new List<string>();
        /// <summary> Regions部分关键字 </summary>
        public List<string> RegionsPartConfiger
        {
            get { return regionsPartConfiger; }
            set { regionsPartConfiger = value; }
        }


        #endregion

        #region - 表格形式关键字工厂 -

        //  关键字注册表
        private List<string> keyTableEclipseConfiger = new List<string>();
        /// <summary> 所有解析的Eclipse表格形式关键字 </summary>
        public List<string> KeyTableEclipseConfiger
        {
            get { return keyTableEclipseConfiger; }
            set { keyTableEclipseConfiger = value; }
        }

        #endregion

        public override void BuildRegister(Type item)
        {
            var objs = item.GetCustomAttributes(typeof(KeyAttribute), false);

            if (objs != null && objs.Length > 0)
            {
                KeyAttribute k = objs[0] as KeyAttribute;

                if (k.EclKeyType == EclKeyType.OutPut)
                {
                    //  注册输出关键字
                    this.keyOutPutConfiger.Add(item.Name);
                }

                if (!string.IsNullOrEmpty(k.AnatherName))
                {
                    //  注册有别名关键字
                    this.AnatherNameConfiger.Add(k.AnatherName, item.Name);
                }

                if (k.EclKeyType == EclKeyType.Grid)
                {
                    //  注册属于Grid部分的关键字
                    this.GridPartConfiger.Add(item.Name);
                }

                if (k.EclKeyType == EclKeyType.Props)
                {
                    //  注册属于Props部分的关键字
                    this.PropsPartConfiger.Add(item.Name);
                }

                if (k.EclKeyType == EclKeyType.Solution)
                {
                    //  注册属于Solution部分的关键字
                    this.SolutionPartConfiger.Add(item.Name);
                }

                if (k.EclKeyType == EclKeyType.Regions)
                {
                    //  注册属于Regions部分的关键字
                    this.RegionsPartConfiger.Add(item.Name);
                }


                if (k.SimKeyType == SimKeyType.EclipseAndSimON || k.SimKeyType == SimKeyType.Eclipse)
                {
                    //  表格形式关键字
                    if (item.IsSubclassOf(typeof(TableKey)))
                    {
                        this.KeyTableEclipseConfiger.Add(item.Name);
                    }
                }
            }

            this.AddRegionList(item.Name);

        }
    }

    /// <summary> SimON关键字工厂基类 </summary>
    public class SimONKeyFactory : BaseKeyFactory
    {
        string _publickeynamespace = "OPT.Product.SimalorManager.RegisterKeys.SimON";

        public SimONKeyFactory()
        {
            base.Publickeynamespace = _publickeynamespace;
        }


        #region - 表格形式关键字工厂 -

        //  关键字注册表
        private List<string> keyTableSimONConfiger = new List<string>();
        /// <summary> 所有解析的SimON表格形式关键字 </summary>
        public List<string> KeyTableSimONConfiger
        {
            get { return keyTableSimONConfiger; }
            set { keyTableSimONConfiger = value; }
        }

        #endregion

        public override void BuildRegister(Type item)
        {
            var objs = item.GetCustomAttributes(typeof(KeyAttribute), false);

            this.AddRegionList(item.Name);


            if (objs != null && objs.Length > 0)
            {
                KeyAttribute k = objs[0] as KeyAttribute;

                if (k.SimKeyType == SimKeyType.EclipseAndSimON || k.SimKeyType == SimKeyType.SimON)
                {
                    //  表格形式关键字
                    if (item.IsSubclassOf(typeof(TableKey)))
                    {
                        this.KeyTableSimONConfiger.Add(item.Name);
                    }
                }
            }

        }


        
        /// <summary> 需要从Eclipse命名空间初始化的项 </summary> 
        public void InitRegisterFromEclipse(Type item)
        {
            var objs = item.GetCustomAttributes(typeof(KeyAttribute), false);

            if (objs != null && objs.Length > 0)
            {
                KeyAttribute k = objs[0] as KeyAttribute;

                if (k.SimKeyType == SimKeyType.EclipseAndSimON || k.SimKeyType == SimKeyType.SimON)
                {
                    //  表格形式关键字
                    if (item.IsSubclassOf(typeof(TableKey)))
                    {
                        this.KeyTableSimONConfiger.Add(item.Name);
                    }
                }
            }

        }
    }


}
