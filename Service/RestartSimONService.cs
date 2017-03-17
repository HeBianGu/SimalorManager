using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using OPT.Product.SimalorManager.RegisterKeys.SimON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.Service
{
    /// <summary> SimON重启服务 </summary>
    public class RestartSimONService : ServiceFactory<RestartSimONService>
    {

        /// <summary> 获取所有重启时间步 </summary>
        public List<TIME> GetAllRestartTime(SCHEDULE sch)
        {
            return sch.FindAll<TIME>(l => l.Find<OPT.Product.SimalorManager.RegisterKeys.SimON.RESTART>() != null);
        }

        /// <summary> 创建初始重启模型 </summary>
        public MainFileRestartSimON InitRestartInfoModel(SimONData mainData)
        {
            TUNING start = mainData.Key.Find<TUNING>();
            if (start == null) return null;
            MainFileRestartSimON restart = new MainFileRestartSimON();
            restart.Parent = null;
            restart.RestartTime = start.Date;
            restart.FileName = Path.GetFileNameWithoutExtension(mainData.FileName);
            restart.FilePath = Path.GetDirectoryName(mainData.FilePath);
            restart.Index = 0;
            restart.ResultFilePath = restart.FilePath + "\\" + Path.GetFileNameWithoutExtension(restart.FileName) + "_rst1.bin";
            restart.MainFilePath = restart.FilePath + "\\" + restart.FileName + ".DAT";

            //  把主文件的SCH和SOLU部分传进来
            restart.Solution = mainData.Key.Find<SOLUTION>();
            restart.Schedule = mainData.Key.Find<SCHEDULE>();
            restart.Well = mainData.Key.Find<WELL>();

            restart.SchPath = restart.FilePath + "\\" + restart.FileName + "_SCH.DAT";
            restart.InitPath = restart.FilePath + "\\" + restart.FileName + "_iNIT.DAT";
            return restart;
        }

        /// <summary> 创建重启模型 </summary>
        public RestartInfoModelSimON CreateRestartAt(RestartInfoModelSimON parent, string name, DateTime time, int index)
        {
            RestartInfoModelSimON restart = new RestartInfoModelSimON();
            restart.Parent = parent;
            restart.RestartTime = time;
            restart.Index = index;
            restart.FileName = name;
            restart.FilePath = parent.FilePath;

            restart.BuildPath();

            //restart.ResultFilePath = restart.FilePath + "\\" + Path.GetFileNameWithoutExtension(parent.FileName) + "_rst" + index + ".bin";
            //restart.MainFilePath = restart.FilePath + "\\" + restart.FileName + ".dat";

            restart.Solution = this.InitRestartSolution(parent, restart, time, index);
            restart.Schedule = this.InitRestartSchdule(this.RefreshRestartSchdule(parent), restart, name, time);
            restart.Well = this.InitRestartWell(parent, restart, name, time);
            return restart;
        }

        ///// <summary> 创建重启模型(目前只应用在FieldGoal案例重启) </summary>
        //public RestartInfoModelSimON CreateRestartAtByRestartCase(RestartInfoModelSimON parent, string name, DateTime time, int index)
        //{
        //    RestartInfoModelSimON restart = new RestartInfoModelSimON();
        //    restart.Parent = parent;
        //    restart.RestartTime = time;
        //    restart.Index = index;
        //    restart.FileName = name;
        //    restart.FilePath = parent.FilePath;

        //    restart.BuildPath();

        //    //restart.ResultFilePath = restart.FilePath + "\\" + Path.GetFileNameWithoutExtension(parent.FileName) + "_rst" + index + ".bin";
        //    //restart.MainFilePath = restart.FilePath + "\\" + restart.FileName + ".dat";

        //    restart.Solution = this.InitRestartSolution(parent, restart, time, index);
        //    //restart.Schedule = this.InitRestartSchdule(this.RefreshRestartSchdule(parent), restart, name, time, endTime, wellProducts);
        //    restart.Well = this.InitRestartWell(parent, restart, name, time);
        //    return restart;
        //}

        /// <summary> 通过模型创建初始化文件 </summary>
        public SOLUTION InitRestartSolution(RestartInfoModelSimON parent, RestartInfoModelSimON mode, DateTime time, int index)
        {
            return this.InitRestartSolution(parent.FileName, mode, time, index);
        }

        /// <summary> 通过模型创建初始化文件 </summary>
        public SOLUTION InitRestartSolution(string parentFileName, RestartInfoModelSimON mode, DateTime time, int index)
        {

            SOLUTION solution = new SOLUTION("SOLUTION");
            INCLUDE include = new INCLUDE("INCLUDE");
            include.FileName = mode.FileName + "_INIT.DAT";
            include.FilePath = mode.FilePath + "\\" + include.FileName;
            solution.Add(include);

            TSTART tstart = new TSTART("TSTART");
            tstart.Date = time;
            include.Add(tstart);

            Key key = new Key("RSTFILE");

            string parentName = "'" + parentFileName + "_rst" + index + ".bin'";
            key.Lines.Add(parentName);
            include.Add(key);

            //INCLUDE ince = new INCLUDE("INCLUDE");
            //ince.FileName = parent.FileName + "_wrst" + index + ".dat";
            //ince.FilePath = parent.FilePath + "\\" + include.FileName;

            string format = @"INCLUDE
'{0}_wrst{1}.dat'";

            key.Lines.Add(string.Format(format, parentFileName, index));

            //// Todo ：只做标识不清空源文件 
            //ince.IsCreateFile = false;
            //include.Add(ince);



            mode.InitPath = include.FilePath;

            return solution;
        }

        /// <summary> 通关生产数据创建生产数据 </summary>
        public SCHEDULE InitRestartSchdule(SCHEDULE sch, RestartInfoModelSimON model, string name, DateTime time)
        {
            //  创建关键字
            SCHEDULE schedule = new SCHEDULE("SCHEDULE");
            INCLUDE include = new INCLUDE("INCLUDE");
            include.FileName = name + "_SCH.DAT";
            include.FilePath = Path.GetDirectoryName(model.ResultFilePath) + "//" + include.FileName;
            model.SchPath = include.FilePath;
            schedule.Add(include);

            //int findIndex = ds.FindIndex(l => (l.Date.Date - time.Date).TotalDays == 0);

            //if (findIndex == -1)
            //{
            //    throw new Exception("SimalorManager.InitRestartSchdule:没有对应日期的时间步:" + time.ToShortDateString());
            //}

            sch.DeleteAll<TIME>(l => l.Date.Date < time.Date);


            List<VFPINJ> Vins = sch.FindAll<VFPINJ>();

            List<VFPPROD> Vpns = sch.FindAll<VFPPROD>();

            //ds.RemoveRange(0, findIndex);

            if (Vins.Count > 0)
                include.AddRange(Vins);

            if (Vpns.Count > 0)
                include.AddRange(Vpns);

            //  处理井数据
            List<TIME> ds = sch.FindAll<TIME>();

            include.AddRange(ds);

            return schedule;
        }

        /// <summary> 通关生产数据创建生产数据(目前只应用在FieldGoal案例重启)  </summary>
        public SCHEDULE InitRestartSchduleRestartCase(SCHEDULE sch, RestartInfoModelSimON model, string name, DateTime time, DateTime endtime, Dictionary<string, double> wellProducts, int datype)
        {
            //  创建关键字
            SCHEDULE schedule = new SCHEDULE("SCHEDULE");
            INCLUDE include = new INCLUDE("INCLUDE");
            include.FileName = name + "_SCH.DAT";
            include.FilePath = Path.GetDirectoryName(model.ResultFilePath) + "//" + include.FileName;
            model.SchPath = include.FilePath;
            schedule.Add(include);

            include.Add(new USESTARTTIME("USESTARTTIME"));
            include.Add(new WELLSCHED("WELLSCHED"));
            TIME start = new TIME("TIME", time);

            foreach (var item in wellProducts)
            {
                WELL well = new WELL("WELL");
                well.WellName0 = item.Key;
                well.ProType = datype == 0 ? SimONProductType.GRAT : SimONProductType.ORAT;
                well.Jcyblxz2 = item.Value.ToString();
                start.Add(well);
            }

            TIME startAdd = new TIME("TIME", time.AddDays(1));

            TIME end = new TIME("TIME", endtime);

            end.Add(new RegisterKeys.SimON.RESTART("RESTART"));

            List<VFPINJ> Vins = sch.FindAll<VFPINJ>();

            List<VFPPROD> Vpns = sch.FindAll<VFPPROD>();

            if (Vins.Count > 0)
                include.AddRange(Vins);

            if (Vpns.Count > 0)
                include.AddRange(Vpns);

            include.Add(start);

            if (startAdd.Date < end.Date)
            {
                include.Add(startAdd);
            }

            if (end.Date.Date == start.Date.Date)
            {
                include.Add(startAdd);
            }
            else
            {
                include.Add(end);
            }



            // HTodo  ：保存生产文件 
            include.Save();

            return schedule;


            // HTodo  ：示例如下 
            //USESTARTTIME
            //WELLSCHED
            //TIME    20140209D
            //       WELL   'PROD1'   4   9000   1500
            //       WELL   'INIJ1'   5   6000   NA

            //TIME    20140210D

            //TIME    20140309D
            //RESTART
        }

        /// <summary> 通关生产数据创建生产数据 </summary>
        public WELL InitRestartWell(RestartInfoModelSimON lastRestart, RestartInfoModelSimON model, string name, DateTime time)
        {
            return InitRestartWell(lastRestart.WellPath, model);
        }

        /// <summary> 由WELL文件生成内存数据 </summary>
        public WELL InitRestartWell(string wellPath, RestartInfoModelSimON model)
        {
            //  创建关键字
            WELL well = new WELL("WELL");
            USE_TF use_tf = new USE_TF("USE_TF");
            well.Add(use_tf);
            INCLUDE include = new INCLUDE("INCLUDE");
            include.FileName = model.FileName + "_WELL.DAT";
            include.FilePath = Path.GetDirectoryName(model.ResultFilePath) + "//" + include.FileName; ;
            //model.WellPath = include.FilePath;
            well.Add(include);

            INCLUDE lastInclude = this.RefreshRestartWellLocation(wellPath);

            if (lastInclude == null) return well;
            // Todo ：将母案例中的Include信息复制到新案例中 
            include.ExChangeData(lastInclude);


            return well;
        }

        /// <summary> 从文件读取生产信息 </summary>
        public SCHEDULE RefreshRestartSchdule(RestartInfoModelSimON restart)
        {
            //  创建关键字
            SCHEDULE schedule = new SCHEDULE("SCHEDULE");

            if (restart.SchPath != null && File.Exists(restart.SchPath))
            {
                INCLUDE include = FileFactoryService.Instance.ThreadLoadFromFile(restart.SchPath, SimKeyType.SimON);
                schedule.Add(include);
            }
            else
            {
                return restart.Schedule;
            }

            return schedule;
        }

        ///// <summary> 从文件读取生产信息 </summary>
        //public INCLUDE RefreshRestartWellLocation(RestartInfoModelSimON restart)
        //{
        //    //  创建关键字
        //    if (restart.WellPath != null && File.Exists(restart.WellPath))
        //    {
        //        INCLUDE include = FileFactoryService.Instance.ThreadLoadFromFile(restart.WellPath, SimKeyType.SimON);
        //        return include;
        //    }
        //    else
        //    {
        //        return restart.Well.Find<INCLUDE>();
        //    }
        //}

        /// <summary> 从文件读取生产信息 </summary>
        public INCLUDE RefreshRestartWellLocation(string wellPath)
        {
            string oldName = Path.GetFileNameWithoutExtension(wellPath) + (KeyConfiger.oldWellLocationName) + KeyConfiger.SimONExtend;

            string oldFullPath = Path.Combine(Path.GetDirectoryName(wellPath), oldName);


            // HTodo  ：兼容老版本 3106_WellLocation.dat 完井格式 
            if (File.Exists(oldFullPath) && !File.Exists(wellPath))
            {
                wellPath = oldFullPath;
            }

            //  创建关键字
            if (wellPath != null && (File.Exists(wellPath)))
            {
                INCLUDE include = FileFactoryService.Instance.ThreadLoadFromFile(wellPath, SimKeyType.SimON);
                return include;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 用重启信息生成新重启数据（只包含主文件，初始化文件和生产文件） </summary>
        public SimONData ChangeRestartModel(SimONData mainData, RestartInfoModelSimON model)
        {
            //  不读取INCLUDE部分数据
            SimONData data = FileFactoryService.Instance.ThreadLoadFunc<SimONData>(() => new SimONData(mainData.FilePath, null, l => false));

            var incs = data.Key.FindAll<INCLUDE>();

            //  设置所有INCLUDE都不生成文件
            incs.ForEach(l => l.IsCreateFile = false);
            //  保存主文件
            SOLUTION sl = data.Key.Find<SOLUTION>();

            SCHEDULE sc = data.Key.Find<SCHEDULE>();

            WELL well = data.Key.Find<WELL>();

            //  更改起始时间
            TUNING tuning = data.Key.Find<TUNING>();

            // Todo ：主文件没有在solotion中找 
            if (tuning == null)
            {
                tuning = sl.Find<TUNING>();
            }

            tuning.Date = model.RestartTime;

            model.Solution.Add(tuning);

            //    替换数据
            sl.ExChangeData(model.Solution);
            sc.ExChangeData(model.Schedule);
            well.ExChangeData(model.Well);

            //  

            //    设置保存部分数据
            List<INCLUDE> slIncludes = sl.FindAll<INCLUDE>();
            slIncludes.ForEach(l => l.IsCreateFile = true);

            List<INCLUDE> scIncludes = sc.FindAll<INCLUDE>();
            scIncludes.ForEach(l => l.IsCreateFile = true);

            List<INCLUDE> wellIncludes = well.FindAll<INCLUDE>();
            wellIncludes.ForEach(l => l.IsCreateFile = true);

            //  保存主文件（目前没用）
            //model.MainData = data;


            // Todo ：插入关键字到最后 
            RPTSCHED rptsched = new RPTSCHED("RPTSCHED");

            data.Key.Add(rptsched);


            // Todo ：插入标识到第二个关键字 
            OPT.Product.SimalorManager.RegisterKeys.SimON.RESTART restart = new OPT.Product.SimalorManager.RegisterKeys.SimON.RESTART("RESTART");
            data.Key.InsertKey(1, restart);

            return data;

        }


        /// <summary> 转换成序列化模型 </summary>
        public RestartSerialize TransSerialize(RestartInfoModelSimON m)
        {
            RestartSerialize s = new RestartSerialize();
            s.FileName = m.FileName;
            s.Index = m.Index;
            s.RestartTime = m.RestartTime;
            s.FilePath = m.FilePath;
            s.ParentName = m.ParentName;
            return s;
        }

        /// <summary> 序列化转换成模型 </summary>
        public RestartInfoModelSimON TransSerialize(RestartSerialize m)
        {
            RestartInfoModelSimON restart = string.IsNullOrEmpty(m.ParentName) ? restart = new MainFileRestartSimON() : new RestartInfoModelSimON();
            restart.FileName = m.FileName;
            restart.Index = m.Index;
            restart.RestartTime = m.RestartTime;
            restart.FilePath = m.FilePath;
            restart.ParentName = m.ParentName;

            restart.BuildPath();

            // Todo ：读取文件生成内存数据 
            restart.Solution = this.InitRestartSolution(m.ParentName, restart, m.RestartTime, m.Index);
            restart.Schedule = this.RefreshRestartSchdule(restart);
            restart.Well = this.InitRestartWell(restart.WellPath, restart);

            return restart;
        }


        /// <summary> 返回生产信息中 指定时间之前(包含当前时间)总共重启个数 </summary>
        public int RestartCount(SCHEDULE sch, DateTime time)
        {
            List<TIME> ts = sch.FindAll<TIME>(l => l.Find<OPT.Product.SimalorManager.RegisterKeys.SimON.RESTART>() != null && l.Date.Date <= time.Date);

            return ts.Count;
        }



    }
}
