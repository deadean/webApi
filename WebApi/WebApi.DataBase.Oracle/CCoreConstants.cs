using System;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	/// Contains core constants.
	/// </summary>
	public class CCoreConstants
	{
		//-------------------------------------------------------------------Oracle Constants
		public const string cErrOracleResourseBusy = "ORA-00054";
		public const string cErrOracleUserRequestedCancel = "ORA-01013";
		public const string cErrOracleErrorInExecutingODCIIndexStartRoutine = "ORA-29902";

		public const string cErrThreadAbortedFromFindRecordProcedure = "AD_FIND.find_record_new";
		public const string cErrOracleErrorWildCardQuaryMaxRecords = "DRG-51030";
		public const string cErrOracleErrorTextQueryParseError = "DRG-50901";
		public const string cProcedureNameForOracleErrorWildCardQuaryMaxRecords = "AD_FIND.find_record_arr1";
		public const string cProcedureNameNewForOracleErrorWildCardQuaryMaxRecords = "AD_FIND.find_record_new";

		public const int MinFolderPathLength = 3;

		public static readonly Guid ApplicationGuid = Guid.NewGuid();

		public const string ApplicationMutexName = "ADAICA";

		internal static class Version
		{
			public const string DefaultFile = "1.0.0.0";
			public const string Default = "1.0";

			public static class RegexConstans
			{
				public const string Version = "Version";
				public const string Major = "Major";
				public const string Minor = "Minor";
				public const string Private = "Private";
				public const string Build = "Build";
			}

			public const string FileRegexString = @"^(?<Version>(?<Major>[0-9]{1,5}).(?<Minor>[0-9]{1,5}).(?<Private>[0-9]{1,5}).(?<Build>[0-9]{1,5}))$";
			public const string RegexString = @"^(?<Version>(?<Major>[0-9]{1,5}).(?<Minor>[0-9]{1,5}))$";
		}
	}
}