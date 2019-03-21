using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _removePersonCommand;
        private RelayCommand<object> _saveCommand;
        private RelayCommand<object> _sortCommand;
        private RelayCommand<object> _radioButtonCommand;

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
            MessageBox.Show("Changes saved successfully");
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
