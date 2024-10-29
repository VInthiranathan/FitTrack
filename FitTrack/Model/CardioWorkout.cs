using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    class CardioWorkout : Workout
    {
        public int Distance { get; set; }
        public CardioWorkout(DateTime dateTime, string type, TimeSpan duration) : base(dateTime, type, duration)
        {

        }

        public override int CalculateCaloriesBurned()
        {
            return 400;
        }
    }
}
