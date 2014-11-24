using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;

namespace WebApi.Common.Implementations.Logging
{
	/// <summary>
	/// Class File System
	/// Work with files and directorys
	/// </summary>
	public class CFileSystem
	{

		#region Fields

		private static string mvAdaicaLogFolder;
		private static string mvAppTempFolder;
		
		#endregion

		#region Ctor

		static CFileSystem()
		{
			mvAppTempFolder = Path.Combine(Path.GetTempPath(), "adaica");
			mvAdaicaLogFolder = Path.Combine(mvAppTempFolder, "Log");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CFileSystem"/> class.
		/// </summary>
		public CFileSystem()
			: base()
		{
		}
		
		#endregion

		#region Public Methods

		/// <summary>
		/// Creates the directory.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static string CreateTempDirectory(string path)
		{
			string tmp = path;
			try
			{
				if (!Directory.Exists(tmp))
					Directory.CreateDirectory(tmp);
			}
			catch (Exception exc)
			{
				tmp = Path.GetTempPath();
				CLog.Log("CreateTempDirectory(" + path + ")", exc);
			}
			return tmp;
		}

		/// <summary>
		/// Gets the adaica log folder.
		/// </summary>
		/// <returns>
		/// C:\Documents and Settings\skuda\Local Settings\Temp\ADAICA\Log
		/// </returns>
		public static string GetAdaicaLogFolder()
		{
			return CreateTempDirectory(mvAdaicaLogFolder);
		}
		
		#endregion

	}
}
