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
        public List<string> woType { get; }

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

        private string selwoType;
        public string SelwoType
        {
            get => selwoType;
            set
            {
                selwoType = value;
                OnPropertyChanged(nameof(SelwoType));
                LoadWorkouts(); // ladda om listan
            }
        }

        public WorkoutViewModel(Accountmanager accountmanager, ObservableCollection<Workout> workouts)
        {
            woType = new List<string>
            {
                "AllWorkouts",
                "StrengthWorkout",
                "CardioWorkout"
            };

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
            // temp collection för filtrera workouts
            IEnumerable<Workout> filteredWorkouts = Enumerable.Empty<Workout>();

            if (accountmanager.CurrentUser is AdminUser)
            {
                filteredWorkouts = Accountmanager.GetAllWorkouts();
            }
            else if (accountmanager.CurrentUser is User user)
            {
                filteredWorkouts = Accountmanager.GetUserWorkouts(user);
            }

            // filtrera baserat på comboboxen
            if (!string.IsNullOrEmpty(SelwoType))
            {
                if (SelwoType == "CardioWorkout")
                {
                    filteredWorkouts = filteredWorkouts.OfType<CardioWorkout>();
                }
                else if (SelwoType == "StrengthWorkout")
                {
                    filteredWorkouts = filteredWorkouts.OfType<StrengthWorkout>();
                }
                else if (SelwoType == "AllWorkouts")
                {
                    filteredWorkouts = filteredWorkouts.OfType<Workout>();
                }
            }

            // Uppdaterar ObservableCollection
            Workouts.Clear();
            foreach (var workout in filteredWorkouts)
            {
                Workouts.Add(workout);
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
