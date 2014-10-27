//-------------------------------------------------------------------
// <copyright file="CFileSystem.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary></summary>
// <author>Victor Galaguza</author>
//-------------------------------------------------------------------

using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	/// Status of file system.
	/// </summary>
	public enum enFileSystemStatus
	{
		FileRead = 0,
		FileWrite = 1,
		DirectoryExists = 2,
		DirectoryCreate = 3,
		GetChildrenDirectories = 4,
		GetChildrenFiles = 5
	}

	/// <summary>
	/// Class File System
	/// Work with files and directorys
	/// </summary>
	public class CFileSystem
	{
		private const int FileSystemMaxDirectoryName = 247;
		private const int FileSystemMaxFullName = 259;

		/// <summary>
		/// FileStream object.
		/// </summary>
		private FileStream mvObjFS = null;

		/// <summary>
		/// 
		/// </summary>
		private string mvStrApplicationPath = String.Empty;
		private enFileSystemStatus mvEnStatus;
		private string mvStrError = String.Empty;

		private static string mvAppTempFolder;
		private static string mvAdaicaLogFolder;
		private static string mvAdaicaDocBackupFolder;

		private static string mvAdaicaTMPFolder;
		private static string modSessionUserId = String.Empty;

		public static string SessionUserId
		{
			get { return modSessionUserId; }
			set { modSessionUserId = value; }
		}

		//private static string mvAdaicaTMPFolder = Path.Combine(mvAppTempFolder, "Temp");

		private static string mvInvalidFileNameChars;

		static CFileSystem()
		{
			mvAppTempFolder = Path.Combine(Path.GetTempPath(), "adaica");
			mvAdaicaLogFolder = Path.Combine(mvAppTempFolder, "Log");

			mvAdaicaDocBackupFolder = Path.Combine(mvAppTempFolder, "DocBackup");

			mvAdaicaTMPFolder = "Temp";

			mvInvalidFileNameChars = new string(Path.GetInvalidFileNameChars().Where(c => !Char.IsControl(c)).ToArray());
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CFileSystem"/> class.
		/// </summary>
		public CFileSystem()
			: base()
		{
			mvStrApplicationPath = GetApplicationPath();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CFileSystem"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		public CFileSystem(string path)
			: base()
		{
			mvStrApplicationPath = GetApplicationPath();
			Start(path, enFileSystemStatus.FileRead);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CFileSystem"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="mode">The mode.</param>
		public CFileSystem(string path, enFileSystemStatus mode)
			: base()
		{
			mvStrApplicationPath = GetApplicationPath();
			Start(path, mode);
		}

		/// <summary>
		/// Starts the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		private void Start(string path)
		{
			enFileSystemStatus mode = enFileSystemStatus.FileRead;
			switch (mode)
			{
				case enFileSystemStatus.FileRead:
					FileOpen(path);
					break;
				case enFileSystemStatus.FileWrite:
					FileOpen(path, true);
					break;
				case enFileSystemStatus.DirectoryExists:
					DirectoryExists(path);
					break;
				case enFileSystemStatus.DirectoryCreate:
					DirectoryCreate(path);
					break;
				case enFileSystemStatus.GetChildrenDirectories:
					GetChildrenDirectorys(path);
					break;
				case enFileSystemStatus.GetChildrenFiles:
					GetChildrenFiles(path);
					break;
			}
			// Added mode if work with file
			if (mode == enFileSystemStatus.FileRead || mode == enFileSystemStatus.FileWrite)
				mvEnStatus = mode;
		}

		/// <summary>
		/// Starts the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="mode">The mode.</param>
		private void Start(string path, enFileSystemStatus mode)
		{
			switch (mode)
			{
				case enFileSystemStatus.FileRead:
					FileOpen(path);
					break;
				case enFileSystemStatus.FileWrite:
					FileOpen(path, true);
					break;
				case enFileSystemStatus.DirectoryExists:
					DirectoryExists(path);
					break;
				case enFileSystemStatus.DirectoryCreate:
					DirectoryCreate(path);
					break;
				case enFileSystemStatus.GetChildrenDirectories:
					GetChildrenDirectorys(path);
					break;
				case enFileSystemStatus.GetChildrenFiles:
					GetChildrenFiles(path);
					break;
			}
			// Added mode if work with file
			if (mode == enFileSystemStatus.FileRead || mode == enFileSystemStatus.FileWrite)
				mvEnStatus = mode;
		}


		/// <summary>
		/// Get start path of my app
		/// </summary>
		/// <returns>String value path of my application</returns>
		public static string GetApplicationPath()
		{
			return Application.StartupPath;
		}

		// Files

		/// <summary>
		/// Check on exists file
		/// </summary>
		/// <param name="path">Path to file</param>
		/// <returns>True - file exists, False - file not exists</returns>
		public static bool FileExists(string path)
		{
			try
			{
				return File.Exists(path);
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Open file to read
		/// </summary>
		/// <param name="path">Path to the file</param>
		/// <returns>True - if file has been opened, False - if error</returns>
		public bool FileOpen(string path)
		{
			return FileOpen(path, false);
		}

		/// <summary>
		/// Open or create file to read or write
		/// </summary>
		/// <param name="path">Path to the file</param>
		/// <param name="isWrite">Is write?</param>
		/// <returns>True - if file has been opened, False - if error</returns>
		public bool FileOpen(string path, bool isWrite)
		{
			if (path == null)
				throw new ArgumentNullException("path");
			else if (path.Length == 0)
				throw new Exception("Path is empty.");
			if (isWrite == true)
			{
				try
				{
					mvObjFS = new FileStream(path, FileMode.OpenOrCreate);
					return true;
				}
				catch (Exception exc)
				{
					mvStrError = exc.Message;
					return false;
				}
			}
			else
			{
				try
				{
					mvObjFS = new FileStream(path, FileMode.Open);
					return true;
				}
				catch (Exception exc)
				{
					mvStrError = exc.Message;
					return false;
				}
			}
		}

		/// <summary>
		/// Append text to the file
		/// </summary>
		/// <param name="text">Text which need append</param>
		/// <returns>True - if added text, False - if file not has been opened</returns>
		public bool AppendText(string text)
		{
			try
			{
				StreamWriter writer = new StreamWriter(mvObjFS);
				//long length = 0;
				//length = mvObjFS.Length + text.Length;
				//mvObjFS.Lock(0, length);
				mvObjFS.Seek(0, SeekOrigin.End);
				writer.WriteLine(text);
				writer.Flush();
				//mvObjFS.Unlock(0, length);
				return true;
			}
			catch
			{
				mvStrError = text;
				return false;
			}
		}

		/// <summary>
		/// Close opened file
		/// </summary>
		/// <returns>True - if file turn close, False - is file already has been closed</returns>
		public bool FileClose()
		{
			try
			{
				if (mvObjFS == null)
					return false;
				else
				{
					mvObjFS.Close();
					return true;
				}
			}
			catch
			{
				return false;
			}
		}

		// Directorys

		/// <summary>
		/// Check on exists directory
		/// </summary>
		/// <param name="path">Path to directory</param>
		/// <returns>True - directory exists, False - directory not exists</returns>
		public static bool DirectoryExists(string path)
		{
			try
			{
				return Directory.Exists(path);
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Create this directory
		/// </summary>
		/// <param name="path">Path to directory</param>
		/// <returns>True - directory has created, False - directory not created</returns>
		public static bool DirectoryCreate(string path)
		{
			bool isResult = true;
			try
			{
				if (!DirectoryExists(path))
				{
					Directory.CreateDirectory(path);
					isResult = true;
				}
			}
			catch (Exception exc)
			{
				CLog.Log("DirectoryCreate(" + path + ")", exc);
				isResult = false;
			}
			return isResult;
		}

		/// <summary>
		/// Gets the app temp folder.
		/// </summary>
		/// <returns>
		/// C:\Documents and Settings\skuda\Local Settings\Temp\ADAICA\
		/// </returns>
		public static string GetAppTempFolder()
		{
			return CreateTempDirectory(mvAppTempFolder);
		}

		/// <summary>
		/// Gets the app temp folder.
		/// </summary>
		/// <returns>
		/// C:\Documents and Settings\skuda\Local Settings\Temp\ADAICA\Update
		/// </returns>
		public static string GetAppUpdateFolder()
		{
			return CreateTempDirectory(Path.Combine(mvAppTempFolder, "Update"));
		}

		/// <summary>
		/// Gets the adaica temp file folder.
		/// </summary>
		/// <returns>
		/// C:\Documents and Settings\skuda\Local Settings\Temp\adaica\4656
		/// </returns>
		public static string GetAdaicaTempFileFolder()
		{
			return CreateTempDirectory(Path.Combine(mvAppTempFolder, Process.GetCurrentProcess().Id.ToString() + modSessionUserId));
		}


		/// <summary>
		/// Gets the adaica documents backup folder.
		/// </summary>
		/// <returns>
		/// C:\Documents and Settings\skuda\Local Settings\Temp\adaica\DocBackup
		/// </returns>
		public static string GetAdaicaDocBackupFolder()
		{
			return CreateTempDirectory(mvAdaicaDocBackupFolder);
		}



		/// <summary>
		/// Converts the path of blob in ADAICA to file in FS.
		/// </summary>
		/// <param name="pathAD">The path in ADAICA</param>
		/// <returns>Converted path</returns>
		/// ===========================================================================================================
		public static string ConvertPathADtoFS(string pathAD)
		{
			List<char> illegalChars = new List<char>();

			illegalChars.AddRange(Path.GetInvalidFileNameChars());
			illegalChars.AddRange(Path.GetInvalidPathChars());

			illegalChars = illegalChars.Distinct().ToList();

			string pathRet = pathAD;

			foreach (char c in illegalChars)
			{
				if (pathAD.Contains(c))
				{
					string replace = String.Format("({0})", (int)(c + 10));

					pathRet = pathRet.Replace(c.ToString(), replace);
				}
			}

			return pathRet;
		}

		/// <summary>
		/// Converts the path of file in FS to blob in ADAICA
		/// </summary>
		/// <param name="pathAD">The path in FS</param>
		/// <returns>Converted path</returns>
		/// ===========================================================================================================
		public static string ConvertPathFStoAD(string pathFS)
		{
			List<char> illegalChars = new List<char>();

			illegalChars.AddRange(Path.GetInvalidFileNameChars());
			illegalChars.AddRange(Path.GetInvalidPathChars());

			illegalChars = illegalChars.Distinct().ToList();

			string pathRet = pathFS;

			foreach (char c in illegalChars)
			{
				string replace = String.Format("({0})", (int)(c + 10));

				pathRet = pathRet.Replace(replace, c.ToString());
			}

			return pathRet;
		}

		/// <summary>
		/// Gets the extention without dot.
		/// </summary>
		/// <param name="extention">The extention.</param>
		/// <returns>extention without dot</returns>
		/// ===========================================================================================================
		public static string GetExtentionWithoutDot(string extention)
		{
			if (!String.IsNullOrEmpty(extention) && extention.StartsWith("."))
				return extention.Substring(1);

			return extention;
		}

		/// <summary>
		/// Get path to file created for holding blob contents in temp folder
		/// </summary>
		/// <returns>Temp file path</returns>
		/// ===========================================================================================================
		public static string GetTempFilePath(string documentName, string id, string extention)
		{
			string folderDest = GetAdaicaTempFileFolder();

			string docNameSafe = ConvertPathADtoFS(documentName);

			string idPar = id;
			if (!String.IsNullOrEmpty(idPar))
				idPar = "(" + id + ")";

			return Path.Combine(folderDest, docNameSafe + idPar + "." + GetExtentionWithoutDot(extention));
		}


		/// <summary>
		/// Gets the file path in temp folder.
		/// </summary>
		/// <param name="folderPath">The folder path.</param>
		/// <param name="documentName">Name of the document.</param>
		/// <param name="extention">The extention.</param>
		/// <returns></returns>
		/// ===========================================================================================================
		public static string GetFilePathInTempFolder(string folderPath, string documentName, string extention)
		{
			var docNameSafe = ConvertPathADtoFS(documentName) ?? string.Empty;
			var docExtSafe = ConvertPathADtoFS(extention) ?? string.Empty;

			if (folderPath.Length + docNameSafe.Length + docExtSafe.Length > FileSystemMaxDirectoryName)
			{
				docNameSafe = Path.GetRandomFileName();
			}

			var fullDocName = String.IsNullOrEmpty(docExtSafe)
				? docNameSafe
				: string.Format("{0}.{1}", docNameSafe, GetExtentionWithoutDot(docExtSafe));

			return Path.Combine(folderPath, fullDocName);
		}

		/// <summary>
		/// Gets the file path in temp unique folder.
		/// </summary>
		/// <param name="subFolderName">Name of the sub folder.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="extention">The extention.</param>
		/// <returns></returns>
		public static string GetFilePathInTempUniqueFolder(string subFolderName, string fileName, string extention)
		{
			int nameAddIndex = 0;
			while (true)
			{
				string folderNameUnique = CFileSystem.GetTempFileFolder(nameAddIndex++, subFolderName);

				if (Directory.Exists(folderNameUnique))
					continue;

				Directory.CreateDirectory(folderNameUnique);

				string docPathUnique = CFileSystem.GetFilePathInTempFolder(folderNameUnique, fileName, extention);

				return docPathUnique;
			}
		}

		/// <summary>
		/// Gets the temp file folder.
		/// </summary>
		/// <param name="addIndex">Index of the add.</param>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		/// ===========================================================================================================
		public static string GetTempFileFolder(int addIndex, string id)
		{
			string folderDest = GetAdaicaTempFileFolder();

			string uniqueFolder = String.Format("{0}^{1}", addIndex, id);

			return Path.Combine(folderDest, uniqueFolder);
		}

		public static bool DeleteAdaicaTempFileFolder()
		{
			bool isResult = true;
			try
			{
				DirectoryClear(GetAdaicaTempFileFolder());
			}
			catch (Exception /*exc*/)
			{
				isResult = false;
			}
			return isResult;
		}

		/// <summary>
		/// Directories the clear.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		/// ===========================================================================================================
		public static bool DirectoryClear(string path)
		{
			bool isResult = true;
			try
			{
				foreach (string currFile in Directory.GetFiles(path))
				{
					File.SetAttributes(currFile, FileAttributes.Normal);
					File.Delete(currFile);
				}

				foreach (string subDir in Directory.GetDirectories(path))
					DirectoryClear(subDir);

				Directory.Delete(path);
			}
			catch (Exception /*exc*/)
			{
				isResult = false;
			}
			return isResult;
		}

		/// <summary>
		/// Gets the adaica temp folder.
		/// </summary>
		/// <returns>
		/// C:\Documents and Settings\skuda\Local Settings\Temp\ADAICA\{PID}\TEMP
		/// </returns>
		public static string GetAdaicaTempFolder()
		{
			return CreateTempDirectory(Path.Combine(GetAdaicaTempFileFolder(), mvAdaicaTMPFolder));
		}

		/// <summary>
		/// Gets the file path in temp folder.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		/// ===========================================================================================================
		public static string GetFilePathInTempFolder(string fileName)
		{
			return Path.Combine(GetAdaicaTempFolder(), fileName);
		}

		/// <summary>
		/// Gets the short name.
		/// </summary>
		/// <param name="folderName">Name of the folder.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>Full path to file with short name of file</returns>
		public static string GetShortName(string folderName, string fileName)
		{
			// Remove extra slashes
			folderName = folderName.TrimEnd(Path.DirectorySeparatorChar);

			string resultPath = folderName + Path.DirectorySeparatorChar + fileName;
			if (resultPath.Length > FileSystemMaxFullName)
			{
				// Not removable file extension
				string fileExt = RemoveFileExtension(ref fileName);
				// Count of available symbols (1 symbol for separator)
				int nameLength = FileSystemMaxFullName - (folderName.Length + 1 + fileExt.Length);
				// Short name
				string shortExt = "~" + fileName.Length;
				string shortName = shortExt;

				if (nameLength >= shortName.Length)
				{
					while (shortName.Length < nameLength)
					{
						shortName = fileName.Substring(0, nameLength - shortExt.Length);
						shortExt = "~" + (fileName.Length - (shortName.Length - shortExt.Length)).ToString();
						shortName += shortExt;
					}

					resultPath = Path.Combine(folderName, shortName + fileExt);
				}
				else
					resultPath = String.Empty;
			}

			return resultPath;
		}

		/// <summary>
		/// Removes the file extension.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>Removed extension</returns>
		public static string RemoveFileExtension(ref string fileName)
		{
			string fileExt = String.Empty;
			int dotPosition = fileName.LastIndexOf('.');
			if (dotPosition >= 0)
			{
				fileExt = fileName.Substring(dotPosition, fileName.Length - dotPosition);
				fileName = fileName.Substring(0, dotPosition);
			}
			return fileExt;
		}

		public static string GetFileExtention(string fileName)
		{
			string extention = String.Empty;
			try
			{
				extention = Path.GetExtension(fileName);
				if (!String.IsNullOrEmpty(extention) && extention.StartsWith("."))
					extention = extention.Substring(1);
			}
			catch (Exception exc)
			{
				CLog.Log("GetFileExtention(" + fileName + ")", exc);
			}
			return extention;
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

		/// <summary>
		/// Gets the user temp folder.
		/// </summary>
		/// <returns>
		/// C:\Documents and Settings\skuda\Local Settings\Temp\
		/// </returns>
		public static string GetUserTempFolder()
		{
			return CreateTempDirectory(Path.GetTempPath());
		}

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

		public static string GetInvalidFileNameChars()
		{
			return mvInvalidFileNameChars;
		}

		/// <summary>
		/// Get all children directorys in this directory
		/// </summary>
		/// <param name="path">Path to directory</param>
		/// <returns>DirectoryInfo Array - all names of children folders</returns>
		public string[] GetChildrenDirectorys(string path)
		{
			try
			{
				return Directory.GetDirectories(path);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Get all files in this directory
		/// </summary>
		/// <param name="path">Path to directory</param>
		/// <returns>FileInfo Array - all names of children files</returns>
		public string[] GetChildrenFiles(string path)
		{
			return GetChildrenFiles(path, "*");
		}

		/// <summary>
		/// Get files which equals to mask in this directory
		/// </summary>
		/// <param name="path">Path to directory</param>
		/// <param name="mask"></param>
		/// <returns>FileInfo Array - all names of children files</returns>
		public string[] GetChildrenFiles(string path, string mask)
		{
			try
			{
				return Directory.GetFiles(path, mask);
			}
			catch
			{
				return null;
			}
		}

		// Properties

		/// <summary>
		/// Gets the application path.
		/// </summary>
		/// <value>The application path.</value>
		public string ApplicationPath
		{
			get
			{
				return mvStrApplicationPath;
			}
		}

		/// <summary>
		/// Gets the status.
		/// </summary>
		/// <value>The status.</value>
		public enFileSystemStatus Status
		{
			get
			{
				return mvEnStatus;
			}
		}

		/// <summary>
		/// Gets the error.
		/// </summary>
		/// <value>The error.</value>
		public string Error
		{
			get
			{
				return mvStrError;
			}
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="CFileSystem"/> is reclaimed by garbage collection.
		/// </summary>
		//~CFileSystem()
		//{
		//  FileClose();
		//  mvObjFS = null;
		//  mvStrApplicationPath = null;
		//  mvStrError = null;
		//}


		public static bool IsFileOpen(string filePath)
		{
			bool isResult = false;
			FileStream fs = null;
			try
			{
				fs = File.Open(filePath, FileMode.Open, FileAccess.Read);
			}
			catch (Exception /*exc*/)
			{
				isResult = true;
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}
			return isResult;
		}

		/// <summary>
		/// Checks the directory access by create delete.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		/// ===========================================================================================================
		public static bool CheckDirectoryAccessByCreateDelete(string path)
		{
			bool isOk = true;

			string fileName = "dummy.###";
			string filePath = Path.Combine(path, fileName);
			FileStream fs = null;

			try
			{
				fs = File.Create(filePath);
				fs.WriteByte((byte)1);
			}
			catch
			{
				isOk = false;
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}

			try
			{
				File.Delete(filePath);
			}
			catch
			{
				isOk = false;
			}


			return isOk;
		}

		/// <summary>
		/// Checks the directory access.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="path">The path.</param>
		/// <param name="expectedRights">The expected rights.</param>
		/// <returns></returns>
		/// ===========================================================================================================
		public static bool CheckDirectoryAccess(WindowsIdentity user, string path, FileSystemRights expectedRights)
		{
			DirectoryInfo di = new DirectoryInfo(path);
			AuthorizationRuleCollection acl = di.GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier));

			return CheckFileSystemObjectAccess(user, acl, expectedRights);
		}

		/// <summary>
		/// Checks the file access.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="path">The path.</param>
		/// <param name="expectedRights">The expected rights.</param>
		/// <returns></returns>
		/// ===========================================================================================================
		public static bool CheckFileAccess(WindowsIdentity user, string path, FileSystemRights expectedRights)
		{
			if (path == null)
				return false;

			FileInfo fi = new FileInfo(path);
			AuthorizationRuleCollection acl = fi.GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier));

			return CheckFileSystemObjectAccess(user, acl, expectedRights);
		}

		/// <summary>
		/// Checks the file system object access.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="acl">The acl.</param>
		/// <param name="expectedRights">The expected rights.</param>
		/// <returns></returns>
		/// ===========================================================================================================
		public static bool CheckFileSystemObjectAccess(WindowsIdentity user, AuthorizationRuleCollection acl, FileSystemRights expectedRights)
		{
			// gets rules that concern the user and his groups
			IEnumerable<AuthorizationRule> userRules = from AuthorizationRule rule in acl
																								 where user.User.Equals(rule.IdentityReference)
																								 || user.Groups.Contains(rule.IdentityReference)
																								 select rule;

			FileSystemRights denyRights = 0;
			FileSystemRights allowRights = 0;

			// iterates on rules to compute denyRights and allowRights
			foreach (FileSystemAccessRule rule in userRules)
			{
				if (rule.AccessControlType.Equals(AccessControlType.Deny))
				{
					denyRights = denyRights | rule.FileSystemRights;
				}
				else if (rule.AccessControlType.Equals(AccessControlType.Allow))
				{
					allowRights = allowRights | rule.FileSystemRights;
				}
			}

			allowRights = allowRights & ~denyRights;

			// are rights sufficient?
			return (allowRights & expectedRights) == expectedRights;
		}


		/// <summary>
		/// Determines whether [is file access read] [the specified path].
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// 	<c>true</c> if [is file access read] [the specified path]; otherwise, <c>false</c>.
		/// </returns>
		/// ===========================================================================================================
		public static bool IsFileAccessRead(string path)
		{
			return CheckFileAccess(WindowsIdentity.GetCurrent(), path, FileSystemRights.Read);
		}

		/// <summary>
		/// Determines whether [is folder access read] [the specified path].
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// 	<c>true</c> if [is folder access read] [the specified path]; otherwise, <c>false</c>.
		/// </returns>
		/// ===========================================================================================================
		public static bool IsFolderAccessRead(string path)
		{
			return CheckDirectoryAccess(WindowsIdentity.GetCurrent(), path, FileSystemRights.Read);
		}

		public static bool IsPathIsLocal(string path)
		{
			bool isResult = false;
			DriveInfo[] driveInfos = DriveInfo.GetDrives();
			foreach (DriveInfo driveInfo in driveInfos)
			{
				if (path.StartsWith(driveInfo.Name) && driveInfo.DriveType != DriveType.Network)
				{
					isResult = true;
					break;
				}
			}
			return isResult;
		}

		public static bool HasPermissionToDirectory(string directoryPath, FileIOPermissionAccess permission)
		{
			if (string.IsNullOrEmpty(directoryPath))
				return false;

			var trimmedDirectoryPath = directoryPath.Trim();

			if (trimmedDirectoryPath.Length < CCoreConstants.MinFolderPathLength || !DirectoryExists(trimmedDirectoryPath))
				return false;

			var permissions = new FileIOPermission(permission, directoryPath);

			try
			{
				permissions.Demand();

				return true;
			}
			catch (ThreadAbortException) { }
			catch { }

			return false;
		}

		public static bool HasPermissionToDirectory(string directoryPath, FileSystemRights permission)
		{
			if (string.IsNullOrEmpty(directoryPath))
				return false;

			var trimmedDirectoryPath = directoryPath.Trim();

			if (trimmedDirectoryPath.Length < CCoreConstants.MinFolderPathLength || !DirectoryExists(trimmedDirectoryPath))
				return false;

			var allow = false;
			var deny = false;

			try
			{
				var accessControlList = Directory.GetAccessControl(trimmedDirectoryPath);

				var accessRules = accessControlList.GetAccessRules(true, true, typeof(SecurityIdentifier));

				foreach (FileSystemAccessRule rule in accessRules)
				{
					if ((permission & rule.FileSystemRights) != permission)
						continue;

					if (rule.AccessControlType == AccessControlType.Allow)
					{
						allow = true;
					}
					else if (rule.AccessControlType == AccessControlType.Deny)
					{
						deny = true;
					}
				}
			}
			catch (ThreadAbortException) { }
			catch { }

			return allow && !deny;
		}

		/// <summary>
		/// Deletes the file safe.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// ===========================================================================================================
		public static void DeleteFileSafe(string filePath)
		{
			try
			{
				if (File.Exists(filePath))
				{
					File.SetAttributes(filePath, FileAttributes.Normal);
					File.Delete(filePath);
				}
			}
			catch { }
		}

		/// <summary>
		/// Gets the file extension without dot.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public static string GetFileExtensionWithoutDot(string filePath)
		{
			if (String.IsNullOrEmpty(filePath))
				return String.Empty;

			string extension = Path.GetExtension(filePath).Trim('.');


			return extension;
		}

		/// <summary>
		///   Copies source file to to file
		/// </summary>
		/// <param name="sourceFilePath">Source file path</param>
		/// <param name="destinationFilePath">Destination file location</param>
		/// <returns>If copy was done - destination file path</returns>
		public static string FileCopy(string sourceFilePath, string destinationFilePath)
		{
			if (!FileExists(sourceFilePath))
				throw new Exception(string.Format(@"Cannot copy non exist file '{0}'", sourceFilePath));

			if (FileExists(destinationFilePath))
			{
				File.SetAttributes(destinationFilePath, FileAttributes.Normal);
				CFileSystem.DeleteFileSafe(destinationFilePath);
			}

			File.Copy(sourceFilePath, destinationFilePath);

			return destinationFilePath;
		}

		/// <summary>
		/// Composes the index of the file name with.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		private static string ComposeFileNameWithIndex(string path, int index)
		{
			const string indexPattern = " ({0})";

			if (string.IsNullOrEmpty(path) || index <= 0)
				return path;

			var fileExt = Path.GetExtension(path);

			var resultPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
			resultPath += string.Format(indexPattern, index);
			resultPath = Path.ChangeExtension(resultPath, fileExt);

			return resultPath;
		}

		/// <summary>
		/// Makes the file names unique.
		/// </summary>
		/// <param name="fileNames">The file names.</param>
		/// <returns></returns>
		public static string[] MakeFileNamesUnique(IEnumerable<string> fileNames)
		{
			return fileNames
				.Select((f, i) => new
					{
						FileName = f,
						Index = fileNames
							.Take(i + 1)
							.Count(f1 => string.Equals(f1, f, StringComparison.InvariantCultureIgnoreCase)) - 1
					})
				.Select(x => ComposeFileNameWithIndex(x.FileName, x.Index))
				.ToArray();
		}
	}
}
