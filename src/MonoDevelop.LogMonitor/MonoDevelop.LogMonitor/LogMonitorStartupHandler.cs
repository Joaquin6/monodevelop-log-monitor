using MonoDevelop.Components.Commands;
using MonoDevelop.Core;

namespace MonoDevelop.LogMonitor
{
	class LogMonitorStartupHandler : CommandHandler
	{
		protected override void Run()
		{
			LoggingService.AddLogger(new LogMonitorLogger());
			CurrentIdeLogFile.Update();
		}
	}
}
