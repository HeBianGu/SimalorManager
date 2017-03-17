using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        Window1ViewModel _vm = null;
        public Window1(Window1ViewModel vm)
        {
            InitializeComponent();

            _vm = vm;

            this.DataContext = vm;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_vm.BkSource);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _vm.Model.MyProperty = this.textBox1.Text;
        }
    }
}
