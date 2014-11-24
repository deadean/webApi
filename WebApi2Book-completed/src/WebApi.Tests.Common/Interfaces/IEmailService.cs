using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests.Common.Interfaces
{
	public interface IEmailService
	{
		void SendTestReport();
		void Reset();
		void AddBadTest();
	}
}
