using System.Data.Common;

//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace WebApi.DataBase.Oracle
{
	public class CCommandWrapper
	{
		private DbCommand cmd = null;
		internal DbCommand Cmd
		{
			set
			{
				cmd = value;
			}
		}
		public CCommandWrapper(DbCommand command)
		{
			cmd = command;
		}
		public CCommandWrapper()
		{
			cmd = null;
		}
		public void Cancel()
		{
			if (cmd != null)
				cmd.Cancel();
		}
	}
}
