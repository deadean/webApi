//-------------------------------------------------------------------
// <copyright file="CDataBaseException.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary>Contains exception class for CDataBase</summary>
// <author>Anton Maryanov</author>
//-------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	/// Exception class for CDataBase. Throw it only in CDataBase.
	/// </summary>
	public class CDataBaseException : Exception
	{
		#region Private Fields
		/// <summary>
		/// Default DB exception error message.
		/// </summary>
		private const string mvStrMessage =	"CDataBase";
		#endregion

		#region Constructors
		/// <summary>
		/// Create new DB exception with default error message.
		/// </summary>
		public CDataBaseException() :
			base(mvStrMessage)
		{ }

		/// <summary>
		/// Create new DB Exception with <paramref name="strMessage"/> error message.
		/// </summary>
		/// <param name="strMessage">Exception error message</param>
		public CDataBaseException(string strMessage) :
			base(String.Format("{0}: {1}",	mvStrMessage, strMessage))
		{ }

		/// <summary>
		/// Create new DB Exception, that have <paramref name="strMessage"/> error message and
		/// uplevel <paramref name="innerException"/> exception
		/// </summary>
		/// <param name="strMessage">Exception error message</param>
		/// <param name="innerException">Uplevel exception</param>
		public CDataBaseException(string strMessage, Exception innerException) :
			base(String.Format("{0}: {1}",	 mvStrMessage, strMessage), innerException)
		{ }
		
		/// <summary>
		/// Create new DB Exception, that was invoked by <paramref name="innerException"/> exception
		/// </summary>
		/// <param name="innerException">Uplevel exception</param>
		public CDataBaseException(Exception innerException) :
			base(mvStrMessage, innerException)
		{ }

		/// <summary>
		///   Initializes a new instance of the <see cref="CDataBaseException"/> class with serialized data.
		/// </summary>
		/// <param name="info">Serialized object data about the exception being thrown</param>
		/// <param name="context">Contextual information about the source or destination</param>
		protected CDataBaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
		#endregion
	}
}
