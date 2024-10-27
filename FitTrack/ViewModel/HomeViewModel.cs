using FitTrack.MVVM;
using MVVMgenomgang.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    class HomeViewModel: ViewModelBase
    {
        private readonly Accountmanager accountmanager;

        public string FirstName => accountmanager.CurrentUser.FirstName;

        public string LastName => accountmanager.CurrentUser.LastName;

        public HomeViewModel()
        {

        }

    }
}
