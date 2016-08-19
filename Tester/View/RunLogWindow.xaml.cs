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
using System.Windows.Shapes;
using Tester.ViewModel;

namespace Tester.View
{
    /// <summary>
    /// RunLogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RunLogWindow : Window
    {

        MainViewModel _mainViewModel;
        public RunLogWindow(MainViewModel vm)
        {
            InitializeComponent();

            _mainViewModel = vm;

            this.DataContext = _mainViewModel;
        }
    }
}
