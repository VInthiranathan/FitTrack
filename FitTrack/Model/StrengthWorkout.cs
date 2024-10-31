using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        public int Repetitions { get; set; }

        public override int CalculateCaloriesBurned()
        {
            // Ingen exat uträkning
            CaloriesBurned = (int)(Repetitions * 0.4 * Duration.TotalMinutes);
            return CaloriesBurned;
        }
    }
}
