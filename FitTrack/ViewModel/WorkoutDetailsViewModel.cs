using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    class WorkoutDetailsViewModel
    {
        private readonly Accountmanager accountmanager;

        public WorkoutDetailsViewModel(Accountmanager accountmanager)
        {
            this.accountmanager = accountmanager;
        }
    }
}
