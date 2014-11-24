namespace WebApi.Common.Implementations.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	///   Represents 
	/// </summary>
	public static class DictionaryExtensions
	{
		private static readonly string DefaultCase = Guid.NewGuid().ToString();

		public static Dictionary<string, Action> BuildCase(this Dictionary<string, Action> source, string caseEntry, Action action)
		{
			if (source == null)
				return null;

			if (caseEntry == null || action == null)
				return source;

			source[caseEntry] = action;

			return source;
		}

		public static Dictionary<string, Action> BuildCases(this Dictionary<string, Action> source, string[] cases, Action action)
		{
			if (source == null)
				return null;

			if (cases.IsNullOrEmpty() || action == null)
				return source;

			foreach (var caseEntry in cases)
			{
				source[caseEntry] = action;
			}

			return source;
		}

		public static Dictionary<string, Action> BuildDefault(this Dictionary<string, Action> source, Action action)
		{
			if (source == null)
				return null;

			if (action == null)
				return source;

			source[DefaultCase] = action;

			return source;
		}

		public static void Switch(this Dictionary<string, Action> source, string switchModifier)
		{
			if (source.IsNullOrEmpty())
				return;

			var switchCase = source.FirstOrDefault(x => source.Comparer.Equals(x.Key, switchModifier)).Value
										?? source.FirstOrDefault(x => x.Key == DefaultCase).Value;

			if (switchCase == null)
				return;

			switchCase();
		}
	}
}