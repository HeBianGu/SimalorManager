using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Parent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.Eclipse.FileInfos
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
                string p = this.filePath + "\\" + this.fileName + ".EGRID";

                return p;
            }
        }

        /// <summary> 结果文件路径 </summary>
        public string MainFilePath
        {
            get
            {
                string p = this.filePath + "\\" + this.fileName + ".data";

                return p;
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

    /// <summary> 重启扩展方法 </summary>
    public static class RestartManager
    {
        /// <summary> 创建重启模型 </summary>
        public static RestartInfoModel CreateRestartAt(this RestartInfoModel parent, string name, DateTime time, int index)
        {
            RestartInfoModel restart = new RestartInfoModel();
            restart.Parent = parent;
            restart.RestartTime = time;
            restart.Index = index;
            restart.FileName = name;
            restart.FilePath = parent.FilePath;
            restart.Solution = parent.InitRestartSolution(restart, name, time, index);
            restart.Schedule = parent.RefreshRestartSchdule().InitRestartSchdule(restart, name, time);
            return restart;
        }

        /// <summary> 创建初始重启模型 </summary>
        public static MainFileRestart InitRestartInfoModel(this EclipseData mainData)
        {
            START start = mainData.Key.Find<START>();
            if (start == null) return null;
            MainFileRestart restart = new MainFileRestart();
            restart.Parent = null;
            restart.RestartTime = start.StartTime;
            restart.FileName = mainData.FileName;
            restart.FilePath = Path.GetDirectoryName(mainData.FilePath);
            restart.Index = 0;
            //  把主文件的SCH和SOLU部分传进来
            restart.Solution = mainData.Key.Find<SOLUTION>();
            restart.Schedule = mainData.Key.Find<SCHEDULE>();
            return restart;
        }

        /// <summary> 通过模型创建初始化文件 </summary>
        public static SOLUTION InitRestartSolution(this RestartInfoModel parent, RestartInfoModel mode, string name, DateTime time, int index)
        {

            SOLUTION solution = new SOLUTION("SOLUTION");
            INCLUDE include = new INCLUDE("INCLUDE");
            include.FileName = name + "_init.inc";
            include.FilePath = parent.FilePath + "\\" + include.FileName;
            solution.Add(include);

            RESTART restart = new RESTART("RESTART");
            RESTART.Item item = new RESTART.Item();
            item.fwjm0 = parent.FileName;
            item.cqss1 = (index).ToString();
            restart.Items.Add(item);
            include.Add(restart);

            mode.InitPath = include.FilePath;

            return solution;
        }

        /// <summary> 通关生产数据创建生产数据 </summary>
        public static SCHEDULE InitRestartSchdule(this SCHEDULE sch, RestartInfoModel model, string name, DateTime time)
        {
            //  创建关键字
            SCHEDULE schedule = new SCHEDULE("SCHEDULE");
            INCLUDE include = new INCLUDE("INCLUDE");
            include.FileName = name + "_sch.inc";
            include.FilePath = Path.GetDirectoryName(model.ResultFilePath) + "//" + include.FileName; ;
            model.SchPath = include.FilePath;
            schedule.Add(include);

            //  处理井数据
            List<DATES> ds = sch.FindAll<DATES>();

            int findIndex = ds.FindIndex(l => (l.DateTime - time).TotalDays == 0);
            if (findIndex == -1)
            {
                throw new Exception("SimalorManager.InitRestartSchdule:没有对应日期的时间步:" + time.ToShortDateString());
            }

           List<VFPINJ> Vins= sch.FindAll<VFPINJ>();

           List<VFPPROD> Vpns = sch.FindAll<VFPPROD>();

            ds.RemoveRange(0, findIndex);

            if (Vins.Count>0)
            include.AddRange(Vins);

            if (Vpns.Count > 0)
            include.AddRange(Vpns);

            include.AddRange(ds);

            return schedule;
        }

        /// <summary> 从文件读取生产信息 </summary>
        public static SCHEDULE RefreshRestartSchdule(this RestartInfoModel restart)
        {
            //  创建关键字
            SCHEDULE schedule = new SCHEDULE("SCHEDULE");

            if (restart.SchPath != null && File.Exists(restart.SchPath))
            {
                INCLUDE include = INCLUDE.ThreadLoadFromFile(restart.SchPath);
                schedule.Add(include);
            }
            else
            {
                return restart.Schedule;
            }

            return schedule;
        }

        /// <summary> 用重启信息生成新重启数据（只包含主文件，初始化文件和生产文件） </summary>
        public static EclipseData ChangeRestartModel(this EclipseData mainData, RestartInfoModel model)
        {
            //  不读取INCLUDE部分数据
            EclipseData data = new EclipseData(mainData.FilePath, null, false);

            var incs = data.Key.FindAll<INCLUDE>();

            //  设置所有INCLUDE都不生成文件
            incs.ForEach(l => l.IsCreateFile = false);
            //  保存主文件
            SOLUTION sl = data.Key.Find<SOLUTION>();

            SCHEDULE sc = data.Key.Find<SCHEDULE>();

            //    替换数据
            sl.ExChangeData(model.Solution);
            sc.ExChangeData(model.Schedule);

            //    设置保存部分数据
            List<INCLUDE> slIncludes = sl.FindAll<INCLUDE>();
            slIncludes.ForEach(l => l.IsCreateFile = true);

            List<INCLUDE> scIncludes = sc.FindAll<INCLUDE>();
            scIncludes.ForEach(l => l.IsCreateFile = true);

            //  保存主文件（目前没用）
            model.MainData = data;

            return data;

        }

        /// <summary> 递归获取所有井数据 </summary>
        static void GetAllParentWellsEx(this RestartInfoModel pModel, ref List<IWelspecslDefine> ws)
        {
            if (pModel.Parent != null)
            {
                List<IWelspecslDefine> wels = pModel.Parent.RefreshRestartSchdule().FindAll<IWelspecslDefine>();

                if (wels != null)
                {
                    ws.AddRange(wels);
                }

                pModel.Parent.GetAllParentWellsEx(ref ws);
            }
        }

        /// <summary> 获取所有井数据 </summary>
        public static List<IWelspecslDefine> GetAllParentWells(this RestartInfoModel pModel)
        {
            List<IWelspecslDefine> ws = new List<IWelspecslDefine>();


            if (pModel.Parent != null)
            {
                pModel.GetAllParentWellsEx(ref ws);
            }

            return ws;
        }

        /// <summary> 获取所有井组数据 </summary>
        public static List<GRUPTREE> GetAllParentWellGroup(this RestartInfoModel pModel)
        {
            List<GRUPTREE> ws = new List<GRUPTREE>();

            if (pModel.Parent != null)
            {
                pModel.GetAllParentWellGroupEx(ref ws);
            }

            return ws;
        }

        /// <summary> 递归获取所有井组数据 </summary>
        static void GetAllParentWellGroupEx(this RestartInfoModel pModel, ref List<GRUPTREE> ws)
        {
            if (pModel.Parent != null)
            {
                List<GRUPTREE> wels = pModel.Parent.RefreshRestartSchdule().FindAll<GRUPTREE>();

                if (wels != null)
                {
                    ws.AddRange(wels);
                }

                pModel.Parent.GetAllParentWellGroupEx(ref ws);
            }
        }

        /// <summary> 转换成序列化模型 </summary>
        public static RestartSerialize TransSerialize(this RestartInfoModel m)
        {
            RestartSerialize s = new RestartSerialize();
            s.FileName = m.FileName;
            s.Index = m.Index;
            s.RestartTime = m.RestartTime;
            s.FilePath = m.FilePath;
            return s;
        }

        /// <summary> 序列化转换成模型 </summary>
        public static RestartInfoModel TransSerialize(this RestartSerialize m)
        {
            RestartInfoModel s = new RestartInfoModel();
            s.FileName = m.FileName;
            s.Index = m.Index;
            s.RestartTime = m.RestartTime;
            s.FilePath = m.FilePath;
            return s;
        }
    }

}
