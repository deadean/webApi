using log4net;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common.Implementations.Logging;
using WebApi.Common.Interfaces.Logging;

namespace WebApi.Common.Implementations.Converters
{
	public class DbReaderConvert
	{
		private const double NumericNullValue = -1E-28;
		private readonly static ILogService modLog = LogService.GetLogService<DbReaderConvert>();

		public DbReaderConvert()
		{
		}


		/// <summary>
		/// Determines whether [is reader ok] [the specified reader].
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <returns>
		///   <c>true</c> if [is reader ok] [the specified reader]; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsReaderOk(DbDataReader reader, int index)
		{
			return reader.FieldCount > index && !reader.IsDBNull(index);
		}

		/// <summary>
		/// Convert to the date.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <returns>Date time</returns>
		public static DateTime ToDate(DbDataReader reader, int index)
		{
			return IsReaderOk(reader, index) ? reader.GetDateTime(index) : DateTime.MinValue;
		}

		/// <summary>
		/// Toes the double.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static double ToDouble(DbDataReader reader, int index, double defaultValue)
		{
			try
			{
				return IsReaderOk(reader, index)
					? DbReaderConvert.ToDouble(Convert.ToString(reader.GetValue(index)))
					: defaultValue;
			}
			catch (Exception exc)
			{
#if DEBUG
				CLog.LogError(string.Format("ConvertNumber.ToDouble => {0}", exc.Message));
#endif
				return default(double);
			}
		}

		/// <summary>
		/// Toes the double.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public static double ToDouble(DbDataReader reader, int index)
		{
			return ToDouble(reader, index, 0);
		}

		/// <summary>
		/// Convert to the int32.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <returns>The int32 number</returns>
		public static int ToInt32(DbDataReader reader, int index)
		{
			return IsReaderOk(reader, index) ? DbReaderConvert.ToInt32(reader.GetValue(index)) : 0;
		}

		/// <summary>
		/// Convert to the int64.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <returns>The int64 number</returns>
		public static long ToInt64(DbDataReader reader, int index)
		{
			return IsReaderOk(reader, index) ? Convert.ToInt64(reader.GetValue(index)) : 0;
		}

		/// <summary>
		/// Gets boolean value for specified index.The value compare this "1".
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <returns>The bool value</returns>
		public static bool ToBoolean(DbDataReader reader, int index)
		{
			string res = IsReaderOk(reader, index) ? reader.GetValue(index).ToString() : String.Empty;
			return res == "1";
		}

		/// <summary>
		/// Gets boolean value for specified index. The value compare this "0".
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public static bool ToBoolean0(DbDataReader reader, int index)
		{
			string res = IsReaderOk(reader, index) ? reader.GetValue(index).ToString() : String.Empty;
			return res != "0";
		}

		/// <summary>
		/// Gets boolean value for specified index
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>The bool value</returns>
		public static bool ToBoolean(string value)
		{
			bool result = false;
			try
			{
				bool.TryParse(value, out result);
			}
			catch (Exception exc)
			{
				CLog.Log("ConvertNumber.ToBoolean", exc);
			}
			return result;
		}

		/// Gets string value for specified index
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="index">The index.</param>
		/// <returns>The string value</returns>
		public static string ToString(DbDataReader reader, int index)
		{
			return IsReaderOk(reader, index) ? reader.GetValue(index).ToString() : String.Empty;
		}

		public static double ToDouble(string s, double defaultValue)
		{
			double number;

			return Double.TryParse(s, out number) ? number : defaultValue;
		}

		/// <summary>
		/// Convert to the double.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns>The double number</returns>
		public static double ToDouble(string s)
		{
			return ToDouble(s, default(double));
		}

		/// <summary>
		/// Convert to the double.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <param name="precision">The precision.</param>
		/// <returns>The double number</returns>
		public static double ToDouble(string s, int precision)
		{
			double number = 0;
			if (!Double.TryParse(s, out number))
				number = 0;

			number = TruncateDouble(number, precision);

			return number;
		}

		/// <summary>
		/// Convert to the int32.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns>The int32 number</returns>
		public static int ToInt32(string s)
		{
			int number = 0;
			if (!Int32.TryParse(s, out number))
				number = 0;
			return number;
		}

		/// <summary>
		/// Convert to the int32.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns>The int32 number</returns>
		public static int ToInt32(object o)
		{
			int number = 0;

			try
			{
				number = DbReaderConvert.ToInt32(o);
			}
			catch
			{
				number = 0;
			}

			return number;
		}

		/// <summary>
		/// Convert to the int64.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns>The int62 number</returns>
		public static long ToInt64(string s)
		{
			long number = 0;
			if (!Int64.TryParse(s, out number))
				number = 0;
			return number;
		}

		/// <summary>
		/// Convert doubles to string.
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="precision">The precision.</param>
		/// <param name="isKeepTrailingZeros">if set to <c>true</c> [is keep trailing zeros].</param>
		/// <returns>
		/// The double presenter
		/// </returns>
		public static string DoubleToString(double d, int precision, bool isKeepTrailingZeros)
		{
			string format = String.Format(isKeepTrailingZeros ? "F{0}" : "G{0}", precision);
			return d.ToString(format);
		}
		/// <summary>
		/// Doubles to string.
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="precision">The precision.</param>
		/// <returns></returns>
		public static string DoubleToString(double d, int precision)
		{
			return DoubleToString(d, precision, true);
		}

		/// <summary>
		/// Truncates the double.
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="precision">The precision.</param>
		/// <returns>The double value</returns>
		public static double TruncateDouble(double d, int precision)
		{
			double result = d;

			try
			{
				long pow = 1;
				for (int i = 0; i < precision; i++)
					pow *= 10;

				result = Math.Round(result * (double)pow) / pow;
			}
			catch { }

			return result;
		}

		/// <summary>
		/// Convert bool to int.
		/// </summary>
		/// <param name="boolValue">if set to <c>true</c> [bool value].</param>
		/// <returns></returns>
		public static int BoolToInt(bool boolValue)
		{
			return boolValue ? 1 : 0;
		}

		/// <summary>
		/// Toes the bool.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool ToBool(object value)
		{
			if (value == null)
				return false;

			return (string)value == "1";
		}
	}
}
