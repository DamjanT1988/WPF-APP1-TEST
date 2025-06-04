using System.Collections.ObjectModel;     // Innehåller ObservableCollection<T>, en lista som uppdaterar UI automatiskt vid ändringar.
using System.ComponentModel;              // Innehåller gränssnittet INotifyPropertyChanged för att notifiera UI vid ändringar av property-värden.
using System.Runtime.CompilerServices;    // Innehåller attributet CallerMemberName, som låter oss automatiskt skicka in property-namn till OnPropertyChanged.
using System.Windows.Input;               // Innehåller ICommand, vilket används för att knyta kommandon i ViewModel till knappar i UI.

namespace WpfApp1
{
    // MainViewModel ansvarar för att hantera datalogik och uppdatera View via datakoppling (Binding).
    public class MainViewModel : INotifyPropertyChanged
    {
        // ObservableCollection är en lista som automatiskt uppdaterar UI om listans innehåll ändras (t.ex. när en uppgift läggs till).
        public ObservableCollection<TaskModel> Tasks { get; set; } = new();

        // Privat backing field för propertyn NewTaskTitle
        private string _newTaskTitle;

        // Denna property är bunden till TextBox i XAML, där användaren skriver in en uppgiftsrubrik.
        public string NewTaskTitle
        {
            get => _newTaskTitle;  // Returnerar nuvarande rubrik
            set
            {
                _newTaskTitle = value;      // Sätter nytt värde från UI
                OnPropertyChanged();        // Meddelar att NewTaskTitle har ändrats så att UI kan uppdateras
            }
        }

        // Detta kommando används för att köra AddTask-metoden när användaren klickar på "Add Task"-knappen.
        public ICommand AddTaskCommand { get; }

        // Konstruktor för ViewModelen
        public MainViewModel()
        {
            // Initialiserar kommandot och säger att det endast får vara aktivt när användaren har skrivit något (dvs. ej null eller whitespace).
            AddTaskCommand = new RelayCommand(AddTask, () => !string.IsNullOrWhiteSpace(NewTaskTitle));
        }

        // Metod som lägger till en ny uppgift till listan Tasks.
        private void AddTask()
        {
            // Skapar en ny uppgift med titeln användaren skrivit in och lägger till den i listan.
            Tasks.Add(new TaskModel { Title = NewTaskTitle });

            // Tömmer TextBoxen efter att uppgiften lagts till.
            NewTaskTitle = string.Empty;
        }

        // Händelse som View använder för att lyssna på förändringar i bindade properties.
        public event PropertyChangedEventHandler PropertyChanged;

        // Metod som triggar PropertyChanged-händelsen med korrekt property-namn, vilket talar om för UI att det ska uppdateras.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
