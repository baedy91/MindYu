using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace WordToJson.Model
{
    public class WordList : INotifyPropertyChanged
    {
        public WordList(String word, ObservableCollection<RelationWord> list)
        {
            Word = word;
            _list = list;
        }

        #region property
        private String _word;
        private ObservableCollection<RelationWord> _list;

        public string Word
        {
            get { return _word; }
            set
            {
                _word = value;
                OnPropertyChanged("Word");
            }
        }
        public ObservableCollection<RelationWord> List
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
