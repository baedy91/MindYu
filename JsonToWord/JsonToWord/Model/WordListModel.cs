using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace JsonToWord.Model
{
    public class WordListModel : INotifyPropertyChanged
    {
        public WordListModel(StrWrapper word, ObservableCollection<RelationWordModel> list)
        {
            Word = word;
            _list = list;
        }

        #region property
        private StrWrapper _word;
        private ObservableCollection<RelationWordModel> _list;

        public StrWrapper Word
        {
            get { return _word; }
            set
            {
                _word = value;
                OnPropertyChanged("Word");
            }
        }
        public ObservableCollection<RelationWordModel> List
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyChanged("List");
            }
        }
        #endregion
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
