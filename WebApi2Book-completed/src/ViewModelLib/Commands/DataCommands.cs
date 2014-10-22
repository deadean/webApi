using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelLib.Commands
{
    public class DataCommands
    {
        private static RoutedUICommand requery;
        private static RoutedUICommand applicationUndo;
        
        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();

            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));

            requery = new RoutedUICommand("Requery", "Requery", typeof(DataCommands), inputs);
            applicationUndo = new RoutedUICommand("ApplicationUndo", "Application Undo", typeof(DataCommands));
        }

        public static RoutedUICommand Requery
        {
            get { return requery; }
        }

        public static RoutedUICommand ApplicationUndo
        {
            get { return applicationUndo; }
        }
    }
}
