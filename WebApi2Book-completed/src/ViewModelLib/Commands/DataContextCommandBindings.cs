using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelLib.Commands
{
    public abstract class RoutedCommandBinding : CommandBinding
    {
        public bool ViewHandledEvents { get; set; }

        public RoutedCommandBinding() { }

        public RoutedCommandBinding(ICommand command) { }

        protected internal abstract void OnPreviewCanExecute(
            object sender, CanExecuteRoutedEventArgs e);

        protected internal abstract void OnCanExecute(
            object sender, CanExecuteRoutedEventArgs e);

        protected internal abstract void OnPreviewExecuted(
            object sender, ExecutedRoutedEventArgs e);

        protected internal abstract void OnExecuted(
            object sender, ExecutedRoutedEventArgs e);
    }

    public class DataContextCommandBinding : RoutedCommandBinding
    {
        public new string CanExecute { get; set; }

        public new string Executed { get; set; }

        public new string PreviewCanExecute { get; set; }

        public new string PreviewExecuted { get; set; }

        public DataContextCommandBinding() { }

        public DataContextCommandBinding(ICommand command) { }

        protected internal override void OnPreviewCanExecute(
            object sender, CanExecuteRoutedEventArgs e) { }

        protected internal override void OnCanExecute(
            object sender, CanExecuteRoutedEventArgs e) { }

        protected internal override void OnPreviewExecuted(
            object sender, ExecutedRoutedEventArgs e) { }

        protected internal override void OnExecuted(
            object sender, ExecutedRoutedEventArgs e) { }
    }
}
