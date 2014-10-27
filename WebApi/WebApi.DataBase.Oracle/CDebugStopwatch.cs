using System;
using System.Diagnostics;

namespace WebApi.DataBase.Oracle
{
	public class CDebugStopwatch
	{
		private static Stopwatch modStopwatch = null;
		private static string modMessage = String.Empty;


		public static bool IsStarted { get; set; }


		static CDebugStopwatch()
		{
#if DEBUG
			modStopwatch = new Stopwatch();
#endif
		}

		public static void Start(string message)
		{
#if DEBUG
			modMessage = message;

			if (IsStarted)
				Result(message);

			IsStarted = true;

			modStopwatch.Start();
#endif
		}

		public static void Result(string addMessage)
		{
#if DEBUG
			System.Diagnostics.Debug.Print(">>> {0} ({1}): {2} ms", modMessage, addMessage, modStopwatch.ElapsedMilliseconds.ToString());
#endif
		}


		public static void Stop()
		{
#if DEBUG
			modStopwatch.Stop();
			System.Diagnostics.Debug.Print(">>> {0} (FINISHED): {1} ms", modMessage, modStopwatch.ElapsedMilliseconds.ToString());
			modStopwatch.Reset();
			IsStarted = false;
#endif
		}

	}
}
