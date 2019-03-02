﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
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
                           EnteredInfoInplementation, CanSignInExecute));
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

        private bool CorrectDate(DateTime birthday)
        {
            DateTime lastCorrectDate = DateTime.Today.AddYears(-135);
            return birthday.Subtract(lastCorrectDate).Days >= 1 && birthday.Subtract(DateTime.Today).Days <= 0;
        }

        private async void EnteredInfoInplementation(object obj)
        {
            if (!CorrectDate(Convert.ToDateTime(_birthday)))
            {
                MessageBox.Show("Wrong date. Your age can't be more than 135 and less than 0");
                return;
            }
            LoaderManeger.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Thread.Sleep(100);
                try
                {
                    if (!new EmailAddressAttribute().IsValid(_email))
                    {
                        MessageBox.Show($"Email {_email} is not valid.");
                        return false;
                    }
                    StationManager.CurrentPerson = new Person(FirstName, LastName, Email, Convert.ToDateTime(Birthday));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In was failed. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                return true;
            });
            LoaderManeger.Instance.HideLoader();
            if (result)
            {
                if (StationManager.CurrentPerson.IsBirthday)
                    MessageBox.Show("Happy Birthday");
                NavigationManager.Instance.Navigate(ViewType.Info);
            }
        }

        private void CloseImplementation(object obj)
        {
            StationManager.CloseApp();
        }


        internal override void RefreshInfo()
        {
        }
    }
}