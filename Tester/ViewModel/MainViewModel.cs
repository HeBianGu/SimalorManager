using DevExpress.Xpf.Grid;
using Microsoft.Practices.Prism.Commands;
using OPT.Product.SimalorManager;
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.SearchFileHandler = new DelegateCommand(this.SearchFile);
        }

        private string _filePath = "--请浏览数模文件路径--";
        /// <summary> 文件路径 </summary>
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        _filePath = "--请浏览数模文件路径--";
                    }
                    else
                    {
                        _filePath = value;

                    }

                    RaisePropertyChanged("FilePath");
                }
            }
        }

        private EclipseData _eclipseData;

        /// <summary> 主文件实体 </summary>
        public EclipseData EclipseData
        {
            get { return _eclipseData; }
            set
            {
                _eclipseData = value;
                RaisePropertyChanged("EclipseData");
            }
        }

        private List<BaseKey> _bkSource = new List<BaseKey>();

        /// <summary> 所有关键字 </summary>
        public List<BaseKey> BkSource
        {
            get { return _bkSource; }
            set
            {
                _bkSource = value;
                RaisePropertyChanged("BkSource");
            }
        }

        private string _total = "0";

        /// <summary> 总计 </summary>
        public string Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged("Total");
            }
        }

        private string _kownToal = "0";
        /// <summary> 解析总数 </summary>
        public string KownTotal
        {
            get { return _kownToal; }
            set
            {
                _kownToal = value;
                RaisePropertyChanged("KownTotal");
            }
        }

        private string _unkownTotal = "0";

        /// <summary> 未解析总数 </summary>
        public string UnKownTotal
        {
            get { return _unkownTotal; }
            set
            {
                _unkownTotal = value;
                RaisePropertyChanged("UnKownTotal");
            }
        }

        private string _unkownTotalType = "0";
        /// <summary> 未解析类型总数 </summary>
        public string UnKownTotalType
        {
            get { return _unkownTotalType; }
            set
            {
                _unkownTotalType = value;
                RaisePropertyChanged("UnKownTotalType");
            }
        }


        private string _kownTotalType = "0";
        /// <summary> 解析类型总数 </summary>
        public string KownTotalType
        {
            get { return _kownTotalType; }
            set
            {
                _kownTotalType = value;
                RaisePropertyChanged("KownTotalType");
            }
        }



        private BaseKey _iteamSel;
        /// <summary> 选中的关键字 </summary>
        public BaseKey IteamSel
        {
            get { return _iteamSel; }
            set
            {
                _iteamSel = value;

                if (_iteamSel is ItemsKeyInterface)
                {
                    ItemsKeyInterface itemKey = _iteamSel as ItemsKeyInterface;

                    this.DataItems = itemKey.GetItems();
                }
                RaisePropertyChanged("IteamSel");
            }
        }


        private IEnumerable<Item> _dataIteams = new List<Item>();

        /// <summary> 选择项的数据源 </summary>
        public IEnumerable<Item> DataItems
        {
            get { return _dataIteams; }
            set
            {
                _dataIteams = value;
                RaisePropertyChanged("DataItems");
            }
        }

        private  List<RunLogModel> _runLog=new List<RunLogModel>();

        public  List<RunLogModel> RunLog
        {
            get { return _runLog; }
            set { _runLog = value; }
        }
         


        void SearchFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.FilePath = dlg.FileName;
            }

            if (!File.Exists(this._filePath)) return;


            EclipseData = FileFactoryService.Instance.ThreadLoadResize(this._filePath);

           //END end= this.EclipseData.Key.Find<END>();

           // if(end!=null)
           // {
           //     end.ClearAllAfter();
           // }

            BkSource = _eclipseData.Key.FindAll<BaseKey>();

            IteamSel = BkSource.FirstOrDefault();

            Total = BkSource.Count.ToString();

            List<BaseKey> unKonwKeys = BkSource.FindAll(l => !l.IsUnKnowKey);

            UnKownTotalType = unKonwKeys.GroupBy(l => l.Name.Split(' ')[0]).Count().ToString();

            List<BaseKey> KonwKeys = BkSource.FindAll(l => l.IsUnKnowKey);

            KownTotalType =KonwKeys.GroupBy(l => l.Name.Split(' ')[0]).Count().ToString();

            UnKownTotal = unKonwKeys.Count.ToString();

            KownTotal = (int.Parse(Total) - int.Parse(UnKownTotal)).ToString();

            RunLog = EclipseData.RunLog;

        }

        public DelegateCommand SearchFileHandler { get; set; }

        public DelegateCommand SelectItemChanged { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
