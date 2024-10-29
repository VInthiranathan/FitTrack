using FitTrack.MVVM;
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
        public string FirstName => accountmanager.CurrentUser.FirstName;
        public string LastName => accountmanager.CurrentUser.LastName;
        public string UserType => accountmanager.CurrentUser.GetType().Name;

        public RelayCommand NavigateUserDetailsCommand { get; }
        public RelayCommand NavigateWorkoutCommand { get; }
        public RelayCommand NavigateAddWorkoutCommand { get; }
        public RelayCommand NavigateWorkoutDetailsCommand { get; }
        public RelayCommand SignOutCommand { get; }

        private readonly NavigationCommandManager navigationCommandManager;
        public AddWorkoutViewModel(Accountmanager accountmanager)
        {
            this.accountmanager = accountmanager;
            navigationCommandManager = new NavigationCommandManager(accountmanager);

            NavigateUserDetailsCommand = navigationCommandManager.NavigateUserDetailsCommand;
            NavigateWorkoutCommand = navigationCommandManager.NavigateWorkoutCommand;
            NavigateAddWorkoutCommand = navigationCommandManager.NavigateAddWorkoutCommand;
            NavigateWorkoutDetailsCommand = navigationCommandManager.NavigateWorkoutDetailsCommand;
            SignOutCommand = navigationCommandManager.SignOutCommand;

        }
    }
}
