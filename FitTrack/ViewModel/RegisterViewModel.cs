using FitTrack.MVVM;
using MVVMgenomgang.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private string username;
        private string password;
        private string confirmPassword;
        private Accountmanager accountmanager;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            accountmanager = new Accountmanager();
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
        }

        private void ExecuteRegister(object parameter)
        {
            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Password dont match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (accountmanager.RegisterUser(Username, Password))
            {
                MessageBox.Show("You have seccesfully created an account", "Sign up", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Username already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteRegister(object parameter)
        {
            // Knappen är endast aktiv om användarnamnet och lösenordet inte är tomma

            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(ConfirmPassword);
        }

    }
}
