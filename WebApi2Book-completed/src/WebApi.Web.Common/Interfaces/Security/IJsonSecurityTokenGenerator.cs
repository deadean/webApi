﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Web.Common.Interfaces.Security
{
	public interface IJsonSecurityTokenGenerator
	{
		string GetJWT();
	}
}
