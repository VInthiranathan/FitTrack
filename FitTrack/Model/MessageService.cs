using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public static class MessageService
    {
        public static event Action<Workout> WorkoutAdded;

        public static void SendWorkoutAdded(Workout workout)
        {
            WorkoutAdded?.Invoke(workout); // Skicka meddelandet om ett nytt träningspass
        }
    }
}
