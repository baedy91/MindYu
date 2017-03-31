using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace JsonToWord.Model
{
    public class IntWrapper : INotifyPropertyChanged
    {
        private int _intData;
        public int IntData
        {
            get
            {
                return _intData;
            }
            set
            {
                _intData = value;
                OnPropertyChanged("IntData");
            }
        }
        public IntWrapper(int metaData)
        {
            IntData = metaData;
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
