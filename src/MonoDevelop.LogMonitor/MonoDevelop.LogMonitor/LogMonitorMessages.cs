using System;
using System.Collections.Generic;
using MonoDevelop.Core.Logging;

namespace MonoDevelop.LogMonitor
{
	static class LogMonitorMessages
	{
		static List<LogMessageEventArgs> messages = new List<LogMessageEventArgs> ();

		static event EventHandler<LogMessageEventArgs> messageLogged;

		public static event EventHandler<LogMessageEventArgs> MessageLogged {
			add {
				lock (messages) {
					messageLogged += value;
					foreach (LogMessageEventArgs message in messages) {
						value (null, message);
					}
					messages.Clear ();
				}
			}

			remove {
				messageLogged -= value;
			}
		}

		public static void ReportLogMessage (LogLevel level, string message)
		{
			var logMessage = new LogMessageEventArgs (level, message);

			if (messageLogged != null) {
				messageLogged (null, logMessage);
			} else {
				lock (messages) {
					messages.Add (logMessage);
				}
			}
		}
	}
}
