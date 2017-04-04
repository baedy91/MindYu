using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace JsonToWord.Model
{
    public class RootAllModel : INotifyPropertyChanged
    {
        #region MyProperty
        private ObservableCollection<WordListModel> _root;
        public ObservableCollection<WordListModel> Root
        {
            get { return _root; }
            set
            {
                if (_root != value)
                {
                    _root = value;
                    OnPropertyChanged("Root");
                }
            }
        }
        #endregion

        public RootAllModel(ObservableCollection<WordListModel> inputdata)
        {
            Root = inputdata;
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
