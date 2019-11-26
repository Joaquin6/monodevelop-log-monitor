using System;
using MonoDevelop.Core.Logging;

namespace MonoDevelop.LogMonitor
{
	class LogMessageEventArgs : EventArgs
	{
		public LogMessageEventArgs (LogLevel level, string message)
		{
			Level = level;
			Message = message;
		}

		public LogLevel Level { get; set; }
		public string Message { get; set; }
	}
}
