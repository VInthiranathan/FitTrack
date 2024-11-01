using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    public class ForgotPassViewModel : ViewModelBase
    {
        private readonly Accountmanager accountmanager;

        public List<string> Questions { get; set; }
        public ICommand GetPassCommand { get; }
        public ICommand GetSecurityCommand { get; }
        public ICommand CloseCommand { get; }
        public ForgotPassViewModel(Accountmanager accountmanager)
        {
            this.accountmanager = accountmanager;
            Questions = new List<string>
            {
                "Favorite food?",
                "First pets name?",
                "Where were you born?",
                "Favorite color?"
            };
            GetPassCommand = new RelayCommand(ExecuteGetPass, CanExecutePass);
            GetSecurityCommand = new RelayCommand(ExecuteGetUser, CanExecuteUser);
            CloseCommand = new RelayCommand(CloseW);
        }
        private string username;
        private string secA;
        private string secQ;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
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
        private void ExecuteGetPass(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(SecA))
            {
                MessageBox.Show("Username and security answer cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Username == accountmanager.CurrentUser.Username && SecA == accountmanager.CurrentUser.SecA)
            {
                MessageBox.Show($"Password for {accountmanager.CurrentUser.Username} is {accountmanager.CurrentUser.Password}", "Password", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                MessageBox.Show("Something went wrong! try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private bool CanExecutePass(object parameter)
        {
            // Validering
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(SecA);
        }
        private bool CanExecuteUser(object parameter)
        {
            // Validering
            return !string.IsNullOrWhiteSpace(Username);
        }
        private void ExecuteGetUser(object parameter)
        {
            if (accountmanager.GetPassword(Username))
            {
                // Sätt SecQ till säkerhetsfrågan från den nuvarande användaren
                SecQ = accountmanager.CurrentUser.GetSecQ();

                System.Windows.MessageBox.Show("Please answer your security question.", "Login", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Wrong username! Try again.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        public void CloseW(object parameter)
        {
            foreach (Window window in Application.Current.Windows)
            {
                // Stänger window som tillhör denna ViewModel
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
