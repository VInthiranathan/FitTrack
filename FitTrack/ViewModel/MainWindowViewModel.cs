using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FitTrack.View;
using System.Collections.ObjectModel;

namespace FitTrack.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string username;
        private string password;
        private readonly Accountmanager accountmanager;
        private readonly ObservableCollection<Workout> workouts;
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
        Random random = new Random();
        int sixDigitNumber;
        private string security = " ";
        public string Security
        {
            get => security;
            set
            {
                security = value;
                OnPropertyChanged(nameof(Security));
            }
        }
        private string inputSecurity;
        public string InputSecurity
        {
            get => inputSecurity;
            set
            {
                inputSecurity = value;
                OnPropertyChanged(nameof(InputSecurity));
            }
        }

        public ICommand LoginCommand { get; }
        public event Action RequestClose; // En händelse/singal
        public ICommand GetSecurityCommand { get; }
        public ICommand ForgotPassCommand { get; }
        public MainWindowViewModel(Accountmanager accountmanager, ObservableCollection<Workout> workouts)
        {
            this.accountmanager = accountmanager;
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            this.workouts = workouts;
            ForgotPassCommand = new RelayCommand(ForgotPass, null);
            GetSecurityCommand = new RelayCommand(GetSecurity);
        }

        public MainWindowViewModel()
        {
            accountmanager = new Accountmanager();
            workouts = new ObservableCollection<Workout>();
            ForgotPassCommand = new RelayCommand(ForgotPass, null);
            GetSecurityCommand = new RelayCommand(GetSecurity);
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private void ExecuteLogin(object parameter)
        {
            if (accountmanager.ValidateLogin(Username, Password) && InputSecurity == Security)
            {
                // Inloggning
                System.Windows.MessageBox.Show("The login was successful", "Login", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                WorkoutWindow homeWindow = new WorkoutWindow()
                {
                    // Skicka värden av accountmanager till HomeWindow
                    DataContext = new WorkoutViewModel(accountmanager, workouts)
                };
                RequestClose?.Invoke(); // Signalerar att fönstret ska stängas.
                homeWindow.Show(); // Nytt fönster

            }
            else if (string.IsNullOrEmpty(InputSecurity))
            {
                System.Windows.MessageBox.Show("Pleage get your 2FA and type in! ", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            else
            {
                // !Inloggning
                System.Windows.MessageBox.Show("Wrong username or password! Try again.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        
        private bool CanExecuteLogin(object parameter)
        {
            // Validering
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ForgotPass(object parameter)
        {
            ForgotPass forgotpassW = new View.ForgotPass()
            {
                // Skicka värden av accountmanager till HomeWindow
                DataContext = new ForgotPassViewModel(accountmanager)
            };
            forgotpassW.Show();
        }
        private void GetSecurity(object parameter)
        {
            Security = Convert.ToString(sixDigitNumber = random.Next(100000, 1000000));
        }
    }
}
