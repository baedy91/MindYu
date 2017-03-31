using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace JsonToWord.Model
{
    public class ContentsListModel : INotifyPropertyChanged
    {
        private ObservableCollection<StrWrapper> _contentsList;
        public ObservableCollection<StrWrapper> ContentsList
        {
            get { return _contentsList; }
            set
            {
                _contentsList = value;
                OnPropertyChanged("ContentsList");
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
