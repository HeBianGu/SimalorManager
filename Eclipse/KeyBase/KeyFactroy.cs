#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 16:55:59
 * 文件名：KeyFactroy
 * 说明：读取本程序集中指定注册命名空间的所有类型添加到静态注册集合中用于解析ECLIPSE的类型
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
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
    public class KeyConfigerFactroy : BaseFactory<KeyConfigerFactroy>
    {
        protected Dictionary<Type, Object> _objectCache = new Dictionary<Type, Object>();

        #region - 初始化注册关键字 -

        //  从配置文件中加载关键字类型(暂时不使用)
        string configerPath = string.Empty;

        /// <summary> 静态构造函数用于初始化注册关键字 </summary>
        static KeyConfigerFactroy()
        {
            InitCompenont();
        }

        /// <summary> 利用反射注册类关键字 </summary>
        static void InitCompenont()
        {
            //  遍历所有Key注册关键字
            Type[] classes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var item in classes)
            {
                //  子节点
                if (item.Namespace == keyChildNameSpace)
                {
                    var objs = item.GetCustomAttributes(typeof(KeyAttribute), false);

                    if (objs != null && objs.Length > 0)
                    {
                        KeyAttribute k = objs[0] as KeyAttribute;

                        if (k.EclKeyType == EclKeyType.OutPut)
                        {
                            //  注册输出关键字
                            keyOutPutConfiger.Add(item.Name);
                        }

                        if (!string.IsNullOrEmpty(k.AnatherName))
                        {
                            //  注册有别名关键字
                            AnatherNameConfiger.Add(k.AnatherName, item.Name);
                        }

                        if (k.EclKeyType == EclKeyType.Grid)
                        {
                            //  注册属于Grid部分的关键字
                            GridPartConfiger.Add(item.Name);
                        }

                        if (k.EclKeyType == EclKeyType.Props)
                        {
                            //  注册属于Props部分的关键字
                            PropsPartConfiger.Add(item.Name);
                        }

                        if (k.EclKeyType == EclKeyType.Solution)
                        {
                            //  注册属于Solution部分的关键字
                            SolutionPartConfiger.Add(item.Name);
                        }

                        if (k.EclKeyType == EclKeyType.Regions)
                        {
                            //  注册属于Regions部分的关键字
                            RegionsPartConfiger.Add(item.Name);
                        }



                        if(k.SimKeyType==SimKeyType.Eclipse)
                        {
                            //  表格形式关键字
                            if (item.IsSubclassOf(typeof(TableKey)))
                            {
                                KeyTableEclipseConfiger.Add(item.Name);

                            }
                        }
                    }

                    keyChildConfiger.Add(item.Name);


           
                }

                //  父节点
                if (item.Namespace == keyParentNameSpace)
                {
                    keyParentConfiger.Add(item.Name);
                }

                //  INCLUDE
                if (item.Namespace == keyINCLUDEtNameSpace)
                {
                    keyINCLUDEConfiger.Add(item.Name);
                }
            }
        }

        #endregion

        #region - 父关键字工厂 -

        const string keyParentNameSpace = "OPT.Product.SimalorManager.Eclipse.RegisterKeys.Parent";

        //  关键字注册表
        static List<string> keyParentConfiger = new List<string>();

        /// <summary> 通过关键字名称找到对应名称的类型 </summary>
        public T CreateParentKey<T>(string keyName) where T : ParentKey
        {
            keyName = KeyChecker.FormatKey(keyName);

            Type objType = Type.GetType(keyParentNameSpace + "." + keyName, true);

            object obj = Activator.CreateInstance(objType, new object[] { keyName });

            return obj as T;

        }

        /// <summary> 验证字符串是否为注册关键字 </summary>
        internal bool IsParentRegisterKey(string keyName)
        {
            string temp = KeyChecker.FormatKey(keyName);

            return keyParentConfiger.Contains(temp);
        }

        #endregion

        #region - 子关键字工厂 -

        const string keyChildNameSpace = "OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child";

        //  关键字注册表
        static List<string> keyChildConfiger = new List<string>();

        /// <summary> 通过关键字名称找到对应名称的类型 </summary>
        public T CreateChildKey<T>(string keyName) where T : BaseKey
        {
            keyName = KeyChecker.FormatKey(keyName);

            Type objType = TypeCacheFactory.GetInstance().GetType(keyChildNameSpace + "." + keyName, false);

            //Type objType = Type.GetType(keyChildNameSpace + "." + keyName, true);

            //object obj = Activator.CreateInstance(typeof(T), new object[] { keyName });

            object obj = Activator.CreateInstance(objType, new object[] { keyName });

            return obj as T;
        }

        /// <summary> 验证字符串是否为注册关键字 </summary>
        internal bool IsChildRegisterKey(string keyName)
        {
            string temp = KeyChecker.FormatKey(keyName);

            return keyChildConfiger.Contains(temp);
        }
        #endregion

        #region - INCLUDE工厂 -

        const string keyINCLUDEtNameSpace = "OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE";

        //  关键字注册表
        static List<string> keyINCLUDEConfiger = new List<string>();

        /// <summary> 通过关键字名称找到对应名称的类型 </summary>
        internal T CreateIncludeKey<T>(string keyName) where T : INCLUDE
        {
            keyName = KeyChecker.FormatKey(keyName);

            Type objType = Type.GetType(keyINCLUDEtNameSpace + "." + keyName, true);

            object obj = Activator.CreateInstance(objType, new object[] { keyName });

            return obj as T;

        }

        /// <summary> 验证字符串是否为注册关键字 </summary>
        internal bool IsINCLUDERegisterKey(string keyName)
        {
            string temp = KeyChecker.FormatKey(keyName);

            return keyINCLUDEConfiger.Contains(temp);



        }

        #endregion

        #region - 表格形式关键字工厂 -

        //  关键字注册表
        private static List<string> keyTableEclipseConfiger = new List<string>();
        /// <summary> 所有解析的Eclipse表格形式关键字 </summary>
        public static List<string> KeyTableEclipseConfiger
        {
            get { return KeyConfigerFactroy.keyTableEclipseConfiger; }
            set { KeyConfigerFactroy.keyTableEclipseConfiger = value; }
        }

        #endregion

        #region - 表格形式关键字工厂 -

        //  关键字注册表
        private static List<string> gridPartConfiger = new List<string>();
        /// <summary> Grid部分关键字 </summary>
        public static List<string> GridPartConfiger
        {
            get { return KeyConfigerFactroy.gridPartConfiger; }
            set { KeyConfigerFactroy.gridPartConfiger = value; }
        }

        //  关键字注册表
        private static List<string> propsPartConfiger = new List<string>();
        /// <summary> Props部分关键字 </summary>
        public static List<string> PropsPartConfiger
        {
            get { return KeyConfigerFactroy.propsPartConfiger; }
            set { KeyConfigerFactroy.propsPartConfiger = value; }
        }

        //  关键字注册表
        private static List<string> solutionPartConfiger = new List<string>();
        /// <summary> Solution部分关键字 </summary>
        public static List<string> SolutionPartConfiger
        {
            get { return KeyConfigerFactroy.solutionPartConfiger; }
            set { KeyConfigerFactroy.solutionPartConfiger = value; }
        }

        //  关键字注册表
        private static List<string> regionsPartConfiger = new List<string>();
        /// <summary> Regions部分关键字 </summary>
        public static List<string> RegionsPartConfiger
        {
            get { return KeyConfigerFactroy.regionsPartConfiger; }
            set { KeyConfigerFactroy.regionsPartConfiger = value; }
        }


        #endregion

        #region - 输出关键字工厂 -

        //  关键字注册表
        internal static List<string> keyOutPutConfiger = new List<string>();

        #endregion

        #region - 别名关键字工厂 -

        //  关键字注册表
        internal static Dictionary<string, string> AnatherNameConfiger = new Dictionary<string, string>();

        #endregion

    }
}
