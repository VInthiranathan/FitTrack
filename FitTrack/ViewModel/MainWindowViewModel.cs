using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MVVMgenomgang.MVVM;
using FitTrack.View;
using System.Collections.ObjectModel;

namespace FitTrack.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string username;
        private string password;
        private readonly Accountmanager accountmanager;

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

        public ICommand LoginCommand { get; }
        public event Action RequestClose; // En händelse/singal

        public MainWindowViewModel()
        {
            accountmanager = new Accountmanager();
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }
        private void ExecuteLogin(object parameter)
        {
            if (accountmanager.ValidateLogin(Username, Password))
            {
                // Inloggning
                System.Windows.MessageBox.Show("The login was successful", "Login", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                HomeWindow homeWindow = new HomeWindow();
                RequestClose?.Invoke(); // Signalerar att fönstret ska stängas.
                homeWindow.Show(); // Nytt fönster

            }
            else
            {
                // !Inloggning
                System.Windows.MessageBox.Show("Wrong username or password! Try again.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        
        private bool CanExecuteLogin(object parameter)
        {
            // Aktivera knappen endast om användarnamnet och lösenordet inte är tomma
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
