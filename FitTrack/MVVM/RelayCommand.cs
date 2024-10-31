using FitTrack.Model;
using FitTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitTrack.MVVM
{
    public class RelayCommand : ICommand
    {
        //Fält för att hålla referenser till metoder som definierar vad som ska göras (Execute)
        private Action<object> execute;

        //Kollar om kommandot kan köras
        private Func<object, bool> canExecute;
        private Action<Accountmanager, Workout> openWorkoutDetailsWindow;


        //Event som signalerar när kommandots möjlighet att köras har ändrats
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<Accountmanager, Workout> openWorkoutDetailsWindow)
        {
            this.openWorkoutDetailsWindow = openWorkoutDetailsWindow;
        }



        //Bestämmer om kommandot kan köras eller inte
        public bool CanExecute(object? parameter) => true;

        //Kör den logik som tilldelats via execute metoden
        public void Execute(object? parameter)
        {
            execute(parameter);
        }

        internal void RaiseCanExecuteChanged()
        {
            throw new NotImplementedException();
        }
    }
}
