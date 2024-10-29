using FitTrack.View;
using FitTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.MVVM
{
    public class NavigationCommandManager
    {
        private readonly Accountmanager accountmanager;

        // Konstruktor som tar in accountManager för att användas i alla kommandon
        public NavigationCommandManager(Accountmanager accountmanager)
        {
            this.accountmanager = accountmanager;

            // kommandon för alla navigeringar
            NavigateUserDetailsCommand = new RelayCommand(OpenUserDetailsWindow);
            NavigateWorkoutCommand = new RelayCommand(OpenWorkoutWindow);
            NavigateAddWorkoutCommand = new RelayCommand(OpenAddWorkoutWindow);
            NavigateWorkoutDetailsCommand = new RelayCommand(OpenWorkoutDetailsWindow);
            SignOutCommand = new RelayCommand(ExecuteSignOut);
        }

        public RelayCommand NavigateUserDetailsCommand { get; }
        public RelayCommand NavigateWorkoutCommand { get; }
        public RelayCommand NavigateAddWorkoutCommand { get; }
        public RelayCommand NavigateWorkoutDetailsCommand { get; }
        public RelayCommand SignOutCommand { get; }

        private void OpenUserDetailsWindow(object parameter)
        {
            var userDetailsWindow = new UserDetailWindow
            {
                DataContext = new UserDetailViewModel(accountmanager)
            };
            userDetailsWindow.Show();
            CloseCurrentWindow(userDetailsWindow);

        }
        private void OpenWorkoutWindow(object parameter)
        {
            var workoutWindow = new WorkoutWindow
            {
                DataContext = new WorkoutViewModel(accountmanager)
            };
            workoutWindow.Show();
            CloseCurrentWindow(workoutWindow);
        }
        private void OpenAddWorkoutWindow(object parameter)
        {
            var addworkoutWindow = new AddWorkoutWindow
            {
                DataContext = new AddWorkoutViewModel(accountmanager)
            };
            addworkoutWindow.Show();
            CloseCurrentWindow(addworkoutWindow);
        }
        private void OpenWorkoutDetailsWindow(object parameter)
        {
            var workoutDetailsWindow = new WorkoutDetailsWindow
            {
                DataContext = new WorkoutDetailsViewModel(accountmanager)
            };
            workoutDetailsWindow.Show();
            CloseCurrentWindow(workoutDetailsWindow);
        }
        private void ExecuteSignOut(object parameter)
        {
            accountmanager.CurrentUser = null;
            MainWindow newmain = new MainWindow();
            newmain.Show();
            CloseCurrentWindow(newmain);
        }
        private static void CloseCurrentWindow(Window newWindow)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != newWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
