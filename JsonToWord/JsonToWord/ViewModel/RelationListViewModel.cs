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
    public class RelationListViewModel : ViewModelBase
    {
        private IDataService dataService = new DataService();   // RelationList
        #region properties
        private RootAllModel _rootAll;                                 //Main , Relation, Contents 메인에서 생성하고, 각 뷰모델의 생성자의 파라메터로 전달
        private RootAllModel RootAll
        {
            get { return _rootAll; }
            set
            {
                _rootAll = value;
                RaisePropertyChanged("RootAll");
            }
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
            }
        }

        private WordListModel _visiableList;                         // RelationList
        public WordListModel VisiableList
        {
            get
            {

                if (_visiableList == null)
                {
                    try
                    {
                        _visiableList = dataService.GetRelaionWord(RootAll, dataService.GetContents(RootAll).ContentsList[0].StrData);
                    }
                    catch
                    {
                        _visiableList = null;
                    }
                }

                return _visiableList;
            }
            set
            {/*
                if (value != null)
                {
                    _visiableList = DoSort(value);                    
                }
                else
                {
                    _visiableList = value;
                }*/
                _visiableList = value;
                RaisePropertyChanged("VisiableList");
            }
        }
        #endregion

        #region Command
        public ICommand SaveRelationCommand { get; private set; }               // 저장 

        private void SaveRelationEvent()                        // RelationList
        {
            List<string> list = new List<string>();
            for (int i = 0; i < RootAll.Root.Count; i++)
            {
                list.Clear();
                foreach (var input in RootAll.Root[i].List)
                {
                    list.Add(input.RelationWord);
                }

                for (int k = 0; k < list.Count; k++)
                {
                    if (list[k] == null)
                    {
                            MessageBox.Show("비어있는 항목이 있는지 확인해주세요.");
                        VisiableList = dataService.GetRelaionWord(RootAll, ContentsListSelected.SelectedItem.StrData);
                        //비어있는 항목
                        return;
                    }
                    int count = 0;
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[k] == list[j])
                        {
                            count++;
                            if (count == 2)
                            {
                                   MessageBox.Show("같은 항목이 있는지 확인해주세요.");
                                VisiableList = dataService.GetRelaionWord(RootAll, ContentsListSelected.SelectedItem.StrData);
                                return;
                            }
                        }
                    }
                }
            }

            dataService.SaveDataService(RootAll);
            VisiableList = dataService.GetRelaionWord(RootAll, ContentsListSelected.SelectedItem.StrData);
            MessengerInstance.Send<RootAllModel>(RootAll, "save");
        }

      private void SelectedEvent(SelectedItemModel selected)
        {
            ContentsListSelected.SelectedItem = selected.SelectedItem;
            try
            {
                VisiableList = dataService.GetRelaionWord(RootAll, ContentsListSelected.SelectedItem.StrData);
            }
            catch
            {
                return;
            }            
        }
        
        private void UpdateEvent(UpdateMsg msg)
        {
            RootAll.Root[msg.index].Word = new StrWrapper(msg.updateData);
            msg.Execute(RootAll);
        }

        private void NewContents(NotificationMessageAction<RootAllModel> noti)
        {
            dataService.NewContentsList(RootAll);
            noti.Execute(RootAll);
        }

        private void RemoveContents()
        {

        }
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the RelationListViewModel class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RelationListViewModel()
        {
            RootAll = dataService.LoadDataService();
            ContentsListSelected = new SelectedItemModel();
            ContentsListSelected.SelectedItem = RootAll.Root[0].Word;
            VisiableList = dataService.GetRelaionWord(RootAll, dataService.GetContents(RootAll).ContentsList[0].StrData);

            //Messenger
            MessengerInstance.Register<PropertyChangedMessage<SelectedItemModel>>(this, "selectedInit", (x) => { ContentsListSelected = x.NewValue; });
            MessengerInstance.Register<SelectedItemModel>(this, "SelectedEvent", SelectedEvent);
            MessengerInstance.Register<UpdateMsg>(this, UpdateEvent);
            MessengerInstance.Register<NotificationMessageAction<RootAllModel>>(this, "NewContent", NewContents);


            //Command
            SaveRelationCommand = new RelayCommand(() => SaveRelationEvent());
        }
    }
}