using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FitTrack.View;

namespace FitTrack.ViewModel
{
    class AddWorkoutViewModel : ViewModelBase
    {
        private readonly Accountmanager accountmanager;
        private readonly ObservableCollection<Workout> workouts;
        public string FirstName => accountmanager.CurrentUser.FirstName;
        public string LastName => accountmanager.CurrentUser.LastName;
        public string UserType => accountmanager.CurrentUser.GetType().Name;

        public RelayCommand NavigateUserDetailsCommand { get; }
        public RelayCommand NavigateWorkoutCommand { get; }
        public RelayCommand SignOutCommand { get; }
        public RelayCommand NavigateShowInfo { get; }
        public RelayCommand NavigateAddWorkoutCommand { get; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand GetTemplateCommand { get; }

        private readonly NavigationCommandManager navigationCommandManager;
        public AddWorkoutViewModel(Accountmanager accountmanager, ObservableCollection<Workout> workouts)
        {
            this.accountmanager = accountmanager;
            this.workouts = workouts;
            navigationCommandManager = new NavigationCommandManager(accountmanager, workouts);

            NavigateUserDetailsCommand = navigationCommandManager.NavigateUserDetailsCommand;
            NavigateWorkoutCommand = navigationCommandManager.NavigateWorkoutCommand;
            NavigateAddWorkoutCommand = navigationCommandManager.NavigateAddWorkoutCommand;
            SignOutCommand = navigationCommandManager.SignOutCommand;
            NavigateShowInfo = navigationCommandManager.ShowInfoCommand;

            SaveCommand = new RelayCommand(SaveWorkout, CanSaveWorkout);
            GetTemplateCommand = new RelayCommand(GetTemplate);

        }
        private string type;
        private TimeSpan duration;
        private int caloriesBurned;
        private string notes;
        private WorkoutType selectedWorkoutType;
        private DateTime date = DateTime.Today;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public TimeSpan Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged(nameof(duration));
            }
        }

        public int CaloriesBurned
        {
            get => caloriesBurned;
            set
            {
                caloriesBurned = value;
                OnPropertyChanged(nameof(caloriesBurned));
            }
        }

        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged(nameof(notes));
            }
        }

        public WorkoutType SelectedWorkoutType
        {
            get => selectedWorkoutType;
            set
            {
                selectedWorkoutType = value;
                OnPropertyChanged(nameof(selectedWorkoutType));
            }
        }


        private void SaveWorkout(object parameter)
        {
            if (accountmanager.CurrentUser == null)
            {
                MessageBox.Show("No user is logged in.");
                return;
            }

            if (Date == default || Duration == default || CaloriesBurned <= 0 || string.IsNullOrWhiteSpace(Type))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            if (workouts == null)
            {
                MessageBox.Show("Workout list is not initialized.");
                return;
            }

            Workout newWorkout;

            if (SelectedWorkoutType == WorkoutType.Cardio)
            {
                newWorkout = new CardioWorkout
                {
                    Date = Date,
                    Duration = Duration,
                    CaloriesBurned = CaloriesBurned,
                    Notes = Notes,
                    Type = Type,
                    Owner = accountmanager.CurrentUser
                };
            }
            else
            {
                newWorkout = new StrengthWorkout
                {
                    Date = Date,
                    Duration = Duration,
                    CaloriesBurned = CaloriesBurned,
                    Notes = Notes,
                    Type = Type,
                    Owner = accountmanager.CurrentUser
                };
            }
            Accountmanager.Workouts.Add(newWorkout); // Lägger till i listan
            MessageService.SendWorkoutAdded(newWorkout); // Meddela om workout

            WorkoutWindow homeWindow = new WorkoutWindow()
            {
                // Skicka värden av accountmanager till HomeWindow
                DataContext = new WorkoutViewModel(accountmanager, workouts)
            };
            foreach (Window window in Application.Current.Windows)
            {
                if (window != homeWindow)
                {
                    window.Close();
                    break;
                }
            }
            homeWindow.Show(); // Nytt fönster
        }

        private bool CanSaveWorkout(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Type) && Date != default && Duration != default && CaloriesBurned > 0;
        }


        public void GetTemplate(object parameter)
        {
            Date = accountmanager.TemplateWorkout.Date;
            Duration = accountmanager.TemplateWorkout.Duration;
            CaloriesBurned = accountmanager.TemplateWorkout.CaloriesBurned;
            Notes = accountmanager.TemplateWorkout.Notes;
            Type = accountmanager.TemplateWorkout.Type;
        }
    }
}
