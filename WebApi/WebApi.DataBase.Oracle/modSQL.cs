namespace WebApi.DataBase.Oracle
{
	internal static class modSQL
	{
		internal static string sqlRealUserContextSet(string realUserId, out CDBParameters parameters)
		{
			parameters = new CDBParameters();
			parameters.Add("p_real_user_id", realUserId);

			return "AD_UTIL.REAL_USER_CONTEXT_SET";
		}

		internal static CDBQuery sqlDatabaseTraceStartForCurrentConnection()
		{
			CDBQuery parameters = new CDBQuery("ad_tools.trace_start");
			return parameters;
		}

		internal static CDBQuery sqlDatabaseTraceStopForCurrentConnection()
		{
			CDBQuery parameters = new CDBQuery("ad_tools.trace_stop");
			return parameters;
		}

		internal static CDBQuery sqlDatabaseTraceStartForSomeConnection(string osUser, string sids)
		{
			CDBQuery parameters = new CDBQuery("ad_tools.traces4user_start");
			parameters.Add("p_osuser", osUser);
			parameters.Add("p_sids", sids);
			return parameters;
		}

		internal static CDBQuery sqlDatabaseTraceStopForSomeConnection(string osUser, string sids)
		{
			CDBQuery parameters = new CDBQuery("ad_tools.traces4user_stop");
			parameters.Add("p_osuser", osUser);
			parameters.Add("p_sids", sids);
			return parameters;
		}

		internal static CDBQuery sqlDatabaseTraceDirectoryGet()
		{
			CDBQuery parameters = new CDBQuery("ad_tools.trace_dir_get");
			return parameters;
		}

		internal static CDBQuery sqlDatabaseServerNameGet()
		{
			CDBQuery parameters = new CDBQuery("ad_tools.server_host_get");
			return parameters;
		}
	}
}