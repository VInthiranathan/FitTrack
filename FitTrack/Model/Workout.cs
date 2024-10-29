using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public abstract class Workout
    {
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        public Workout(DateTime dateTime, string type, TimeSpan duration)
        {
            DateTime = dateTime;
            Type = type;
            Duration = duration;
        }
        public abstract int CalculateCaloriesBurned();
    }
}
