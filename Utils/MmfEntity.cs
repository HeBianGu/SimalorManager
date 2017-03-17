using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 泛型 T 内存映射文件 </summary>
    public partial class MmfEntity<T> where T : struct
    {
        #region - 成员变量 -

        private long _size;
        /// <summary> 文件大小 </summary>
        public long Size
        {
            get { return _size; }
        }

        /// <summary> 实体大小 </summary>
        public int MLeight
        {
            get { return Marshal.SizeOf(typeof(T)); }
        }

        private int _count;
        /// <summary> T 类型的数量 </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                //  设置容器大小
                this._size = MLeight * value;

                _count = value;
            }
        }

        private string _file;
        /// <summary> 文件全路径 </summary>
        public string FileInf
        {
            get { return _file; }
            set { _file = value; }
        }

        private string _name;
        /// <summary> 镜像名称 </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = Path.GetFileNameWithoutExtension(_file);
                }
                return _name;
            }
        }

        private MemoryMappedFile _mmf;
        /// <summary> 内存镜像 </summary>
        protected MemoryMappedFile Mmf
        {
            get
            {
                BuildFile(false);

                return _mmf;
            }
        }

        /// <summary> 创建文件 </summary>
        void BuildFile(bool isCreate)
        {

            //// Todo ：已经存在文件删除文件 
            //if (File.Exists(this.FileInf) && isCreate)
            //{
            //    this.Dispose();

            //    File.Delete(this.FileInf);
            //}

            // Todo ：不存在文件夹创建文件夹 
            if (!Directory.Exists(this.FileInf.GetDirectoryName()))
            {
                Directory.CreateDirectory(this.FileInf.GetDirectoryName());
            }

            _mmf = MemoryMappedFile.CreateFromFile(this.FileInf, FileMode.OpenOrCreate, _name, _size, MemoryMappedFileAccess.ReadWriteExecute);


        }

        private MemoryMappedViewAccessor _mapView;
        /// <summary> 随机访问视图  </summary>
        protected MemoryMappedViewAccessor MapView
        {
            get
            {
                if (_mmf == null || _mmf.SafeMemoryMappedFileHandle.IsClosed)
                {
                    this.BuildFile(false);
                }

                if (_mapView == null || _mapView.SafeMemoryMappedViewHandle.IsClosed)
                {
                    _mapView = _mmf.CreateViewAccessor();
                }

                return _mapView;
            }
        }

        private MemoryMappedViewStream _mapStream;
        /// <summary> 按循序访问的流 </summary>
        protected MemoryMappedViewStream MapStream
        {
            get
            {

                if (_mmf == null)
                {
                    this.BuildFile(false);
                }

                if (_mapStream == null)
                {
                    _mapStream = _mmf.CreateViewStream();
                }


                return _mapStream;
            }
            private set { _mapStream = value; }
        }

        #endregion

        /// <summary> 将 T 类型的结构从访问器读取到提供的引用中 </summary>
        public T GetPostion(long position)
        {
            T structure;

            MapView.Read<T>(position, out structure);

            return structure;
        }

        /// <summary> 读取指定索引处结构 </summary>
        public T GetIndex(int index)
        {
            long postion = index * this.MLeight;

            return this.GetPostion(postion);
        }

        /// <summary> 将 T 类型的结构从访问器读取到 T 类型的数组中 </summary>
        public T[] GetPostion(int count, long position = 0)
        {
            T[] arr = new T[count];

            MapView.ReadArray<T>(position, arr, 0, count);

            return arr;
        }

        /// <summary> 将 T 类型的结构从访问器读取到 T 类型的数组中 </summary>
        public T[] GetAll(long position = 0)
        {
            T[] arr = new T[this._count];

            MapView.ReadArray<T>(position, arr, 0, this._count);

            return arr;
        }

        /// <summary> 将一个结构写入访问器 </summary>
        public void SetPosition(long position, T structure)
        {
            MapView.Write<T>(position, ref structure);
        }

        /// <summary> 写入指定索引处结构 </summary>
        public void SetIndex(int index, T structure)
        {
            long postion = index * this.MLeight;

            this.SetPosition(postion, structure);
        }

        /// <summary> 将结构从 T 类型的数组写入访问器 </summary>
        public void SetPosition(long position, T[] arr)
        {
            MapView.WriteArray<T>(position, arr, 0, arr.Length);
        }

        /// <summary> 将结构从 T 类型的数组写入访问器 </summary>
        public void SetAll(T[] arr)
        {
            MapView.WriteArray<T>(0, arr, 0, arr.Length);
        }

        /// <summary> 将结构从 T 类型的数组写入访问器 </summary>
        public void SetAll(T t)
        {
            T[] arr = new T[this.Count];

            this.Count.DoCountWhile(l => arr[l] = t);

            MapView.WriteArray<T>(0, arr, 0, this.Count);
        }

        /// <summary> 重置大小 </summary>
        public void ReSetSize(int count)
        {
            this._count = count;

            this._size = count * this.MLeight;


            // Todo ：重置大小重新生成新文件 

            // Todo ：已经存在文件删除文件 
            if (File.Exists(this.FileInf))
            {
                this.Dispose();

                File.Delete(this.FileInf);
            }

            this.BuildFile(true);
        }

        /// <summary> 更改路径 isCopyFile 是否复制文件</summary>
        public void ChangePath(string newPath, bool isCopyFile = false)
        {
            if (string.IsNullOrEmpty(newPath)) return;

            newPath = Path.Combine(newPath, KeyConfiger.tableMapCache, this.Name + ".mp");

            // Todo ：释放资源 
            this.Close();

            if (newPath == this.FileInf) return;

            if (isCopyFile)
            {
                if (File.Exists(this.FileInf))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(newPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                    }

                    File.Copy(this.FileInf, newPath, true);

                }

                Func<string, string> getcase = l => Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(l)));

                //  删除镜像文件
                if (File.Exists(this.FileInf))
                {
                    FileInfo f = new FileInfo(this.FileInf);
                    f.Attributes = FileAttributes.Normal;
                    File.Delete(this.FileInf);

                    if (f.Directory.GetFileSystemInfos().Length == 0)
                    {
                        f.Directory.Delete(true);

                        //  同级拷贝删除原有文件夹,只适用于案例重命名，其他情况另行扩展方法
                        if (getcase(this.FileInf) == getcase(newPath))
                        {
                            f.Directory.Parent.Delete(true);
                        }
                    }
                }
            }

            this.FileInf = newPath;

            //this.BuildFile(true);
        }


    }

    partial class MmfEntity<T> : IDisposable
    {
        #region - 构造函数 -

        public MmfEntity(string fileFullPath, int Tcount)
        {
            this._file = fileFullPath;

            this._count = Tcount;

            this._size = Tcount * this.MLeight;

            this._name = Path.GetFileNameWithoutExtension(fileFullPath);

            this.BuildFile(true);
        }

        #endregion

        #region - 资源释放 -

        private bool _isDisposed = false;

        ~MmfEntity()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        /// <summary> 释放资源删除镜像文件 </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                //if (disposing)
                //{
                this.Close();

                //  删除镜像文件
                if (File.Exists(this.FileInf))
                {
                    FileInfo f = new FileInfo(this.FileInf);
                    f.Attributes = FileAttributes.Normal;
                    File.Delete(this.FileInf);

                    if (f.Directory.GetFileSystemInfos().Length == 0)
                    {
                        f.Directory.Delete(true);
                    }
                }
                this._isDisposed = true;
            }
        }

        /// <summary> 释放资源  不删除镜像文件 </summary>
        public void Close()
        {
            if (this._mapView != null) this._mapView.Dispose();

            if (this._mmf != null) this._mmf.Dispose();
        }
        #endregion
    }

    public class DxyMmfEntity<T> : MmfEntity<T> where T : struct
    {
        #region - 构造函数 -
        public DxyMmfEntity(string fileFullPath, int Tcount)
            : base(fileFullPath, Tcount)
        {
        }

        #endregion

        private int _x;
        /// <summary> X维数 </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        private int _y;
        /// <summary> Y维数 </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public void Set(int x, int y, T value)
        {
            this.SetIndex(TranFucntion(x, y), value);
        }

        /// <summary> 获取指定二维值 </summary>
        public T Get(int x, int y)
        {
            return this.GetIndex(TranFucntion(x, y));
        }

        Func<int, int, int> tranFucntion;
        /// <summary> 设置指定二维值 </summary>
        public Func<int, int, int> TranFucntion
        {
            get { return (x, y) => y * this.X + x; }
        }


    }

    public class DxyzMmfEntity<T> : MmfEntity<T> where T : struct
    {
        #region - 构造函数 -
        public DxyzMmfEntity(string fileFullPath, int x, int y, int z)
            : base(fileFullPath, x * y * z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary> 初始化方法 </summary>
        public void Init(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            base.ReSetSize(x * y * z);
        }
        #endregion

        private int _x;
        /// <summary> X维数 </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        private int _y;
        /// <summary> Y维数 </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        private int _z;
        /// <summary> Z方向维数 </summary>
        public int Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public void Set(int x, int y, int z, T value)
        {
            this.SetIndex(TranFucntion(x, y, z), value);
        }

        /// <summary> 获取指定二维值 </summary>
        public T Get(int x, int y, int z)
        {
            return this.GetIndex(TranFucntion(x, y, z));
        }

        Func<int, int, int> tranFucntion;
        /// <summary> 设置指定二维值 </summary>
        public Func<int, int, int, int> TranFucntion
        {
            get { return (x, y, z) => z * this.X * this.Y + y * this.X + x; }
        }


        //public DxyzMmfEntity<T> CopyTo(string newPath)
        //{
        //    DxyzMmfEntity<T> dxy = new DxyzMmfEntity<T>(newPath, this.X, this.Y, this.Z);

        //    return dxy;
        //}


    }
}


