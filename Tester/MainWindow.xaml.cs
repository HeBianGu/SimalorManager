using OPT.Product.SimalorManager;
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tester
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EclipseData ecl = null;
            //try
            //{
                ecl = new EclipseData(@"D:\BaiduYunDownload\CHX\CHX.DATA");

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return;
            //}

            var bs= ecl.Key.FindAll<BaseKey>();

            this.tv_all.ItemsSource = bs;

            this.tv_all.View.KeyFieldName = "ID";

            this.tv_all.View.ParentFieldName = "Pid";
           
        }
    }
}
