//-------------------------------------------------------------------
// <summary>http://marlongrech.wordpress.com/2008/12/04/attachedcommandbehavior-aka-acb/</summary>
//-------------------------------------------------------------------

namespace ViewModelLib.Commands
{
	using System;

	/// <summary>
	///   Defines the interface for a strategy of execution for the CommandBehaviorBinding
	/// </summary>
	public interface IExecutionStrategy
	{
		/// <summary>
		///   Gets or sets the Behavior that we execute this strategy
		/// </summary>
		CommandBehaviorBinding Behavior { get; }

		/// <summary>
		///   Executes according to the strategy type
		/// </summary>
		/// <param name="parameter">The parameter to be used in the execution</param>
		void Execute(object parameter);
	}

	public abstract class ExecutionStrategyBase : IExecutionStrategy
	{
		#region IExecutionStrategy Members

		/// <summary>
		///   Gets or sets the Behavior that we execute this strategy
		/// </summary>
		public CommandBehaviorBinding Behavior { get; set; }

		/// <summary>
		///   Executes according to the strategy type
		/// </summary>
		/// <param name="parameter">The parameter to be used in the execution</param>
		public abstract void Execute(object parameter);

		#endregion
	}

	/// <summary>
	///   Executes a command 
	/// </summary>
	public class CommandExecutionStrategy : ExecutionStrategyBase
	{
		/// <summary>
		///   Executes the Command that is stored in the CommandProperty of the CommandExecution
		/// </summary>
		/// <param name="parameter">The parameter for the command</param>
		public override void Execute(object parameter)
		{
			if (Behavior == null)
				throw new InvalidOperationException("Behavior property cannot be null when executing a strategy");

			if (Behavior.Command.CanExecute(Behavior.CommandParameter))
			{
				Behavior.Command.Execute(Behavior.CommandParameter);
			}
		}
	}

	/// <summary>
	///   Executes a delegate
	/// </summary>
	public class ActionExecutionStrategy : ExecutionStrategyBase
	{
		/// <summary>
		///   Executes an MvAction delegate
		/// </summary>
		/// <param name="parameter">The parameter to pass to the MvAction</param>
		public override void Execute(object parameter)
		{
			Behavior.Action(parameter);
		}
	}
}