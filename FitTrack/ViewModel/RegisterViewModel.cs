using FitTrack.MVVM;
using FitTrack.View;
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
        private string firstName;
        private string lastName;
        private string email;
        private string country;

        private readonly Accountmanager accountmanager = new Accountmanager();

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

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        private string selectedQ;
        public List<string> Questions { get; set; }
        public string SelectedQ
        {
            get => selectedQ;
            set
            {
                selectedQ = value;
                OnPropertyChanged(nameof(SelectedQ));
            }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            Questions = new List<string>
        {
            "Item 1",
            "Item 2",
            "Item 3",
            "Item 4"
        };
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);

        }

        private void ExecuteRegister(object parameter)
        {
            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Password dont match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (accountmanager.RegisterUser(Username, Password, FirstName, LastName, Email, Country))
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
                && !string.IsNullOrWhiteSpace(ConfirmPassword)
                && !string.IsNullOrWhiteSpace(FirstName)
                && !string.IsNullOrWhiteSpace(LastName)
                && !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Country);
        }

    }
}
