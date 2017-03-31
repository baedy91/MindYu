using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; 

namespace JsonToWord.Model
{
    public class StrWrapper : INotifyPropertyChanged
    {
        private string _strData;
        public string StrData
        {
            get
            {
                return _strData;
            }
            set 
            { 
                _strData = value;
                OnPropertyChanged("StrData");
            }
        }
        public StrWrapper(string metaData)
        {
            StrData = metaData;
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
