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

namespace WebApi.DataBase.Oracle
{
	public class CDBQuery : IEnumerable<KeyValuePair<string, object>>
	{
		public CDBQuery()
		{
		}

		public CDBQuery(string procedureName)
		{
			ProcedureName = procedureName;
		}

		public string ProcedureName { get; set; }
		public CDBParameters Parameters { get; internal set; }
		public void Add(string pName, object pValue)
		{
			if (Parameters == null)
				Parameters = new CDBParameters();
			Parameters.Add(pName, pValue);
		}

		public void SetEmptyParameters()
		{
			if (Parameters == null)
				Parameters = new CDBParameters();
		}

		public void ClearParameters()
		{
			Parameters = new CDBParameters();
		}

		#region IEnumerable<KeyValuePair<string,object>> Members

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return Parameters == null
				? new CDBParameters().GetEnumerator()
				: Parameters.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}



	/// <summary>
	/// DB parameters class. Wrapper around Dictionary class.
	/// </summary>
	public class CDBParameters : Dictionary<string, object>
	{
		/// <summary>
		/// Get parameters joined to string with "," delimiter
		/// </summary>
		/// <returns>String interpretation of parameters collection</returns>
		public override string ToString()
		{
			string result = String.Empty;
			try
			{
				//result = string.Join("', '", this.Values.Select(t=>t.ToString()).Cast<string>().ToArray());
				foreach (object obj in this.Values)
				{
					if (obj != null)
						result = result + "'" + obj.ToString() + "', ";
					else
						result = result + "null, ";
				}

				if (result.Length > 1)
				{
					result = result.Substring(0, result.Length - 2);
					//result = "'" + result + "'";
				}

			}
			catch (Exception exc)
			{
				result = "";
				System.Diagnostics.Debug.Print("CDBParameters.ToString(): " + exc.ToString());
			}
			return result;
		}

		public string ToParamString()
		{
			string result = String.Empty;
			try
			{
				//result = string.Join("', '", this.Values.Select(t=>t.ToString()).Cast<string>().ToArray());
				foreach (object obj in this.Keys)
				{
					if (obj != null)
						result = result + obj.ToString() + "=" + (this[obj.ToString()] ?? String.Empty).ToString() + ", ";
					else
						result = result + "null, ";
				}

				if (result.Length > 1)
				{
					result = result.Substring(0, result.Length - 2);
					result = "'" + result + "'";
				}

			}
			catch (Exception exc)
			{
				result = "";
				System.Diagnostics.Debug.Print("CDBParameters.ToString(): " + exc.ToString());
			}
			return result;
		}

		public void PrintDebug()
		{
			string result = String.Empty;
			try
			{
				foreach (KeyValuePair<string, object> kvp in this)
				{
					result = kvp.Key + ": ";
					if (kvp.Value == null)
						result += "null";
					else
						result += kvp.Value.ToString();

					System.Diagnostics.Debug.Print(result);
				}
			}
			catch (Exception exc)
			{
				System.Diagnostics.Debug.Print("CDBParameters.PrintDebug(): " + exc.ToString());
			}
		}
	}
}
