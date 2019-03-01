using System.Collections.Specialized;
using System.Windows;
using Lab_Pyvovar.Tools;
using Lab_Pyvovar.Tools.Managers;

namespace Lab_Pyvovar.View
{
    internal class MainWindowViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        #endregion

        #region Properties
        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        internal MainWindowViewModel()
        {
            LoaderManeger.Instance.Initialize(this);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        internal override void RefreshInfo()
        {
            
        }
    }
}
