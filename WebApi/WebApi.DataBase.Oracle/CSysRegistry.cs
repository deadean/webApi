using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace WebApi.DataBase.Oracle
{
	public static class enUsersLoginKeys
	{
		public const string UsersLoginName = "Name";
		public const string UsersLoginPassword = "Password";
		public const string UsersLoginIsPrimary = "PrimaryUser";
	}

	public static class CSysRegistry
	{
		private const string REG_ADAICA_CONNECTION_KEY = "Connection";
		private const string REG_ADAICA_DBTRACE_KEY = "TRACE";

		private const string REG_ADAICA_THEME_NAME_KEY = "ThemeName";
		private const string DEFAULT_THEME_NAME = "Aero";
		#region Private Member Variables
		/// <summary>
		/// Regisrty key for default connection
		/// </summary>
		static string  softwareADAICAPathConnection = @"SOFTWARE\ADAICA\Connection";

		/// <summary>
		/// Regisrty key for ADAICA
		/// </summary>
		static string  softwareADAICAPath = @"SOFTWARE\ADAICA\";

//		private static string softwareADAICAUsers = @"\Users";

		#endregion Private Member Variables

		public static RegistryKey GetRegADAICAPath()
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.CurrentUser.OpenSubKey(softwareADAICAPath, true);
				if (registryKey == null)
					registryKey = Registry.CurrentUser.CreateSubKey(softwareADAICAPath);
			}
			catch (Exception exc)
			{
				CLog.Log("CheckADAICAPath()", exc);
				registryKey = null;
			}
			return registryKey;
		}

		private static RegistryKey GetRegADAICAConnectionPath()
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.CurrentUser.OpenSubKey(softwareADAICAPathConnection, true);
				if (registryKey == null)
					registryKey = Registry.CurrentUser.CreateSubKey(softwareADAICAPathConnection);
			}
			catch (Exception exc)
			{
				CLog.Log("CheckADAICAConnectionPath()", exc);
				registryKey = null;
			}
			return registryKey;
		}

		private static RegistryKey GetOrCreateRegKey(RegistryKey parentKey, string keyName)
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = parentKey.OpenSubKey(keyName, true);
				if (registryKey == null)
					registryKey = parentKey.CreateSubKey(keyName);
			}
			catch (Exception exc)
			{
				CLog.Log("CheckADAICAConnectionPath()", exc);
				registryKey = null;
			}
			return registryKey;
		}

		private static RegistryKey GetRegADAICASchemaPath(string dbName, string schemaName)
		{
			string result = String.Empty;
			RegistryKey registryKey = null;
			try
			{
				using (RegistryKey registryKeyConnect = GetRegADAICAConnectionPath())
				{
					if (registryKeyConnect != null)
						using (RegistryKey registryKeyDBName = GetOrCreateRegKey(registryKeyConnect, dbName))
						{
							if (registryKeyDBName != null)
								registryKey = GetOrCreateRegKey(registryKeyDBName, schemaName);
						}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("GetRegADAICASchemaPath()", exc);
				registryKey = null;
			}
			return registryKey;
		}

		public static string GetCryptDBPassword()
		{
			return GetCryptDBPassword(CDataBase.DataSource, CDataBase.UserID);
		}

		public static string GetCryptDBPassword(string DBName, string DBSchema)
		{
			string result = String.Empty;
			try
			{
				using (RegistryKey registryKeyConnect = GetRegADAICAConnectionPath())
				{
					if (registryKeyConnect != null)
						foreach (string subKeyName in registryKeyConnect.GetSubKeyNames())
						{
							if (subKeyName == DBName)
							{
								using (RegistryKey registryKeyDBName = registryKeyConnect.OpenSubKey(subKeyName))
								{
									if (registryKeyDBName != null)
									{
										object objValue = registryKeyDBName.GetValue(DBSchema);
										if (objValue != null && !String.IsNullOrEmpty(objValue.ToString()))
											result = objValue.ToString();
										//  result = CDataBase.PasswordCryptDefault;
									}
								}
							}
						}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("GetCryptDBPassword()", exc);
				result = String.Empty;
			}
			return result;
		}

		public static bool EraseCryptDBPassword()
		{
			return SetCryptDBPassword(CDataBase.DataSource, CDataBase.UserID, String.Empty, true);
		}

		public static bool SetCryptDBPassword(string cryptPassword)
		{
			return SetCryptDBPassword(CDataBase.DataSource, CDataBase.UserID, cryptPassword, false);
		}

		public static bool SetCryptDBPassword(string DBName, string DBSchema, string cryptPassword, bool isPWDErase)
		{
			bool result = false;
			RegistryKey registryKeyDBName = null;
			try
			{
				using (RegistryKey registryKey = GetRegADAICAPath())
				{
					if (registryKey != null)
						using (RegistryKey registryKeyConnect = GetRegADAICAConnectionPath())
						{
							if (registryKeyConnect != null)
							{
								bool isExist = false;
								foreach (string subKeyName in registryKeyConnect.GetSubKeyNames())
								{
									if (subKeyName == DBName)
									{
										registryKeyDBName = registryKeyConnect.OpenSubKey(subKeyName,true);
										isExist = true;
									}
								}
								if (!isExist && !isPWDErase)
									registryKeyDBName = registryKeyConnect.CreateSubKey(DBName);

								if (registryKeyDBName != null)
								{
									registryKeyDBName.SetValue(DBSchema, cryptPassword, RegistryValueKind.String);
									result = true;
									registryKeyDBName.Close();
								}
							}
						}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("SetCryptDBPassword()", exc);
				result = false;
			}
			finally
			{
				if (registryKeyDBName != null) registryKeyDBName.Close();
			}
			return result;
		}

		#region Save Logins

		public static Dictionary<string, string> GetUserLogins(string DBName, string DBSchema, out string primaryUser, bool isReadPass)
		{
			//Get users <users, password>
			Dictionary<string,string> users = new Dictionary<string, string>();
			string primUser = String.Empty;
			try
			{
				using (RegistryKey registryKeyConnect = GetRegADAICAConnectionPath())
				{
					if (registryKeyConnect != null)
					{
						using (RegistryKey registryKeyDBName = registryKeyConnect.OpenSubKey(DBName))
						{
							if (registryKeyDBName != null)
							{
								using (RegistryKey registryKeySchemaName = registryKeyDBName.OpenSubKey(DBSchema))
								{
									if (registryKeySchemaName != null)
									{
										foreach (string subKeyUserName in registryKeySchemaName.GetValueNames())
										{
											if (subKeyUserName != enUsersLoginKeys.UsersLoginIsPrimary)
											{
												object objValue = registryKeySchemaName.GetValue(subKeyUserName);
												if (objValue != null && (objValue is String))
													users.Add(subKeyUserName, isReadPass ? (string) objValue : String.Empty);

											}
											else
											{
												object objValue = registryKeySchemaName.GetValue(subKeyUserName);
												if (objValue != null && (objValue is String) && !String.IsNullOrEmpty(objValue.ToString()))
													primUser = (string) objValue;
											}
										}
									}

								}
							}
						}
					}
				}

			}
			catch (Exception exc)
			{
				CLog.Log("GetCryptDBPassword()", exc);
				users = null;
				primUser = String.Empty;
			}
			primaryUser = primUser;
			return users;
		}

		public static bool SetUserLogin(string DBName, string DBSchema, string userName, string password, bool isPrimaryUser)
		{
			bool isResult = false;
			try
			{
				using (RegistryKey registryKeySchema = GetRegADAICASchemaPath(DBName, DBSchema))
				{
					if (registryKeySchema != null)
					{
						if (isPrimaryUser)
							registryKeySchema.SetValue(enUsersLoginKeys.UsersLoginIsPrimary, userName);
						registryKeySchema.SetValue(userName, password, RegistryValueKind.String);
						isResult = true;
					}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("SetUserLogin(string DBName, string DBSchema, string userName, string password, bool isPrimaryUser)", exc);
				isResult = false;
			}
			return isResult;
		}

		

		#endregion Save Logins

		#region Database trace option

		public static bool SetDBTraceOpt(string DBName, string DBSchema, string userID, bool isStart)
		{
			bool isResult = false;
			try
			{
				using (RegistryKey registryKeySchema = GetRegADAICASchemaPath(DBName, DBSchema))
				{
					if (registryKeySchema != null)
					{
						using (RegistryKey registryKeyTrace = GetOrCreateRegKey(registryKeySchema, REG_ADAICA_DBTRACE_KEY))
						{
							if (registryKeyTrace != null)
							{
								if (isStart)
								{
									registryKeyTrace.SetValue(userID, "1", RegistryValueKind.String);
									isResult = true;
								}
								else
								{
									object objValue = registryKeyTrace.GetValue(userID);
									if (objValue != null)
										registryKeyTrace.DeleteValue(userID);
								}
							}
						}
					}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("SetDBTraceOpt(string DBName, string DBSchema, string userID)", exc);
				isResult = false;
			}
			return isResult;
		}

		public static bool IsDBTraceOn(string DBName, string DBSchema, string userID)
		{
			bool isResult = false;
			try
			{
				using (RegistryKey registryKeySchema = GetRegADAICASchemaPath(DBName, DBSchema))
				{
					if (registryKeySchema != null)
					{
						using (RegistryKey registryKeyTrace = GetOrCreateRegKey(registryKeySchema, REG_ADAICA_DBTRACE_KEY))
						{
							if (registryKeyTrace != null)
								isResult = registryKeyTrace.GetValue(userID) != null;
						}
					}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("IsDBTraceOn(string DBName, string DBSchema, string userID)", exc);
				isResult = false;
			}
			return isResult;
		}

		public static bool SetDBTraceOptForOSUser(string userID, bool isStart)
		{
			bool isResult = false;
			try
			{
				using (RegistryKey registryKeySchema = GetRegADAICAPath())
				{
					if (registryKeySchema != null)
					{
						using (RegistryKey registryKeyTrace = GetOrCreateRegKey(registryKeySchema, REG_ADAICA_DBTRACE_KEY))
						{
							if (registryKeyTrace != null)
							{
								if (isStart)
								{
									registryKeyTrace.SetValue(userID, "1", RegistryValueKind.String);
									isResult = true;
								}
								else
								{
									object objValue = registryKeyTrace.GetValue(userID);
									if (objValue != null)
										registryKeyTrace.DeleteValue(userID);
								}
							}
						}
					}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("SetDBTraceOpt(string DBName, string DBSchema, string userID)", exc);
				isResult = false;
			}
			return isResult;
		}

		public static bool IsDBTraceOnForOSUser(string userID)
		{
			bool isResult = false;
			try
			{
				using (RegistryKey registryKeySchema = GetRegADAICAPath())
				{
					if (registryKeySchema != null)
					{
						using (RegistryKey registryKeyTrace = GetOrCreateRegKey(registryKeySchema, REG_ADAICA_DBTRACE_KEY))
						{
							if (registryKeyTrace != null)
								isResult = registryKeyTrace.GetValue(userID) != null;
						}
					}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("IsDBTraceOn(string DBName, string DBSchema, string userID)", exc);
				isResult = false;
			}
			return isResult;
		}

		#endregion Database trace option

		#region ThemeName

		/// <summary>
		/// Get name of the last theme.
		/// </summary>
		/// <returns></returns>
		public static string GetLastThemeName()
		{
			string result = String.Empty;
			try
			{
				using (RegistryKey registryKeyConnect =  GetRegADAICAPath())
				{
					if (registryKeyConnect != null)
					{
						object objValue = registryKeyConnect.GetValue(REG_ADAICA_THEME_NAME_KEY);
						if (objValue != null)
						{
							result = objValue.ToString();
						}
					}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("GetLastThemeName()", exc);
			}
			if (String.IsNullOrEmpty(result))
				result = DEFAULT_THEME_NAME;
			return result;
		}

		/// <summary>
		/// Save the name of the theme to registry.
		/// </summary>
		/// <param name="themeName">Name of the theme.</param>
		/// <returns></returns>
		public static bool SetThemeName(string themeName)
		{
			bool isResult = false;
			string theme = String.IsNullOrEmpty(themeName) ? DEFAULT_THEME_NAME : themeName;
			try
			{
				using (RegistryKey registryKeySchema = GetRegADAICAPath())
				{
					if (registryKeySchema != null)
					{
						registryKeySchema.SetValue(REG_ADAICA_THEME_NAME_KEY, theme, RegistryValueKind.String);
						isResult = true;
					}
				}
			}
			catch (Exception exc)
			{
				CLog.Log("SetThemeName(string themeName)", exc);
				isResult = false;
			}
			return isResult;
		}

		#endregion

	}
}
