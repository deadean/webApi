//-------------------------------------------------------------------
// <summary>http://marlongrech.wordpress.com/2008/12/04/attachedcommandbehavior-aka-acb/</summary>
//-------------------------------------------------------------------

namespace ViewModelLib.Commands
{
	using System;
	using System.Windows;
	using System.Windows.Input;

	/// <summary>
	///   Defines the attached properties to create a CommandBehaviorBinding
	/// </summary>
	public class CommandBehavior
	{
		#region Behavior

		private static readonly DependencyProperty BehaviorProperty = DependencyProperty.RegisterAttached("Behavior", typeof(CommandBehaviorBinding), typeof(CommandBehavior), new FrameworkPropertyMetadata((CommandBehaviorBinding)null));

		private static CommandBehaviorBinding GetBehavior(DependencyObject dependencyObject)
		{
			return (CommandBehaviorBinding)dependencyObject.GetValue(BehaviorProperty);
		}

		private static void SetBehavior(DependencyObject dependencyObject, CommandBehaviorBinding value)
		{
			dependencyObject.SetValue(BehaviorProperty, value);
		}

		#endregion // Behavior

		#region Command

		public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandBehavior), new FrameworkPropertyMetadata(null, OnCommandChanged));

		public static ICommand GetCommand(DependencyObject d)
		{
			return (ICommand)d.GetValue(CommandProperty);
		}

		public static void SetCommand(DependencyObject dependencyObject, ICommand value)
		{
			dependencyObject.SetValue(CommandProperty, value);
		}

		private static void OnCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var binding = FetchOrCreateBinding(dependencyObject);

			binding.Command = (ICommand)args.NewValue;
		}

		#endregion // Command

		#region Action

		public static readonly DependencyProperty ActionProperty = DependencyProperty.RegisterAttached("Action", typeof(Action<object>), typeof(CommandBehavior), new FrameworkPropertyMetadata(null, OnActionChanged));

		public static Action<object> GetAction(DependencyObject dependencyObject)
		{
			return (Action<object>)dependencyObject.GetValue(ActionProperty);
		}

		public static void SetAction(DependencyObject dependencyObject, Action<object> value)
		{
			dependencyObject.SetValue(ActionProperty, value);
		}

		private static void OnActionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var binding = FetchOrCreateBinding(dependencyObject);

			binding.Action = (Action<object>)args.NewValue;
		}

		#endregion // Action

		#region Command Parameter

		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(CommandBehavior), new FrameworkPropertyMetadata(null, OnCommandParameterChanged));

		public static object GetCommandParameter(DependencyObject dependencyObject)
		{
			return dependencyObject.GetValue(CommandParameterProperty);
		}

		public static void SetCommandParameter(DependencyObject dependencyObject, object value)
		{
			dependencyObject.SetValue(CommandParameterProperty, value);
		}

		private static void OnCommandParameterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var binding = FetchOrCreateBinding(dependencyObject);

			binding.CommandParameter = args.NewValue;
		}

		#endregion // Command Parameter

		#region Event

		public static readonly DependencyProperty EventProperty = DependencyProperty.RegisterAttached("Event", typeof(string), typeof(CommandBehavior), new FrameworkPropertyMetadata(string.Empty, OnEventChanged));

		public static string GetEvent(DependencyObject dependencyObject)
		{
			return (string)dependencyObject.GetValue(EventProperty);
		}

		public static void SetEvent(DependencyObject dependencyObject, string value)
		{
			dependencyObject.SetValue(EventProperty, value);
		}

		private static void OnEventChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var binding = FetchOrCreateBinding(dependencyObject);

			//check if the Event is set. If yes we need to rebind the Command to the new event and unregister the old one
			if (binding.Event != null && binding.Owner != null)
			{
				binding.Dispose();
			}

			//bind the new event to the command
			binding.BindEvent(dependencyObject, args.NewValue.ToString());
		}

		#endregion // Event

		#region Helpers

		/// <summary>
		///   Tries to get a CommandBehaviorBinding from the element. Creates a new instance if there is not one attached
		/// </summary>
		/// <param name="dependencyObject"></param>
		/// <returns></returns>
		private static CommandBehaviorBinding FetchOrCreateBinding(DependencyObject dependencyObject)
		{
			var result = GetBehavior(dependencyObject);

			if (result == null)
			{
				result = new CommandBehaviorBinding();
				SetBehavior(dependencyObject, result);
			}

			return result;
		}

		#endregion // Helpers
	}
}