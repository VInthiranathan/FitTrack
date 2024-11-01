using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    class WorkoutDetailsViewModel : ViewModelBase
    {
        private readonly Accountmanager accountmanager;
        public string FirstName => accountmanager.CurrentUser.FirstName;
        public string LastName => accountmanager.CurrentUser.LastName;
        public string UserType => accountmanager.CurrentUser.GetType().Name;

        public ICommand ToggleEditCommand { get; }
        public ICommand SaveCommand { get; }
        private readonly ObservableCollection<Workout> workouts;
        private Workout workout;
        private bool isEditing;

        public ICommand SaveTemplateCommand { get; }

        public WorkoutDetailsViewModel(ObservableCollection<Workout> workouts, Workout workout, Accountmanager accountmanager)
        {
            this.accountmanager = accountmanager;
            this.workout = workout;
            this.workouts = workouts;
            ToggleEditCommand = new RelayCommand(change => ToggleEditing());
            SaveCommand = new RelayCommand(SaveWorkout);
            SaveTemplateCommand = new RelayCommand(SaveTemplate);
        }

        public DateTime Date
        {
            get => workout.Date;
            set
            {
                workout.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string Type
        {
            get => workout.Type;
            set
            {
                workout.Type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public TimeSpan Duration
        {
            get => workout.Duration;
            set
            {
                workout.Duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public int CaloriesBurned
        {
            get => workout.CaloriesBurned;
            set
            {
                workout.CaloriesBurned = value;
                OnPropertyChanged(nameof(CaloriesBurned));
            }
        }

        public string Notes
        {
            get => workout.Notes;
            set
            {
                workout.Notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public bool IsEditing
        {
            get => isEditing;
            set
            {
                isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(IsReadOnly)); // Notify read-only state change
            }
        }

        public bool IsReadOnly => !isEditing;

        public void ToggleEditing()
        {
            IsEditing = !IsEditing;
        }

        public bool ValidateInputs()
        {
            // validering
            return !string.IsNullOrWhiteSpace(Type) &&
                Duration.TotalSeconds > 0 &&
                CaloriesBurned >= 0;
        }
        private void SaveWorkout(object parameter)
        {
            if (!ValidateWorkout())
            {
                return; // valideringen fail
            }
            // Öppna det nya WorkoutWindow
            WorkoutWindow homeWindow = new WorkoutWindow()
            {
                // Skicka värden av accountmanager till HomeWindow
                DataContext = new WorkoutViewModel(accountmanager, workouts)
            };
            homeWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window != homeWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
        private bool ValidateWorkout()
        {
            // Kontrollera värden på workout
            if (Date == default(DateTime))
            {
                MessageBox.Show("Please enter a valid date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Type))
            {
                MessageBox.Show("Please enter a workout type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Duration <= TimeSpan.Zero)
            {
                MessageBox.Show("Duration must be greater than zero.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // giltiga värden
            return true;
        }
        private void SaveTemplate(object parameter)
        {
            accountmanager.TemplateWorkout = workout;
        }
    }
}
