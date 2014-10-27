//-------------------------------------------------------------------
// <copyright file="CVersionException.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary>Holds CVersionException exception</summary>
// <author>Igor Kobylinskyi</author>
//-------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	///   CVersionException is the default exception which is raised on file version differs from target
	/// </summary>
	public class CVersionException : Exception
	{
		/// <summary>
		///   Initializes a new instance of the <see cref="CVersionException"/> class
		/// </summary>
		public CVersionException() { }

		/// <summary>
		///   Initializes a new instance of the <see cref="CVersionException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		public CVersionException(string message) : base(message) { }

		/// <summary>
		///   Initializes a new instance of the <see cref="CVersionException"/> class
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="innerException">The inner exception.</param>
		public CVersionException(string message, Exception innerException) : base(message, innerException) { }

		/// <summary>
		///   Initializes a new instance of the <see cref="CVersionException"/> class with serialized data
		/// </summary>
		/// <param name="info">Serialized object data about the exception being thrown</param>
		/// <param name="context">Contextual information about the source or destination</param>
		protected CVersionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}