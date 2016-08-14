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
    public class BaseKey
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
        [Browsable(false), ReadOnly(true)]
        public List<string> Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        bool isUnKnowKey = false;

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

        Predicate<string> _match = l => KeyChecker.IsKeyFormat(l);
        /// <summary> 当前关键字定义的检验是否为普通未识别关键字的方法 </summary>
        public Predicate<string> Match
        {
            get { return _match; }
            set { _match = value; }
        }


        Action<BaseKey, BaseKey> _createrHandler = BaseKeyHandleFactory.Instance.AddNodeHandler;
        /// <summary> 创建节点结构关系  T1本节点 T2下一节点 </summary>
        public Action<BaseKey, BaseKey> CreaterHandler
        {
            get { return _createrHandler; }
            set { _createrHandler = value; }
        }


        Action<BaseKey, BaseKey> _builderHandler;
        /// <summary> 读取到下一关键字前要做的处理方法 T1本节点 T2下一节点  </summary>
        public Action<BaseKey, BaseKey> BuilderHandler
        {
            get { return _builderHandler; }
            set { _builderHandler = value; }
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

                Guid tempId;
                if (!Guid.TryParse(str, out tempId))
                {
                    writer.WriteLine(str);
                }
                //else
                //{
                //    index = this.keys.Find(l => l.ID == str.Trim());

                //    if (index != null)
                //    {
                //        //  写入到指定位置
                //        index.WriteKey(writer);
                //    }
                //    else
                //    {
                //        writer.WriteLine(str);
                //    }
                //}
            }


            //  写子关键字
            foreach (BaseKey key in this.keys)
            {
                key.WriteKey(writer);
            }

        }

        /// <summary> 读取关键字内容 (具体关键字读取方法不同) </summary>
        public virtual BaseKey ReadKeyLine(StreamReader reader)
        {
            string tempStr = string.Empty;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().TrimEnd();

                if (tempStr.IsKeyFormat())
                {
                    BaseKey newKey = KeyConfigerFactroy.Instance.CreateKey<BaseKey>(tempStr);

                    if (this._builderHandler != null)
                    {
                        //  当碰到新关键字 触发本节点构建方法
                        this._builderHandler.Invoke(this, newKey);
                    }

                    if (newKey._createrHandler != null)
                    {
                        //  触发新关键字构建节点结构的方法
                        newKey._createrHandler.Invoke(this, newKey);
                    }

                    //  读到未解析关键字触发事件
                    if (newKey is UnkownKey)
                    {
                        //  触发事件
                        if (newKey.BaseFile != null && newKey.BaseFile.OnUnkownKey != null)
                        {
                            newKey.BaseFile.OnUnkownKey(newKey.BaseFile, newKey);
                        }
                    }

                    //  开始读取新关键字
                    newKey.ReadKeyLine(reader);
                }
                else
                {
                    if (tempStr.IsNotExcepLine())
                    {
                        //  不是记录行
                        this.Lines.Add(tempStr);
                    }
                }
            }

            if (this._builderHandler != null)
            {
                //  读到最后触发一次创建方法
                this._builderHandler.Invoke(this, this);
            }
            //  读到末尾返
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

        /// <summary> 递归获取节点 </summary>
        void GetKeys<T>(ref List<T> findKey, BaseKey key, Predicate<BaseKey> match) where T : class
        {

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
                        GetKeys(ref findKey, kn, match);
                    }
                }
            }
        }

        public List<T> FindAll<T>(Predicate<BaseKey> match) where T : class
        {
            List<T> findKeys = new List<T>();

            GetKeys<T>(ref findKeys, this, match);

            return findKeys;


        }

        /// <summary> 查找所有关键字类型 </summary>
        public List<T> FindAll<T>() where T : class
        {
            return FindAll<T>(l => l is T) as List<T>;
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
        public T Find<T>() where T : class
        {
            return this.Find<T>(l => true);
        }

        /// <summary> 查找所有关键字类型 </summary>
        public T Find<T>(Predicate<BaseKey> match) where T : class
        {
            return GetKeys<T>(this, match);
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
            if (keys == null || keys.Count == 0)
            {
                return;
            }
            foreach (T b in keys)
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
                int findLine = parentKey.lines.FindIndex(l => l == this.ID);

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

        /// <summary> 增加节点 注意： 此方法改变了原节点的父节点引用 </summary>
        public void Add(BaseKey key)
        {
            key.ParentKey = this;
            //  记录位置
            //this.Lines.Add(key.ID);
            this.keys.Add(key);
            key.baseFile = this.baseFile;
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

        /// <summary> 替换对应节点的所有内容 </summary>
        public bool ExChangeData(BaseKey key)
        {
            //  删除本节点所有数据
            this.Keys.RemoveAll(l => true);

            //  添加替换的数据
            this.Keys.AddRange(key.Keys);

            return true;

        }

        ///// <summary> 复制对象 </summary>
        //public BaseKey Clone()
        //{
        //    if(this.keys.Count>0)
        //    {  
        //        List<BaseKey> ks=new List<BaseKey>();

        //         foreach(var k in this.keys)
        //        {
        //            ks.Add(k.Clone());
        //        }
        //        this.Keys.Clear();
        //        this.Keys = ks;
        //    }

        //     //object obj = Activator.CreateInstance(objType, new object[] { objType.Name });

        //     BaseKey c = new BaseKey(this.name);

        //     return c;


        //}
        #endregion

        public override string ToString()
        {
            return this.name;
        }

    }

    /// <summary> 标示节点是父节点 </summary>
    public interface IRootNode
    {

    }

    public static class BaseKeyExtend
    {
        /// <summary> 递归查找指定节点的八大关键字 </summary>
        public static ParentKey GetParentKey(this BaseKey bk)
        {
            if (bk.ParentKey != null)
            {
                if (bk.ParentKey is ParentKey)
                {
                    return bk.ParentKey as ParentKey;
                }
                else
                {
                    return bk.ParentKey.GetParentKey();
                }
            }
            else
            {
                return null;
            }
        }
    }
}
