using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    class StrenthWorkout : Workout
    {
        public int Repetitions { get; set; }
        public StrenthWorkout(DateTime dateTime, string type, TimeSpan duration) : base(dateTime, type, duration)
        {

        }

        public override int CalculateCaloriesBurned()
        {
            return 300;
        }
    }
}
