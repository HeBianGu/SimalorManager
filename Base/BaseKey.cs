using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 关键字基类 包含子节点 父节点 文件基类一级基本读写方法 </summary>
    public partial class BaseKey : IDisposable
    {
        #region - 关键字成员属性 -
        public BaseKey(string pname)
        {
            name = pname;
            this.ID = Guid.NewGuid().ToString();
        }

        BaseFile baseFile = null;
        /// <summary> 文件基类 </summary>
        [Browsable(false), ReadOnly(true)]
        public BaseFile BaseFile
        {
            get { return baseFile; }
            set { baseFile = value; }
        }

        List<BaseKey> keys = new List<BaseKey>();
        /// <summary> 子关键字 </summary>
        [Browsable(false), ReadOnly(true)]
        public List<BaseKey> Keys
        {
            get { return keys; }
            set { keys = value; }
        }

        private BaseKey parentKey;
        /// <summary> 父节点 </summary>
        [Browsable(false), ReadOnly(true)]
        public BaseKey ParentKey
        {
            get { return parentKey; }
            set { parentKey = value; }
        }

        string _ID;
        /// <summary> 关键字的唯一标识 </summary>
        [Browsable(false), ReadOnly(true)]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string pid;
        [Browsable(false), ReadOnly(true)]
        public string Pid
        {
            get
            {
                return parentKey == null ? string.Empty : parentKey.ID;
            }
            set { parentKey.ID = value; }
        }

        string name;
        /// <summary> 关键字 </summary>
        [Browsable(false), ReadOnly(true)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        List<string> lines = new List<string>();
        /// <summary> 关键字内容 </summary>
        //[Browsable(false), ReadOnly(true)]
        public List<string> Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        bool isUnKnowKey = false;
        /// <summary> 是否解析 true为解析 </summary>
        public bool IsUnKnowKey
        {
            get
            {
                isUnKnowKey = !(this is UnkownKey);
                return isUnKnowKey;
            }
            set { isUnKnowKey = value; }
        }

        string titleStr;
        /// <summary> 描述信息 </summary>
        public string TitleStr
        {
            get { return titleStr; }
            set { titleStr = value; }
        }

        private ReadState _runState;

        /// <summary> 解析状态 </summary>
        public ReadState RunState
        {
            get { return _runState; }
            set { _runState = value; }
        }


        Predicate<string> _match = l => KeyChecker.IsKeyFormat(l);
        /// <summary> 当前关键字定义的检验是否为普通未识别关键字的方法 </summary>
        [Browsable(false), ReadOnly(true)]
        public Predicate<string> Match
        {
            get { return _match; }
            set { _match = value; }
        }


        Action<BaseKey, BaseKey> _createrHandler = BaseKeyHandleFactory.Instance.AddNodeHandler;
        /// <summary> 创建节点结构关系  T1本节点 T2下一节点 </summary>
        [Browsable(false), ReadOnly(true)]
        public Action<BaseKey, BaseKey> CreaterHandler
        {
            get { return _createrHandler; }
            set { _createrHandler = value; }
        }


        Func<BaseKey, BaseKey, BaseKey> _builderHandler = BaseKeyHandleFactory.Instance.InitLineHandler;


        /// <summary> 读取到下一关键字前要做的处理方法 T1上一节点 T2下一节点 T3 构建返回的节点一般是本节点 DATES特殊情况  </summary>
        [Browsable(false), ReadOnly(true)]
        public Func<BaseKey, BaseKey, BaseKey> BuilderHandler
        {
            get { return _builderHandler; }
            set { _builderHandler = value; }
        }


        Action<string> _readNewLineHandler;
        /// <summary> 当读取到一行信息触发 </summary>
        public Action<string> ReadNewLineHandler
        {
            get { return _readNewLineHandler; }
            set { _readNewLineHandler = value; }
        }

        private Func<string, string> eachLineCmdHandler = BaseKeyHandleFactory.Instance.EachLineCmdHandler;
        /// <summary> 每到一行，对这一行进行的处理 </summary>
        public Func<string, string> EachLineCmdHandler
        {
            get { return eachLineCmdHandler; }
            set { eachLineCmdHandler = value; }
        }

        private Predicate<string> _isKeyChar = BaseKeyHandleFactory.Instance.IsKeyFormat;
        /// <summary> 当前关键字下判断子内容是否是关键字的方法 </summary>
        public Predicate<string> IsKeyChar
        {
            get { return _isKeyChar; }
            set { _isKeyChar = value; }
        }


        #endregion

        #region - 关键字操作方法 -

        /// <summary> 写关键字  </summary>
        public virtual void WriteKey(StreamWriter writer)
        {
            BaseKey index = null;

            //  写本行
            foreach (var str in this.lines)

            {

                //Guid tempId;

                //if (!Guid.TryParse(str, out tempId))
                //{
                writer.WriteLine(str);
                //}
            }


            //  写子关键字
            foreach (BaseKey key in this.keys)
            {
                key.WriteKey(writer);
            }

        }

        /// <summary> 读取关键字内容 (具体关键字读取方法不同)  return  用于关键字传递 </summary>
        public virtual BaseKey ReadKeyLine(StreamReader reader)
        {
            string tempStr = string.Empty;

            #region - 读取数据 -

            while (!reader.EndOfStream)
            {

                // HTodo  ：读取数据要处理的方法 一般用来截取前面空格判断是否解析成关键字 
                tempStr = this.eachLineCmdHandler(reader.ReadLine());

                try
                {
                    // HTodo  ：当前关键字用来判断文本是否为关键字的方法 
                    if (this.IsKeyChar(tempStr))
                    {
                        #region - 交接关键字 -

                        // Todo ：没有找到主文件默认Eclipse关键字 
                        SimKeyType typesim = this.baseFile == null ? SimKeyType.Eclipse : this.baseFile.SimKeyType;

                        BaseKey newKey = KeyConfigerFactroy.Instance.CreateKey<BaseKey>(tempStr, typesim);

                        LogProviderHandler.Instance.OnRunLog("", "正在读取关键字 - " + newKey.Name);

                        BaseKey perTempKey = this;

                        if (this._builderHandler != null)
                        {
                            // Todo ：当碰到新关键字 触发本节点构建方法 
                            BaseKey temp = this._builderHandler.Invoke(this, newKey);

                            if (temp != null)
                            {
                                perTempKey = temp;
                            }

                            if (this.baseFile != null && this is IProductTime)
                            {
                                // HTodo  ：将读取到的生产信息记录到主文件中，用于解析TSTEP 
                                IProductTime p = this as IProductTime;
                                this.baseFile.ReadTempTime = p.DateTime;
                            }
                        }

                        if (newKey._createrHandler != null)
                        {
                            // Todo ：触发新关键字构建节点结构的方法 
                            newKey._createrHandler.Invoke(perTempKey, newKey);
                        }


                        // Todo ：读到未解析关键字触发事件 
                        if (newKey is UnkownKey)
                        {
                            // Todo ：触发事件 
                            if (newKey.BaseFile != null && newKey.BaseFile.OnUnkownKey != null)
                            {
                                newKey.BaseFile.OnUnkownKey(newKey.BaseFile, newKey);
                            }
                        }


                        // Todo ：开始读取新关键字 
                        newKey.ReadKeyLine(reader);

                        #endregion
                    }
                    else
                    {
                        #region - 记录数据 -

                        if (tempStr.IsNotExcepLine())
                        {

                            if (this.ReadNewLineHandler == null)
                            {
                                // Todo ：当前关键字没有实时读取方法 
                                this.Lines.Add(tempStr);
                            }
                            else
                            {
                                // Todo ：当前关键字实现实时读取方法 
                                this.ReadNewLineHandler(tempStr);
                            }
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    LogProviderHandler.Instance.OnErrLog("读取关键字" + this.GetType().Name + "错误!", ex);
                }
                finally
                {

                }
            }

            #endregion

            return this;
        }

        #endregion

        #region - 关键字查询操作 -

        /// <summary> 是否相等(只比较名称) </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is BaseKey)) return false;

            BaseKey pKey = obj as BaseKey;

            return pKey.ID.Equals(this.ID);
        }

        /// <summary> 递归获取节点 match1 = 查找匹配条件 match2 = 结束查找匹配条件 </summary>
        public bool GetKeys<T>(ref List<T> findKey, BaseKey key, Predicate<BaseKey> match, Predicate<BaseKey> endOfMatch) where T : class
        {
            if (endOfMatch(key))
            {
                return true;
            }

            if (match(key) && key is T)
            {
                T find = key as T;
                findKey.Add(find);

            }

            if (key.Keys.Count > 0)
            {
                foreach (var k in key.Keys)
                {
                    if (k is BaseKey)
                    {
                        BaseKey kn = k as BaseKey;
                        //  递归处
                        bool isEndOfMatch = GetKeys(ref findKey, kn, match, endOfMatch);

                        if (isEndOfMatch) return true;
                    }
                }
            }
            return false;
        }

        /// <summary> 查找所有匹配类型和匹配规则的项 </summary>
        public List<T> FindAll<T>(Predicate<T> match) where T : class
        {
            Predicate<BaseKey> m = l =>
            {
                if (l is T)
                {
                    T t = l as T;

                    return match(t);
                }

                return false;
            };

            List<T> findKeys = new List<T>();

            //    l=>false 一直查询
            GetKeys<T>(ref findKeys, this, m, l => false);

            return findKeys;


        }

        /// <summary> 查找所有关键字类型 </summary>
        public List<T> FindAll<T>() where T : class
        {
            return FindAll<T>(l => l is T);
        }

        /// <summary> 对每个节点执行方法 </summary>
        public void Foreach(Action<BaseKey> act)
        {
            //  执行方法
            act(this);

            //  子节点执行方法
            if (this.Keys.Count > 0)
            {
                foreach (BaseKey k in this.Keys)
                {
                    //  递归处
                    k.Foreach(act);
                }
            }
        }

        /// <summary> 对指定类型的节点执行方法 </summary>
        public void Foreach<T>(Action<BaseKey> act) where T : BaseKey
        {
            if (this is T)
            {
                //  执行方法
                act(this);
            }


            //  子节点执行方法
            if (this.Keys.Count > 0)
            {
                foreach (BaseKey k in this.Keys)
                {
                    //  递归处
                    k.Foreach(act);
                }
            }
        }


        /// <summary> 获取匹配的一个节点 找到一个立即返回 </summary>
        T GetKeys<T>(BaseKey key, Predicate<BaseKey> match) where T : class
        {

            if (match(key) && key is T)
            {
                T find = key as T;
                return find;

            }

            if (key.Keys.Count > 0)
            {
                foreach (var k in key.Keys)
                {
                    if (k is BaseKey)
                    {
                        BaseKey kn = k as BaseKey;

                        T temp = GetKeys<T>(kn, match);
                        //  递归处
                        if (temp != null)
                        {
                            return temp;
                        }
                    }
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 查找所有关键字类型 </summary>
        public T Find<T>() where T : BaseKey
        {
            return this.Find<T>(l => true);
        }

        /// <summary> 查找所有关键字类型 按基类BaseKey查找 </summary>
        public T Find<T>(Predicate<T> match) where T : BaseKey
        {
            Predicate<BaseKey> m = l =>
            {
                if (l is T)
                {
                    T t = l as T;

                    return match(t);
                }

                return false;
            };

            return GetKeys<T>(this, m);
        }


        /// <summary> 查找关键字类型最后一个  按泛型查找 </summary>
        public T FindLast<T>(Predicate<T> match) where T : BaseKey
        {
            Predicate<BaseKey> m = l =>
              {
                  if (l is T)
                  {
                      T t = l as T;

                      return match(t);
                  }

                  return false;
              };

            var fs = this.FindAll<T>(m);

            if (fs != null && fs.Count > 0) return fs.Last();

            return null;
        }

        /// <summary> 移除 </summary>
        public void Delete(BaseKey key)
        {
            this.keys.Remove(key);
        }

        /// <summary> 移除 </summary>
        public void Delete()
        {
            this.parentKey.Delete(this);
        }

        /// <summary> 删除所有节点 （不是父节点也删除） </summary>
        public void DeleteAll<T>(List<T> keys) where T : BaseKey
        {
            if (keys == null || keys.Count == 0) return;

            foreach (T b in keys) // 注：这个删除方法用迭代器删除可能会有问题
            {
                b.Delete();
            }
        }

        /// <summary> 删除所有类型节点） </summary>
        public void DeleteAll<T>() where T : BaseKey
        {
            var keys = this.FindAll<T>();

            foreach (var v in keys)
            {
                if (v.parentKey != null)
                {
                    v.parentKey.Delete(v);
                }
            }
        }

        /// <summary> 删除所有类型节点 </summary>
        public void DeleteAll<T>(Predicate<T> match) where T : BaseKey
        {
            var keys = this.FindAll<T>();

            foreach (var v in keys)
            {
                if (!match(v)) continue;

                if (v.parentKey != null)
                {

                    v.parentKey.Delete(v);
                }
            }
        }

        /// <summary> 插入节点到指定位置 </summary>
        public void InsertKey(int index, BaseKey key)
        {
            this.keys.Insert(index, key);
            key.parentKey = this;
            this.Lines.Add(key.ID);
            key.baseFile = this.baseFile;
        }

        /// <summary> 插入在指定节点后 </summary>
        public bool InsertAfter(BaseKey key, BaseKey inKey)
        {
            BaseKey parentKey = key.parentKey;

            inKey.parentKey = parentKey;

            if (parentKey == null)
            {
                return false;
            }
            else
            {
                int findKey = parentKey.Keys.FindIndex(l => l.Equals(key));

                //  找到到当前行的占位标记
                int findLine = parentKey.lines.FindIndex(l => l == key.ID);

                if (findKey == -1 || findLine == -1)
                {
                    return false;
                }
                else
                {
                    parentKey.Keys.Insert(findKey + 1, inKey);
                    parentKey.lines.Insert(findLine + 1, inKey.ID);
                    return true;
                }
            }
        }

        /// <summary> 插入在本节点后 </summary>
        public bool InsertAfter(BaseKey inKey)
        {
            BaseKey parentKey = this.parentKey;
            inKey.parentKey = parentKey;
            if (parentKey == null)
            {
                return false;
            }
            else
            {
                int findKey = parentKey.Keys.FindIndex(l => l.Equals(this));

                ////  找到到当前行的占位标记
                //int findLine = parentKey.lines.FindIndex(l => l == this.ID);

                if (findKey == -1)
                {
                    return false;
                }
                else
                {
                    parentKey.Keys.Insert(findKey + 1, inKey);
                    return true;
                }
            }
        }

        /// <summary> 插入在指定节点前 </summary>
        public bool InsertBefore(BaseKey key, BaseKey inKey)
        {
            BaseKey parentKey = key.parentKey;
            inKey.parentKey = parentKey;

            if (parentKey == null)
            {
                return false;
            }
            else
            {
                //  当前关键字标记
                int findKey = parentKey.Keys.FindIndex(l => l.Equals(key));

                //  找到到当前行的占位标记
                int findLine = parentKey.lines.FindIndex(l => l == key.ID);

                if (findKey == -1 || findLine == -1)
                {
                    return false;
                }
                else
                {
                    parentKey.Keys.Insert(findKey, inKey);
                    parentKey.lines.Insert(findLine, inKey.ID);
                    return true;
                }
            }
        }

        /// <summary> 插入在本节点前 </summary>
        public bool InsertBefore(BaseKey inKey)
        {
            BaseKey parentKey = this.parentKey;

            inKey.parentKey = parentKey;

            if (parentKey == null)
            {
                return false;
            }
            else
            {
                //  当前关键字标记
                int findKey = parentKey.Keys.FindIndex(l => l.Equals(this));

                //  找到到当前行的占位标记
                //int findLine = parentKey.lines.FindIndex(l => l == this.ID);

                if (findKey == -1)//|| findLine == -1
                {
                    return false;
                }
                else
                {
                    parentKey.Keys.Insert(findKey, inKey);
                    //parentKey.lines.Insert(findLine, inKey.ID);
                    return true;
                }
            }
        }

        /// <summary> 是否存在关键字 </summary>
        public bool Exist(string key)
        {
            return this.Keys.Exists(l => l.Name == key);
        }

        /// <summary> 查找Key </summary>
        public BaseKey Find(BaseKey key)
        {
            return this.Keys.Find(l => l.Equals(key));
        }

        /// <summary> 查找关键字Key </summary>
        public BaseKey Find(string key)
        {
            return this.Keys.Find(l => l.Name.Equals(key));
        }

        /// <summary> 查找指定关键字处的索引 </summary>
        public int FindIndex(BaseKey key)
        {
            return this.keys.FindIndex(l => l == key);
        }

        /// <summary> 从索引出移除所有关键字 </summary>
        public void RemoveRange(int index)
        {
            this.keys.RemoveRange(index, this.keys.Count - index);
        }

        /// <summary> 从索引出移除所有关键字 </summary>
        public void RemoveRange<T>(int index) where T : BaseKey
        {
            if (index >= this.keys.Count)
            {
                return;
            }

            List<T> fs = new List<T>();

            for (int i = index; i < this.keys.Count; i++)
            {
                if (this.keys[i] is T)
                {
                    fs.Add(this.keys[i] as T);
                }
            }

            foreach (var v in fs)
            {
                //  清除数据
                this.keys.Remove(v);

                //  清除占位标识
                this.lines.Remove(v._ID);

            }
            string ss = null;
        }

        /// <summary> 在本节点下面查找 如果有返回 如果没有创建并插入到本节点下</summary>
        public T CreateSingle<T>(string keyName) where T : BaseKey
        {
            Type t = typeof(T);

            T find = this.Find<T>();

            if (find == null)
            {
                find = Activator.CreateInstance(t, new string[] { keyName }) as T;
                this.Add(find);
            }

            return find;
        }

        /// <summary> 在本节点下面查找指定数量的关键字 如果有返回 如果没有创建并插入到本节点下</summary>
        public List<T> CreateOfCount<T>(int count) where T : BaseKey
        {
            Type t = typeof(T);

            List<T> find = this.FindAll<T>();

            int addCount = count - find.Count;

            addCount.DoCountWhile(l =>
                {
                    T f = Activator.CreateInstance(t, new string[] { typeof(T).Name }) as T;
                    this.Add(f);
                    find.Add(f);
                });

            return find;
        }

        /// <summary> 增加节点 注意： 此方法改变了原节点的父节点引用 </summary>
        public void Add(BaseKey key)
        {
            key.ParentKey = this;
            //  记录位置
            //this.Lines.Add(key.ID);
            this.keys.Add(key);

            key.baseFile = this.baseFile;

            // Todo ：替换所有关键字文件 
            key.Foreach(l => l.baseFile = this.baseFile);
        }

        /// <summary> 批量增加节点 </summary>
        public void AddRange<T>(List<T> keys) where T : BaseKey
        {
            foreach (var v in keys)
            {
                this.Add(v);
            }
        }

        /// <summary> 增加节点 注意： 此方法改变了原节点的父节点引用 </summary>
        public void AddClone(BaseKey key)
        {
            this.keys.Add(key);
            key.baseFile = this.baseFile;
        }

        /// <summary> 批量增加节点 </summary>
        public void AddCloneRange<T>(List<T> keys) where T : BaseKey
        {
            foreach (var v in keys)
            {
                this.AddClone(v);
            }
        }

        /// <summary> 清理数据 </summary>
        public void Clear()
        {
            this.Lines.Clear();
            this.Keys.Clear();
        }

        /// <summary> 清理数据 </summary>
        public void ClearChild(Predicate<BaseKey> match)
        {
            this.Lines.Clear();

            List<BaseKey> bk = this.keys.FindAll(l => match(l));


            bk.ForEach(l => this.keys.Remove(l));

            //this.keys.RemoveAll(bk);

            //this.keys.ForEach(l =>
            //    {
            //        if (match(l))
            //        {
            //            this.Keys.Remove(l);
            //        }
            //    }
            //);
        }



        /// <summary> 替换对应节点的所有内容 </summary>
        public bool ExChangeData(BaseKey key)
        {
            //  删除本节点所有数据
            this.Keys.RemoveAll(l => true);

            if (key == null) return true;

            ////  添加替换的数据
            //this.Keys.AddRange(key.Keys);

            foreach (var item in key.Keys)
            {
                this.Add(item);
            }

            return true;

        }

        /// <summary> 将本节点替换为指定节点 </summary>
        public void ReplaceTo(BaseKey key)
        {
            this.InsertBefore(key);

            this.Delete();
        }
        
        #endregion

        public override string ToString()
        {
            return this.name;
        }

    }



    partial class BaseKey : IDisposable
    {
        #region - 资源释放 -

        protected bool _isDisposed = false;
        ~BaseKey()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            //  告诉GC不需要再次调用
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                // 释放托管资源

                // 释放非托管资源

                // 释放大对象

                this._isDisposed = true;
            }
        }

        #endregion
    }


    /// <summary> 标示节点是父节点 </summary>  
    /// 

    public interface IRootNode
    {
        List<string> GetChildKeys();
    }


    public enum ReadState
    {
        [Desc("完成")]
        Success = 0,
        [Desc("错误")]
        Error
    }


}
