using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Web.Data.Implementations.Requests
{
	public class StatusRequest
	{
		[Required(AllowEmptyStrings = false)]
		public string NameStatus { get; set; }
	}
}
