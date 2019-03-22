using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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
        private SortBy _sortValue;

        private string _filtrationByFirstName;
        private string _filtrationByLastName;
        private string _filtrationByEmail;
        private DateTime? _filtrationByBirthdayFrom;
        private DateTime? _filtrationByBirthdayTo;
        private string _filtrationBySunSign;
        private string _filtrationByChineseSign;

        private RadioButton[] _filtrationByIsBirthdayItems;
        private int _selectedIndexIsBirthdayFilter;
        private RadioButton[] _filtrationByIsAdultItems;
        private int _selectedIndexIsAdultFilter;

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _removePersonCommand;
        private RelayCommand<object> _sortCommand;
        private RelayCommand<object> _radioButtonCommand;
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
        
        private SortBy SortValue
        {
            get { return _sortValue; }
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

        public RadioButton[] FiltrationByIsBirthdayItems
        {
            get { return _filtrationByIsBirthdayItems; }
            private set
            {
                _filtrationByIsBirthdayItems = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndexIsBirthdayFilter
        {
            get
            {
                return _selectedIndexIsBirthdayFilter = FiltrationByIsBirthdayItems[1].IsChecked != null && ((bool)FiltrationByIsBirthdayItems[1].IsChecked) ? 1
                    : (FiltrationByIsBirthdayItems[0].IsChecked != null && ((bool)FiltrationByIsBirthdayItems[0].IsChecked) ? 0 : -1);
            }
            set
            {
                _selectedIndexIsBirthdayFilter = value;
                OnPropertyChanged();
            }
        }

        public RadioButton[] FiltrationByIsAdultItems
        {
            get { return _filtrationByIsAdultItems; }
            private set
            {
                _filtrationByIsAdultItems = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndexIsAdultFilter
        {
            get
            {
                return _selectedIndexIsAdultFilter = FiltrationByIsAdultItems[1].IsChecked != null && ((bool)FiltrationByIsAdultItems[1].IsChecked) ? 1
                    : (FiltrationByIsAdultItems[0].IsChecked != null && ((bool)FiltrationByIsAdultItems[0].IsChecked) ? 0 : -1);
            }
            set
            {
                _selectedIndexIsAdultFilter = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPersonCommand
        {
            get
            {
                return _addPersonCommand ?? (_addPersonCommand = new RelayCommand<object>(
                           AddPersonImplementation));
            }
        }

        public ICommand EditPersonCommand
        {
            get
            {
                return _editPersonCommand ?? (_editPersonCommand = new RelayCommand<object>(
                           EditPersonImplementation));
            }
        }

        public ICommand RemovePersonCommand
        {
            get
            {
                return _removePersonCommand ?? (_removePersonCommand = new RelayCommand<object>(
                           RemovePersonImplementation));
            }
        }

        public ICommand SortPersonCommand
        {
            get
            {
                return _sortCommand ?? (_sortCommand = new RelayCommand<object>(
                           SortPersonImplementation));
            }
        }

        public ICommand RadioButtonCommand
        {
            get
            {
                return _radioButtonCommand ?? (_radioButtonCommand = new RelayCommand<object>(
                           RadioButtonImplementation));
            }
        }

        public ICommand FilterPersonCommand
        {
            get
            {
                return _filterPersonCommand ?? (_filterPersonCommand = new RelayCommand<object>(
                           FilterImplementation));
            }
        }

        public ICommand ClearFilterPersonCommand
        {
            get
            {
                return _clearFilterPersonCommand ?? (_clearFilterPersonCommand = new RelayCommand<object>(
                           ClearFilterImplementation));
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
            if (SelectedPerson == null)
            {
                MessageBox.Show("Select a person to edit");
                return;
            }
            StationManager.CurrentPerson = SelectedPerson;
            NavigationManager.Instance.Navigate(ViewType.EnterInfo);
        }

        private void RemovePersonImplementation(Object obj)
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("Select a person to remove");
                return;
            }
            StationManager.DataStorage.RemovePerson(SelectedPerson);
            MessageBox.Show("Person was deleted");
            RefreshInfo();
        }

        private void SortPersonImplementation(Object obj)
        {
            StationManager.DataStorage.Sort(SortValue);
            RefreshInfo();
        }

        private void RadioButtonImplementation(Object obj)
        {
            Enum.TryParse(obj.ToString(), false, out _sortValue);
        }

        private void FilterImplementation(Object obj)
        {
            CheckCorrectFiltration();

            People = new ObservableCollection<Person>(Filtration.ToFilter(StationManager.DataStorage.PeopleList, 
                FiltrationByFirstName, FiltrationByLastName, FiltrationByEmail, 
                Convert.ToDateTime(FiltrationByBirthdayFrom), 
                Convert.ToDateTime(FiltrationByBirthdayTo)));
            if (SelectedIndexIsAdultFilter != -1)
                People = new ObservableCollection<Person>(Filtration.ToFilterByIsAdult(People.ToList(), SelectedIndexIsAdultFilter == 1));
            if (SelectedIndexIsBirthdayFilter != -1)
                People = new ObservableCollection<Person>(Filtration.ToFilterByIsBirthday(People.ToList(), SelectedIndexIsBirthdayFilter == 1));
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
            RefreshInfo();
        }

        internal InfoViewModel()
        {
            InitButton();
            _people = new ObservableCollection<Person>(StationManager.DataStorage.PeopleList);
        }

        internal void RefreshInfo()
        {
            RefreshButtons();
            People = new ObservableCollection<Person>(StationManager.DataStorage.PeopleList);
        }
        
        private void InitButton()
        {
            RadioButton yesIsAdultButton = new RadioButton();
            yesIsAdultButton.GroupName = "IsAdultFilter";
            yesIsAdultButton.Content = "Yes";
            RadioButton noIsAdultButton = new RadioButton();
            noIsAdultButton.GroupName = "IsAdultFilter";
            noIsAdultButton.Content = "No";

            RadioButton yesIsBirthdayButton = new RadioButton();
            yesIsBirthdayButton.GroupName = "IsBirthdayFilter";
            yesIsBirthdayButton.Content = "Yes";
            RadioButton noIsBirthdayButton = new RadioButton();
            noIsBirthdayButton.GroupName = "IsBirthdayFilter";
            noIsBirthdayButton.Content = "No";

            FiltrationByIsAdultItems = new RadioButton[]{noIsAdultButton, yesIsAdultButton};
            FiltrationByIsBirthdayItems = new RadioButton[] { noIsBirthdayButton, yesIsBirthdayButton };
        }

        private void RefreshButtons()
        {
            FiltrationByIsAdultItems[0].IsChecked = false;
            FiltrationByIsAdultItems[1].IsChecked = false;
            FiltrationByIsBirthdayItems[0].IsChecked = false;
            FiltrationByIsBirthdayItems[1].IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
