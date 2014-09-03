using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LizenzaDevelopment.ADAICA.Data.GLOB.ADGLOB.Extensions
{
	/// <summary>
	/// String Extensions
	/// </summary>
	public static class StringExtensions
	{
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
			letters[0] = char.ToUpper(letters[0]);

			// return the array made of the new char array
			return new string(letters);
		}

	}
}
