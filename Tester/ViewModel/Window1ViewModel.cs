using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tester.ViewModel
{
    public class Window1ViewModel : INotifyPropertyChanged
    {
        public Window1ViewModel(TestModel t)
        {
            _model = t;
        }

        private TestModel _model;
        /// <summary> 说明 </summary>
        public TestModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        /// <summary> 所有关键字 </summary>
        public string BkSource
        {
            get { return _model.MyProperty; }
            set
            {
                _model.MyProperty = value;
                RaisePropertyChanged("BkSource");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    public class TestModel
    {
        private string  myVar;
        /// <summary> 说明 </summary>
        public string  MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

    }
}
