using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    class UserDetailViewModel : ViewModelBase
    {
        private readonly Accountmanager accountmanager;
        private readonly ObservableCollection<Workout> workouts;
        //public string FirstName => accountmanager.CurrentUser.FirstName;
        //public string LastName => accountmanager.CurrentUser.LastName;
        public string UserType => accountmanager.CurrentUser.GetType().Name;

        public RelayCommand NavigateUserDetailsCommand { get; }
        public RelayCommand NavigateWorkoutCommand { get; }
        public RelayCommand SignOutCommand { get; }
        public RelayCommand NavigateShowInfo { get; }
        public RelayCommand NavigateAddWorkoutCommand { get; }

        private readonly NavigationCommandManager navigationCommandManager;
        public RelayCommand SaveCommand { get; }
        public UserDetailViewModel(Accountmanager accountmanager, ObservableCollection<Workout> workouts)
        {
            SecQ = accountmanager.CurrentUser.GetSecQ();
            this.accountmanager = accountmanager;
            this.workouts = workouts;
            navigationCommandManager = new NavigationCommandManager(accountmanager, workouts);

            NavigateUserDetailsCommand = navigationCommandManager.NavigateUserDetailsCommand;
            NavigateWorkoutCommand = navigationCommandManager.NavigateWorkoutCommand;
            NavigateAddWorkoutCommand = navigationCommandManager.NavigateAddWorkoutCommand;
            SignOutCommand = navigationCommandManager.SignOutCommand;
            NavigateShowInfo = navigationCommandManager.ShowInfoCommand;

            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            Username = accountmanager.CurrentUser.Username;
            FirstName = accountmanager.CurrentUser.FirstName;
            LastName = accountmanager.CurrentUser.LastName;
            Email = accountmanager.CurrentUser.Email;
            Country = accountmanager.CurrentUser.Country;

        }
        private string username;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string confirmPassword;
        private string country;
        public string Username
        {
            get => username;
            set { username = value; OnPropertyChanged(); }
        }

        public string FirstName
        {
            get => firstName;
            set { firstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => lastName;
            set { lastName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set { confirmPassword = value; OnPropertyChanged(); }
        }

        public string Country
        {
            get => country;
            set { country = value; OnPropertyChanged(); }
        }
        private string secA;
        private string secQ;
        public string SecA
        {
            get => secA;
            set
            {
                secA = value;
                OnPropertyChanged(nameof(SecA));
            }
        }
        public string SecQ
        {
            get => secQ;
            set
            {
                if (secQ != value)
                {
                    secQ = value;
                    OnPropertyChanged(nameof(SecQ));
                }
            }
        }

        private void ExecuteSave(object parameter)
        {
            // Validera att lösenorden matchar
            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Password dont match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Spara ändringar
            if (accountmanager.UpdateUser(Username, Password, FirstName, LastName, Email, Country))
            {
                MessageBox.Show("User has been updated!", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteSave(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(FirstName)
                && !string.IsNullOrWhiteSpace(LastName)
                && !string.IsNullOrWhiteSpace(Email)
                && IsPasswordStrong(Password);
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
    }
}
