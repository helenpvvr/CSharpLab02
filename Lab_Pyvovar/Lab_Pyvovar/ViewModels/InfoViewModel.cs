using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Lab_Pyvovar.Filter;
using Lab_Pyvovar.Models;
using Lab_Pyvovar.Tools;
using Lab_Pyvovar.Tools.Managers;
using Lab_Pyvovar.Tools.Navigation;

namespace Lab_Pyvovar.View
{
    internal class InfoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _people;
        private Person _selectedPerson;
        private SortBy _sort;

        private string _filtrationByFirstName;
        private string _filtrationByLastName;
        private string _filtrationByEmail;
        private DateTime? _filtrationByBirthdayFrom;
        private DateTime? _filtrationByBirthdayTo;
        private string _filtrationIsAdult;
        private string _filtrationBySunSign;
        private string _filtrationByChineseSign;
        private string _filtrationIsBirthday;

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _removePersonCommand;
        private RelayCommand<object> _saveCommand;
        private RelayCommand<object> _sortCommand;
        private RelayCommand<object> _radioButtonCommand;
        private RelayCommand<object> _filterIsAdultCommand;
        private RelayCommand<object> _filterIsBirthdayCommand;
        private RelayCommand<object> _filterPersonCommand;
        private RelayCommand<object> _clearFilterPersonCommand;

        public ObservableCollection<Person> People
        {
            get { return _people; }
            private set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        // TODO public change or not?
        public SortBy Sort
        {
            get { return _sort; }
            private set
            {
                _sort = value;
                OnPropertyChanged();
            }
        }
        
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public string FiltrationByFirstName
        {
            get { return _filtrationByFirstName; }
            set
            {
                _filtrationByFirstName = value;
                OnPropertyChanged();
            }
        }

        public string FiltrationByLastName
        {
            get { return _filtrationByLastName; }
            set
            {
                _filtrationByLastName = value;
                OnPropertyChanged();
            }
        }

        public string FiltrationByEmail
        {
            get { return _filtrationByEmail; }
            set
            {
                _filtrationByEmail = value;
                OnPropertyChanged();
            }
        }

        public DateTime? FiltrationByBirthdayFrom
        {
            get { return _filtrationByBirthdayFrom; }
            set
            {
                _filtrationByBirthdayFrom = value;
                OnPropertyChanged();
            }
        }

        private string FiltrationIsAdult
        {
            get { return _filtrationIsAdult; }
            set
            {
                _filtrationIsAdult = value;
                OnPropertyChanged();
            }
        }

        private string FiltrationIsBirthday
        {
            get { return _filtrationIsBirthday; }
            set
            {
                _filtrationIsBirthday = value;
                OnPropertyChanged();
            }
        }

        public DateTime? FiltrationByBirthdayTo
        {
            get { return _filtrationByBirthdayTo; }
            set
            {
                _filtrationByBirthdayTo = value;
                OnPropertyChanged();
            }
        }

        public string FiltrationBySunSign
        {
            get { return _filtrationBySunSign; }
            set
            {
                _filtrationBySunSign = value;
                OnPropertyChanged();
            }
        }

        public string FiltrationByChineseSign
        {
            get { return _filtrationByChineseSign; }
            set
            {
                _filtrationByChineseSign = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPersonCommand
        {
            get
            {
                return _addPersonCommand ?? (_addPersonCommand = new RelayCommand<object>(
                           AddPersonImplementation)); // , CanSignInExecute
            }
        }

        public ICommand EditPersonCommand
        {
            get
            {
                return _editPersonCommand ?? (_editPersonCommand = new RelayCommand<object>(
                           EditPersonImplementation)); // , CanSignInExecute
            }
        }

        public ICommand RemovePersonCommand
        {
            get
            {
                return _removePersonCommand ?? (_removePersonCommand = new RelayCommand<object>(
                           RemovePersonImplementation)); // , CanSignInExecute
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand<object>(
                           SaveImplementation)); // , CanSignInExecute
            }
        }

        public ICommand SortPersonCommand
        {
            get
            {
                return _sortCommand ?? (_sortCommand = new RelayCommand<object>(
                           SortPersonImplementation)); // , CanSignInExecute
            }
        }

        public ICommand RadioButtonCommand
        {
            get
            {
                return _radioButtonCommand ?? (_radioButtonCommand = new RelayCommand<object>(
                           RadioButtonImplementation)); // , CanSignInExecute
            }
        }

        public ICommand FilterIsAdultCommand
        {
            get
            {
                return _filterIsAdultCommand ?? (_filterIsAdultCommand = new RelayCommand<object>(
                           FilterIsAdultSelectedImplementation)); // , CanSignInExecute
            }
        }

        public ICommand FilterIsBirthdayCommand
        {
            get
            {
                return _filterIsBirthdayCommand ?? (_filterIsBirthdayCommand = new RelayCommand<object>(
                           FilterIsBirthdaySelectedImplementation)); // , CanSignInExecute
            }
        }

        public ICommand FilterPersonCommand
        {
            get
            {
                return _filterPersonCommand ?? (_filterPersonCommand = new RelayCommand<object>(
                           FilterImplementation)); // , CanSignInExecute
            }
        }

        public ICommand ClearFilterPersonCommand
        {
            get
            {
                return _clearFilterPersonCommand ?? (_clearFilterPersonCommand = new RelayCommand<object>(
                           ClearFilterImplementation)); // , CanSignInExecute
            }
        }

        private void AddPersonImplementation(Object obj)
        {
            StationManager.CurrentPerson = null;
            NavigationManager.Instance.Navigate(ViewType.EnterInfo);
        }

        private void EditPersonImplementation(Object obj)
        {
            StationManager.DataStorage.RemovePerson(SelectedPerson);
            StationManager.CurrentPerson = SelectedPerson;
            NavigationManager.Instance.Navigate(ViewType.EnterInfo);
        }

        private void RemovePersonImplementation(Object obj)
        {
            StationManager.DataStorage.RemovePerson(SelectedPerson);
            RefreshInfo();
        }

        private void SaveImplementation(Object obj)
        {
            // SerializationManager.Serialize(_people, FileFolderHelper.StorageFilePath);
            // TODO change here
            // MessageBox.Show("Changes saved successfully");
        }

        private void SortPersonImplementation(Object obj)
        {
            StationManager.DataStorage.UsersList = Sorting.ToSortBy(Sort);
            RefreshInfo();
        }

        private void RadioButtonImplementation(Object obj)
        {
            Enum.TryParse(obj.ToString(), false, out _sort);
        }

        private void FilterIsAdultSelectedImplementation(Object obj)
        {
            FiltrationIsAdult = obj.ToString();
        }

        private void FilterIsBirthdaySelectedImplementation(Object obj)
        {
            FiltrationIsBirthday = obj.ToString();
        }

        private void FilterImplementation(Object obj)
        {
            CheckCorrectFiltration();

            People = new ObservableCollection<Person>(Filtration.ToFilter(StationManager.DataStorage.UsersList, 
                FiltrationByFirstName, 
                FiltrationByLastName, FiltrationByEmail, 
                Convert.ToDateTime(FiltrationByBirthdayFrom), 
                Convert.ToDateTime(FiltrationByBirthdayTo)));
            if (!String.IsNullOrEmpty(FiltrationIsAdult))
                People = new ObservableCollection<Person>(Filtration.ToFilterByIsAdult(People.ToList(), FiltrationIsAdult.Equals("Yes")));
            if (!String.IsNullOrEmpty(FiltrationIsBirthday))
                People = new ObservableCollection<Person>(Filtration.ToFilterByIsBirthday(People.ToList(), FiltrationIsBirthday.Equals("Yes")));
            if(!String.IsNullOrEmpty(FiltrationBySunSign))
                People = new ObservableCollection<Person>(Filtration.ToFilterBySunSign(People.ToList(), FiltrationBySunSign));
            if(!String.IsNullOrEmpty(FiltrationByChineseSign))
                People = new ObservableCollection<Person>(Filtration.ToFilterByChinese(People.ToList(), FiltrationByChineseSign));
        }

        private void CheckCorrectFiltration()
        {
            if (FiltrationByFirstName == null)
                FiltrationByFirstName = "";
            if (FiltrationByLastName == null)
                FiltrationByLastName = "";
            if (FiltrationByEmail == null)
                FiltrationByEmail = "";
            if (FiltrationByBirthdayFrom == null)
                FiltrationByBirthdayFrom = DateTime.Today.AddYears(-135);
            if (FiltrationByBirthdayTo == null)
                FiltrationByBirthdayTo = DateTime.Today;
            if (FiltrationByEmail == null)
                FiltrationByEmail = "";
        }

        private void ClearFilterImplementation(Object obj)
        {
            FiltrationByFirstName = "";
            FiltrationByLastName = "";
            FiltrationByEmail = "";
            FiltrationByBirthdayFrom = null;
            FiltrationByBirthdayTo = null;
            FiltrationBySunSign = "";
            FiltrationByChineseSign = "";
            FiltrationIsAdult = "";
            FiltrationIsBirthday = "";
            RefreshInfo();

            
        }

        internal InfoViewModel()
        {
            _people = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }

        internal void RefreshInfo()
        {
            People = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
