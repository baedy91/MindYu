using GalaSoft.MvvmLight;
using JsonToWord.Model;
using Microsoft.Practices.ServiceLocation;

namespace JsonToWord.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

         //   CurrentViewModel = ServiceLocator.Current.GetInstance<WordManageViewModel>();
            ContentsListVM = ServiceLocator.Current.GetInstance<ContentsListViewModel>();
            RelationListVM = ServiceLocator.Current.GetInstance<RelationListViewModel>();
          //  _ContentsListVM = new ContentsListViewModel();
          //  _relationListVM = new RelationListViewModel();
        }
        #region currentViewModel
        private ViewModelBase _ContentsListVM;
        public ViewModelBase ContentsListVM
        {
            get
            {
                return _ContentsListVM;
            }
            set
            {
                Set("ContentsListVM", ref _ContentsListVM, value);
            }
        }
        private ViewModelBase _relationListVM;
        public ViewModelBase RelationListVM
        {
            get
            {
                return _relationListVM;
            }
            set
            {
                Set("RelationListVM", ref _relationListVM, value);
            }
        }

        #endregion

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}