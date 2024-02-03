using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainApplication.Commands
{
    public class RoutedUICommands
    {
        public static readonly RoutedUICommand Start = new RoutedUICommand("Start", "Start", typeof(RoutedUICommands), new InputGestureCollection()
        {
            new KeyGesture(Key.None, ModifierKeys.None)
        });

        public static readonly RoutedUICommand Stop = new RoutedUICommand("Stop", "Stop", typeof(RoutedUICommands), new InputGestureCollection()
        {
            new KeyGesture(Key.None, ModifierKeys.None)
        });
    }
}
