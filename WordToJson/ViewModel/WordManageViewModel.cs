using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WordToJson.Model;
using System.Windows.Input;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;   //ObservableCollection
using System.Windows.Threading;
using System.Threading;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using System.ComponentModel;

namespace WordToJson.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WordManageViewModel : ViewModelBase
    {
        #region Value
        private IDataService dataService = new DataService();
        private bool NowStatusNCL { get; set; }
        #endregion

        #region properties
        private SelectedItemModel _contentsListSelected;
        public SelectedItemModel ContentsListSelected
        {
            get
            {
                return _contentsListSelected;
            }
            set
            {
                _contentsListSelected = value;
            }
        }

        private StringList _contentsWordList;
        public StringList ContentsWordList
        {
            get
            {
                if (_contentsWordList == null)
                {
                    _contentsWordList = dataService.LoadContentsList();
                }
                return _contentsWordList;
            }
            set
            {
                _contentsWordList = value;
                RaisePropertyChanged("ContentsWordList");
            }
        }

        private IntWrapper _selectedIndex = new IntWrapper(-1);
        public IntWrapper SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        private WordList _visiableList;
        public WordList VisiableList
        {
            get
            {
                if (_visiableList == null)
                {
                    try 
                    { 
                        _visiableList = dataService.LoadRelationWord(ContentsListSelected.SelectedItem.InData); 
                    }
                    catch
                    {
                        _visiableList = null; 
                    }                    
                }
                return _visiableList;
            }
            set
            {
                if (value != null)
                {
                    _visiableList = DoSort(value);                    
                }
                else
                {
                    _visiableList = value;
                }
                RaisePropertyChanged("VisiableList");               
            }
        }
        // 자동 역순 정렬 매소드
        private WordList DoSort(WordList InputData)
        {
            
                ObservableCollection<RelationWord> OCRW = new ObservableCollection<RelationWord>();
                WordList wl = new WordList(InputData.Word, OCRW);

                List<RelationWord> sortList = new List<RelationWord>();               
                foreach (RelationWord rw in InputData.List)
                {
                    sortList.Add(rw);
                }
                sortList.Sort(Comparable);

                foreach (RelationWord rw in sortList)
                {
                    wl.List.Add(rw);
                }
         
            return wl;
        }
        private int Comparable(RelationWord x, RelationWord y)
        {
            return y.Count.CompareTo(x.Count);
        }   

        #endregion

        #region Command
        public ICommand SelectedCommand { get; private set; } 
        public ICommand SaveRelationCommand { get; private set; }
        public ICommand SaveDeleteCommand { get; private set; }
        public ICommand UpdateListCommand { get; private set; }
        public ICommand NewContentsListCommand { get; private set; }
        public ICommand RemoveContentsListCommand { get; private set; }
       
        private void SelectedChangeEvent(SelectedItemModel sim)
        {
            try
            {
                VisiableList = dataService.LoadRelationWord(sim.SelectedItem.InData);
            }
            catch
            {
                return;
            }            
        }

        private void SaveRelationEvent(WordList saveList)
        {   
            dataService.SaveRelationWord(saveList);
            VisiableList = dataService.LoadRelationWord(saveList.Word);           
        }
        //같은 항목 존재 여부 체크
        private bool CanSaveRelationEvent(WordList ckitem)
        {
            List<string> itemWord = new List<string>();
            try
            {
                foreach (RelationWord allword in ckitem.List)
                {
                    if (allword.Word == "" || allword.Word == null)
                    {
                        MessageBox.Show("비어있는 항목이 있는지 확인해주세요. \n\r저장되지 않았습니다.", "저장실패");
                        VisiableList = dataService.LoadRelationWord(ContentsListSelected.SelectedItem.InData);
                        return false;   //비어있는 항목
                    }
                    itemWord.Add(allword.Word);
                }
            }
            catch
            {
                return false;
            }

            itemWord.Sort();
            if (itemWord.Count <= 1)
            {
                return true;
            }         
            
            for (int i = 0; i <= itemWord.Count - 2; i++)
            {
                if (itemWord[i] == itemWord[i + 1])
                {
                    MessageBox.Show("같은 항목이 있는지 확인해주세요. \n\r저장되지 않았습니다.", "저장실패");
                    VisiableList = dataService.LoadRelationWord(ContentsListSelected.SelectedItem.InData);
                    return false;   //같은항목
                }
            }
            return true;
        }

        private void UpdateListEvent(StringList sl)
        {
            int index = SelectedIndex.InData;
            string updateData = sl.STL[index].InData;
            string oldData;
            if (!dataService.UpdateContentsList(index, updateData, out oldData))
            {
                if (updateData != oldData)
                {
                    MessageBox.Show("같은 항목이 있는지 확인해주세요. \n\r저장되지 않았습니다.");
                    ContentsWordList = dataService.LoadContentsList();
                }                
                return;
            }
            if (oldData == "새항목")
            {
                NowStatusNCL = true;
            }
            ContentsWordList = dataService.LoadContentsList();
        }

        private void NewContentsListEvent()
        {
            dataService.NewContentsList();   
            dataService.SaveContentsList("새항목");            
            try
            {
                StringList tempSL = dataService.LoadContentsList();
            }
            catch { }
            NowStatusNCL = false;
            ContentsWordList = dataService.LoadContentsList();
            ContentsListSelected.SelectedItem = ContentsWordList.STL[0];
        }
        private bool CanNewContentsListEvent()
        {
            if (NowStatusNCL)
            {
                return true;
            }
            else
            {
                MessageBox.Show("이미 새항목이 존재합니다.\n단어이름을 수정해 주세요", "추가불가");
                return false;
            }
        }

        private void RemoveContentsListEvent()
        {
            int index = SelectedIndex.InData;
            removeResult re = dataService.RemoveContentsList(index);
            if (re.Result)
            {
                MessageBox.Show("삭제완료", "삭제");
                if (re.RemoveWord == "새항목")
                {
                    NowStatusNCL = true;
                }
            }
            else
            {
                MessageBox.Show("삭제실패", "삭제실패");
            }
           
            ContentsWordList = dataService.LoadContentsList();
            if (ContentsWordList.STL.Count > 0)
            {
                ContentsListSelected.SelectedItem = ContentsWordList.STL[0];
            }
        }
        private bool CanRemoveContentsListEvent()
        {
            if (ContentsWordList.STL.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the WordManageViewModel class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WordManageViewModel()
        {
            NowStatusNCL = true;
            ContentsListSelected = new SelectedItemModel();
            ContentsWordList = dataService.LoadContentsList();

            if (ContentsWordList.STL.Count > 0)
            {
                ContentsListSelected.SelectedItem = ContentsWordList.STL[0];
            }
           
            SelectedCommand = new RelayCommand<SelectedItemModel>((x) => SelectedChangeEvent(x));
            SaveRelationCommand = new RelayCommand<WordList>((x) => SaveRelationEvent(x), (x) => CanSaveRelationEvent(x));
            UpdateListCommand = new RelayCommand<StringList>((x) => UpdateListEvent(x));
            NewContentsListCommand = new RelayCommand(() => NewContentsListEvent(), () => CanNewContentsListEvent());
            RemoveContentsListCommand = new RelayCommand(() => RemoveContentsListEvent(), () => CanRemoveContentsListEvent());
            SaveDeleteCommand = new RelayCommand<WordList>((x) => SaveRelationEvent(x));
        }
        #endregion
    }
}