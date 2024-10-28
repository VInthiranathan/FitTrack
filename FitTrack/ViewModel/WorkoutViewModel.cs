using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    internal class WorkoutViewModel : ViewModelBase
    {
        private readonly Accountmanager accountmanager;
        public RelayCommand SignOutCommand { get; }

        public string FirstName => accountmanager.CurrentUser.FirstName;

        public string LastName => accountmanager.CurrentUser.LastName;
        public string UserType => accountmanager.CurrentUser.GetType().Name;
        public WorkoutViewModel(Accountmanager accountmanager)
        {
            this.accountmanager = accountmanager;
            SignOutCommand = new RelayCommand(ExecuteSignOut);
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
