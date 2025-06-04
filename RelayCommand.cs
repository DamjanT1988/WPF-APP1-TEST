using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    // RelayCommand implementerar ICommand och används för att binda UI-knappar till metoder i ViewModel
    public class RelayCommand : ICommand
    {
        // Fält för att lagra metoden som ska köras när kommandot exekveras
        private readonly Action _execute;

        // Fält för att lagra en metod som avgör om kommandot är tillgängligt (kan köras eller ej)
        private readonly Func<bool> _canExecute;

        // Konstruktor som tar emot metoden att exekvera och valfritt en metod som bestämmer om exekvering är tillåten
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;         // tilldelar vad som ska köras
            _canExecute = canExecute;   // tilldelar logik för om kommandot får köras (kan vara null)
        }

        // Avgör om kommandot får exekveras just nu – returnerar true om _canExecute är null
        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        // Kör kommandot – anropar _execute-metoden som skickades in till konstruktorn
        public void Execute(object parameter) => _execute();

        // Den här händelsen används av WPF:s CommandManager för att veta när UI:t ska uppdatera knappens tillstånd (aktiv/inaktiv)
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;    // kopplar på observer
            remove => CommandManager.RequerySuggested -= value; // kopplar bort observer
        }
    }
}
