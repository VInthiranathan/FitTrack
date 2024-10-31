using FitTrack.Model;
using FitTrack.View;
using FitTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly ObservableCollection<Workout> workouts;
        // Konstruktor som tar in accountManager för att användas i alla kommandon
        public NavigationCommandManager(Accountmanager accountmanager, ObservableCollection<Workout> workouts)
        {
            this.accountmanager = accountmanager;
            this.workouts = workouts;

            // kommandon för alla navigeringar
            NavigateUserDetailsCommand = new RelayCommand(OpenUserDetailsWindow);
            NavigateWorkoutCommand = new RelayCommand(OpenWorkoutWindow);
            NavigateAddWorkoutCommand = new RelayCommand(OpenAddWorkoutWindow);
            SignOutCommand = new RelayCommand(ExecuteSignOut);
            ShowInfoCommand = new RelayCommand(param => ShowInfo(null, null));
        }

        public RelayCommand NavigateUserDetailsCommand { get; }
        public RelayCommand NavigateWorkoutCommand { get; }
        public RelayCommand NavigateAddWorkoutCommand { get; }
        public RelayCommand NavigateWorkoutDetailsCommand { get; }
        public RelayCommand SignOutCommand { get; }
        public RelayCommand ShowInfoCommand { get; }

        private void OpenUserDetailsWindow(object parameter)
        {
            var userDetailsWindow = new UserDetailWindow
            {
                DataContext = new UserDetailViewModel(accountmanager, workouts)
            };
            userDetailsWindow.Show();
            CloseCurrentWindow(userDetailsWindow);

        }
        private void OpenWorkoutWindow(object parameter)
        {
            var workoutWindow = new WorkoutWindow
            {
                DataContext = new WorkoutViewModel(accountmanager, workouts)
            };
            workoutWindow.Show();
            CloseCurrentWindow(workoutWindow);
        }
        private void OpenAddWorkoutWindow(object parameter)
        {
            var addworkoutWindow = new AddWorkoutWindow
            {
                DataContext = new AddWorkoutViewModel(accountmanager, workouts)
            };
            addworkoutWindow.Show();
            CloseCurrentWindow(addworkoutWindow);
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
        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("With the FitTrack app you can easily log all your workouts and stay on top of your fitness journey.\n" +
                "As administrators can monitor the workout logs of all their team members, ensuring everyone stays motivated and accountable.\n" +
                "Get fit with FitTrack!", "About FitTrack", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
