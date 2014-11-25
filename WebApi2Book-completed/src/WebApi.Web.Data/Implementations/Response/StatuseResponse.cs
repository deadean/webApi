using Blank.Data.Implementations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data.Implementations.Entities;

namespace WebApi.Web.Data.Implementations.Response
{
	public class StatusesResponse
	{
		public IEnumerable<Status> Items { get; set; }
	}

}
