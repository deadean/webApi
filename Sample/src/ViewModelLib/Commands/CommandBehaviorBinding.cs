//-------------------------------------------------------------------
// <summary>http://marlongrech.wordpress.com/2008/12/04/attachedcommandbehavior-aka-acb/</summary>
//-------------------------------------------------------------------

namespace ViewModelLib.Commands
{
	using System;
	using System.Reflection;
	using System.Windows;
	using System.Windows.Input;

	/// <summary>
	///   Defines the command behavior binding
	/// </summary>
	public class CommandBehaviorBinding : IDisposable
	{
		#region Fields

		//stores the strategy of how to execute the event handler
		private Action<object> mvAction;
		private ICommand mvCommand;
		private IExecutionStrategy mvStrategy;

		#endregion // Fields

		#region Properties

		/// <summary>
		///   Get the owner of the CommandBinding ex: a Button
		///   This property can only be set from the BindEvent Method
		/// </summary>
		public DependencyObject Owner { get; private set; }

		/// <summary>
		///   The event name to hook up to
		///   This property can only be set from the BindEvent Method
		/// </summary>
		public string EventName { get; private set; }

		/// <summary>
		///   The event info of the event
		/// </summary>
		public EventInfo Event { get; private set; }

		/// <summary>
		///   Gets the EventHandler for the binding with the event
		/// </summary>
		public Delegate EventHandler { get; private set; }

		/// <summary>
		///   
		/// </summary>
		public bool IsDisposed { get; private set; }

		/// <summary>
		///   Gets or sets a CommandParameter
		/// </summary>
		public object CommandParameter { get; set; }

		/// <summary>
		///   The command to execute when the specified event is raised
		/// </summary>
		public ICommand Command
		{
			get { return mvCommand; }
			set
			{
				mvCommand = value;
				mvStrategy = new CommandExecutionStrategy { Behavior = this };
			}
		}

		/// <summary>
		///   Gets or sets the MvAction
		/// </summary>
		public Action<object> Action
		{
			get { return mvAction; }
			set
			{
				mvAction = value;
				mvStrategy = new ActionExecutionStrategy { Behavior = this };
			}
		}

		#endregion // Properties

		#region Methods

		//Creates an EventHandler on runtime and registers that handler to the Event specified
		public void BindEvent(DependencyObject owner, string eventName)
		{
			EventName = eventName;
			Owner = owner;
			Event = Owner.GetType().GetEvent(EventName, BindingFlags.Public | BindingFlags.Instance);

			if (Event == null)
				throw new InvalidOperationException(String.Format("Could not resolve event name {0}", EventName));

			//Create an event handler for the event that will call the ExecuteCommand method
			EventHandler = Delegate.CreateDelegate(Event.EventHandlerType, this, GetType().GetMethod("OnEventRaised", BindingFlags.NonPublic | BindingFlags.Instance));

			//Register the handler to the Event
			Event.AddEventHandler(Owner, EventHandler);
		}

		/// <summary>
		///   Unregisters the EventHandler from the Event
		/// </summary>
		public void Dispose()
		{
			if (IsDisposed)
				return;

			Event.RemoveEventHandler(Owner, EventHandler);

			IsDisposed = true;
		}

		/// <summary>
		///   Runs the ICommand when the requested RoutedEvent fires
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnEventRaised(object sender, EventArgs e)
		{
			if (Command == null)
				return;

			if (CommandParameter == null)
			{
				Command.Execute(e);
			}
			else
			{
				Command.Execute(CommandParameter);
			}
		}

		#endregion // Methods
	}
}