using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class CardioWorkout : Workout
    {
        public int Distance { get; set; }

        public override int CalculateCaloriesBurned()
        {
            // Ingen exat uträkning
            CaloriesBurned = (int)(Distance * 0.5 * Duration.TotalMinutes);
            return CaloriesBurned;
        }
    }
}
