//-------------------------------------------------------------------
// <copyright file="CVersionBase.cs" company="Lizenza Development Ltd.">
//     Copyright (c) Lizenza Development Ltd. All rights reserved.
// </copyright>
// <summary>Holds CVersionBase class</summary>
// <author>Igor Kobylinskyi</author>
//-------------------------------------------------------------------

using System;
using System.Xml.Serialization;

namespace WebApi.DataBase.Oracle
{
	/// <summary>
	///   Represents 
	/// </summary>
	[Serializable]
	public abstract class CVersionBase
	{
		#region Fields

		private CVersion mvVersion;

		#endregion // Fields

		#region Ctor

		/// <summary>
		///   Initializes instance of CVersionBase class
		/// </summary>
		protected CVersionBase()
		{

		}

		protected CVersionBase(string fileVersion)
		{
			mvVersion = new CVersion(fileVersion);
		}

		#endregion // Ctor

		#region Properties

		[XmlIgnore]
		public CVersion Version
		{
			get { return mvVersion; }
			set { mvVersion = value; }
		}

		[XmlAttribute("Version")]
		public string VersionString
		{
			get { return mvVersion; }
			set { mvVersion = value; }
		}

		#endregion // Properties

		#region Methods

		public bool ShouldSerializeVersionString()
		{
			return Version != CCoreConstants.Version.Default && ShouldSerializeVersion();
		}

		protected virtual bool ShouldSerializeVersion()
		{
			return true;
		}

		#endregion // Methods
	}
}