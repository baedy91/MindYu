using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonToWord.Model
{
    public interface IDataService
    {
        void NewContentsList(RootAllModel root);
        RootAllModel LoadDataService();
        void SaveDataService(RootAllModel saveData);
        ContentsListModel GetContents(RootAllModel rootData);
        WordListModel GetRelaionWord(RootAllModel rootData, string searchWord);
        bool IsContainWord(RootAllModel rootData, string checkWord);
    }
}
