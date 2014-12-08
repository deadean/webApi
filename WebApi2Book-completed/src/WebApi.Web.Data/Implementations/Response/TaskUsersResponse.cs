using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi2Book.Web.Api.Models;

namespace WebApi.Web.Data.Implementations.Response
{
	public class TaskUsersResponse
	{
		private List<Link> _links;
		public long TaskId { get; set; }

		public List<User> Users { get; set; }

		public List<Link> Links
		{
			get { return _links ?? (_links = new List<Link>()); }
			set { _links = value; }
		}

		public void AddLink(Link link)
		{
			Links.Add(link);
		}
	}
}
