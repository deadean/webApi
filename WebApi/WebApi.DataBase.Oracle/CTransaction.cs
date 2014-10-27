using System;
using System.Threading;

namespace WebApi.DataBase.Oracle
{
	public class CTransaction : IDisposable
	{
		#region Properties

		public string ID { get; set; }

		public bool IsOpenTransaction { get; set; }

		#endregion // Properties

		#region Public Methods

		public static bool Execute(Func<CTransaction, bool> actionToExecuteWithTransaction)
		{
			return Execute(actionToExecuteWithTransaction, _ => { }, false);
		}

		public static bool Execute(Func<CTransaction, bool> actionToExecuteWithTransaction, Action<CTransaction> onTransactionException, bool isNeedRethrow)
		{
			return Execute(actionToExecuteWithTransaction, () => { }, onTransactionException, isNeedRethrow);
		}

		public static bool Execute(Func<CTransaction, bool> actionToExecuteWithTransaction, Action onMaxTransactionsReached, Action<CTransaction> onDatabaseException, bool isNeedRethrow)
		{
			if (actionToExecuteWithTransaction == null || onMaxTransactionsReached == null || onDatabaseException == null)
				return false;

			var result = false;

			using (var transaction = new CTransaction())
			{
				try
				{
					if (transaction.OpenTransaction())
					{
						var isExecuted = actionToExecuteWithTransaction(transaction);

						if (transaction.IsOpenTransaction)
						{
							transaction.CloseTransaction(isExecuted);
						}

						result = isExecuted;
					}
					else
					{
						onMaxTransactionsReached();
					}
				}
				catch (CDataBaseException exc)
				{
					CLog.Log(LogType.ERR, string.Format("OnTransactionException => {0}", exc.Message));

					onDatabaseException(transaction);

					if (isNeedRethrow) throw;
				}
				catch (ThreadAbortException) { }
				catch (Exception exc)
				{
					CLog.Log("Execute(Func<CTransaction, bool> actionToExecuteWithTransaction, Action onMaxTransactionsReached, Action<CTransaction> onDatabaseException, bool isNeedRethrow)", exc);

					if (isNeedRethrow) throw;
				}
			}

			return result;
		}

		public void CloseTransaction(bool isSaveOk)
		{
			try
			{
				if (IsOpenTransaction)
				{
					if (CDataBase.IsTraceNeedOn)
					{
						CDataBase.SetDatabaseTraceAction(false, this);
					}

					if (isSaveOk)
					{
						CDataBase.CommitTransaction(ID);
					}
					else
					{
						CDataBase.RollbackTransaction(ID);
					}

					IsOpenTransaction = false;
				}
			}
			catch (ThreadAbortException) { }
			catch (CDataBaseException) { throw; }
			catch (Exception exc)
			{
				CLog.Log("CloseTransaction(bool isSaveOk)", exc);
			}
		}

		public void Commit()
		{
			CloseTransaction(true);
		}

		public bool OpenTransaction()
		{
			if (IsOpenTransaction)
				return true;

			ID = CDataBase.OpenTransaction2();

			if (string.IsNullOrEmpty(ID))
				return false;

			IsOpenTransaction = true;

			if (CDataBase.IsTraceNeedOn)
			{
				CDataBase.SetDatabaseTraceAction(true, this);
			}

			return IsOpenTransaction;
		}

		public void Rollback()
		{
			CloseTransaction(false);
		}

		#endregion // Public Methods

		#region IDisposable Members

		public void Dispose()
		{
			if (IsOpenTransaction)
			{
				Rollback();
			}
		}

		#endregion // IDisposable Members
	}
}