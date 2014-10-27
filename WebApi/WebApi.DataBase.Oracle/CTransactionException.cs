//-------------------------------------------------------------------
// <copyright file="CTransactionException.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary>Holds CTransactionException exception</summary>
// <author>Igor Kobylinskyi</author>
//-------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	///   CTransactionException is the default exception which holds transaction object
	/// </summary>
	public class CTransactionException : CDataBaseException
	{
		#region Fields

		private readonly CTransaction mvTransaction;

		#endregion // Fields

		#region Ctor

		/// <summary>
		///   Initializes a new instance of the <see cref="CTransactionException"/> class.
		/// </summary>
		public CTransactionException() { }

		/// <summary>
		///   Initializes a new instance of the <see cref="CTransactionException"/> class.
		/// </summary>
		/// <param name="transaction">Transaction for which exception was thrown</param>
		public CTransactionException(CTransaction transaction)
		{
			if (transaction == null)
				throw new Exception("Cannot initialize transaction exception from non exist transaction");

			mvTransaction = transaction;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="CTransactionException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		public CTransactionException(string message) : base(message) { }

		/// <summary>
		///   Initializes a new instance of the <see cref="CTransactionException"/> class.
		/// </summary>
		/// <param name="transaction">Transaction for which exception was thrown</param>
		/// <param name="message">The error message.</param>
		public CTransactionException(CTransaction transaction, string message)
			: base(message)
		{
			if (transaction == null)
				throw new Exception("Cannot initialize transaction exception from non exist transaction");
			if (string.IsNullOrEmpty(transaction.ID))
				throw new Exception("Cannot initialize transaction exception for non exist transaction ID");

			mvTransaction = transaction;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="CTransactionException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="innerException">The inner exception.</param>
		public CTransactionException(string message, Exception innerException) : base(message, innerException) { }

		/// <summary>
		///   Initializes a new instance of the <see cref="CTransactionException"/> class.
		/// </summary>
		/// <param name="transaction">Transaction for which exception was thrown</param>
		/// <param name="message">The error message.</param>
		/// <param name="innerException">The inner exception.</param>
		public CTransactionException(CTransaction transaction, string message, Exception innerException)
			: base(message, innerException)
		{
			if (transaction == null)
				throw new Exception("Cannot initialize transaction exception from non exist transaction");
			if (string.IsNullOrEmpty(transaction.ID))
				throw new Exception("Cannot initialize transaction exception for non exist transaction ID");

			mvTransaction = transaction;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="CTransactionException"/> class with serialized data.
		/// </summary>
		/// <param name="info">Serialized object data about the exception being thrown</param>
		/// <param name="context">Contextual information about the source or destination</param>
		protected CTransactionException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion // Ctor

		#region Properties

		public CTransaction Transaction
		{
			get { return mvTransaction; }
		}

		#endregion // Properties
	}
}