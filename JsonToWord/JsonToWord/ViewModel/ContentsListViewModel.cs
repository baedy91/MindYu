using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JsonToWord.Model;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using JsonToWord.Message;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
namespace JsonToWord.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ContentsListViewModel : ViewModelBase
    {
        IDataService dataService = new DataService();
        bool NowStatusNCL;
        #region Property
        private RootAllModel _rootAll;
        public void SetRoot(RootAllModel root)
        {
            _rootAll = root;
        }
       
        private List<string> _oldCurrentList;                       //ContentList   수정되기전 항목 리스트 (ContentsListBeginEditCommand)
        public List<string> OldCurrentList
        {
            get { return _oldCurrentList; }
            set { _oldCurrentList = value; }
        }

        private SelectedItemModel _contentsListSelected;    //ContentsList , RelationList(messenger)
        public SelectedItemModel ContentsListSelected
        {
            get
            {
                return _contentsListSelected;
            }
            set
            {
               
                _contentsListSelected = value;
                RaisePropertyChanged("ContentsListSelected");
                //  RaisePropertyChanged("VisiableList");
                MessengerInstance.Send<PropertyChangedMessage<SelectedItemModel>>
            (new PropertyChangedMessage<SelectedItemModel>(null, ContentsListSelected, "ContentsListSelected"), "selectedInit");
            }
        }
        private ContentsListModel _conList;
        public ContentsListModel ConList
        {
            get
            {
                try
                {
                    _conList = _conList ?? dataService.GetContents(dataService.LoadDataService());   // init 처음에만                
                }
                catch { }
                return _conList;
            }
            set
            {
                _conList = value;
                RaisePropertyChanged("ConList");
            }
        }

        public void SetConList(RootAllModel root)       // RelationList Save할때 메세지 받고 갱신
        {
            ConList = dataService.GetContents(root);
            
            try
            {
                OldCurrentList = CurrentListChange(root);
                ContentsListSelected.SelectedItem = ConList.ContentsList[ConList.ContentsList.Count - 1];
            }
            catch { }
        }

        private IntWrapper _selectedIndex = new IntWrapper(-1);         //contentsList
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

        #endregion
        #region Command
        // ContentsList
        public ICommand SelectedCommand { get; private set; }                    // 선택 
        public ICommand UpdateListCommand { get; private set; }                 // 수정    
        public ICommand NewContentsListCommand { get; private set; }         // 추가
        public ICommand RemoveContentsListCommand { get; private set; }     // 삭제

        private void SelectedChangeEvent(SelectedItemModel selected)
        {
            Messenger.Default.Send<SelectedItemModel>(selected, "SelectedEvent");
        }
        private void UpdateListEvent(ContentsListModel clm)
        {
            UpdateMsg msg = new UpdateMsg("update", UpdateCallback);
            msg.index = SelectedIndex.IntData;
            msg.oldData = OldCurrentList[SelectedIndex.IntData];
            msg.updateData = clm.ContentsList[msg.index].StrData;
            
            MessageBox.Show( clm.ContentsList[0].StrData + "  " + _rootAll.Root[0].Word.StrData);
            if (clm.ContentsList[msg.index].StrData == _rootAll.Root[msg.index].Word.StrData) // 수정값이 없다면
            {
                ConList = dataService.GetContents(_rootAll);
                OldCurrentList = CurrentListChange(_rootAll);
                return;
            }
            
            if (OldCurrentList.Contains(msg.updateData)) // 수정값이 중복이면
            {
                MessageBox.Show("같은 항목이 있는지 확인해주세요. \n\r저장되지 않았습니다.");
                clm.ContentsList[msg.index].StrData = msg.oldData;
                ConList = dataService.GetContents(_rootAll);
                OldCurrentList = CurrentListChange(_rootAll);
                return;
            }            
            //수정            
            MessengerInstance.Send<UpdateMsg>(msg);

            if (msg.updateData == "새항목")
            {
                NowStatusNCL = false;
            }

            if (msg.oldData == "새항목")
            {
                NowStatusNCL = true;
            }

       //     ConList = dataservice.GetContents(_rootAll);    //갱신            

          //  Messenger.Default.Send
        }
        private void UpdateCallback(RootAllModel root)
        {
            ConList = dataService.GetContents(root);
            OldCurrentList = CurrentListChange(root);
        }

        private void NewContentsEvent()
        {
            if (NowStatusNCL)
            {
                WordListModel newContent = new WordListModel(new StrWrapper("새항목"), new ObservableCollection<RelationWordModel>());

                _rootAll.Root.Add(newContent);  // messenger 새로운 항목 추가하고 피드백 받아   
                SetConList(_rootAll);
                NowStatusNCL = false;
            }
            else
            {
                MessageBox.Show("이미 새항목이 존재합니다.\n단어이름을 수정해 주세요", "추가불가");
            }
        }

        private List<string> CurrentListChange(RootAllModel root)
        {
            var list = new List<string>();
            foreach (WordListModel wl in root.Root)
            {
                list.Add(wl.Word.StrData);
            }
            return list;
        }

        private void RemoveContentsEvent()
        {
            int index = SelectedIndex.IntData;
            // removeResult re = dataService.RemoveContentsList(index);
            WordListModel removeItem = _rootAll.Root[index];

            _rootAll.Root.RemoveAt(index);       // message

            if (removeItem.Word.StrData == "새항목")
            {
                NowStatusNCL = true;
            }

            ConList = dataService.GetContents(_rootAll);
            if (ConList.ContentsList.Count > 0)
            {
                ContentsListSelected.SelectedItem = ConList.ContentsList[0];
            }
        }

        private bool CanRemoveContentsEvent()
        {
            if (ConList.ContentsList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the ContentsListViewModel class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContentsListViewModel()
        {
            SetRoot(dataService.LoadDataService()); // 맨 처음 로드시 저장된 데이타에서 Root 뽑아냄
            SetConList(_rootAll);
            if (dataService.IsContainWord(_rootAll, "새항목"))
            {
                NowStatusNCL = false;
            }
            else
            {
                NowStatusNCL = true;
            }
            ContentsListSelected = new SelectedItemModel();
            ConList = dataService.GetContents(dataService.LoadDataService());
                       
            if (ConList.ContentsList.Count > 0)
            {
                ContentsListSelected.SelectedItem = ConList.ContentsList[0];
            }
            OldCurrentList = CurrentListChange(_rootAll); 



            //messenger         
            MessengerInstance.Register<RootAllModel>(this, "save", SetRoot);


            //Command
            SelectedCommand = new RelayCommand<SelectedItemModel>((x) => SelectedChangeEvent(x));
            UpdateListCommand = new RelayCommand<ContentsListModel>((x) => UpdateListEvent(x));
            NewContentsListCommand = new RelayCommand(() => NewContentsEvent());
            RemoveContentsListCommand = new RelayCommand(() => RemoveContentsEvent(), () => CanRemoveContentsEvent());
        }
    }
}