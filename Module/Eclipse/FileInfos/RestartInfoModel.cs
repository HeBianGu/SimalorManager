using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
using HeBianGu.Product.SimalorManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using HeBianGu.Product.SimalorManager.RegisterKeys.SimON;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> 主文件模型 </summary>
    public class MainFileRestart : RestartInfoModel
    {

    }

    /// <summary> 重启模型 </summary>
    public class RestartInfoModel
    {
        DateTime restartTime;
        /// <summary> 重启时间 </summary>
        public DateTime RestartTime
        {
            get { return restartTime; }
            set { restartTime = value; }
        }

        string fileName;
        /// <summary> 重启名称 </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        RestartInfoModel parent;
        /// <summary> 父重启点 </summary>
        public RestartInfoModel Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        string parentName;
        /// <summary> 父节点文件名称 </summary>
        public string ParentName
        {
            get
            {
                if (parent != null)
                {
                    return parentName = parent.fileName;
                }
                else
                {
                    return parentName;
                }

            }
            set
            {
                parentName = value;
            }
        }

        string filePath;
        /// <summary> 文件全路径 </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        string _resultFilePath;

        /// <summary> 结果文件路径(用于判断结果文件是否存在) </summary>
        public string ResultFilePath
        {
            get
            {

                return _resultFilePath;
                //string p = this.filePath + "\\" + Path.GetFileNameWithoutExtension(this.fileName) + ".EGRID";

                //return p;
            }
            set
            {
                _resultFilePath = value;
            }
        }

        string _mainFilePath;
        /// <summary> 结果文件路径 </summary>
        public string MainFilePath
        {
            get
            {

                return _mainFilePath;
                //string _maifilePath = this.filePath + "\\" + this.fileName + ".data";

                //return _maifilePath;
            }
            set
            {
                _mainFilePath = value;
            }

        }

        private int index;
        /// <summary> 重启索引值 </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        string schPath;

        public string SchPath
        {
            get { return schPath; }
            set { schPath = value; }
        }

        string initPath;

        public string InitPath
        {
            get { return initPath; }
            set { initPath = value; }
        }

        [NonSerialized]
        SOLUTION solution = null;
        ///<summary> 初始化部分数据 </summary>
        public SOLUTION Solution
        {
            get { return solution; }
            set { solution = value; }
        }
        [NonSerialized]
        SCHEDULE schedule = null;
        /// <summary> 生产部分数据 </summary>

        public SCHEDULE Schedule
        {
            get
            {
                return schedule;
            }
            set { schedule = value; }
        }
        //[NonSerialized]
        //EclipseData mainData;
        /////<summary> 主文件 </summary>

        //public EclipseData MainData
        //{
        //    get { return mainData; }
        //    set { mainData = value; }
        //}


        /// <summary> 清理 </summary>
        public virtual void Clear()
        {
            //  删除文件
            File.Delete(this.MainFilePath);
            File.Delete(this.SchPath);
            File.Delete(this.InitPath);
            this.schedule.Clear();
            this.solution.Clear();
            this.parent = null;
        }

    }

    /// <summary> 主文件模型 </summary>
    public class MainFileRestartSimON : RestartInfoModelSimON
    {

    }

    /// <summary> SimON模型案例重启 (比Eclipse多一个WELL需要保存)</summary>
    public class RestartInfoModelSimON : RestartInfoModel
    {
        [NonSerialized]
        WELL well = null;
        ///<summary> 井口坐标部分 </summary>
        public WELL Well
        {
            get { return well; }
            set { well = value; }
        }


        string wellPath;

        /// <summary> WELL文件路径 </summary>
        public string WellPath
        {
            get
            {
                if (well != null)
                {
                    INCLUDE include = well.Find<INCLUDE>();

                    if (include != null)
                    {
                        wellPath = include.FilePath;
                    }
                }
                return wellPath;// wellPath; Path.Combine(this.FilePath, this.FileName + "_WELL.DAT"); }
            }

        }

        /// <summary> 构建路径 </summary>
        internal void BuildPath()
        {
            this.ResultFilePath = this.FilePath + "\\" + Path.GetFileNameWithoutExtension(this.ParentName) + "_rst" + this.Index + ".bin";
            this.MainFilePath = this.FilePath + "\\" + this.FileName + KeyConfiger.SimONExtend;
            this.SchPath = this.FilePath + "\\" + this.FileName + "_SCH.DAT";
            this.InitPath = this.FilePath + "\\" + this.FileName + "_iNIT.DAT";
        }
    }


    /// <summary> 重启模型 </summary>
    [Serializable]
    public class RestartSerialize
    {
        DateTime restartTime;
        /// <summary> 重启时间 </summary>
        public DateTime RestartTime
        {
            get { return restartTime; }
            set { restartTime = value; }
        }

        string fileName;
        /// <summary> 重启名称 </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        string parentName;
        /// <summary> 父重启节点 </summary> 
        public string ParentName
        {
            get { return parentName; }
            set { parentName = value; }
        }

        string filePath;

        /// <summary> 文件全路径 </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        private int index;
        /// <summary> 重启索引值 </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        string schPath;

        public string SchPath
        {
            get { return schPath; }
            set { schPath = value; }
        }

        string initPath;

        public string InitPath
        {
            get { return initPath; }
            set { initPath = value; }
        }


    }



}
