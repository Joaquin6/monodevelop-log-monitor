using System;
using System.Collections.Generic;
using MonoDevelop.Core.LogReporting;

namespace MonoDevelop.LogMonitor
{
	class LogMonitorCrashReporter : CrashReporter
	{
		public override void ReportCrash(Exception ex, bool willShutDown, IEnumerable<string> tags)
		{

		}
	}
}
