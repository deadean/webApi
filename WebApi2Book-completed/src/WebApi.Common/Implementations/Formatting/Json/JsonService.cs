using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common.Interfaces.Formatting.Json;

namespace WebApi.Common.Implementations.Formatting.Json
{
	public class JsonService : IJsonService
	{
		public string ConvertToJson(object obj)
		{
			return JsonConvert.SerializeObject(obj); 
		}
	}
}
