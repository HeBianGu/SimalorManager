using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 文件基础类 </summary>
    public abstract class BaseFile : IDisposable
    {

        #region - Start 文件公用成员 -

        public BaseFile()
        {
            this.Key = new FileKey("BaseFile");
            this.Key.BaseFile = this;

        }
        public BaseFile(string _filePath)
        {
            filePath = _filePath;

            fileName = _filePath.GetFileNameWithoutExtension();

            this.Key = new FileKey(fileName);
            this.Key.BaseFile = this;

            InitializeComponent();
        }

        /// <summary> 加载数据 数模全路径 </summary>
        public void Load(string fileFulPath)
        {
            filePath = fileFulPath;

            fileName = fileFulPath.GetFileNameWithoutExtension();

            this.Key = new FileKey(fileName);
            this.Key.BaseFile = this;

            InitializeComponent();
        }

        public BaseFile(string _filePath, WhenUnkownKey UnkownEvent)
        {
            filePath = _filePath;
            fileName = _filePath.GetFileNameWithoutExtension();
            this.Key = new FileKey(fileName);
            this.Key.BaseFile = this;
            this.OnUnkownKey = UnkownEvent;
            InitializeComponent();
        }

        public BaseFile(string _filePath, WhenUnkownKey UnkownEvent, bool isReadInclu = false)
        {
            filePath = _filePath;
            fileName = _filePath.GetFileNameWithoutExtension();
            this.Key = new FileKey(fileName);
            this.Key.BaseFile = this;
            this.OnUnkownKey = UnkownEvent;
            isReadIclude = isReadInclu;

            InitializeComponent();
        }

        bool isReadIclude = true;


        SimKeyType _simKeyType = SimKeyType.Eclipse;

        public SimKeyType SimKeyType
        {
            get { return _simKeyType; }
            set { _simKeyType = value; }
        }

        public bool IsReadIclude
        {
            get { return isReadIclude; }
            set { isReadIclude = value; }
        }
        string fileName;
        /// <summary> 文件名 </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                //  更新文件路径
                filePath = this.FilePath.GetDirectoryName() + "\\" + value + this.FilePath.GetExtension();
                fileName = value;
            }
        }

        string filePath;
        /// <summary> 文件全路径 包含文件名</summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        FileKey key;
        /// <summary> 关键字集合 </summary>
        public FileKey Key
        {
            get { return key; }
            set { key = value; }
        }

        List<string> lines = new List<string>();
        /// <summary> 文件注释内容 </summary>
        public List<string> Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        bool isReadBigData = false;
        /// <summary> 是否读取大数据 </summary>
        public bool IsReadBigData
        {
            get { return isReadBigData; }
            set { isReadBigData = value; }
        }

        List<BaseKey> modify = new List<BaseKey>();
        /// <summary> 修正参数的临时列表 </summary>
        public List<BaseKey> Modify
        {
            get { return modify; }
            set { modify = value; }
        }


        List<string> runLog = new List<string>();
        /// <summary> 运行日志 </summary>
        public List<string> RunLog
        {
            get { return runLog; }
            set { runLog = value; }
        }

        List<string> errLog = new List<string>();

        /// <summary> 错误日志 </summary>
        public List<string> ErrLog
        {
            get { return errLog; }
            set { errLog = value; }
        }


        RegionParam tempRegion;
        /// <summary> 用于记录修正网格的临时范围 记录过程包括 DIMENS BOX 和其他修正关键字 </summary>
        public RegionParam TempRegion
        {
            get { return tempRegion; }
            set { tempRegion = value; }
        }

        #endregion - 文件公用成员 End -


        #region - Start 文件公用方法 -


        #endregion - 文件公用方法 End -


        #region - Start 子类扩展方法 -

        /// <summary> 初始化文件 </summary>
        protected abstract void InitializeComponent();

        /// <summary> 保存 </summary>
        public abstract void Save();

        /// <summary> 另存为 </summary>
        public abstract void SaveAs(string path);

        #endregion - 子类扩展方法 End -


        public void Dispose()
        {
            this.filePath = null;
            this.fileName = null;
            this.key = null;
            this.lines = null;
            this.modify.Clear();
            this.isReadBigData = default(bool);
        }

        //public void Delete(Predicate<BaseKey> match)
        //{
        //    this.key.Keys.RemoveAll(match);
        //}

        //public bool Exist(Predicate<BaseKey> match)
        //{
        //    return this.key.Keys.Exists(match);
        //}

        //public BaseKey Find(Predicate<BaseKey> match)
        //{
        //    BaseKey findKey = this.key.Keys.Find(match);
        //    return findKey;
        //}

        //public List<BaseKey> FindAll(Predicate<BaseKey> match)
        //{
        //    return this.key.Keys.FindAll(match);
        //}


        /// <summary> 读取到为解析的 </summary>
        public WhenUnkownKey OnUnkownKey;

    }

    /// <summary> 解析到未注册关键字的事件 </summary>
    public delegate void WhenUnkownKey(object sender, BaseKey key);
}
