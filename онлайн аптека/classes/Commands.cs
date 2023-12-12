using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace онлайн_аптека.classes
{
    internal class Commands  //Команды для toolbara
    {
        public static RoutedCommand Delete { get; set; } //Удаление
        public static RoutedCommand Edit { get; set; } //Редактироване

        static Commands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"));
            Edit = new RoutedCommand("Edit", typeof(Commands), inputs);
            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.D, ModifierKeys.Control, "Ctrl+D"));
            Delete = new RoutedCommand("Delete", typeof(Commands), inputs);

        }
    }
}
