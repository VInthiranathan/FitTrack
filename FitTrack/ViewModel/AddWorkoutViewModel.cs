using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    class AddWorkoutViewModel
    {
        private readonly Accountmanager accountmanager;

        public AddWorkoutViewModel(Accountmanager accountmanager)
        {
            this.accountmanager = accountmanager;
        }
    }
}
