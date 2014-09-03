//-------------------------------------------------------------------
// <copyright file="ControlBehaviour.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <author>Smyk Aleksandr</author>
//-------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ViewModelLib.Behaviours
{
	/// <summary>
	/// Class represents logic of Text Behaviour 
	/// </summary>
	public static class ControlBehaviour
	{

        public static object Parameter { get; set; }
		#region Dependency Property

		public static readonly DependencyProperty OnMouseLeaveCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(
												Control.MouseLeaveEvent,
                                                "OnMouseLeaveCommand",
												typeof(ControlBehaviour));

        public static readonly DependencyProperty OnMouseEnterCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(
                                                Control.MouseEnterEvent,
                                                "OnMouseEnterCommand",
                                                typeof(ControlBehaviour));
        #region MouseEnter

        public static readonly DependencyProperty OnMouseEnterCommandButton = DependencyProperty.RegisterAttached
            ("MouseEnter", typeof(ICommand), typeof(ControlBehaviour), new FrameworkPropertyMetadata(null, OnMouseEnterChanged));

        public static void SetMouseEnter(DependencyObject content, ICommand comm)
        {
            content.SetValue(OnMouseEnterCommandButton, comm);
        }

        public static ICommand GetMouseEnter(DependencyObject content)
        {
            return content.GetValue(OnMouseEnterCommandButton) as ICommand;
        }

        private static void OnMouseEnterChanged(object dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var content = dependencyObject as UIElement;

                if (content == null)
                    return;

                var mouseEnterCommand = args.NewValue as ICommand;

                content.MouseEnter -= OnMouseEnter;

                if (mouseEnterCommand == null)
                    return;

                content.MouseEnter += OnMouseEnter;
            }
            catch { }
        }

        private static void OnMouseEnter(object sender, MouseEventArgs e)
        {
            var content = sender as UIElement;

            if (content == null)
                return;

            var mouseEnterCommand = GetMouseEnter(content);

            if (mouseEnterCommand == null)
                return;

            var contentControl = content as ContentControl;
            if (contentControl != null)
            {
                if (mouseEnterCommand.CanExecute(contentControl.Content))
                {
                    mouseEnterCommand.Execute(contentControl.Content);
                }
            }
            else
            {
                if (mouseEnterCommand.CanExecute(content))
                {
                    mouseEnterCommand.Execute(content);
                }
            }

        }

        #endregion

        #region MouseLeftButtonDown

        public static readonly DependencyProperty OnMouseLeftButtonDownCommand = DependencyProperty.RegisterAttached
            ("MouseLeftButtonDown", typeof(ICommand), typeof(ControlBehaviour), new FrameworkPropertyMetadata(null, OnMouseLeftButtonDownChanged));

        public static void SetMouseLeftButtonDown(DependencyObject content, ICommand comm)
        {
            content.SetValue(OnMouseLeftButtonDownCommand, comm);
        }

        public static ICommand GetMouseLeftButtonDown(DependencyObject content)
        {
            return content.GetValue(OnMouseLeftButtonDownCommand) as ICommand;
        }

        private static void OnMouseLeftButtonDownChanged(object dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var content = dependencyObject as UIElement;

                if (content == null)
                    return;

                var mouseEnterCommand = args.NewValue as ICommand;

                content.MouseLeftButtonDown -= OnMouseLeftButtonDown;

                if (mouseEnterCommand == null)
                    return;

                content.MouseLeftButtonDown += OnMouseLeftButtonDown;
            }
            catch { }
        }

        private static void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var content = sender as UIElement;

            if (content == null)
                return;

            var mouseEnterCommand = GetMouseLeftButtonDown(content);

            if (mouseEnterCommand == null)
                return;

            var contentControl = content as ContentControl;
            if (contentControl != null)
            {
                if (mouseEnterCommand.CanExecute(contentControl.Content))
                {
                    mouseEnterCommand.Execute(contentControl.Content);
                }
            }
            else
            {
                if (mouseEnterCommand.CanExecute(e))
                {
                    mouseEnterCommand.Execute(new CBehaviourType() { MouseEventArguments = e,PanelTarget = (content as Panel)});
                }
            }

        }

        #endregion

        #region MouseRightButtonDown

        public static readonly DependencyProperty OnMouseRightButtonDownCommand = DependencyProperty.RegisterAttached
            ("MouseRightButtonDown", typeof(ICommand), typeof(ControlBehaviour), new FrameworkPropertyMetadata(null, OnMouseRightButtonDownChanged));

        public static void SetMouseRightButtonDown(DependencyObject content, ICommand comm)
        {
            content.SetValue(OnMouseRightButtonDownCommand, comm);
        }

        public static ICommand GetMouseRightButtonDown(DependencyObject content)
        {
            return content.GetValue(OnMouseRightButtonDownCommand) as ICommand;
        }

        private static void OnMouseRightButtonDownChanged(object dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var content = dependencyObject as UIElement;

                if (content == null)
                    return;

                var mouseEnterCommand = args.NewValue as ICommand;

                content.MouseRightButtonDown -= OnMouseRightButtonDown;

                if (mouseEnterCommand == null)
                    return;

                content.MouseRightButtonDown += OnMouseRightButtonDown;
            }
            catch { }
        }

        private static void OnMouseRightButtonDown(object sender, MouseEventArgs e)
        {
            var content = sender as UIElement;

            if (content == null)
                return;

            var mouseEnterCommand = GetMouseRightButtonDown(content);

            if (mouseEnterCommand == null)
                return;

            var contentControl = content as ContentControl;
            if (contentControl != null)
            {
                if (mouseEnterCommand.CanExecute(contentControl.Content))
                {
                    mouseEnterCommand.Execute(contentControl.Content);
                }
            }
            else
            {
                if (mouseEnterCommand.CanExecute(e))
                {
                    mouseEnterCommand.Execute(new CBehaviourType() { MouseEventArguments = e, PanelTarget = (content as Panel) , Sender = sender });
                }
            }
        }

        #endregion

        #region TextChanged

        public static readonly DependencyProperty OnTextChangedProperty = DependencyProperty.RegisterAttached
            ("TextChanged", typeof(ICommand), typeof(ControlBehaviour), new PropertyMetadata(default(ICommand), OnTextChanged));

        public static void SetTextChanged(TextBox content, ICommand comm)
        {
            content.SetValue(OnTextChangedProperty, comm);
        }

        public static ICommand GetTextChanged(TextBox content)
        {
            return content.GetValue(OnTextChangedProperty) as ICommand;
        }

        private static void OnTextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var content = dependencyObject as TextBox;

                if (content == null)
                    return;

                var command = args.NewValue as ICommand;

                content.TextChanged -= OnText;

                if (command == null)
                    return;

                content.TextChanged += OnText;
            }
            catch { }
        }

        private static void OnText(object sender, TextChangedEventArgs e)
        {
            var content = sender as TextBox;

            if (content == null)
                return;

            var mouseEnterCommand = GetTextChanged(content);

            if (mouseEnterCommand == null)
                return;

            if (mouseEnterCommand.CanExecute(content))
            {
                mouseEnterCommand.Execute(content);
            }
        }

        #endregion

        #region TextCursor

        public static readonly DependencyProperty OnTextChangedAndSetToEndCursorProperty = DependencyProperty.RegisterAttached
            ("TextChangedAndSetToEndCursor", typeof(string), typeof(ControlBehaviour), new PropertyMetadata(default(string), OnTextChangedAndSetToEndCursor));

        public static void SetTextChangedAndSetToEndCursor(TextBox content, string comm)
        {
            content.SetValue(OnTextChangedAndSetToEndCursorProperty, comm);
        }

        public static string GetTextChangedAndSetToEndCursor(TextBox content)
        {
            return content.GetValue(OnTextChangedAndSetToEndCursorProperty) as string;
        }

        private static void OnTextChangedAndSetToEndCursor(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var content = dependencyObject as TextBox;

                if (content == null)
                    return;

                var command = args.NewValue as string;

                content.TextChanged -= OnTextAndSetToEndCursor;

                if (command == null)
                    return;

                content.TextChanged += OnTextAndSetToEndCursor;
            }
            catch { }
        }

        private static void OnTextAndSetToEndCursor(object sender, TextChangedEventArgs e)
        {
            var content = sender as TextBox;

            if (content == null)
                return;

            var mouseEnterCommand = GetTextChangedAndSetToEndCursor(content);

            if (mouseEnterCommand == null)
                return;

            content.CaretIndex = GetIsMustUseSetCaretIndexToEnd(content) ? mouseEnterCommand.Length : content.CaretIndex;
        }

        #endregion

        #region IsMustUseSetCaretIndexToEnd

        public static readonly DependencyProperty OnIsMustUseSetCaretIndexToEndProperty = DependencyProperty.RegisterAttached
            ("IsMustUseSetCaretIndexToEnd", typeof(bool), typeof(ControlBehaviour), new PropertyMetadata(default(bool), null));

        public static void SetIsMustUseSetCaretIndexToEnd(TextBox content, bool comm)
        {
            content.SetValue(OnIsMustUseSetCaretIndexToEndProperty, comm);
        }

        public static bool GetIsMustUseSetCaretIndexToEnd(TextBox content)
        {
            return (bool)content.GetValue(OnIsMustUseSetCaretIndexToEndProperty);
        }

        /*private static void OnIsMustUseSetCaretIndexToEnd(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var content = dependencyObject as TextBox;

                if (content == null)
                    return;

                var command = args.NewValue as string;

                content.TextChanged -= OnIsMustUseSetCaretIndexToEnd;

                if (command == null)
                    return;

                content.TextChanged += OnTextAndSetToEndCursor;
            }
            catch { }
        }

        private static void OnIsMustUseSetCaretIndexToEnd(object sender, TextChangedEventArgs e)
        {
            var content = sender as TextBox;

            if (content == null)
                return;

            var mouseEnterCommand = GetIsMustUseSetCaretIndexToEnd(content);

            if (mouseEnterCommand == null)
                return;

            content.CaretIndex = mouseEnterCommand.Length;
        }*/

        #endregion

        #region Click Event

        public static readonly DependencyProperty OnClickCommand = DependencyProperty.RegisterAttached
            ("Click", typeof(ICommand), typeof(ControlBehaviour), new FrameworkPropertyMetadata(null, OnClickChanged));

        public static void SetClick(ButtonBase content, ICommand comm)
        {
            content.SetValue(OnClickCommand, comm);
        }

        public static ICommand GetClick(ButtonBase content)
        {
            return content.GetValue(OnClickCommand) as ICommand;
        }

        private static void OnClickChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var content = dependencyObject as ButtonBase;

                if (content == null)
                    return;

                var command = args.NewValue as ICommand;

                content.Click -= OnClick;

                if (command == null)
                    return;

                content.Click += OnClick;
            }
            catch { }
        }

        private static void OnClick(object sender, RoutedEventArgs e)
        {
            var content = sender as ButtonBase;

            if (content == null)
                return;

            var command = GetClick(content);

            if (command == null)
                return;

            if (content is RadioButton)
            {
                if (command.CanExecute(content.Tag))
                {
                    command.Execute(content.Tag);
                }
                return;
            }
            if (command.CanExecute(content.Content))
            {
                command.Execute(new CBehaviourType(){MouseEventArguments = e, Sender = sender, Content = content.Content.ToString(), CommandParameter = content.CommandParameter });
            }
        }

        #endregion

        #region ScrollBarValueChanged

        public static readonly DependencyProperty OnValueChangeCommand = DependencyProperty.RegisterAttached
            ("ValueChanged", typeof(ICommand), typeof(ControlBehaviour), new FrameworkPropertyMetadata(null, OnValueChanged));

        public static void SetValueChange(ScrollBar content, ICommand comm)
        {
            content.SetValue(OnValueChangeCommand, comm);
        }

        public static ICommand GetValueChange(ScrollBar content)
        {
            return content.GetValue(OnValueChangeCommand) as ICommand;
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var content = dependencyObject as ScrollBar;

                if (content == null)
                    return;

                var command = args.NewValue as ICommand;

                content.ValueChanged -= OnValue;

                if (command == null)
                    return;

                content.ValueChanged += OnValue;
            }
            catch { }
        }

        private static void OnValue(object sender, RoutedEventArgs e)
        {
            var content = sender as ScrollBar;

            if (content == null)
                return;

            var command = GetValueChange(content);

            if (command == null)
                return;

            if (command.CanExecute(content.Value))
            {
                command.Execute(content.Value);
            }
        }

        #endregion

        

        #region Public Services

        /// <summary>
		/// Sets the configuration click command.
		/// </summary>
		/// <param name="o">The automatic.</param>
		/// <param name="value">The value.</param>
        public static void SetOnMouseLeaveCommand(DependencyObject o, ICommand value)
		{
            o.SetValue(OnMouseLeaveCommand, value);
		}

		/// <summary>
		/// Gets the configuration click command.
		/// </summary>
		/// <param name="o">The automatic.</param>
		/// <returns></returns>
        public static ICommand GetOnMouseLeaveCommand(DependencyObject o)
		{
            return o.GetValue(OnMouseLeaveCommand) as ICommand;
		}

        /// <summary>
        /// Sets the configuration click command.
        /// </summary>
        /// <param name="o">The automatic.</param>
        /// <param name="value">The value.</param>
        public static void SetOnMouseEnterCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(OnMouseEnterCommand, value);
        }

        /// <summary>
        /// Gets the configuration click command.
        /// </summary>
        /// <param name="o">The automatic.</param>
        /// <returns></returns>
        public static ICommand GetOnMouseEnterCommand(DependencyObject o)
        {
            return o.GetValue(OnMouseEnterCommand) as ICommand;
        }

		#endregion

        #endregion

    }
}
