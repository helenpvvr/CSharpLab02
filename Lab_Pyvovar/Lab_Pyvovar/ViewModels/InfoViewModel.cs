using System.Windows.Input;
using Lab_Pyvovar.Tools;
using Lab_Pyvovar.Tools.Managers;
using Lab_Pyvovar.Tools.Navigation;

namespace Lab_Pyvovar.View
{
    internal class InfoViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _birthday;
        private bool _isAdult;
        private string _sunSign;
        private string _chineseSign;
        private bool _isBirthday;

        
        private RelayCommand<object> _backCommand;

        public string FirstName
        {
            get { return _firstName; }
            private set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            private set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            private set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get { return _birthday; }
            private set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
            private set
            {
                _isAdult = value;
                OnPropertyChanged();
            }
        }

        public string SunSign
        {
            get { return _sunSign; }
            private set
            {
                _sunSign = value;
                OnPropertyChanged();
            }
        }

        public string ChineseSign
        {
            get { return _chineseSign; }
            private set
            {
                _chineseSign = value;
                OnPropertyChanged();
            }
        }

        public bool IsBirthday
        {
            get { return _isBirthday; }
            private set
            {
                _isBirthday = value;
                OnPropertyChanged();
            }
        }
        

        public ICommand BackCommand =>
            _backCommand ?? (_backCommand = new RelayCommand<object>(
                BackImplementation));

        private void BackImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.EnterInfo);
        }

        internal override void RefreshInfo()
        {
            FirstName = StationManager.CurrentPerson.FirstName;
            LastName = StationManager.CurrentPerson.LastName;
            Email = StationManager.CurrentPerson.Email;
            Birthday = StationManager.CurrentPerson.Birthday.ToShortDateString();
            IsAdult = StationManager.CurrentPerson.IsAdult;
            SunSign = StationManager.CurrentPerson.SunSign;
            ChineseSign = StationManager.CurrentPerson.ChineseSing;
            IsBirthday = StationManager.CurrentPerson.IsBirthday;
        }
    }
}
