//-------------------------------------------------------------------
// <copyright file="CDataBase.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary>Holds class for accessing to DBMS</summary>
// <author>Anton Maryanov</author>
//-------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	/// Class for accessing DBMS. Based on ADO.NET 2.0 factory concept.
	/// This class has main connection object and collection of connection objects for using
	/// in simultanious transaction feature.
	/// If you wish to use transactions, than you should use <see cref="CTransaction"/>
	/// property to get transaction object, which then you can use to make changes on DB in transaction.
	/// </summary>
	/// <threadsafety static="true"/>
	[System.Runtime.InteropServices.GuidAttribute("84D6805D-96E1-4DD5-AD84-19A5B9957CF4")]
	public static class CDataBase
	{

		/// <summary>
		/// If query execution time greater than this constant, ERR message will be posted to log
		/// </summary>
		private const double CRITICAL_QUERY_EXECUTION_TIME_MS = 5000;

		/// <summary>
		/// Global transaction id constant
		/// </summary>
		private const string GLOBAL_TRANSACTION_ID = "Global";

		#region Private Fields
		/// <summary>
		/// Factory for data provider.
		/// </summary>
		private static DbProviderFactory modProvider;

		private static readonly CTransaction mvGlobalTransaction;

		/// <summary>
		/// The maximum count of simultenious transactions
		/// </summary>
		private const int modMaxCountOfSimultaneousTransactions = 10;

		/// <summary>
		/// List of connections for transactions.
		/// </summary>
		//private static List<DbConnection> modConnectionsForTransactions = new List<DbConnection>(modMaxCountOfSimultaneousTransactions);

		/// <summary>
		/// Mutexes for transaction connections. It is neccessary for locking appropriate transaction connection, so it will not be used in another thread
		/// </summary>
		//private static List<object> modConnectionsForTransactionsMutexes = new List<object>(modMaxCountOfSimultaneousTransactions);

		/// <summary>
		/// Command builder, use it for deriving command parameters and build SQL queries
		/// </summary>
		private static DbCommandBuilder modDBCommandBuilder;

		/// <summary>
		/// Connection to DBMS
		/// </summary>
		private static DbConnection modDBConnection;

		/// <summary>
		/// Transaction TODO: Need to do changes in logic of transactions
		/// </summary>
		private static DbTransaction modTransaction;

		/// <summary>
		/// Cache of stored procedures/function parameters.
		/// 
		/// </summary>
		private static Hashtable modParametersCache;

		/// <summary>
		/// Last Oracle error message
		/// </summary>
		private static string mvDBMessage;

		/// <summary>
		/// Method, that retrives information about modDBCommandBuilder.DeriveParameters
		/// method. It is neccessary to get this method at runtime, because abstract
		/// DbCommandBuilder does not provide this method.
		/// </summary>
		private static MethodInfo modDeriveParameters;

		/// <summary>
		/// Oracle scheme name
		/// </summary>
		private static string mvDataSource = String.Empty;

		/// <summary>
		/// DBMS User Name
		/// </summary>
		private static string mvUserID = "adaica";

		/// <summary>
		/// User password for mvUserID user.
		/// </summary>
		private static string mvPassword = String.Empty;

		/// <summary>
		/// User password for mvUserID user.
		/// </summary>
		//private static string mvPasswordCryptDefault = "Wfmc631p/HXqgqw30m/0Zw==";
		private static string mvPasswordCryptDefault = "WYybngNlhXnuhqgz1mvwYw==";
		private static string OracleDataAccessClient = "Oracle.DataAccess.Client";



		//Default Login
		private static string mvDefaultSchemaUser = "ADAICA_CONNECT";
		private static string mvDefaultSchemaPassword = "TIyMngVllHnfjtY7uGOQa6Ye+wqqKnqbT3NYCIPCXg8=";

		/// <summary>
		/// Whether we already said to user that we cant establish connection
		/// </summary>
		static bool alreadySaidAboutNullConnection = false;

		private static bool modIsStartConnection = true;

		//private static Dictionary<string, DbConnection> modTransactionConnections = null;
		private static Dictionary<string, CDbConnectionProperties> mvTransactionConnections = null;
		private static Dictionary<string, object> modTransactionConnectionsMutexes = null;

		private static string mvCurrentRealUserId;

		private static DateTime mvLastSuccessRequestTime;

		#endregion

		#region Delegates
		/// <summary>
		/// Handler for processing Result of execudion RDBMS operation.
		/// Use this delegate to pass result handler in threaded RDBMS operation execution.
		/// </summary>
		/// <param name="res">Result of execution</param>
		public delegate void ExampleCallback(ref object res);
		#endregion

		#region Constructors
		/// <summary>
		/// Static constructor for initializing fields.
		/// </summary>

		static void Alert(string mess, Exception exc)
		{
			string message = String.Format("{0} {1} {2}",
				mess,
				exc == null ? " success" : "not success: ",
				exc == null ? String.Empty : exc.Message);
			CLog.Log(LogType.DBG, message);
		}

		// This example assumes a reference to System.Data.Common.
		static DataTable GetProviderFactoryClasses()
		{
			// Retrieve the installed providers and factories.
			DataTable table = DbProviderFactories.GetFactoryClasses();

			// Display each row and column value.
			foreach (DataRow row in table.Rows)
			{
				foreach (DataColumn column in table.Columns)
				{
					Console.WriteLine(row[column]);
				}
			}
			return table;
		}

		/// <summary>
		/// CProvider	 class
		/// </summary>
		public class CProvider
		{
			/// <summary>
			/// Gets or sets the name.
			/// </summary>
			/// <value>
			/// The name.
			/// </value>
			public string Name { get; set; }
			/// <summary>
			/// Gets or sets the description.
			/// </summary>
			/// <value>
			/// The description.
			/// </value>
			public string Description { get; set; }
			/// <summary>
			/// Gets or sets the name of the invariant.
			/// </summary>
			/// <value>
			/// The name of the invariant.
			/// </value>
			public string InvariantName { get; set; }
			/// <summary>
			/// Gets or sets the name of the assembly qualified.
			/// </summary>
			/// <value>
			/// The name of the assembly qualified.
			/// </value>
			public string AssemblyQualifiedName { get; set; }
			/// <summary>
			/// Gets or sets the classes data row.
			/// </summary>
			/// <value>
			/// The classes data row.
			/// </value>
			public DataRow ClassesDataRow { get; set; }
		}

		/// <summary>
		/// colProviders class
		/// </summary>
		public class colProviders : List<CProvider>
		{

		}

		static CDataBase()
		{
			mvGlobalTransaction = new CTransaction { ID = GLOBAL_TRANSACTION_ID, IsOpenTransaction = true };

			try
			{
				var dataTable = GetProviderFactoryClasses();
				var dataColumns = dataTable.Columns;
				var tableRows = dataTable.Select();
				var providers = new colProviders();
				foreach (var tableRow in tableRows)
				{
					var name = tableRow[dataColumns["Name"]] as string;
					var description = tableRow[dataColumns["Description"]] as string;
					var invariantName = tableRow[dataColumns["InvariantName"]] as string;
					var assemblyQualifiedName = tableRow[dataColumns["AssemblyQualifiedName"]] as string;
					providers.Add(new CProvider()
													{
														Name = name,
														Description = description,
														InvariantName = invariantName,
														AssemblyQualifiedName = assemblyQualifiedName,
														ClassesDataRow = tableRow
													});
				}
				var oracleClients = providers.Where(item => item.InvariantName.ToLower().Contains("oracle"));
				foreach (var oracleClient in oracleClients)
				{
					try
					{
						modProvider = DbProviderFactories.GetFactory(oracleClient.ClassesDataRow);
						if (modProvider != null)
							break;
					}
					catch (Exception)
					{
						Alert(oracleClient.InvariantName + " faild", null);
					}
				}
				CLog.Log(LogType.DBG, "CDataBase(): Get provider start");
				modProvider = null;

				string mess = String.Empty;
				try
				{
					mess = " - Try GetFactory(Oracle.DataAccess.Client)";
					modProvider = DbProviderFactories.GetFactory(OracleDataAccessClient);
					Alert(mess, null);
				}
				catch (Exception exc)
				{
					Alert(mess, exc);
					Assembly amb = null;
					try
					{
						mess = @" - Try LoadFrom(.\Ora10\oracle.dataaccess.dll)";
						amb = Assembly.LoadFrom(@".\Ora10\oracle.dataaccess.dll");
						Alert(mess, null);
					}
					catch (Exception exc1)
					{
						Alert(mess, exc1);
						try
						{
							mess = "Load(oracle.dataaccess)";
							amb = Assembly.Load("oracle.dataaccess");
							Alert(mess, null);
						}
						catch (Exception exc2)
						{
							Alert(mess, exc2);
							//try
							//{
							//  mess = " - Try LoadWithPartialName(oracle.dataaccess)";
							//  amb = Assembly.LoadWithPartialName("oracle.dataaccess");
							//  Alert(mess,  null);
							//}
							//catch (Exception exc3) 
							//{
							//  Alert(mess,  exc3);
							//}
						}
					}
					if (amb != null)
					{
						try
						{
							mess = " - Try Oracle.DataAccess.Client.OracleClientFactory";
							//CLog.Log(LogType.DBG, " - Try Oracle.DataAccess.Client.OracleClientFactory");
							modProvider = (DbProviderFactory)amb.CreateInstance("Oracle.DataAccess.Client.OracleClientFactory");
							//CLog.Log(LogType.DBG, "success");
							Alert(mess, null);
						}
						catch (Exception exc4)
						{
							Alert(mess, exc4);
						}
					}
				}
				if (modProvider == null)
				{
					CLog.Log(new CDataBaseException("Can't load DB provider."));
					return;
				}
				modDBCommandBuilder = modProvider.CreateCommandBuilder();
				modDBConnection = modProvider.CreateConnection();
				modParametersCache = Hashtable.Synchronized(new Hashtable());
				modDeriveParameters = modDBCommandBuilder.GetType().GetMethod("DeriveParameters");
				mvDBMessage = string.Empty;
				int i;

				modTransactionConnectionsMutexes = new Dictionary<string, object>(modMaxCountOfSimultaneousTransactions);
				for (i = 0; i < modMaxCountOfSimultaneousTransactions; i++)
				{
					//modConnectionsForTransactionsMutexes.Add(new object());
					//modConnectionsForTransactions.Add(modProvider.CreateConnection());

					//modTransactionConnections.Add(i.ToString(), modProvider.CreateConnection());
					modTransactionConnectionsMutexes.Add(i.ToString(), new object());
				}
				//Try it, because this class can be used not only in WPF applications
				try
				{
                    //if (System.Windows.Application.Current != null) //it is null when run through unit test method
                    //    System.Windows.Application.Current.Exit += new ExitEventHandler(CloseConnections);
				}
				catch (Exception e)
				{
					CLog.Log("Can't add event hander on application closing. May be you are using CDataBase no in WPF environment?", e);
				}
			}
			catch (Exception exc)
			{
				CLog.Log("CDataBase()", exc);
				if (exc.Source == "Oracle Data Provider for .NET")
					CLog.Log("CDataBase", exc);
				else
					CLog.Log("Error", exc);
			}
		}
		#endregion

		#region Properties

		/// <summary>
		/// Gets global transaction
		/// </summary>
		public static CTransaction GlobalTransaction
		{
			[DebuggerStepThrough]
			get { return mvGlobalTransaction; }
		}

		#endregion // Properties

		#region Private Methods

		/// <summary>
		/// Closes the all connections.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.ExitEventArgs"/> instance containing the event data.</param>
		private static void CloseConnections(object sender, ExitEventArgs e)
		{
			if (mvIsTraceNeedOn)
				SetDatabaseTraceForAllConnection(false);

			if (modDBConnection.State != ConnectionState.Closed)
				modDBConnection.Close();

			//int i;
			//for (i = 0; i < modMaxCountOfSimultaneousTransactions; i++)
			//  if (modConnectionsForTransactions[i].State != ConnectionState.Closed)
			//    modConnectionsForTransactions[i].Close();
			if (mvTransactionConnections != null)
				foreach (var xConn in mvTransactionConnections.Values)
					if (xConn.DBConnection.State != ConnectionState.Closed)
						xConn.DBConnection.Close();
		}
		/// <summary>
		/// Check whether connection is open
		/// </summary>
		/// <returns>Opened or not connection</returns>
		private static bool isConnectOk(DbConnection connection)
		{
			return isConnectOk(connection, true);
		}
		/// <summary>
		/// Determines whether [is connect ok] [the specified connection].
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <param name="tryToConnect">if set to <c>true</c> [try to connect].</param>
		/// <returns>
		/// 	<c>true</c> if [is connect ok] [the specified connection]; otherwise, <c>false</c>.
		/// </returns>
		private static bool isConnectOk(DbConnection connection, bool tryToConnect)
		{
			if (connection == null)
				connection = modDBConnection;
			if (connection == null)
			{
				if (!alreadySaidAboutNullConnection && !modIsStartConnection)
				{
					MessageBox.Show("Can't establish connect to Database.", "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
					alreadySaidAboutNullConnection = true;
				}
				return false;
			}
			bool res = (connection.State != ConnectionState.Closed);
			mvDBMessage = string.Empty;
			if (connection.State == System.Data.ConnectionState.Closed
				&& tryToConnect)
			{
				res = connect(connection);
			}
			return res;
		}
		/// <summary>
		/// Determines whether [is connect ok].
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if [is connect ok]; otherwise, <c>false</c>.
		/// </returns>
		private static bool isConnectOk()
		{
			return isConnectOk(true);
		}
		/// <summary>
		/// Determines whether [is connect ok] [the specified try to connect].
		/// </summary>
		/// <param name="tryToConnect">if set to <c>true</c> [try to connect].</param>
		/// <returns>
		/// 	<c>true</c> if [is connect ok] [the specified try to connect]; otherwise, <c>false</c>.
		/// </returns>
		private static bool isConnectOk(bool tryToConnect)
		{
			return isConnectOk(modDBConnection, tryToConnect);
		}

		/// <summary>
		/// Wrapper for DbCommandBuilder method, that derives parameters for stored
		/// procedure/function
		/// </summary>
		/// <param name="command">Command for which we should retrive parameters</param>
		private static void DeriveParameters(DbCommand command)
		{
			modDeriveParameters.Invoke(null, new object[] { command });
		}

		/// <summary>
		/// Attaches parameters for appropriate command
		/// </summary>
		/// <param name="command">Command object</param>
		private static void attachParameters(DbCommand command)
		{
			try
			{
				//string hashKey = command.Connection.ConnectionString + ":" + command.CommandText;
				//DbParameter[] cachedParameters = modParametersCache[hashKey] as DbParameter[];
				//if (cachedParameters == null)
				//{
				DeriveParameters(command);
				//DbCommandBuilder.DeriveParameters(command);
				//  DbParameter[] discoveredParameters = new DbParameter[command.Parameters.Count];
				//  command.Parameters.CopyTo(discoveredParameters, 0);
				//  foreach (DbParameter discoveredParameter in discoveredParameters)
				//  {
				//    discoveredParameter.Value = DBNull.Value;
				//  }
				//  modParametersCache[hashKey] = discoveredParameters;
				//}
				//else
				//{
				//  foreach (DbParameter p in cachedParameters)
				//  {
				//    try
				//    {
				//      p.Value = null;
				//      if (!command.Parameters.Contains(p.ParameterName))
				//        command.Parameters.Add(p);
				//    }
				//    catch (Exception exc)
				//    {
				//    }
				//  }

				//foreach (DbParameter p in cachedParameters)
				//{
				//    if (command.Parameters.Contains(p))
				//        command.Parameters.Remove(p);
				//    p.Value = null;
				//    command.Parameters.Add(p);
				//}
				//}
			}
			catch (ThreadAbortException) { }
			catch (Exception exc)
			{
				CLog.Log("attachParameters() " + command.CommandText, exc, command.Connection);
				throw new CDataBaseException(command.CommandText, exc);
			}
		}

		/// <summary>
		/// Calls in thread method that doing operation on DBMS
		/// </summary>
		/// <param name="callback">Method that should be called with th result
		/// of DBMS operation</param>
		/// <param name="args_p">Parameters for method, that should be calling in new thread</param>
		/// <returns>Thread in which appropriate method is called</returns>
		private static Thread callInThread(ref ExampleCallback callback, object[] args_p)
		{
			Dictionary<string, object> args = new Dictionary<string, object>();
			args["params"] = args_p;
			args["callback"] = callback;
			string meth_name = (new StackFrame(1)).GetMethod().Name;
			if (meth_name.Substring(0, 5) == "begin")
			{
				Type[] parametersTypes = new Type[args_p.Length];
				for (int i = 0, j = args_p.Length; i < j; i++)
				{
					parametersTypes[i] = args_p[i].GetType();
				}
				args["meth"] = typeof(CDataBase).GetMethod(meth_name.Substring(5, 1).ToLower() + meth_name.Substring(6), parametersTypes);
			}
			Thread t = new Thread(threadedCall);
			t.IsBackground = true;
			t.Start(args);
			return t;
		}

		/// <summary>
		/// Method that called in new thread for calling appropriate
		/// method for doing operation on DBMS
		/// </summary>
		/// <param name="args1">A dictionary object, that contains appropriate
		/// method for doing operation on DBMS and parameters for this method</param>
		static void threadedCall(object args1)
		{
			Dictionary<string, object> args = (Dictionary<string, object>)args1;
			ExampleCallback clb = (ExampleCallback)args["callback"];
			MethodInfo mInfo = (MethodInfo)args["meth"];
			try
			{
				object res = mInfo.Invoke(null, (object[])args["params"]);
				clb(ref res);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public static string ConnectionString(bool isShowPassword)
		{
			string connectionString = String.Empty;
			try
			{
				connectionString =
					String.Format("Data Source= {0};Persist Security Info=True;User ID={1};Password={2};Pooling=True;Min Pool Size=1;Max Pool Size=25;Incr Pool Size=1",
					!String.IsNullOrEmpty(DataSource) ? DataSource : "*******",
					!String.IsNullOrEmpty(UserID) ? UserID : "*******",
					isShowPassword ? Password : "*******"
					);
			}
			catch (Exception exc)
			{
				connectionString = String.Empty;
				CLog.Log("ConnectionString()", exc);
			}
			return connectionString;
		}


		/// <summary>
		/// Initializes connection to DBMS
		/// </summary>
		/// <returns>Success or failure of initializing connection</returns>
		static bool connect(DbConnection connection)
		{
			bool result = false;

			try
			{


				if (!TryConnect(connection))
				{
					if (IsSchemaPasswordNotCorrect)
					{
						Password = PasswordDefault;
						IsLocalSchemaPasswordNotCorrect = true;
						result = TryConnect(connection);
					}
				}
				else
				{
					IsLocalSchemaPasswordNotCorrect = false;
					result = true;
				}
				//CLog.Log("ConnectionString for " + System.Windows.Application.ResourceAssembly.ManifestModule.Name +" "+ ConnectionString(false));
			}
			catch (Exception exc)
			{
				CLog.Log("connect(DbConnection connection)", exc);
			}

			return result;
		}

		public static void OnConnectSuccessfully()
		{
			OnConnectSuccessfully(modDBConnection);
		}

		private static void OnConnectSuccessfully(DbConnection con)
		{
			//set real user context
			if (!String.IsNullOrEmpty(CurrentRealUserId))
			{
				CDBParameters parameters;
				string procedureName = modSQL.sqlRealUserContextSet(CurrentRealUserId, out parameters);
				getScalar(procedureName, parameters, con);
			}
		}

		/// <summary>
		/// Tries the connect.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <returns></returns>
		static bool TryConnect(DbConnection connection)
		{
			bool res = false;
			if (init(connection))
			{
				try
				{
					IsSchemaPasswordNotCorrect = false;
					connection.Open();
					CLog.Log(LogType.INF, String.Format("ConnectionString: {0}", ConnectionString(false)));
					if (connection.State == ConnectionState.Open)
					{
						//doSql("ad_util.nls_sycnronize");
						res = true;

						if (UserID != DefaultSchemaUser)
						{
							// Set sid.

							if (mvSid == -1)
								//mvSid = (int)(decimal)getScalar("ad_tools.sid_get", connection);
								mvSid = (int)(decimal)getScalar("ad_tools.sid_union_get", connection);
							//MessageBox.Show("Time out connection.", "Connection", MessageBoxImage.Error, "OK Description", "Cancel Description");

							if (mvIsTraceNeedOn && connection == modDBConnection)
							{
								SetDatabaseTraceAction(true, connection);
							}
							OnConnectSuccessfully(connection);
						}

						alreadySaidAboutNullConnection = false;
						modIsStartConnection = false;
						//set real user context

					}
				}
				catch (Exception exc)
				{
					try
					{
						if (exc.Message.IndexOf("ORA-1017") != -1 || exc.Message.IndexOf("ORA-1005") != -1)
							IsSchemaPasswordNotCorrect = true;
						else
						{
							if (!alreadySaidAboutNullConnection)
							{
								MessageBox.Show("Time out connection.", "Connection", MessageBoxButton.OK, MessageBoxImage.Error);
								alreadySaidAboutNullConnection = true;
							}
						}
						mvDBMessage = exc.Message;

						CLog.Log("connect()", exc);
					}
					catch (Exception exc1)
					{
						CLog.Log("TryConnect(DbConnection connection): Catch", exc1);
					}
				}
			}

			return res;
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public static void Clear()
		{
			try
			{
				CloseConnections(null, null);
				mvTransactionConnections = null;
				mvSid = -1;
			}
			catch (Exception exc)
			{
				CLog.Log("Clear()", exc);
			}
		}

		/// <summary>
		/// Open connection for man connection object.
		/// </summary>
		/// <returns>Whether connection opened or no</returns>
		public static bool connect()
		{
			return connect(modDBConnection);
		}
		#endregion

		#region Public Properties

		public static Dictionary<string, CDbConnectionProperties> TransactionConnections
		{
			get { return mvTransactionConnections; }
		}

		/// <summary>
		/// Time of last successful request to DataBase
		/// </summary>
		public static DateTime LastSuccessRequestTime
		{
			get { return mvLastSuccessRequestTime; }
			set { mvLastSuccessRequestTime = value; }
		}

		/// <summary>
		/// Gets the transaction.
		/// </summary>
		/// <value>
		/// The transaction.
		/// </value>
		/// <example>
		///   <code>
		/// using LizenzaDevelopment.ADAICA.Core;
		/// class TestClass
		/// {
		/// static void Main(string[] args)
		/// {
		/// DbTransaction tr = CDataBase.Transaction;
		/// Thread threaderLog = new Thread( TestTransaction );
		/// threaderLog.IsBackground = true;
		/// threaderLog.Start( tr );
		/// }
		/// private static void TestTransaction( object objTransaction )
		/// {
		/// DbTransaction transaction = (DbTransaction) objTransaction;
		/// CLog.Log( "test from another thread", transaction.Connection );
		/// transaction.Dispose();
		/// }
		/// }
		///   </code>
		///   </example>

		//public static DbConnection Transaction
		//{
		//  get
		//  {
		//    DbConnection res = null;
		//    int i;
		//    DbConnection currentConnection;
		//    for (i = 0; i < modConnectionsForTransactions.Count; i++)
		//    {
		//      lock (modConnectionsForTransactionsMutexes[i])
		//      {
		//        currentConnection = modConnectionsForTransactions[i];
		//        if (currentConnection.State == ConnectionState.Closed)
		//        {
		//          if (connect(currentConnection))
		//          {
		//            currentConnection.BeginTransaction();
		//            res = currentConnection;
		//          }
		//        }
		//        else if (currentConnection.State == ConnectionState.Open &&
		//          currentConnection.CreateCommand().Transaction == null)
		//        {
		//          currentConnection.BeginTransaction();
		//          res = currentConnection;
		//        }
		//        if (res != null)
		//          break;
		//      }
		//    }
		//    return res;
		//  }
		//}

		private static string GetConnectionIdForTransaction()
		{
			string res = String.Empty; ;
			DbConnection currentConnection;

			if (mvTransactionConnections == null || mvTransactionConnections.Count < modMaxCountOfSimultaneousTransactions)
			{
				lock (modTransactionConnectionsMutexes)
				{
					if (mvTransactionConnections == null)
					{
						mvTransactionConnections = new Dictionary<string, CDbConnectionProperties>(modMaxCountOfSimultaneousTransactions);
						for (int i = 0; i < modMaxCountOfSimultaneousTransactions; i++)
						{
							CDbConnectionProperties newConn = new CDbConnectionProperties();
							newConn.DBConnectionID = i.ToString();
							DbConnection connection = modProvider.CreateConnection();
							newConn.DBConnection = connection;
							mvTransactionConnections.Add(i.ToString(), newConn);
						}
					}
				}
			}

			foreach (var kvp in mvTransactionConnections)
			{
				lock (modTransactionConnectionsMutexes[kvp.Key])
				{
					currentConnection = mvTransactionConnections[kvp.Key].DBConnection;
					if (currentConnection.State == ConnectionState.Closed)
					{
						if (connect(currentConnection))
						{
							////set real user context
							//if (!String.IsNullOrEmpty(CurrentRealUserId))
							//{
							//  CDBParameters parameters;
							//  string procedureName = modSQL.sqlRealUserContextSet(CurrentRealUserId, out parameters);
							//  getScalar(procedureName, parameters, modDBConnection);
							//}
							mvTransactionConnections[kvp.Key].DBConnectionSID = Sid(currentConnection).ToString();

							currentConnection.BeginTransaction();
							//OnConnectSuccessfully(currentConnection);
							res = kvp.Key;
						}
					}
					else
						if (currentConnection.State == ConnectionState.Open &&
							currentConnection.CreateCommand().Transaction == null)
						{
							currentConnection.BeginTransaction();
							//OnConnectSuccessfully(currentConnection);
							res = kvp.Key;
						}
					if (res != String.Empty)
						break;
				}
			}

			return res;
		}
		/// <summary>
		/// Last DBMS error message
		/// </summary>
		public static string DBMessage
		{
			get { return mvDBMessage; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is schema password not correct.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is schema password not correct; otherwise, <c>false</c>.
		/// </value>
		public static bool IsSchemaPasswordNotCorrect
		{
			get;
			internal set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is local schema password not correct.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is schema password is not correct; otherwise, <c>false</c>.
		/// </value>
		public static bool IsLocalSchemaPasswordNotCorrect
		{
			get;
			internal set;
		}

		public static string DefaultSchemaUser
		{
			get { return mvDefaultSchemaUser; }
			set { mvDefaultSchemaUser = value; }
		}

		public static string DefaultSchemaPassword
		{
			get { return mvDefaultSchemaPassword; }
			set { mvDefaultSchemaPassword = value; }
		}

		/// <summary>
		/// Get/Set Password
		/// </summary>
		public static string Password
		{
			get
			{
				//if (!String.IsNullOrEmpty(mvPassword))
				//  return modDefaultPassword;
				//else
				return mvPassword;
			}
			set { mvPassword = value; }
		}

		/// <summary>
		/// Gets the crypt value default password to schema.
		/// </summary>
		/// <value>The password crypt default.</value>
		public static string PasswordCryptDefault
		{
			get { return mvPasswordCryptDefault; }
		}

		/// <summary>
		/// Get/Set Default Password
		/// </summary>
		public static string PasswordDefault
		{
			get;
			set;
		}


		/// <summary>
		/// Get/Set User name
		/// </summary>
		public static string UserID
		{
			get { return mvUserID; }
			set { mvUserID = value; }
		}

		/// <summary>
		/// Get/Set Data Source (Scheme name)
		/// </summary>
		public static string DataSource
		{
			get { return mvDataSource; }
			set { mvDataSource = value; }
		}

		public static string CurrentRealUserId
		{
			get { return CDataBase.mvCurrentRealUserId; }
			set { CDataBase.mvCurrentRealUserId = value; }
		}

		public static bool AlreadySaidAboutNullConnection
		{
			get { return alreadySaidAboutNullConnection; }
			set { alreadySaidAboutNullConnection = value; }
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Get the session identificator for main connection object.
		/// </summary>
		/// <returns>Database session identificator</returns>
		public static int Sid()
		{
			if (mvSid == -1)
				mvSid = Sid(modDBConnection);
			return mvSid;
		}

		static int mvSid = -1;
		/// <summary>
		/// Get the session identificator for apropriate connection object.
		/// </summary>
		/// <param name="connection">The connection from which SID should be getten</param>
		/// <returns>Database session identificator</returns>
		public static int Sid(DbConnection connection)
		{
			int sid = -1;
			//if (isConnectOk(connection, true))
			if (isConnectOk(connection, false))
			{
				sid = (int)(decimal)getScalar("ad_tools.sid_get", connection);
			}
			return sid;
		}
		/// <summary>
		/// Initialize connection. Do not use it. Use isConnectionOk instead.
		/// </summary>
		/// <returns>Open or not connection</returns>
		public static bool init(DbConnection connection)
		{
			bool res = false;
			if ((connection == null) ||
				((connection.State != ConnectionState.Open) &&
				 (DataSource.Length != 0) &&
				 (UserID.Length != 0)))
			{
				/*if ( ConfigurationManager.ConnectionStrings["ADAICA.Properties.Settings.ADAICA"] != null )
					connection.ConnectionString = ConfigurationManager.ConnectionStrings["ADAICA.Properties.Settings.ADAICA"].ConnectionString;*/
				if (!String.IsNullOrEmpty(Password))
				{
					connection.ConnectionString = "Data Source=" + DataSource + ";Persist Security Info=True;User ID=" + UserID + ";Password=" + Password + ";Pooling=True;Min Pool Size=1;Max Pool Size=25;Incr Pool Size=1;Enlist=false";
					res = true;
				}
				else
					IsSchemaPasswordNotCorrect = true;
			}
			return res;
		}

		/// <summary>
		/// Sets ConnectionString value for main connection object.
		/// </summary>
		/// <returns>Whether initing was successful or not</returns>
		public static bool init()
		{
			return init(modDBConnection);
		}

		/// <summary>
		/// Closes main connection.
		/// </summary>
		public static void close()
		{
			if (isConnectOk(false))
			{
				if (mvIsTraceNeedOn)
					SetDatabaseTraceAction(false, modDBConnection);
				modDBConnection.Close();
			}
			mvSid = -1;
		}

		/// <summary>
		/// Get result of execution of stored procedures/functions as object.
		/// Be carefully, this method is only for stored procedures/functions.
		/// </summary>
		/// <param name="commandText">The name of stored procedure/function</param>
		/// <returns>Result of execution stored procedure/function</returns>
		public static object getScalar(string commandText)
		{
			return getScalar(commandText, (DbConnection)null);
		}

		/// <summary>
		/// Get result of execution of stored procedures/functions as object.
		/// Be carefully, this method is only for stored procedures/functions.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <param name="connection">Connection in which command should be run.</param>
		/// <returns>Result of execution stored procedure/function</returns>
		public static object getScalar(string commandText, DbConnection connection)
		{
			return getScalar(commandText, CommandType.StoredProcedure, connection);
		}

		/// <summary>
		/// Get result of SQL or PL/SQL.
		/// For SQL - returns the first column of the first row in the result set returned by the query.
		/// PL/SQL - Result of stored function.
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <returns>Result of execution SQL query or stored procedure/function</returns>
		public static object getScalar(string commandText, CommandType commandType)
		{
			return getScalar(commandText, commandType, (DbConnection)null);
		}

		public static object getScalar(string commandText, CommandType commandType, CTransaction transaction)
		{
			return getScalar(commandText, commandType, (DbConnection)null, transaction);
		}

		public static object getScalar(CDBQuery parameters, CTransaction transaction)
		{
			return getScalar(parameters.ProcedureName, parameters.Parameters, transaction);
		}

		public static object getScalar(string commandText, CDBParameters parameters, CTransaction transaction)
		{
			return getScalar(commandText, CommandType.StoredProcedure, parameters, null, transaction);
		}

		/// <summary>
		/// Get result of SQL or PL/SQL.
		/// For SQL - returns the first column of the first row in the result set returned by the query.
		/// PL/SQL - Result of stored function.
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="connection">The connection.</param>
		/// <returns>
		/// Result of execution SQL query or stored procedure/function
		/// </returns>
		public static object getScalar(string commandText, CommandType commandType, DbConnection connection)
		{
			return getScalar(commandText, commandType, (CDBParameters)null, connection, null);
		}

		public static object getScalar(string commandText, CommandType commandType, DbConnection connection, CTransaction transaction)
		{
			return getScalar(commandText, commandType, (CDBParameters)null, connection, transaction);
		}

		public static object getScalar(string commandText, CommandType commandType, DbConnection connection, CTransaction transaction, CCommandWrapper cmdWrapper)
		{
			return getScalar(commandText, commandType, (CDBParameters)null, connection, transaction, cmdWrapper);
		}

		/// <summary>
		/// Gets the scalar.
		/// </summary>
		/// <param name="commandText">The command text.</param>
		/// <param name="commandParameters">The command parameters.</param>
		/// <returns></returns>
		public static object getScalar(string commandText, CDBParameters commandParameters)
		{
			return getScalar(commandText, commandParameters, (CTransaction)null);
		}


		/// <summary>
		/// Get result of execution of stored procedures/functions as object.
		/// Be carefully, this method is only for stored procedures/functions.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <param name="commandParameters">Values of parameters, that should be passed to the
		/// stored procedure/function</param>
		/// <param name="connection">The connection.</param>
		/// <returns>
		/// Result of execution stored procedure/function
		/// </returns>
		public static object getScalar(string commandText, CDBParameters commandParameters, DbConnection connection)
		{
			return getScalar(commandText, CommandType.StoredProcedure, commandParameters, connection, null);
		}

		/// <summary>
		/// Get result of SQL or PL/SQL.
		/// For SQL - returns the first column of the first row in the result set returned by the query.
		/// PL/SQL - Result of stored function.
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="commandParameters">Values of parameters, that should be passed to the 
		/// stored procedure/function or SQL query</param>
		/// <returns>Result of execution stored procedure/function</returns>
		public static object getScalar(string commandText, CommandType commandType, CDBParameters commandParameters)
		{
			return getScalar(commandText, commandType, commandParameters, (DbConnection)null, null);
		}

		/// <summary>
		/// Gets the scalar.
		/// </summary>
		/// <param name="commandText">The command text.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandParameters">The command parameters.</param>
		/// <param name="connection">The connection.</param>
		/// <param name="transaction">The transaction.</param>
		/// <returns></returns>
		public static object getScalar(string commandText, CommandType commandType, CDBParameters commandParameters, DbConnection connection, CTransaction transaction)
		{
			return getScalar(commandText, commandType, commandParameters, connection, transaction, null);
		}

		/// <summary>
		/// Get result of SQL or PL/SQL.
		/// For SQL - returns the first column of the first row in the result set returned by the query.
		/// PL/SQL - Result of stored function.
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="commandParameters">Values of parameters, that should be passed to the
		/// stored procedure/function or SQL query</param>
		/// <param name="connection">The connection.</param>
		/// <returns>
		/// Result of execution SQL query or stored procedure/function
		/// </returns>
		public static object getScalar(string commandText, CommandType commandType, CDBParameters commandParameters, DbConnection connection, CTransaction transaction, CCommandWrapper cmdWrapper)
		{
			object result = null;
			DbCommand command = null;
			if (connection == null)
				connection = modDBConnection;

			try
			{
				if (transaction != null && transaction.ID != GLOBAL_TRANSACTION_ID)
				{
					if (!transaction.IsOpenTransaction || String.IsNullOrEmpty(transaction.ID))
						throw new CDataBaseException("Trasaction is not open or transaction ID is empty");
					else
						if (mvTransactionConnections.ContainsKey(transaction.ID))
							connection = mvTransactionConnections[transaction.ID].DBConnection;
				}

				//if (commandText=="AD_UTIL.option_value_get")
				//    throw new Exception("ORA-12342: Error");

				if (isConnectOk(connection))
				{
					command = modProvider.CreateCommand();

					if (cmdWrapper != null)
						cmdWrapper.Cmd = command;

					command.CommandText = commandText;
					command.CommandType = commandType;

					command.Connection = connection;
					//command.Transaction = modTransaction;

					Stopwatch sw = new Stopwatch();
					sw.Start();

					if (commandType == CommandType.StoredProcedure)
					{
						attachParameters(command);
						if (command.Parameters["RETURN_VALUE"] != null)
						{
							if (commandParameters != null)
							{
								foreach (KeyValuePair<string, object> kvp in commandParameters)
								{
									if (command.Parameters[kvp.Key] != null)
										command.Parameters[kvp.Key].Value = kvp.Value;
									else
										command.Parameters.Add(kvp.Value);
								}
							}
							command.ExecuteNonQuery();
							result = command.Parameters["RETURN_VALUE"].Value;
						}
						if (commandParameters != null)
						{
							foreach (DbParameter p in command.Parameters)
							{
								if (p.Direction == ParameterDirection.Output ||
										p.Direction == ParameterDirection.InputOutput)
									commandParameters[p.ParameterName] = p.Value;
							}
						}
						command.Parameters.Clear();
					}
					else
					{
						if (commandParameters != null)
						{
							foreach (KeyValuePair<string, object> kvp in commandParameters)
							{
								command.Parameters.Add(kvp.Value);
							}
						}
						result = command.ExecuteScalar();
					}

					if (cmdWrapper != null)
						cmdWrapper.Cmd = null;

					command.Parameters.Clear();
					command.Dispose();

					sw.Stop();

					if (CLog.LogLevel == LogType.DBG)
					{
						CLogPerformance.WriteMessage(commandText, commandParameters == null ? String.Empty : commandParameters.ToParamString(), sw.ElapsedMilliseconds.ToString());
					}

					if (sw.ElapsedMilliseconds > CRITICAL_QUERY_EXECUTION_TIME_MS)
					{
						string paramsString = string.Empty;
						if (commandParameters != null)
						{
							paramsString = commandParameters.ToString();
						}

						CLog.Log(command.CommandText + "(" + paramsString + ")", new System.TimeoutException("CRITICAL_QUERY_EXECUTION_TIME_MS exceeded"), connection);
					}
				}
				LastSuccessRequestTime = DateTime.Now;
				return result;
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (CDataBaseException exc)
			{
				string paramsString;
				string paramsParamString;
				ExtractCommandParametersString(commandParameters, out paramsString, out paramsParamString);

				CLog.Log(command.CommandText + "(" + paramsString + ")", exc, connection);
				CheckIfDBPackageError(exc, command.CommandText);
				CDataBaseException dataBaseException = new CDataBaseException(command.CommandText + "(" + paramsString + ")" + " DB Parameters " + paramsParamString, exc);
				CDataBaseExceptionsList.Add(transaction, dataBaseException);
				throw dataBaseException;
				//throw exc;
			}
			catch (Exception exc)
			{
				if (command != null)
				{
					command.Parameters.Clear();
					command.Dispose();
				}

				string paramsString;
				string paramsParamString;
				ExtractCommandParametersString(commandParameters, out paramsString, out paramsParamString);

				if (!String.IsNullOrEmpty(exc.Message) && exc.Message.IndexOf("ORA-00054") == -1)
					CLog.Log(command.CommandText + "(" + paramsString + ")", exc, connection);
				CheckIfDBPackageError(exc, command.CommandText);
				CDataBaseException dataBaseException = new CDataBaseException(command.CommandText + "(" + paramsString + ")" + " DB Parameters " + paramsParamString, exc);
				CDataBaseExceptionsList.Add(transaction, dataBaseException);
				throw dataBaseException;
			}
			//return null;
		}

		/// <summary>
		/// Result of stored procedure/function, which return type is SYS_REFCURSOR
		/// Be carefully, this method is only for stored procedures/functions.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <returns>A result set of records</returns>
		public static DbDataReader getReader(string commandText)
		{
			return getReader(commandText, (CCommandWrapper)null);
		}

		public static DbDataReader getReader(string commandText, CCommandWrapper cmdWrapper)
		{
			return getReader(commandText, (DbConnection)null, cmdWrapper);
		}
		/// <summary>
		/// Result of stored procedure/function, which return type is SYS_REFCURSOR
		/// Be carefully, this method is only for stored procedures/functions.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <param name="connection">Connection on which command should be run</param>
		/// <returns></returns>
		public static DbDataReader getReader(string commandText, DbConnection connection)
		{
			return getReader(commandText, connection, (CCommandWrapper)null);
		}
		public static DbDataReader getReader(string commandText, DbConnection connection, CCommandWrapper cmdWrapper)
		{
			return getReader(commandText, CommandType.StoredProcedure, connection, cmdWrapper);
		}

		/// <summary>
		/// Get a set of records.
		/// For SQL - result of execution SELECT statement.
		/// For PL/SQL - result of execution stored procedure/function,
		/// which return type is SYS_REFCURSOR
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <returns>A result set of records</returns>
		public static DbDataReader getReader(string commandText, CommandType commandType)
		{
			return getReader(commandText, commandType, (CCommandWrapper)null);
		}
		public static DbDataReader getReader(string commandText, CommandType commandType, CCommandWrapper cmdWrapper)
		{
			return getReader(commandText, commandType, (DbConnection)null, cmdWrapper);
		}
		/// <summary>
		/// Get a set of records.
		/// For SQL - result of execution SELECT statement.
		/// For PL/SQL - result of execution stored procedure/function,
		/// which return type is SYS_REFCURSOR
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="connection">Connection on which command should be run</param>
		/// <returns></returns>
		public static DbDataReader getReader(string commandText, CommandType commandType, DbConnection connection)
		{
			return getReader(commandText, commandType, connection, (CCommandWrapper)null);
		}
		public static DbDataReader getReader(string commandText, CommandType commandType, DbConnection connection, CCommandWrapper cmdWrapper)
		{
			return getReader(commandText, commandType, (CDBParameters)null, connection, cmdWrapper, (CTransaction)null);
		}

		public static DbDataReader getReader(string commandText, CommandType commandType, DbConnection connection, CCommandWrapper cmdWrapper, CTransaction transaction)
		{
			return getReader(commandText, commandType, (CDBParameters)null, connection, cmdWrapper, transaction);
		}

		/// <summary>
		/// Result of stored procedure/function, which return type is SYS_REFCURSOR
		/// Be carefully, this method is only for stored procedures/functions.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <param name="commandParameters">Parameters values for stored procedure/function</param>
		/// <returns>A result set of records</returns>
		public static DbDataReader getReader(string commandText, CDBParameters commandParameters)
		{
			return getReader(commandText, commandParameters, (CCommandWrapper)null);
		}

		public static DbDataReader getReader(CDBQuery dbQuery)
		{
			return getReader(dbQuery.ProcedureName, dbQuery.Parameters);
		}

		public static DbDataReader getReader(CDBQuery dbQuery, CTransaction transaction)
		{
			return getReader(dbQuery.ProcedureName, dbQuery.Parameters, null, null, transaction);
		}

		public static DbDataReader getReader(string commandText, CDBParameters commandParameters, CTransaction transaction)
		{
			return getReader(commandText, commandParameters, null, null, transaction);
		}

		//+++++++++++++++++++++++++++++++++++++++
		public static DbDataReader getReader(string commandText, CDBParameters commandParameters, CCommandWrapper cmdWrapper)
		{
			return getReader(commandText, commandParameters, (DbConnection)null, cmdWrapper);
		}
		/// <summary>
		/// Result of stored procedure/function, which return type is SYS_REFCURSOR
		/// Be carefully, this method is only for stored procedures/functions.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <param name="commandParameters">Parameters values for stored procedure/function</param>
		/// <param name="connection">Connection on which command should be run</param>
		/// <returns></returns>
		public static DbDataReader getReader(string commandText, CDBParameters commandParameters, DbConnection connection)
		{
			return getReader(commandText, commandParameters, connection, (CCommandWrapper)null);
		}
		public static DbDataReader getReader(string commandText, CDBParameters commandParameters, DbConnection connection, CCommandWrapper cmdWrapper)
		{
			return getReader(commandText, CommandType.StoredProcedure, commandParameters, connection, cmdWrapper, (CTransaction)null);
		}

		public static DbDataReader getReader(string commandText, CDBParameters commandParameters, DbConnection connection, CCommandWrapper cmdWrapper, CTransaction transaction)
		{
			return getReader(commandText, CommandType.StoredProcedure, commandParameters, connection, cmdWrapper, transaction);
		}

		/// <summary>
		/// Get a set of records.
		/// For SQL - result of execution SELECT statement.
		/// For PL/SQL - result of execution stored procedure/function,
		/// which return type is SYS_REFCURSOR
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="commandParameters">Parameters values for stored procedure/function
		/// or SQL query</param>
		/// <returns>A result set of records</returns>
		public static DbDataReader getReader(string commandText, CommandType commandType, CDBParameters commandParameters)
		{
			return getReader(commandText, commandType, commandParameters, (CCommandWrapper)null);
		}
		public static DbDataReader getReader(string commandText, CommandType commandType, CDBParameters commandParameters, CCommandWrapper cmdWrapper)
		{
			return getReader(commandText, commandType, commandParameters, (DbConnection)null, cmdWrapper, (CTransaction)null);
		}

		public static DbDataReader getReader(string commandText, CommandType commandType, CDBParameters commandParameters, CCommandWrapper cmdWrapper, CTransaction transaction)
		{
			return getReader(commandText, commandType, commandParameters, (DbConnection)null, cmdWrapper, transaction);
		}
		/// <summary>
		/// Get a set of records.
		/// For SQL - result of execution SELECT statement.
		/// For PL/SQL - result of execution stored procedure/function,
		/// which return type is SYS_REFCURSOR
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="commandParameters">Parameters values for stored procedure/function
		/// or SQL query</param>
		/// <param name="connection">Connection on which command should be run</param>
		/// <returns>A result set of records</returns>
		public static DbDataReader getReader(string commandText, CommandType commandType, CDBParameters commandParameters, DbConnection connection, CCommandWrapper cmdWrapper, CTransaction transaction)
		{
			DbDataReader result = null;
			DbCommand command = null;
			if (connection == null)
				connection = modDBConnection;

			try
			{
				if (transaction != null && transaction.ID != GLOBAL_TRANSACTION_ID)
				{
					if (!transaction.IsOpenTransaction || String.IsNullOrEmpty(transaction.ID))
						throw new CDataBaseException("Trasaction is not open or transaction ID is empty");
					else if (mvTransactionConnections.ContainsKey(transaction.ID))
						connection = mvTransactionConnections[transaction.ID].DBConnection;
				}

				if (isConnectOk(connection))
				{
					Stopwatch sw = new Stopwatch();
					sw.Start();

					command = modProvider.CreateCommand();
					if (cmdWrapper != null)
						cmdWrapper.Cmd = command;
					command.CommandText = commandText;
					command.Connection = connection;
					command.CommandType = commandType;
					if (command.CommandType == CommandType.StoredProcedure)
						attachParameters(command);
					if (command.CommandType == CommandType.Text || (command.CommandType == CommandType.StoredProcedure && command.Parameters["RETURN_VALUE"] != null))
					{
						if (commandParameters != null)
						{
							foreach (KeyValuePair<string, object> kvp in commandParameters)
							{
								if (command.Parameters[kvp.Key] != null)
									command.Parameters[kvp.Key].Value = kvp.Value;
								else
									command.Parameters.Add(kvp.Value);
							}
						}

						result = command.ExecuteReader();

						if (commandParameters != null)
						{
							foreach (DbParameter p in command.Parameters)
							{
								if (p.Direction == ParameterDirection.Output ||
								    p.Direction == ParameterDirection.InputOutput)
									commandParameters[p.ParameterName] = p.Value;
							}
						}
					}
					if (cmdWrapper != null)
						cmdWrapper.Cmd = null;

					command.Parameters.Clear();
					command.Dispose();


					sw.Stop();

					if (CLog.LogLevel == LogType.DBG)
					{
						CLogPerformance.WriteMessage(commandText, commandParameters == null ? String.Empty : commandParameters.ToParamString(), sw.ElapsedMilliseconds.ToString());
					}

					if (sw.ElapsedMilliseconds > CRITICAL_QUERY_EXECUTION_TIME_MS)
					{
						string paramsStr = string.Empty;
						if (commandParameters != null)
							paramsStr = commandParameters.ToString();

						CLog.Log(command.CommandText + "(" + paramsStr + ")", new System.TimeoutException("CRITICAL_QUERY_EXECUTION_TIME_MS exceeded"), connection);
					}
				}
				LastSuccessRequestTime = DateTime.Now;
			}
			catch (Exception exc)
			{
				if (command != null)
				{
					if (cmdWrapper != null)
						cmdWrapper.Cmd = null;

					command.Parameters.Clear();
					command.Dispose();
				}

				if (!exc.Message.Contains(CCoreConstants.cErrOracleUserRequestedCancel)
						&& !exc.Message.Contains(CCoreConstants.cErrOracleErrorInExecutingODCIIndexStartRoutine)
					)
				{
					string paramsString;
					string paramsParamString;
					ExtractCommandParametersString(commandParameters, out paramsString, out paramsParamString);

					CheckIfAbortedProcedure(exc, commandText);
					// do not write to log if resources are busy
					if (!exc.Message.Contains(CCoreConstants.cErrOracleResourseBusy))
					{
						if (exc is ThreadAbortException)
						{
							CLog.Log(LogType.DBG, commandText + "(" + paramsString + ")", connection);
						}
						else
						{
							CLog.Log(commandText + "(" + paramsString + ")", exc, connection);
						}
					}
					CheckIfDBPackageError(exc, commandText);
					throw new CDataBaseException(commandText + "(" + paramsString + ")" + " DB Parameters " + paramsParamString, exc);
				}
				else if (
					(exc.Message.Contains(CCoreConstants.cErrOracleErrorWildCardQuaryMaxRecords) || exc.Message.Contains(CCoreConstants.cErrOracleErrorTextQueryParseError)) &&
					(commandText == CCoreConstants.cProcedureNameForOracleErrorWildCardQuaryMaxRecords || commandText == CCoreConstants.cProcedureNameNewForOracleErrorWildCardQuaryMaxRecords))
				{
					throw new CDataBaseException(CCoreConstants.cErrOracleErrorWildCardQuaryMaxRecords);
				}
				else
					result = null;
			}
			return result;
		}

		private static void CheckIfDBPackageError(Exception exc, string packageName)
		{
			if (exc == null)
				return;
			if (exc.Message.Contains("ORA-04063"))
			{
				string packageN = packageName.Split('.')[0];
				MessageBox.Show(String.Format("Package: {0} has error. ADAICA will be stoped! Please contact with Administrator", packageN), "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
				IsCriticalDBError = true;
				Application.Current.MainWindow.Close();
			}
		}

		public static bool IsCriticalDBError { get; set; }

		/// <summary>
		/// Execute stored proccedure/function, result of which is count of changed records.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <returns>Number of affected rows</returns>
		public static int doSql(string commandText)
		{
			return doSql(commandText, (DbConnection)null);
		}
		/// <summary>
		/// Execute stored proccedure/function, result of which is count of changed records.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function.</param>
		/// <param name="connection">Connection on which command should be run</param>
		/// <returns>Number of affected rows</returns>
		public static int doSql(string commandText, DbConnection connection)
		{
			return doSql(commandText, CommandType.StoredProcedure, connection);
		}

		/// <summary>
		/// Execute SQL query or stored proccedure/function, which make changes on records
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <returns>Number of affected rows</returns>
		public static int doSql(string commandText, CommandType commandType)
		{
			return doSql(commandText, commandType, (DbConnection)null);
		}
		/// <summary>
		/// Execute SQL query or stored proccedure/function, which make changes on records
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="connection">Connection on which command should be run</param>
		/// <returns>Connection on which command should be run</returns>
		public static int doSql(string commandText, CommandType commandType, DbConnection connection)
		{
			return doSql(commandText, commandType, (CDBParameters)null, connection, (CCommandWrapper)null);
		}

		/// <summary>
		/// Execute stored proccedure/function, result of which is count of changed records.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <param name="commandParameters">Parameters values for stored procedure/function</param>
		/// <returns>Number of affected rows</returns>
		public static int doSql(string commandText, CDBParameters commandParameters)
		{
			return doSql(commandText, commandParameters, (DbConnection)null);
		}
		/// <summary>
		/// Execute stored proccedure/function, result of which is count of changed records.
		/// </summary>
		/// <param name="commandText">Name of stored procedure/function</param>
		/// <param name="commandParameters">Parameters values for stored procedure/function</param>
		/// <param name="connection">Connection on which command should be run</param>
		/// <returns>Number of affected rows</returns>
		public static int doSql(string commandText, CDBParameters commandParameters, DbConnection connection)
		{
			return doSql(commandText, CommandType.StoredProcedure, commandParameters, connection, (CCommandWrapper)null);
		}

		/// <summary>
		/// Execute SQL query or stored proccedure/function, which make changes on records
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="commandParameters">Parameters values for stored procedure/function
		/// or SQL query</param>
		/// <returns>Number of affected rows</returns>
		public static int doSql(string commandText, CommandType commandType, CDBParameters commandParameters)
		{
			return doSql(commandText, commandType, commandParameters, (DbConnection)null, (CCommandWrapper)null);
		}

		public static int doSql(string commandText, CommandType commandType, CDBParameters commandParameters, string transactionID)
		{
			DbConnection connection = null;
			if (mvTransactionConnections.ContainsKey(transactionID))
				connection = mvTransactionConnections[transactionID].DBConnection;

			return doSql(commandText, commandType, commandParameters, connection, (CCommandWrapper)null);
		}
		/// <summary>
		/// Execute SQL query or stored proccedure/function, which make changes on records
		/// </summary>
		/// <param name="commandText">SQL query or name of stored procedure/function</param>
		/// <param name="commandType">Type of command, one of CommandType.StoredProcedure
		/// or CommandType.Text</param>
		/// <param name="commandParameters">Parameters values for stored procedure/function
		/// or SQL query</param>
		/// <param name="connection">Connection on which command should be run</param>
		/// <returns>Number of affected rows</returns>
		public static int doSql(string commandText, CommandType commandType, CDBParameters commandParameters, DbConnection connection, CCommandWrapper cmdWrapper)
		{
			int result = -1;
			DbCommand command = null;
			if (connection == null)
				connection = modDBConnection;
			try
			{
				if (isConnectOk(connection))
				{
					command = modProvider.CreateCommand();
					if (cmdWrapper != null)
						cmdWrapper.Cmd = command;
					command.CommandText = commandText;
					command.Connection = connection;
					command.CommandType = commandType;
					if (command.CommandType == CommandType.StoredProcedure)
						attachParameters(command);
					if (commandParameters != null)
					{
						foreach (KeyValuePair<string, object> kvp in commandParameters)
						{
							if (command.Parameters[kvp.Key] != null)
								command.Parameters[kvp.Key].Value = kvp.Value;
							else
								command.Parameters.Add(kvp.Value);
						}
					}
					if (command.CommandType == CommandType.Text || (command.CommandType == CommandType.StoredProcedure && command.Parameters["RETURN_VALUE"] != null))
						result = command.ExecuteNonQuery();
					else
						command.ExecuteNonQuery();
					if (commandParameters != null)
					{
						foreach (DbParameter p in command.Parameters)
						{
							if (p.Direction == ParameterDirection.Output ||
								p.Direction == ParameterDirection.InputOutput)
								commandParameters[p.ParameterName] = p.Value;
						}
					}
					if (cmdWrapper != null)
						cmdWrapper.Cmd = null;
					command.Parameters.Clear();
					command.Dispose();
				}
				LastSuccessRequestTime = DateTime.Now;
			}
			catch (Exception exc)
			{
				//CLog.Log(exc);
				if (command != null)
				{
					if (cmdWrapper != null)
						cmdWrapper.Cmd = null;
					command.Parameters.Clear();
					command.Dispose();
				}

				string paramsString;
				string paramsParamString;
				ExtractCommandParametersString(commandParameters, out paramsString, out paramsParamString);

				CLog.Log(command.CommandText + "(" + paramsString + ")", exc, connection);
				CheckIfDBPackageError(exc, command.CommandText);
				throw new CDataBaseException(command.CommandText + "(" + paramsString + ")" + " DB Parameters " + paramsParamString, exc);
			}
			return result;
		}

		/// <summary>
		/// Close connection in new thread.
		/// </summary>
		/// <param name="callback">Method that should be called with result of closing.</param>
		/// <param name="args">Parameters for Close method.</param>
		/// <returns>New thread.</returns>
		public static Thread beginClose(ref ExampleCallback callback, params object[] args)
		{
			return callInThread(ref callback, args);
		}

		/// <summary>
		/// Gets result set of records in new thread.
		/// </summary>
		/// <param name="callback">Method that should be called with result set.</param>
		/// <param name="args">Parameters for GetReader method.</param>
		/// <returns>New thread.</returns>
		public static Thread beginGetReader(ref ExampleCallback callback, params object[] args)
		{
			return callInThread(ref callback, args);
		}

		/// <summary>
		/// Get result of SQL or PL/SQL.
		/// For SQL - returns the first column of the first row in the result set returned by the query.
		/// PL/SQL - Result of stored function.
		/// </summary>
		/// <param name="callback">Method that should be called with result.</param>
		/// <param name="args">Parameters for GetScalar method.</param>
		/// <returns>New thread.</returns>
		public static Thread beginGetScalar(ref ExampleCallback callback, params object[] args)
		{
			return callInThread(ref callback, args);
		}

		/// <summary>
		/// Execute stored proccedure/function, result of which is count of changed records.
		/// </summary>
		/// <param name="callback">Method that should be called with result.</param>
		/// <param name="args">Parameters for DoSql method.</param>
		/// <returns>New thread.</returns>
		public static Thread beginDoSql(ref ExampleCallback callback, params object[] args)
		{
			return callInThread(ref callback, args);
		}



		/// <summary>
		/// Commit a transaction.
		/// </summary>
		public static void CommitTransaction()
		{
			modTransaction.Commit();
		}

		/// <summary>
		/// Rallback a transaction.
		/// </summary>
		public static void RollbackTransaction()
		{
			modTransaction.Rollback();
		}

		/// <summary>
		/// Open a transaction
		/// </summary>
		public static void OpenTransaction()
		{
			modTransaction = modDBConnection.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		internal static void CommitTransaction(string connectionID)
		{
			if (mvTransactionConnections.ContainsKey(connectionID))
				mvTransactionConnections[connectionID].DBConnection.CreateCommand().Transaction.Commit();
		}

		/// <summary>
		/// Rallback a transaction.
		/// </summary>
		internal static void RollbackTransaction(string connectionID)
		{
			if (mvTransactionConnections.ContainsKey(connectionID))
				mvTransactionConnections[connectionID].DBConnection.CreateCommand().Transaction.Rollback();
		}

		/// <summary>
		/// Open a transaction
		/// </summary>
		internal static string OpenTransaction2()
		{
			string res = String.Empty;

			res = GetConnectionIdForTransaction();

			return res;
		}

		public delegate void DependencyDelegate(object sender, EventArgs args);

		public static string RegisterForChanges(string commandText, DependencyDelegate handler)
		{
			return RegisterForChanges(commandText, CommandType.StoredProcedure, (CDBParameters)null, handler, (DbConnection)null);
		}
		public static void UnregisterForChanges(string key)
		{
			UnregisterForChanges(key, (DbConnection)null);
		}
		public static void UnregisterForChanges(string key, DbConnection connection)
		{
			try
			{
				if (modDependencies.ContainsKey(key))
				{
					object obj = modDependencies[key];
					if (connection == null)
						connection = modDBConnection;
					obj.GetType().GetMethod("RemoveRegistration").Invoke(obj, new object[] { connection });
					modDependencies.Remove(key);
				}
			}
			catch (Exception exc)
			{
				CLog.Log(exc, connection);
				throw new CDataBaseException(exc);
			}
		}
		private static Dictionary<string, object> modDependencies = new Dictionary<string, object>();

		public static string RegisterForChanges(string commandText, CommandType commandType, DependencyDelegate handler)
		{
			return RegisterForChanges(commandText, commandType, (CDBParameters)null, handler);
		}
		public static string RegisterForChanges(string commandText, CommandType commandType, CDBParameters commandParameters, DependencyDelegate handler)
		{
			return RegisterForChanges(commandText, commandType, commandParameters, handler, (DbConnection)null);
		}
		public static string RegisterForChanges(string commandText, CommandType commandType, CDBParameters commandParameters, DependencyDelegate handler, DbConnection connection)
		{
			string res = string.Empty;
			DbCommand command = null;
			try
			{
				if (connection == null)
					connection = modDBConnection;
				if (isConnectOk(connection))
				{
					command = modProvider.CreateCommand();
					command.CommandText = commandText;
					command.Connection = connection;
					command.CommandType = commandType;
					command.GetType().GetProperty("AddRowid").SetValue(command, true, null);
					if (command.CommandType == CommandType.StoredProcedure)
						attachParameters(command);
					if (commandParameters != null)
						foreach (KeyValuePair<string, object> kvp in commandParameters)
							if (command.Parameters[kvp.Key] != null)
								command.Parameters[kvp.Key].Value = kvp.Value;
							else
								command.Parameters.Add(kvp.Value);
					Assembly asb = modProvider.GetType().Assembly;
					Type oraDep = asb.GetType("Oracle.DataAccess.Client.OracleDependency");
					object oraDepObj = oraDep.GetConstructor(new Type[] { asb.GetType("Oracle.DataAccess.Client.OracleCommand"), typeof(bool), typeof(int), typeof(bool) })
						.Invoke(new object[] { command, false, 0, true });
					res = (string)oraDep.GetProperty("Id").GetValue(oraDepObj, null);
					modDependencies.Add(res, oraDepObj);
					/*Random rand = new Random();
					while(true)
					{
						res = rand.Next();
						try
						{
							modDependencies.Add( oraDep.GetProperty( "Id" ).GetValue( oraDepObj , null), oraDepObj );
							break;
						}
						catch{}
					}*/
					EventInfo evt = oraDep.GetEvent("OnChange");
					evt.AddEventHandler(oraDepObj, Delegate.CreateDelegate(evt.EventHandlerType, handler.Method));

					command.ExecuteNonQuery();
					command.Parameters.Clear();
					command.Dispose();
				}
			}
			catch (Exception exc)
			{
				if (command != null)
				{
					command.Parameters.Clear();
					command.Dispose();
				}

				string paramsString;
				string paramsParamString;
				ExtractCommandParametersString(commandParameters, out paramsString, out paramsParamString);

				CLog.Log(command.CommandText + "(" + paramsString + ")", exc, connection);
				throw new CDataBaseException(command.CommandText + "(" + paramsString + ")" + " DB Parameters " + paramsParamString, exc);
			}
			return res;
		}
		#endregion

		public static bool IsCorrectClientInstalled()
		{
			try
			{
				DbProviderFactories.GetFactory(OracleDataAccessClient);
			}
			catch
			{
				return false;
			}
			return true;
		}

		#region Database Trace

		private static bool mvIsTraceNeedOn = false;
		private static bool mvIsTraceOn = false;

		public static bool IsTraceNeedOn
		{
			get { return mvIsTraceNeedOn; }
			set { mvIsTraceNeedOn = value; }
		}

		public static bool IsTraceOn
		{
			get { return mvIsTraceOn; }
			set { mvIsTraceOn = value; }
		}

		public static bool SetDatabaseTraceAction(bool isTraceStart, CTransaction transaction)
		{
			return SetDatabaseTraceAction(isTraceStart, transaction, null);
		}

		public static bool SetDatabaseTraceAction(bool isTraceStart, DbConnection connection)
		{
			return SetDatabaseTraceAction(isTraceStart, null, connection);
		}

		public static bool SetDatabaseTraceAction(bool isTraceStart, CTransaction transaction, DbConnection connection)
		{
			bool isResult = false;
			try
			{
				CDBQuery p = isTraceStart
											? modSQL.sqlDatabaseTraceStartForCurrentConnection()
											: modSQL.sqlDatabaseTraceStopForCurrentConnection();
				object result = 0;
				if (p != null)
				{
					if (transaction != null && transaction.IsOpenTransaction)
						result = getScalar(p, transaction);
					else if (connection != null)
						result = getScalar(p.ProcedureName, null, connection);
					isResult = Convert.ToInt32(result) > 0;
				}

				if (isTraceStart)
					mvIsTraceOn = isResult;
				else
				{
					mvIsTraceOn = !isResult;
				}
			}
			catch (Exception exc)
			{
				CLog.Log("SetDatabaseTraceAction(bool isTraceStart, CTransaction transaction)", exc);
			}
			return isResult;
		}

		public static bool SetDatabaseTraceForAllConnection(bool isStart)
		{
			bool isResult = false;
			try
			{
				DbConnection connection = modProvider.CreateConnection();
				if (connection == null)
					return isResult;
				if (connect(connection))
				{
					connection.BeginTransaction();
					string sids = String.Empty;
					if (mvTransactionConnections != null)
					{
						foreach (var kvp in mvTransactionConnections)
						{
							lock (modTransactionConnectionsMutexes[kvp.Key])
							{
								CDbConnectionProperties curConnect = mvTransactionConnections[kvp.Key];
								if (curConnect.DBConnection.State == ConnectionState.Open)
								{
									if (!String.IsNullOrEmpty(sids))
										sids = sids + ",";
									sids = sids + curConnect.DBConnectionSID;
								}
							}
						}
					}
					if (modDBConnection != null && modDBConnection.State == ConnectionState.Open)
					{
						string sid = (getScalar("ad_tools.sid_union_get", connection)).ToString();
						if (!String.IsNullOrEmpty(sid))
						{
							sids = sids + (!String.IsNullOrEmpty(sids) ? "," : "") + sid;
						}
					}
					CDBQuery p = isStart
												? modSQL.sqlDatabaseTraceStartForSomeConnection(Environment.UserName, sids)
												: modSQL.sqlDatabaseTraceStopForSomeConnection(Environment.UserName, sids);
					object result = null;
					if (p != null)
					{
						result = getScalar(p.ProcedureName, p.Parameters, connection);
						isResult = Convert.ToInt32(result) > 0;
					}

					if (isStart)
						mvIsTraceOn = isResult;
					else
					{
						mvIsTraceOn = !isResult;
					}
					connection.Close();
				}
			}
			catch (Exception exc)
			{
				CLog.Log("SetDatabaseTraceAction(bool isTraceStart, CTransaction transaction)", exc);
			}
			return isResult;
		}

		public static bool SetDatabaseTraceForDefaultConnection(bool isTraceStart)
		{
			bool isResult = false;
			try
			{
				CDBQuery p = isTraceStart
											? modSQL.sqlDatabaseTraceStartForCurrentConnection()
											: modSQL.sqlDatabaseTraceStopForCurrentConnection();
				object result = 0;
				if (p != null)
				{
					if (modDBConnection != null)
						result = getScalar(p.ProcedureName, p.Parameters, modDBConnection);
					isResult = Convert.ToInt32(result) > 0;
				}

				if (isTraceStart)
					mvIsTraceOn = isResult;
				else
				{
					mvIsTraceOn = !isResult;
				}
			}
			catch (Exception exc)
			{
				CLog.Log("SetDatabaseTraceAction(bool isTraceStart, CTransaction transaction)", exc);
			}
			return isResult;
		}

		public static string GetDatabaseTraceDirectoryPath()
		{
			string traceDirectory = String.Empty;
			try
			{
				CDBQuery p = modSQL.sqlDatabaseTraceDirectoryGet();
				object result = 0;
				if (p != null)
				{
					result = getScalar(p, null);
					if (result != null)
						traceDirectory = result.ToString();
				}
			}
			catch (Exception exc)
			{
				CLog.Log("GetDatabaseTraceDirectoryPath()", exc);
			}
			return traceDirectory;
		}

		#endregion Database Trace

		public static string CurrentDatabaseHostName
		{
			get
			{
				string DBServerName = String.Empty;
				try
				{
					CDBQuery p = modSQL.sqlDatabaseServerNameGet();
					object result = 0;
					if (p != null)
					{
						result = getScalar(p, null);
						if (result != null)
							DBServerName = result.ToString();
					}
				}
				catch (Exception exc)
				{
					CLog.Log("CurrentDatabaseHostName {get}", exc);
				}
				return DBServerName;
			}
		}

		// This method check on aborted procedure, that we know.
		private static void CheckIfAbortedProcedure(Exception ex, string procedureName)
		{
			bool isAborted = ex is ThreadAbortException && procedureName.Contains(CCoreConstants.cErrThreadAbortedFromFindRecordProcedure);
			if (isAborted)
				throw ex;
		}

		private static void ExtractCommandParametersString(CDBParameters commandParameters, out string paramsString, out string paramsParamString)
		{
			paramsString = string.Empty;
			paramsParamString = string.Empty;

			if (commandParameters != null)
			{
				paramsString = commandParameters.ToString();
				paramsParamString = commandParameters.ToParamString();
			}
		}
	}
}