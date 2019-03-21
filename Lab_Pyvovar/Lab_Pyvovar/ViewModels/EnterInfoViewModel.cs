using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Lab_Pyvovar.Exceptions;
using Lab_Pyvovar.Models;
using Lab_Pyvovar.Tools;
using Lab_Pyvovar.Tools.Managers;
using Lab_Pyvovar.Tools.Navigation;

namespace Lab_Pyvovar.View
{
    internal class EnterInfoViewModel : BaseViewModel
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _birthday;

        #region Commands
        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _closeCommand;
        #endregion
        #endregion

        #region Properties
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value.Replace(" ", "");
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value.Replace(" ", "");
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value.Replace(" ", "");
                OnPropertyChanged();
            }
        }

        public DateTime? Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }



        #region Commands

        public ICommand ProceedCommand
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(
                           EnteredInfoImplementation, CanSignInExecute));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseImplementation));
            }
        }

        #endregion
        #endregion

        private bool CanSignInExecute(object obj)
        {
            return !String.IsNullOrEmpty(_firstName) 
                   && !String.IsNullOrEmpty(_lastName) 
                   && !String.IsNullOrEmpty(_email) 
                   && _birthday != null;
        }

        private async void EnteredInfoImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Thread.Sleep(100);
                try
                {
                    var person = new Person(FirstName, LastName, Email, Convert.ToDateTime(Birthday));
                    StationManager.DataStorage.AddPerson(person);
                    StationManager.CurrentPerson = person;
                }
                catch (EmailException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                catch (AgeException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed fo user {_firstName}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                RefreshInfo();
                NavigationManager.Instance.Navigate(ViewType.Info);
            }
        }

        private void CloseImplementation(object obj)
        {
            StationManager.CloseApp();
        }


        internal override void RefreshInfo()
        {
            if (StationManager.CurrentPerson != null)
            {
                FirstName = StationManager.CurrentPerson.FirstName;
                LastName = StationManager.CurrentPerson.LastName;
                Email = StationManager.CurrentPerson.Email;
                Birthday = StationManager.CurrentPerson.Birthday;
                return;
            }
            FirstName = "";
            LastName = "";
            Email = "";
            Birthday = null;
        }
    }
}
