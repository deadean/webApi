using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLib.Behaviours
{
    public static class ShapesBehaviour
    {
        //#region ScrollBarValueChanged

        //public static readonly DependencyProperty OnValueChangeCommand = DependencyProperty.RegisterAttached
        //    ("ValueChanged", typeof(ICommand), typeof(ControlBehaviour), new FrameworkPropertyMetadata(null, OnValueChanged));

        //public static void SetValueChange(ScrollBar content, ICommand comm)
        //{
        //    content.SetValue(OnValueChangeCommand, comm);
        //}

        //public static ICommand GetValueChange(ScrollBar content)
        //{
        //    return content.GetValue(OnValueChangeCommand) as ICommand;
        //}

        //private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        //{
        //    try
        //    {
        //        var content = dependencyObject as ScrollBar;

        //        if (content == null)
        //            return;

        //        var command = args.NewValue as ICommand;

        //        content.ValueChanged -= OnValue;

        //        if (command == null)
        //            return;

        //        content.ValueChanged += OnValue;
        //    }
        //    catch { }
        //}

        //private static void OnValue(object sender, RoutedEventArgs e)
        //{
        //    var content = sender as ScrollBar;

        //    if (content == null)
        //        return;

        //    var command = GetValueChange(content);

        //    if (command == null)
        //        return;

        //    if (command.CanExecute(content.Value))
        //    {
        //        command.Execute(content.Value);
        //    }
        //}

        //#endregion
    }
}
