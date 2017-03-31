using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WordToJson.Model
{
    public class IntWrapper : INotifyPropertyChanged
    {
        private int _inData;
        public int InData
        {
            get
            {
                return _inData;
            }
            set 
            {
                _inData = value;
                OnPropertyChanged("InData");
            }
        }
        public IntWrapper(int metaData)
        {
            InData = metaData;
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
