using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public abstract class Workout : ViewModelBase
    {
        private DateTime date;
        private string type;
        private TimeSpan duration;
        private int caloriesBurned;
        private string notes;
        public Person Owner { get; set; }
        public string OwnerFullName => $"{Owner.FirstName} {Owner.LastName}";

        public DateTime Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string Type
        {
            get => type;
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        public TimeSpan Duration
        {
            get => duration;
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        public int CaloriesBurned
        {
            get => caloriesBurned;
            set
            {
                if (caloriesBurned != value)
                {
                    caloriesBurned = value;
                    OnPropertyChanged(nameof(CaloriesBurned));
                }
            }
        }

        public string Notes
        {
            get => notes;
            set
            {
                if (notes != value)
                {
                    notes = value;
                    OnPropertyChanged(nameof(Notes));
                }
            }
        }
        public abstract int CalculateCaloriesBurned();
    }

}
