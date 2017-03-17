using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using OPT.Product.SimalorManager.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 文件基础类 </summary>
    public abstract partial class BaseFile : IDisposable
    {

        #region - 文件公用成员 -

        public BaseFile()
        {
            this.Key = new FileKey("BaseFile");
            this.Key.BaseFile = this;

        }
        public BaseFile(string _filePath, string mmfDirPath = null)
        {
            filePath = _filePath;
            _mmfDirPath = mmfDirPath == null ? _filePath.GetDirectoryName() : mmfDirPath;
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

            _mmfDirPath = fileFulPath.GetDirectoryName();

            this.Key = new FileKey(fileName);
            this.Key.BaseFile = this;

            InitializeComponent();
        }

        public BaseFile(string _filePath, WhenUnkownKey UnkownEvent, string mmfDirPath = null)
        {
            filePath = _filePath;
            _mmfDirPath = mmfDirPath == null ? _filePath.GetDirectoryName() : mmfDirPath;
            fileName = _filePath.GetFileNameWithoutExtension();
            this.Key = new FileKey(fileName);
            this.Key.BaseFile = this;
            this.OnUnkownKey = UnkownEvent;
            InitializeComponent();
        }

        public BaseFile(string _filePath, WhenUnkownKey UnkownEvent, Predicate<INCLUDE> isReadIncHandle, string mmfDirPath = null)
        {
            filePath = _filePath;
            fileName = _filePath.GetFileNameWithoutExtension();
            _mmfDirPath = mmfDirPath == null ? _filePath.GetDirectoryName() : mmfDirPath;
            this.Key = new FileKey(fileName);
            this.Key.BaseFile = this;
            this.OnUnkownKey = UnkownEvent;
            _isReadIncHandle = isReadIncHandle;

            InitializeComponent();
        }

        private string _mmfDirPath;
        /// <summary> 镜像文件夹路径 </summary>
        public string MmfDirPath
        {
            get { return _mmfDirPath; }
            set { _mmfDirPath = value; }
        }

        private Predicate<INCLUDE> _isReadIncHandle = l => true;

        /// <summary> 匹配INCLUDE是否读取 </summary>
        public Predicate<INCLUDE> IsReadIncHandle
        {
            get { return _isReadIncHandle; }
            set { _isReadIncHandle = value; }
        }

        SimKeyType _simKeyType = SimKeyType.Eclipse;

        public SimKeyType SimKeyType
        {
            get { return _simKeyType; }
            set { _simKeyType = value; }
        }

        string fileName;
        /// <summary> 文件名  Case.data </summary>
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
        /// <summary> 文件全路径 包含文件名 E:\WorkArea\LaoBB\3106\Case.data</summary>
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


        List<RunLogModel> runLog = new List<RunLogModel>();
        /// <summary> 运行日志 </summary>
        public List<RunLogModel> RunLog
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

        private string myVar;

        public string MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        RegionParam tempRegion;
        /// <summary> 用于记录修正网格的临时范围 记录过程包括 DIMENS BOX 和其他修正关键字 </summary>
        public RegionParam TempRegion
        {
            get { return tempRegion; }
            set { tempRegion = value; }
        }


        private int _x;
        /// <summary> x维数 </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        private int _y;
        /// <summary> y维数 </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        private int _z;
        /// <summary> z维数 </summary>
        public int Z
        {
            get { return _z; }
            set { _z = value; }
        }


        /// <summary> 说明 </summary>
        public string FielDetail
        {
            get
            {
                string version = string.Empty;
                try
                {
                    version = Assembly.GetEntryAssembly().GetName().Version.ToString();
                }
                catch
                {

                }

                string machineName = Environment.MachineName + "  " + Environment.UserName;

                return string.Format(KeyConfiger.MainFileDetial, this.FileName, this.FilePath, DateTime.Now.ToString("yyyy-MM-dd hh:mi:ss"), version, machineName);
            }
        }

        private DateTime _readTempTime;
        /// <summary> 当前解析到的生产时间 </summary>
        internal DateTime ReadTempTime
        {
            get { return _readTempTime; }
            set { _readTempTime = value; }
        }


        private DoubleType _doubleType;
        /// <summary> 孔隙类型 </summary>
        public DoubleType DoubleType
        {
            get { return _doubleType; }
            set { _doubleType = value; }
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
        public void SaveAs(string path)
        {
            // Todo ：保存时占用一个信号(设计原则：不同类型的模拟器保存只允许同时保存一个，其他时等待结束)
            EngineConfigerService.Instance.SaveKeyTypeLock=  this.SimKeyType;

            if(this.key.Find<FIELD>()!=null)
            {
                EngineConfigerService.Instance.Unittype = UnitType.FIELD;
            }

            if (this.key.Find<METRIC>() != null)
            {
                EngineConfigerService.Instance.Unittype = UnitType.METRIC;
            }


            try
            {
                this.SaveAsExtend(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Todo ：释放一个信号 
                EngineConfigerService.Instance.SaveLockRelease();
            }

        }

        public abstract void SaveAsExtend(string path);

        #endregion - 子类扩展方法 End -


        /// <summary> 读取到为解析的 </summary>
        public WhenUnkownKey OnUnkownKey;

    }
    partial class BaseFile : IDisposable
    {
        #region - 资源释放 -
        
        // Todo ：保证重复释放资源时系统异常 
        private bool _isDisposed = false;

        ~BaseFile()
        {
            // 此处只需要释放非托管代码即可，因为GC调用时该对象资源可能还不需要释放
            Dispose(false);
        }
       
        public void Dispose()
        {
            Dispose(true);
            
            // Todo ：告诉GC不需要再次调用 
            GC.SuppressFinalize(this); 
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                //  查找出所有要释放资源的关键字释放资源
                var dis = this.Key.FindAll<TableKey>();

                foreach (var item in dis)
                {
                    item.Dispose();
                }

                this.filePath = null;
                this.fileName = null;
                this.key.Clear();
                this.lines = null;
                this.modify.Clear();
                this.isReadBigData = default(bool);

                this._isDisposed = true;
            }
        }

        #endregion
    }


    /// <summary> 解析到未注册关键字的事件 </summary>
    public delegate void WhenUnkownKey(object sender, BaseKey key);

    /// <summary> 单位类型 </summary>
    public enum UnitType : int
    {
        /// <summary> 公制 </summary>
        [Description("公制")]
        METRIC = 0,
        /// <summary> 英制 </summary>
        [Description("英制")]
        FIELD
    }
    /// <summary> 流体类型 </summary>
    public enum MetricType
    {
        /// <summary> 黑油模型 </summary>
        [Description("黑油模型")]
        BLACKOIL = 0,
        /// <summary> 油水模型 </summary>
        [Description("油水模型")]
        OILWATER,
        /// <summary> 气水模型 </summary>
        [Description("气水模型")]
        GASWATER,
        /// <summary> 挥发油模型 </summary>
        [Description("挥发油模型")]
        HFOIL

    }

    /// <summary> 孔隙类型 </summary>
    public enum DoubleType
    {
        /// <summary> 单孔介质 </summary>
        [Description("单孔介质")]
        DKJZMX = 0,
        /// <summary> 双孔单渗 </summary>
        [Description("双孔单渗")]
        SKDSMX,
        /// <summary> 双孔双渗 </summary>
        [Description("双孔双渗")]
        SKSSMX,
    }


    public class RunLogModel
    {
        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private ReadState _state;

        public ReadState State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _detial;

        public string Detial
        {
            get { return _detial; }
            set { _detial = value; }
        }

        private string _desc;

        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }



    }

}
