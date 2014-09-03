﻿//-------------------------------------------------------------------
// <summary>http://marlongrech.wordpress.com/2008/12/04/attachedcommandbehavior-aka-acb/</summary>
//-------------------------------------------------------------------

namespace ViewModelLib.Commands
{
	using System;
	using System.Windows;
	using System.Windows.Input;

	/// <summary>
	///   Defines a Command Binding
	///   This inherits from freezable so that it gets inheritance context for DataBinding to work
	/// </summary>
	public class BehaviorBinding : Freezable
	{
		private CommandBehaviorBinding mvBehavior;
		private DependencyObject mvOwner;

		/// <summary>
		///   Stores the Command Behavior Binding
		/// </summary>
		internal CommandBehaviorBinding Behavior
		{
			get { return mvBehavior ?? (mvBehavior = new CommandBehaviorBinding()); }
		}

		/// <summary>
		///   Gets or sets the Owner of the binding
		/// </summary>
		public DependencyObject Owner
		{
			get { return mvOwner; }
			set
			{
				mvOwner = value;

				ResetEventBinding();
			}
		}

		#region Command

		/// <summary>
		///   Command Dependency Property
		/// </summary>
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(BehaviorBinding), new FrameworkPropertyMetadata(null, OnCommandChanged));

		/// <summary>
		///     Gets or sets the Command property
		/// </summary>
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		/// <summary>
		///   Handles changes to the Command property.
		/// </summary>
		private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BehaviorBinding)d).OnCommandChanged(e);
		}

		/// <summary>
		///     Provides derived classes an opportunity to handle changes to the Command property.
		/// </summary>
		protected virtual void OnCommandChanged(DependencyPropertyChangedEventArgs e)
		{
			Behavior.Command = Command;
		}

		#endregion

		#region Action

		/// <summary>
		///   Action Dependency Property
		/// </summary>
		public static readonly DependencyProperty ActionProperty = DependencyProperty.Register("Action", typeof(Action<object>), typeof(BehaviorBinding), new FrameworkPropertyMetadata(null, OnActionChanged));

		/// <summary>
		///   Gets or sets the Action property.
		/// </summary>
		public Action<object> Action
		{
			get { return (Action<object>)GetValue(ActionProperty); }
			set { SetValue(ActionProperty, value); }
		}

		/// <summary>
		///   Handles changes to the Action property.
		/// </summary>
		private static void OnActionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((BehaviorBinding)dependencyObject).OnActionChanged(args);
		}

		/// <summary>
		///   Provides derived classes an opportunity to handle changes to the Action property.
		/// </summary>
		protected virtual void OnActionChanged(DependencyPropertyChangedEventArgs args)
		{
			Behavior.Action = Action;
		}

		#endregion

		#region CommandParameter

		/// <summary>
		///   CommandParameter Dependency Property
		/// </summary>
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(BehaviorBinding), new FrameworkPropertyMetadata(null, OnCommandParameterChanged));

		/// <summary>
		///   Gets or sets the CommandParameter property.
		/// </summary>
		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		/// <summary>
		///   Handles changes to the CommandParameter property.
		/// </summary>
		private static void OnCommandParameterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((BehaviorBinding)dependencyObject).OnCommandParameterChanged(args);
		}

		/// <summary>
		///   Provides derived classes an opportunity to handle changes to the CommandParameter property.
		/// </summary>
		protected virtual void OnCommandParameterChanged(DependencyPropertyChangedEventArgs args)
		{
			Behavior.CommandParameter = CommandParameter;
		}

		#endregion

		#region Event

		/// <summary>
		///   Event Dependency Property
		/// </summary>
		public static readonly DependencyProperty EventProperty = DependencyProperty.Register("Event", typeof(string), typeof(BehaviorBinding), new FrameworkPropertyMetadata(null, OnEventChanged));

		/// <summary>
		///   Gets or sets the Event property.
		/// </summary>
		public string Event
		{
			get { return (string)GetValue(EventProperty); }
			set { SetValue(EventProperty, value); }
		}

		/// <summary>
		///   Handles changes to the Event property.
		/// </summary>
		private static void OnEventChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((BehaviorBinding)dependencyObject).OnEventChanged(args);
		}

		/// <summary>
		///   Provides derived classes an opportunity to handle changes to the Event property.
		/// </summary>
		protected virtual void OnEventChanged(DependencyPropertyChangedEventArgs args)
		{
			ResetEventBinding();
		}

		#endregion

		private void ResetEventBinding()
		{
			if (Owner == null)
				return;

			//check if the Event is set. If yes we need to rebind the Command to the new event and unregister the old one
			if (Behavior.Event != null && Behavior.Owner != null)
			{
				Behavior.Dispose();
			}

			//bind the new event to the command
			Behavior.BindEvent(Owner, Event);
		}

		/// <summary>
		///   This is not actually used. This is just a trick so that this object gets WPF Inheritance Context
		/// </summary>
		/// <returns></returns>
		protected override Freezable CreateInstanceCore()
		{
			throw new NotImplementedException();
		}
	}
}