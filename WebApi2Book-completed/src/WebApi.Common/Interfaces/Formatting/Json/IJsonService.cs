using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Common.Interfaces.Formatting.Json
{
	public interface IJsonService
	{
		string ConvertToJson(object obj);
	}
}
