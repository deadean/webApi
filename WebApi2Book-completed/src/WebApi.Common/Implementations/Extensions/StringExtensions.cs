namespace WebApi.Common.Implementations.Extensions
{
	using System;

	/// <summary>
	/// string Extensions
	/// </summary>
	public static class StringExtensions
	{
		public static string Append(this string sourceString, string appendWith)
		{
			if (string.IsNullOrEmpty(sourceString))
				return string.Empty;
			if (string.IsNullOrEmpty(appendWith))
				return sourceString;

			if (sourceString.EndsWith(appendWith))
				return sourceString;

			return sourceString + appendWith;
		}

		/// <summary>
		/// The upper first letter.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static string ToUpperFirstLetter(this string source)
		{
			if (string.IsNullOrEmpty(source))
				return string.Empty;

			// convert to char array of the string
			char[] letters = source.ToCharArray();

			// upper case the first char
			letters[0] = Char.ToUpper(letters[0]);

			// return the array made of the new char array
			return new string(letters);
		}

		public static bool HasValue(this string sourceString)
		{
			return !IsNullOrEmptyOrWhitespace(sourceString);
		}

		public static bool IsNullOrEmptyOrWhitespace(this string sourceString)
		{
			if (string.IsNullOrEmpty(sourceString))
				return true;

			return string.IsNullOrEmpty(sourceString.Trim());
		}

		public static bool NotEqual(this string sourceString, string targetstring)
		{
			if (string.IsNullOrEmpty(sourceString) && string.IsNullOrEmpty(targetstring))
				return false;

			return !string.Equals(sourceString, targetstring, StringComparison.CurrentCulture);
		}


		public static string RemoveLineBreaks(this string sourceString)
		{
			return ReplaceLineBreaks(sourceString, string.Empty);
		}

		public static string ReplaceLineBreaks(this string sourceString, string targetstring)
		{
			if (string.IsNullOrEmpty(sourceString))
				return string.Empty;

			return sourceString.Replace(Environment.NewLine, targetstring);
		}
	}
}
