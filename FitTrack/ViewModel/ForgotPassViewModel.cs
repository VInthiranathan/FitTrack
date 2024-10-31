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

        }
        private string username;
        private string secQ;
        private string secA;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
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
            // Knappen är endast aktiv om användarnamnet och lösenordet inte är tomma
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(SecQ)
                && !string.IsNullOrWhiteSpace(SecA);
        }

    }
}
