//-------------------------------------------------------------------
// <copyright file="CVersion.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary>Holds CVersion class</summary>
// <author>Igor Kobylinskyi</author>
//-------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	///   Represents 
	/// </summary>
	[Serializable]
	public class CVersion : ISerializable
	{
		#region Fields

		private static readonly Regex VersionRegex = new Regex(CCoreConstants.Version.RegexString, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

		private readonly string mvVersion;

		private readonly int mvMajor;
		private readonly int mvMinor;

		#endregion // Fields

		#region Ctor

		public CVersion()
			: this(CCoreConstants.Version.Default)
		{

		}

		/// <summary>
		///   Initializes instance of CVersion class
		/// </summary>
		public CVersion(string version)
		{
			var match = VersionRegex.Match(version);

			if (match.Success)
			{
				mvVersion = match.Groups[CCoreConstants.Version.RegexConstans.Version].Value;

				mvMajor = int.Parse(match.Groups[CCoreConstants.Version.RegexConstans.Major].Value);
				mvMinor = int.Parse(match.Groups[CCoreConstants.Version.RegexConstans.Minor].Value);
			}
			else
			{
				throw new Exception("Cannot initialize version from not valid version string");
			}
		}

		protected CVersion(SerializationInfo info, StreamingContext context)
		{
			var versionString = (string)info.GetValue("Version", typeof(string));

			var version = new CVersion(versionString);

			mvVersion = version.mvVersion;

			mvMajor = version.mvMajor;
			mvMinor = version.mvMinor;
		}

		#endregion // Ctor

		#region Properties

		[XmlIgnore]
		public int Major
		{
			get { return mvMajor; }
		}

		[XmlIgnore]
		public int Minor
		{
			get { return mvMinor; }
		}

		[XmlIgnore]
		public string Version
		{
			get { return mvVersion; }
		}

		#endregion // Properties

		#region Methods

		protected bool Equals(CVersion other)
		{
			return mvMajor == other.mvMajor && mvMinor == other.mvMinor;
		}

		/// <summary>
		///   Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;

			if (ReferenceEquals(this, obj))
				return true;

			if (obj.GetType() != GetType())
				return false;

			return Equals((CVersion)obj);
		}

		/// <summary>
		///   Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		///   A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = mvMajor;

				hashCode = (hashCode * 397) ^ mvMinor;

				return hashCode;
			}
		}

		/// <summary>
		///   Returns a string that represents the current object
		/// </summary>
		/// <returns>A string that represents the current object</returns>
		public override string ToString()
		{
			return mvVersion;
		}

		#endregion // Methods

		#region Static Methods

		public static bool IsValid(string fileVersion)
		{
			if (string.IsNullOrEmpty(fileVersion))
				return false;

			return VersionRegex.IsMatch(fileVersion.Trim());
		}

		public static bool IsNewer(string firstFileVersionString, string secondFileVersionString)
		{
			return Compare(firstFileVersionString, secondFileVersionString, (x, y) => x > y);
		}

		public static bool IsNewer(CVersion firstFileVersion, CVersion secondFileVersion)
		{
			return Compare(firstFileVersion, secondFileVersion, (x, y) => x > y);
		}

		public static bool IsOlder(string firstFileVersionString, string secondFileVersionString)
		{
			return Compare(firstFileVersionString, secondFileVersionString, (x, y) => x < y);
		}

		public static bool IsOlder(CVersion firstFileVersion, CVersion secondFileVersion)
		{
			return Compare(firstFileVersion, secondFileVersion, (x, y) => x < y);
		}

		public static implicit operator CVersion(string fileVersionString)
		{
			return new CVersion(fileVersionString);
		}

		public static implicit operator string(CVersion fileVersionString)
		{
			return fileVersionString.ToString();
		}

		public static bool operator >(CVersion firstFileVersion, CVersion secondFileVersion)
		{
			return IsNewer(firstFileVersion, secondFileVersion);
		}

		public static bool operator <(CVersion firstFileVersion, CVersion secondFileVersion)
		{
			return IsOlder(firstFileVersion, secondFileVersion);
		}

		public static bool operator ==(CVersion firstFileVersion, CVersion secondFileVersion)
		{
			if (ReferenceEquals(firstFileVersion, null))
			{
				if (ReferenceEquals(secondFileVersion, null))
					return true;

				return false;
			}

			if (ReferenceEquals(secondFileVersion, null))
				return false;

			return firstFileVersion.Major == secondFileVersion.Major && firstFileVersion.Minor == secondFileVersion.Minor;
		}

		public static bool operator !=(CVersion firstFileVersion, CVersion secondFileVersion)
		{
			if (ReferenceEquals(firstFileVersion, null))
			{
				if (ReferenceEquals(secondFileVersion, null))
					return false;

				return true;
			}

			if (ReferenceEquals(secondFileVersion, null))
				return true;

			return firstFileVersion.Major != secondFileVersion.Major || firstFileVersion.Minor != secondFileVersion.Minor;
		}

		private static bool Compare(string firstFileVersionString, string secondFileVersionString, Func<int, int, bool> condition)
		{
			if (string.IsNullOrEmpty(firstFileVersionString) || string.IsNullOrEmpty(secondFileVersionString))
				throw new Exception("Cannot compare non exist file version strings");

			return Compare(new CVersion(firstFileVersionString), new CVersion(secondFileVersionString), condition);
		}

		private static bool Compare(CVersion firstFileVersion, CVersion secondFileVersion, Func<int, int, bool> condition)
		{
			if (ReferenceEquals(firstFileVersion, null) || ReferenceEquals(secondFileVersion, null))
				throw new Exception("Cannot compare non exist file version strings");

			if (condition == null)
				throw new Exception("Cannot compare file versions with non exist condition");

			if (condition(firstFileVersion.Major, secondFileVersion.Major))
				return true;

			if (firstFileVersion.Major == secondFileVersion.Major)
			{
				if (condition(firstFileVersion.Minor, secondFileVersion.Minor))
					return true;
			}

			return false;
		}

		#endregion // Static Methods

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Version", mvVersion);
		}
	}
}