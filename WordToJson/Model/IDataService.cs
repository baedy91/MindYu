/****
 * filename   IDataService
 * date        17.3.23 
 * update     17.3.24
 * author     baedy91@painone.com
 * brief        DataService interface
 ****/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordToJson.Model
{
    public struct removeResult
    {
        private String _removeWord;
        private bool _result;
        public String RemoveWord
        {
            get { return _removeWord; }
            set { _removeWord = value; }
        }
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }

    public interface IDataService
    {
        WordList LoadRelationWord(String word);
        void SaveRelationWord(WordList list);
        StringList LoadContentsList();
        removeResult RemoveContentsList(int index);
        bool UpdateContentsList(int index, String newData, out string oldData);
        void SaveContentsList(String newWord);
        void NewContentsList();
    }
}
