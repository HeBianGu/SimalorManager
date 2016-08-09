using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using OPT.Product.SimalorManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OPT.Product.SimalorManager.Eclipse.FileInfos;

namespace OPT.Product.SimalorManager
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

        /// <summary> 结果文件路径 </summary>
        public string ResultFilePath
        {
            get
            {
                string p = this.filePath + "\\" + Path.GetFileNameWithoutExtension(this.fileName) + ".EGRID";

                return p;
            }
        }

        /// <summary> 结果文件路径 </summary>
        public string MainFilePath
        {
            get
            {
                string _maifilePath = this.filePath + "\\" + this.fileName + ".data";

                return _maifilePath;
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
        [NonSerialized]
        EclipseData mainData;
        ///<summary> 主文件 </summary>

        public EclipseData MainData
        {
            get { return mainData; }
            set { mainData = value; }
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
