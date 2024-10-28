using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    class UserDetailViewModel : ViewModelBase
    {
        private readonly Accountmanager accountmanager;

        public UserDetailViewModel(Accountmanager accountmanager)
        {
            this.accountmanager = accountmanager;
        }
    }
}
