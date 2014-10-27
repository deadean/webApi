//-------------------------------------------------------------------
// <copyright file="CDataBaseExtensions.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary>Holds extension methods for data processing & logging</summary>
// <author>Anton Daniloff</author>
//-------------------------------------------------------------------

using System;
using System.Data.Common;
using System.Diagnostics;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	/// Holds extension methods for data processing & logging
	/// </summary>
	public static class CDataBaseExtensions
	{
		/// <summary>
		/// If query execution time greater than this constant, ERR message will be posted to log
		/// </summary>
		private const double HASROWS_CRITICAL_QUERY_EXECUTION_TIME_MS = 7000;


		/// <summary>
		/// Logs error if accessing to reader.HasRows was longer than HASROWS_CRITICAL_QUERY_EXECUTION_TIME_MS
		/// </summary>
		/// <param name="reader">reader</param>
		/// <param name="dbQuery">dbQuery</param>
		/// <returns>reader.HasRows</returns>
		public static bool HasRows(this DbDataReader reader, CDBQuery dbQuery)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			bool result = reader.HasRows;


			sw.Stop();
			if (sw.ElapsedMilliseconds > HASROWS_CRITICAL_QUERY_EXECUTION_TIME_MS)
			{
				string paramsStr = string.Empty;
				if (dbQuery.Parameters != null)
					paramsStr = dbQuery.Parameters.ToString();

				CLog.Log(dbQuery.ProcedureName + "(" + paramsStr + ")", new System.TimeoutException(String.Format("HASROWS_CRITICAL_QUERY_EXECUTION_TIME_MS exceeded ({0} ms)", sw.ElapsedMilliseconds)));
			}


			return result;
		}

		/// <summary>
		/// Logs error if accessing to reader.HasRows was longer than HASROWS_CRITICAL_QUERY_EXECUTION_TIME_MS 
		/// </summary>
		/// <param name="reader">reader</param>
		/// <param name="procedureName">procedureName</param>
		/// <param name="parameters">parameters</param>
		/// <returns>reader.HasRows</returns>
		public static bool HasRows(this DbDataReader reader, string procedureName, CDBParameters parameters)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			bool result = reader.HasRows;


			sw.Stop();
			if (sw.ElapsedMilliseconds > HASROWS_CRITICAL_QUERY_EXECUTION_TIME_MS)
			{
				string paramsStr = string.Empty;
				if (parameters != null)
					paramsStr = parameters.ToString();

				CLog.Log(procedureName + "(" + paramsStr + ")", new System.TimeoutException(String.Format("CRITICAL_QUERY_EXECUTION_TIME_MS exceeded ({0} ms)", sw.ElapsedMilliseconds)));
			}


			return result;
		}
	}
}
