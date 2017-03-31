using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; 
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace WordToJson.Model
{
    public class StringList : INotifyPropertyChanged
    {
        private ObservableCollection<StrWrapper> _stl;
        public ObservableCollection<StrWrapper> STL
        {
            get { return _stl; }
            set
            {
                _stl = value;
                OnPropertyChanged("STL");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
