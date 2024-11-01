using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    internal class WorkoutViewModel : ViewModelBase
    {
        private readonly Accountmanager accountmanager;

        public string FirstName => accountmanager.CurrentUser.FirstName;
        public string LastName => accountmanager.CurrentUser.LastName;
        public string UserType => accountmanager.CurrentUser.GetType().Name;

        public RelayCommand NavigateUserDetailsCommand { get; }
        public RelayCommand NavigateWorkoutCommand { get; }
        public RelayCommand SignOutCommand { get; }
        public RelayCommand NavigateShowInfo { get; }
        public RelayCommand NavigateAddWorkoutCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public RelayCommand WorkoutDetailsCommand { get; }

        private readonly NavigationCommandManager navigationCommandManager;

        public ObservableCollection<Workout> Workouts { get; set; }
        public Workout SelectedWorkout { get; set; }

        public WorkoutViewModel(Accountmanager accountmanager, ObservableCollection<Workout> workouts)
        {
            this.accountmanager = accountmanager;
            Workouts = workouts;
            navigationCommandManager = new NavigationCommandManager(accountmanager, Workouts);

            NavigateUserDetailsCommand = navigationCommandManager.NavigateUserDetailsCommand;
            NavigateWorkoutCommand = navigationCommandManager.NavigateWorkoutCommand;
            NavigateAddWorkoutCommand = navigationCommandManager.NavigateAddWorkoutCommand;
            SignOutCommand = navigationCommandManager.SignOutCommand;
            NavigateShowInfo = navigationCommandManager.ShowInfoCommand;
            WorkoutDetailsCommand = new RelayCommand(OpenWorkoutDetailsWindow);
            
            LoadWorkouts();
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout, CanExecuteWorkoutCommand);
            // Hämtar händelsen när ny workou läggs till
            MessageService.WorkoutAdded += OnWorkoutAdded;
        }
        private void LoadWorkouts()
        {
            // Hämta och lägg till träningspass för nuvarande användare
            if (accountmanager.CurrentUser is AdminUser)
            {
                foreach (var workout in Accountmanager.GetAllWorkouts())
                {
                    if (!Workouts.Contains(workout))
                    {
                        Workouts.Add(workout); // Lägg till det nya träningspasset i listan
                    }
                }
            }
            else if (accountmanager.CurrentUser is User user)
            {
                foreach (var workout in Accountmanager.GetUserWorkouts(user))
                {
                    if (!Workouts.Contains(workout))
                    {
                        Workouts.Add(workout); // Lägg till det nya träningspasset i listan
                    }
                }
            }
        }
        private void RemoveWorkout(object parameter)
        {
            if (SelectedWorkout != null)
            {
                Workouts.Remove(SelectedWorkout);
                MessageBox.Show("Workout removed successfully.");
            }
            else
            {
                MessageBox.Show("Please select a workout to remove.");
            }
        }
        private bool CanExecuteWorkoutCommand(object parameter)
        {
            return SelectedWorkout != null;
        }
        private void OpenWorkoutDetailsWindow(object parameter)
        {
            if (SelectedWorkout != null)
            {
                var workoutDetailsWindow = new WorkoutDetailsWindow
                {
                    DataContext = new WorkoutDetailsViewModel(Workouts, SelectedWorkout, accountmanager)
                };
                workoutDetailsWindow.Show();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != workoutDetailsWindow)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a workout for details.");
            }

        }
        private void OnWorkoutAdded(Workout workout)
        {
            Console.WriteLine($"New Workout Added: {workout.Type} for {workout.Owner.FirstName}");
            if (!Workouts.Contains(workout))
            {
                Workouts.Add(workout); // Lägg till det nya träningspasset i listan
            }
        }
    }
}
