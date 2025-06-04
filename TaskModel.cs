using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    // TaskModel representerar en enskild uppgift i applikationen.
    // Den används som datamodell i MVVM-mönstret, ofta bunden till gränssnittet via ViewModel.
    public class TaskModel
    {
        // Title är uppgiftens beskrivning (t.ex. "Tvätta", "Skicka rapport").
        // Denna egenskap bindas till en CheckBox-text i gränssnittet.
        public string Title { get; set; }

        // IsDone visar om uppgiften är avklarad eller inte.
        // Denna egenskap bindas till CheckBoxens kryssruta i gränssnittet.
        public bool IsDone { get; set; }
    }
}
