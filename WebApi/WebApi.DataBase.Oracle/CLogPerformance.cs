//-------------------------------------------------------------------
// <copyright file="CLogPerformance.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <author>Smyk Aleksandr</author>
//-------------------------------------------------------------------

using System;
using System.IO;
using System.Threading;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	/// Class represents logic for log`s performance operation in the code.
	/// </summary>
	public class CLogPerformance
	{
		private static string IDENTIFIER_TIME = ":";
		private static string modLogPath = String.Empty;

		private static string MainEndTag = "</Main>";

		private static string TimeTag = "<Time>";
		private static string TimeTagEnd = "</Time>";

		private static string NameTag = "<Name>";
		private static string NameTagEnd = "</Name>";

		private static string ParamsTag = "<Params>";
		private static string ParamsTagEnd = "</Params>";

		private static string RecordTag = "<Record>";
		private static string RecordTagEnd = "</Record>";

		private static string TimeWorkTag = "<TimeWork>";
		private static string TimeWorkTagEnd = "</TimeWork>";

		static CLogPerformance()
		{
			try
			{
				string timenow = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
				string modLogFileName = DateTime.Now.ToString("yyyy-MM-dd") + "_" + timenow + ".logPerformance.xml";
				modLogPath = Path.Combine(CFileSystem.GetAdaicaLogFolder(), modLogFileName);

				using (FileStream fs = new FileStream(modLogPath, FileMode.Append, FileAccess.Write, FileShare.Write))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
						sw.WriteLine("<Main>");
						sw.Close();
					}
					fs.Close();
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				CLog.Log("CLogPerformance.Constructor", ex);
			}
		}

		/// <summary>
		/// Writes the message.
		/// </summary>
		/// <param name="procedure">The procedure.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="timeWork">The time work.</param>
		public static void WriteMessage(string procedure, string parameters, string timeWork)
		{
			try
			{
				string timenow = DateTime.Now.Hour.ToString() + IDENTIFIER_TIME + DateTime.Now.Minute.ToString() + IDENTIFIER_TIME + DateTime.Now.Second.ToString();
				using (FileStream fs = new FileStream(modLogPath, FileMode.Append, FileAccess.Write, FileShare.Write))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						sw.Write(RecordTag);
						sw.WriteLine(TimeTag + timenow + TimeTagEnd);
						sw.WriteLine(NameTag + procedure + NameTagEnd);
						sw.WriteLine(ParamsTag + parameters + ParamsTagEnd);
						sw.WriteLine("<Case>1</Case>");
						sw.WriteLine(TimeWorkTag + timeWork + TimeWorkTagEnd);
						sw.WriteLine(RecordTagEnd);
						sw.Close();
					}
					fs.Close();
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				CLog.Log("CLogPerformance.WriteMessage", ex);
			}
		}

		/// <summary>
		/// Writes the end messgae.
		/// </summary>
		public static void WriteEndMessgae()
		{
			try
			{
				using (FileStream fs = new FileStream(modLogPath, FileMode.Append, FileAccess.Write, FileShare.Write))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						sw.WriteLine(MainEndTag);
						sw.Close();
					}
					fs.Close();
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				CLog.Log("CLogPerformance.WriteEndMessgae", ex);
			}
		}
	}
}
