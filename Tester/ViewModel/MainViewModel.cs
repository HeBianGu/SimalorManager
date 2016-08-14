using DevExpress.Xpf.Grid;
using Microsoft.Practices.Prism.Commands;
using OPT.Product.SimalorManager;
using OPT.Product.SimalorManager.Eclipse.FileInfos;
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


        private BaseKey _iteamSel;
        /// <summary> 选中的关键字 </summary>
        public BaseKey IteamSel
        {
            get { return _iteamSel; }
            set
            {
                _iteamSel = value;

                if(_iteamSel is ItemsKeyInterface)
                {
                    ItemsKeyInterface itemKey = _iteamSel as ItemsKeyInterface;

                    this.DataItems = itemKey.GetItems().ToList();
                }
                RaisePropertyChanged("SelectIteam");
            }
        }


        private List<Item> _dataIteams = new List<Item>();

        /// <summary> 选择项的数据源 </summary>
        public List<Item> DataItems
        {
            get { return _dataIteams; }
            set
            {
                _dataIteams = value;
                RaisePropertyChanged("DataItems");
            }
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

            BkSource = _eclipseData.Key.FindAll<BaseKey>();

            IteamSel = BkSource.FirstOrDefault();

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
