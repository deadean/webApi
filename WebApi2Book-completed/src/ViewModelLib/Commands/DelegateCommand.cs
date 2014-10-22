namespace ViewModelLib.Commands
{
	using System;
	using System.Windows.Input;
	using System.Windows;
	using System.Windows.Threading;

	/// <summary>
	/// An <see cref="ICommand"/> whose delegates can be attached for <see cref="Execute"/> and <see cref="CanExecute"/>.
	/// It also implements the <see cref="IActiveAware"/> interface, which is
	/// useful when registering this command in a <see cref="CompositeCommand"/>
	/// that monitors command's activity.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public class DelegateCommand<T> : ICommand
	{
		private readonly Action<T> executeMethod = null;
		private readonly Func<T, bool> canExecuteMethod = null;
		//private bool _isActive;

		/// <summary>
		/// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
		/// </summary>
		/// <param name="executeMethod">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
		/// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
		public DelegateCommand(Action<T> executeMethod)
			: this(executeMethod, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
		/// </summary>
		/// <param name="executeMethod">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
		/// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command.  This can be null.</param>
		/// <exception cref="ArgumentNullException">When both <paramref name="executeMethod"/> and <paramref name="canExecuteMethod"/> ar <see langword="null" />.</exception>
		public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
		{
			if (executeMethod == null && canExecuteMethod == null)
				throw new ArgumentNullException("executeMethod", "Delegate Command Delegates Cannot Be Null");

			this.executeMethod = executeMethod;
			this.canExecuteMethod = canExecuteMethod;
		}

		///<summary>
		///Defines the method that determines whether the command can execute in its current state.
		///</summary>
		///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
		///<returns>
		///<see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
		///</returns>
		public bool CanExecute(T parameter)
		{
			if (canExecuteMethod == null) return true;
			return canExecuteMethod(parameter);
		}

		///<summary>
		///Defines the method to be called when the command is invoked.
		///</summary>
		///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
		public void Execute(T parameter)
		{
			if (executeMethod == null) return;
			executeMethod(parameter);
		}

		///<summary>
		///Defines the method that determines whether the command can execute in its current state.
		///</summary>
		///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		///<returns>
		///true if this command can be executed; otherwise, false.
		///</returns>
		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute((T)parameter);
		}

		///<summary>
		///Occurs when changes occur that affect whether or not the command should execute.
		///</summary>
		public event EventHandler CanExecuteChanged;

		///<summary>
		///Defines the method to be called when the command is invoked.
		///</summary>
		///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		void ICommand.Execute(object parameter)
		{
			Execute((T)parameter);
		}

		/// <summary>
		/// Raises <see cref="CanExecuteChanged"/> on the UI thread so every command invoker can requery to check if the command can execute.
		/// </summary>
		protected virtual void OnCanExecuteChanged()
		{
			Dispatcher dispatcher = null;
			if (Application.Current != null)
			{
				dispatcher = Application.Current.Dispatcher;
			}

			EventHandler canExecuteChangedHandler = CanExecuteChanged;
			if (canExecuteChangedHandler != null)
			{
				if (dispatcher != null && !dispatcher.CheckAccess())
				{
					dispatcher.BeginInvoke(DispatcherPriority.Normal,
																 (Action)OnCanExecuteChanged);
				}
				else
				{
					canExecuteChangedHandler(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Raises <see cref="CanExecuteChanged"/> on the UI thread so every command invoker
		/// can requery to check if the command can execute.
		/// <remarks>Note that this will trigger the execution of <see cref="CanExecute"/> once for each invoker.</remarks>
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
		public void RaiseCanExecuteChanged()
		{
			OnCanExecuteChanged();
		}
	}

	/// <summary>
	/// An <see cref="ICommand"/> whose delegates can be attached for <see cref="Execute"/> and <see cref="CanExecute"/>.
	/// It also implements the <see cref="IActiveAware"/> interface, which is
	/// useful when registering this command in a <see cref="CompositeCommand"/>
	/// that monitors command's activity.
	/// </summary>
	public class DelegateCommand : ICommand
	{
		private readonly Action executeMethod = null;
		private readonly Func<bool> canExecuteMethod = null;
		//private bool _isActive;

		/// <summary>
		/// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
		/// </summary>
		/// <param name="executeMethod">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
		/// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
		public DelegateCommand(Action executeMethod)
			: this(executeMethod, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
		/// </summary>
		/// <param name="executeMethod">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
		/// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command.  This can be null.</param>
		/// <exception cref="ArgumentNullException">When both <paramref name="executeMethod"/> and <paramref name="canExecuteMethod"/> ar <see langword="null" />.</exception>
		public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
		{
			if (executeMethod == null && canExecuteMethod == null)
				throw new ArgumentNullException("executeMethod", "Delegate Command Delegates Cannot Be Null");

			this.executeMethod = executeMethod;
			this.canExecuteMethod = canExecuteMethod;
		}

		///<summary>
		///Defines the method that determines whether the command can execute in its current state.
		///</summary>
		///<returns>
		///<see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
		///</returns>
		public bool CanExecute()
		{
			if (canExecuteMethod == null) return true;
			return canExecuteMethod();
		}

		///<summary>
		///Defines the method to be called when the command is invoked.
		///</summary>
		public void Execute()
		{
			if (executeMethod == null) return;
			executeMethod();
		}

		///<summary>
		///Defines the method that determines whether the command can execute in its current state.
		///</summary>
		///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		///<returns>
		///true if this command can be executed; otherwise, false.
		///</returns>
		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute();
		}

		///<summary>
		///Occurs when changes occur that affect whether or not the command should execute.
		///</summary>
		public event EventHandler CanExecuteChanged;

		///<summary>
		///Defines the method to be called when the command is invoked.
		///</summary>
		///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		void ICommand.Execute(object parameter)
		{
			Execute();
		}

		/// <summary>
		/// Raises <see cref="CanExecuteChanged"/> on the UI thread so every command invoker can requery to check if the command can execute.
		/// </summary>
		protected virtual void OnCanExecuteChanged()
		{
			Dispatcher dispatcher = null;
			if (Application.Current != null)
			{
				dispatcher = Application.Current.Dispatcher;
			}

			EventHandler canExecuteChangedHandler = CanExecuteChanged;
			if (canExecuteChangedHandler != null)
			{
				if (dispatcher != null && !dispatcher.CheckAccess())
				{
					dispatcher.BeginInvoke(DispatcherPriority.Normal,
																 (Action)OnCanExecuteChanged);
				}
				else
				{
					canExecuteChangedHandler(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Raises <see cref="CanExecuteChanged"/> on the UI thread so every command invoker
		/// can requery to check if the command can execute.
		/// <remarks>Note that this will trigger the execution of <see cref="CanExecute"/> once for each invoker.</remarks>
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
		public void RaiseCanExecuteChanged()
		{
			OnCanExecuteChanged();
		}
	}
}
