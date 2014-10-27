using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.DataBase.Oracle
{
	public class CDBExceptionMessage
	{
		#region Private Member Variables
		#endregion

		#region Private Properties
		#endregion

		#region Private Methods
		private void ToStringRecursively(Exception dataBaseException, StringBuilder exceptionMessageList)
		{
			try
			{
				exceptionMessageList.Append(ToString(dataBaseException));
				if (dataBaseException.InnerException != null)
					ToStringRecursively(dataBaseException.InnerException, exceptionMessageList);
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.Print(exc.ToString());
			}
		}

		#endregion

		#region Constractors
		/// <summary>
		/// Static constructor for initializing fields.
		/// </summary>
		public CDBExceptionMessage()
		{
			TransactionID = "-1";
			DataBaseExceptionList = new List<Exception>();
		}

		public CDBExceptionMessage(CTransactionException exception)
		{
			if (exception == null)
				throw new Exception("Cannot retrieve db exception message from non exist transaction");

			TransactionID = exception.Transaction.ID;

			DataBaseExceptionList = new List<Exception> { exception };
		}

		public CDBExceptionMessage(CTransaction transaction, CDataBaseException exception)
		{
			if (transaction == null)
				TransactionID = "-1";
			else
				TransactionID = transaction.ID;

			DataBaseExceptionList = new List<Exception>();
			DataBaseExceptionList.Add(exception);
		}
		#endregion

		#region Public Properties
		public string TransactionID { get; internal set; }
		public List<Exception> DataBaseExceptionList { get; internal set; }
		public string ExceptionMessage
		{
			get
			{
				StringBuilder result = new StringBuilder();
				foreach (Exception exception in DataBaseExceptionList)
				{
					StringBuilder exceptionMessageList = new StringBuilder();
					ToStringRecursively(exception, exceptionMessageList);

					result.Append(exceptionMessageList.ToString());
				}

				return result.ToString();
			}
		}

		#endregion

		#region Public Methods
		public void Add(Exception exception)
		{
			if (DataBaseExceptionList.Contains(exception) == false)
				DataBaseExceptionList.Add(exception);
			else
				System.Diagnostics.Debug.Print("CDBExceptionMessage: Exception is exists.");
		}

		public string ToString(Exception dataBaseException)
		{
			string result = "{EXCMESSAGE}\n{STACK}\n";

			try
			{
				string excStackTrace = String.Empty;
				string excMessage = String.Empty;

				if (dataBaseException != null)
				{
					if (dataBaseException.InnerException != null)
					{
						excStackTrace = dataBaseException.InnerException.StackTrace;
						excMessage = dataBaseException.InnerException.Message;
					}
					excStackTrace = excStackTrace + excStackTrace == null ? Environment.NewLine : "" + dataBaseException.StackTrace;
					excStackTrace = "EXCEPTION STACK TRACE:" + excStackTrace == null ? Environment.NewLine : "" + excStackTrace + Environment.NewLine + "FULL STACK TRACE:" + Environment.NewLine + Environment.StackTrace;
					excMessage = excMessage + " - " + dataBaseException.Message;
				}

				result = result.Replace("{STACK}", excStackTrace);
				result = result.Replace("{EXCMESSAGE}", excMessage);
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.Print(exc.ToString());
			}

			return result;//.Replace("\n", "\n\t");
		}
		#endregion
	}

	public static class CDataBaseExceptionsList
	{
		#region Private Member Variables
		private static List<CDBExceptionMessage> modListErrors;
		#endregion

		#region Private Properties
		#endregion

		#region Private Methods
		#endregion

		#region Constractors
		static CDataBaseExceptionsList()
		{
			modListErrors = new List<CDBExceptionMessage>();
		}
		#endregion

		#region Public Properties
		#endregion

		#region Public Methods

		/// <summary>
		/// Check if list contain given transaction number
		/// </summary>
		/// <param name="transactionNumber"></param>
		/// <returns></returns>
		static public bool IsExists(CTransaction transaction)
		{
			bool result = modListErrors.Any(x => x.TransactionID == transaction.ID);

			return result;
		}

		/// <summary>
		/// Add to list new exception with corresponded transaction
		/// </summary>
		/// <param name="transactionNumber"></param>
		/// <param name="exception"></param>
		static public void Add(CTransaction transaction, CDataBaseException exception)
		{
			if (transaction == null)
			{
				CDBExceptionMessage oracleException = new CDBExceptionMessage();
				oracleException.Add(exception);
				modListErrors.Add(oracleException);
				return;
			}
			if (IsExists(transaction) == false)
			{
				CDBExceptionMessage oracleException = new CDBExceptionMessage(transaction, exception);
				modListErrors.Add(oracleException);
			}
			else
			{
				modListErrors.Where(x => x.TransactionID == transaction.ID)
										 .First()
										 .Add(exception);
			}
		}

		/// <summary>
		/// Return exception for corresponded transaction number
		/// </summary>
		/// <param name="transactionNumber"></param>
		/// <returns></returns>
		//static public CDBExceptionMessage Get(CTransaction transactionNumber)
		//{
		//  if (IsExists(transactionNumber) == false)
		//    return null;

		//  var listErrorsByNumber = modListErrors.Where(x => x.modTransactionNumber == transactionNumber);

		//  return listErrorsByNumber.First();
		//}

		static public string GetExceptionMessage(CTransaction transaction)
		{
			if (IsExists(transaction))
			{
				string exception = modListErrors.Where(x => x.TransactionID == transaction.ID)
																				.First().ExceptionMessage;
				return exception;
			}
			else
				return String.Empty;
		}

		static public IEnumerable<CDBExceptionMessage> GetExceptionMessageList(CTransaction transaction)
		{
			if (IsExists(transaction))
			{
				IEnumerable<CDBExceptionMessage> exceptions = modListErrors.Where(x => x.TransactionID == transaction.ID);
				return exceptions;
			}
			else
				return new List<CDBExceptionMessage>();
		}

		/// <summary>
		/// Clear exceptions list
		/// </summary>
		static public void Clear()
		{
			modListErrors.Clear();
		}

		/// <summary>
		/// Clear all exceptions from list that has given transaction number
		/// </summary>
		/// <param name="transactionNumber"></param>
		/// <returns></returns>
		static public bool Clear(CTransaction transaction)
		{
			bool result = modListErrors.RemoveAll(x => x.TransactionID == transaction.ID) > 0;

			return result;
		}
		#endregion
	}
}
