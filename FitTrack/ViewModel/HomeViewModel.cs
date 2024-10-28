using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    class HomeViewModel: ViewModelBase
    {
        private readonly Accountmanager accountmanager;

        public RelayCommand SignOutCommand { get; }
        public RelayCommand WorkoutCommand { get; }
        public RelayCommand WorkoutDetailsCommand { get; }
        public RelayCommand AddWourkoutCommand { get; }
        public RelayCommand UserDetailsCommand { get; }

        public string FirstName => accountmanager.CurrentUser.FirstName;

        public string LastName => accountmanager.CurrentUser.LastName;
        public string UserType => accountmanager.CurrentUser.GetType().Name;

        public HomeViewModel(Accountmanager accountmanager)
        {
            // Ge värden från mainwindows accountmanager
            this.accountmanager = accountmanager;
            SignOutCommand = new RelayCommand(ExecuteSignOut);
            WorkoutCommand = new RelayCommand(NavigateWorkoutWindow);
            WorkoutDetailsCommand = new RelayCommand(NavigateWourkoutDetailsWindow);
            AddWourkoutCommand = new RelayCommand(NavigateAddWourkoutWindow);
            UserDetailsCommand = new RelayCommand(NavigateUserDetailsWindow);

        }

        private void NavigateUserDetailsWindow(object parameter)
        {
            Application.Current.MainWindow = new UserDetailWindow()
            {
                DataContext = new UserDetailViewModel(accountmanager)
            };
            Application.Current.MainWindow.Show();

            CloseWindow((Window)parameter);
        }

        private void NavigateAddWourkoutWindow(object parameter)
        {
            Application.Current.MainWindow = new AddWorkoutWindow()
            {
                DataContext = new AddWorkoutViewModel(accountmanager)
            };
            Application.Current.MainWindow.Show();

            CloseWindow((Window)parameter);
        }

        private void NavigateWourkoutDetailsWindow(object parameter)
        {
            Application.Current.MainWindow = new WorkoutDetailsWindow()
            {
                DataContext = new WorkoutDetailsViewModel(accountmanager)
            };
            Application.Current.MainWindow.Show();

            CloseWindow((Window)parameter);
        }

        private void NavigateWorkoutWindow(object parameter)
        {
            Application.Current.MainWindow = new WorkoutWindow()
            {
                DataContext = new WorkoutViewModel(accountmanager)
            };
            Application.Current.MainWindow.Show();

            CloseWindow((Window)parameter);
        }


        public void ExecuteSignOut(object parameter)
        {
            // nollställer CurrentUser
            accountmanager.CurrentUser = null;

            // öppnar mainwindow
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();

            CloseWindow((Window)parameter);
        }
        static void CloseWindow(Window parameter)
        {
            // stänger homewindow
            if (parameter is Window homeWindow)
            {
                homeWindow.Close();
            }
        }

    }
}
