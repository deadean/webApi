using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModelLib
{
    public class CBehaviourType
    {
        public RoutedEventArgs MouseEventArguments { get; set; }
        public Panel PanelTarget { get; set; }
        public object Sender { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public object CommandParameter { get; set; }
    }
}
