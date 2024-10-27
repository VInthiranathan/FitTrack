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
        private string secQ;
        private string secA;
        public List<string> Questions { get; set; }

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

        public string SecQ
        {
            get => secQ;
            set
            {
                secQ = value;
                OnPropertyChanged(nameof(SecQ));
            }
        }
        public string SecA
        {
            get => secA;
            set
            {
                secA = value;
                OnPropertyChanged(nameof(SecA));
            }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            Questions = new List<string>
            {
                "Favorite food?",
                "First pets name?",
                "Where were you born?",
                "Favorite color?"
            };
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);

        }

        private void ExecuteRegister(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Username and password cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            /*
            if (!IsPasswordStrong(Password))
            {
                MessageBox.Show("Password must be at least 8 characters long, include uppercase and lowercase letters, a number, and a special character.", "Weak Password", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            */
            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Password dont match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (accountmanager.RegisterUser(Username, Password, FirstName, LastName, Email, Country, SecQ, SecA))
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
                && !string.IsNullOrWhiteSpace(Country)
                && !string.IsNullOrWhiteSpace(SecQ)
                && !string.IsNullOrWhiteSpace(SecA);
        }
        private bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            bool isLongEnough = password.Length >= 8;
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return isLongEnough && hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        private void ClearAll()
        {
            Username = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            Email = string.Empty;
            SecA = string.Empty;
            Country = null;
            SecQ = null;
        }

    }
}
