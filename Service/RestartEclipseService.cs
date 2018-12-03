using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> Eclipse重启服务 </summary>
    public class RestartEclipseService : ServiceFactory<RestartEclipseService>
    {
        /// <summary> 创建重启模型 </summary>
        public RestartInfoModel CreateRestartAt(RestartInfoModel parent, string name, DateTime time, int index)
        {
            RestartInfoModel restart = new RestartInfoModel();
            restart.Parent = parent;
            restart.RestartTime = time;
            restart.Index = index;
            restart.FileName = name;
            restart.FilePath = parent.FilePath;
            restart.ResultFilePath = restart.FilePath + "\\" + Path.GetFileNameWithoutExtension(restart.FileName) + ".EGRID";
            restart.MainFilePath = restart.FilePath + "\\" + restart.FileName + ".data";
            restart.Solution = this.InitRestartSolution(parent, restart, name, time, index);
            restart.Schedule = this.InitRestartSchdule(this.RefreshRestartSchdule(parent), restart, name, time);
            return restart;
        }

        /// <summary> 创建初始重启模型 </summary>
        public MainFileRestart InitRestartInfoModel(EclipseData mainData)
        {
            START start = mainData.Key.Find<START>();
            if (start == null) return null;
            MainFileRestart restart = new MainFileRestart();
            restart.Parent = null;
            restart.RestartTime = start.StartTime;
            restart.FileName = Path.GetFileNameWithoutExtension(mainData.FileName);
            restart.FilePath = Path.GetDirectoryName(mainData.FilePath);
            restart.ResultFilePath = restart.FilePath + "\\" + Path.GetFileNameWithoutExtension(restart.FileName) + ".EGRID";
            restart.MainFilePath = restart.FilePath + "\\" + restart.FileName + ".data";
            restart.Index = 0;
            //  把主文件的SCH和SOLU部分传进来
            restart.Solution = mainData.Key.Find<SOLUTION>();
            restart.Schedule = mainData.Key.Find<SCHEDULE>();
            return restart;
        }

        /// <summary> 通过模型创建初始化文件 </summary>
        public SOLUTION InitRestartSolution(RestartInfoModel parent, RestartInfoModel mode, string name, DateTime time, int index)
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
        public SCHEDULE InitRestartSchdule(SCHEDULE sch, RestartInfoModel model, string name, DateTime time)
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

            List<VFPINJ> Vins = sch.FindAll<VFPINJ>();

            List<VFPPROD> Vpns = sch.FindAll<VFPPROD>();

            ds.RemoveRange(0, findIndex);

            if (Vins.Count > 0)
                include.AddRange(Vins);

            if (Vpns.Count > 0)
                include.AddRange(Vpns);

            include.AddRange(ds);

            return schedule;
        }

        /// <summary> 从文件读取生产信息 </summary>
        public SCHEDULE RefreshRestartSchdule(RestartInfoModel restart)
        {
            //  创建关键字
            SCHEDULE schedule = new SCHEDULE("SCHEDULE");

            if (restart.SchPath != null && File.Exists(restart.SchPath))
            {
                INCLUDE include = FileFactoryService.Instance.ThreadLoadFromFile(restart.SchPath);
                schedule.Add(include);
            }
            else
            {
                return restart.Schedule;
            }

            return schedule;
        }

        /// <summary> 用重启信息生成新重启数据（只包含主文件，初始化文件和生产文件） </summary>
        public EclipseData ChangeRestartModel(EclipseData mainData, RestartInfoModel model)
        {
            //  不读取INCLUDE部分数据
            EclipseData data = new EclipseData(mainData.FilePath, null, l=>false);

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
            //model.MainData = data;

            return data;

        }

        /// <summary> 递归获取所有井数据 </summary>
        void GetAllParentWellsEx(RestartInfoModel pModel, ref List<IWelspecslDefine> ws)
        {
            if (pModel.Parent != null)
            {
                List<IWelspecslDefine> wels = this.RefreshRestartSchdule(pModel.Parent).FindAll<IWelspecslDefine>();

                if (wels != null)
                {
                    ws.AddRange(wels);
                }

                this.GetAllParentWellsEx(pModel.Parent, ref ws);
            }
        }

        /// <summary> 获取所有井数据 </summary>
        public List<IWelspecslDefine> GetAllParentWells(RestartInfoModel pModel)
        {
            List<IWelspecslDefine> ws = new List<IWelspecslDefine>();


            if (pModel.Parent != null)
            {
                this.GetAllParentWellsEx(pModel, ref ws);
            }

            return ws;
        }

        /// <summary> 获取所有井组数据 </summary>
        public List<GRUPTREE> GetAllParentWellGroup(RestartInfoModel pModel)
        {
            List<GRUPTREE> ws = new List<GRUPTREE>();

            if (pModel.Parent != null)
            {
                this.GetAllParentWellGroupEx(pModel, ref ws);
            }

            return ws;
        }

        /// <summary> 递归获取所有井组数据 </summary>
        void GetAllParentWellGroupEx(RestartInfoModel pModel, ref List<GRUPTREE> ws)
        {
            if (pModel.Parent != null)
            {
                List<GRUPTREE> wels = this.RefreshRestartSchdule(pModel.Parent).FindAll<GRUPTREE>();

                if (wels != null)
                {
                    ws.AddRange(wels);
                }

                this.GetAllParentWellGroupEx(pModel.Parent, ref ws);
            }
        }

        /// <summary> 转换成序列化模型 </summary>
        public RestartSerialize TransSerialize(RestartInfoModel m)
        {
            RestartSerialize s = new RestartSerialize();
            s.FileName = m.FileName;
            s.Index = m.Index;
            s.RestartTime = m.RestartTime;
            s.FilePath = m.FilePath;
            return s;
        }

        /// <summary> 序列化转换成模型 </summary>
        public RestartInfoModel TransSerialize(RestartSerialize m)
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
