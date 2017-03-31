using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace JsonToWord.Model
{
    public class RelationWordModel : INotifyPropertyChanged
    {
        private String _relationWord = "";
        private int _count = 1;

        public String RelationWord 
        {
            get { return _relationWord; }
            set
            {
                _relationWord = value;
                OnPropertyChanged("RelationWord");
            }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                int val = value;
                if (val < 0)
                {
                    val = 0;
                }
                _count = val;
                OnPropertyChanged("Count");
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
