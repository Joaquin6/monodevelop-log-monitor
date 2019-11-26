using System;
using MonoDevelop.Core;
using MonoDevelop.Core.Logging;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.LogMonitor.Gui;

namespace MonoDevelop.LogMonitor
{
	public class LogMonitorLogger : ILogger
	{
		StatusBarIcon statusBarIcon;
		int errorsCount;

		public EnabledLoggingLevel EnabledLevel => EnabledLoggingLevel.UpToWarn;

		public string Name => nameof (LogMonitorLogger);

		public void Log(LogLevel level, string message)
		{
			LogMonitorMessages.ReportLogMessage (level, message);

			switch (level) {
				case LogLevel.Error:
					OnLogError (message);
					break;
				case LogLevel.Fatal:
					OnLogFatal (message);
					break;
				case LogLevel.Warn:
					OnLogWarn (message);
					break;
			}
		}

		void OnLogWarn (string message)
		{

		}

		void OnLogFatal (string message)
		{
			errorsCount++;
		}

		void OnLogError (string message)
		{
			errorsCount++;
			ShowStatusIcon ();
		}

		void ShowStatusIcon ()
		{
			Runtime.RunInMainThread (() => {
				if (statusBarIcon == null) {
					var icon = ImageService.GetIcon (Stock.TextFileIcon, Gtk.IconSize.Menu);
					statusBarIcon = IdeApp.Workbench.StatusBar.ShowStatusIcon (icon);
					statusBarIcon.Clicked += StatusBarIconClicked;
				}
				statusBarIcon.Title = GettextCatalog.GetString ("IDE log errors");
				statusBarIcon.ToolTip = GettextCatalog.GetPluralString ("{0} IDE log error", "{0} IDE log errors", errorsCount, errorsCount);
				statusBarIcon.SetAlertMode (1);
			}).Ignore ();
		}

		void StatusBarIconClicked (object sender, StatusBarIconClickedEventArgs e)
		{
			Pad pad = IdeApp.Workbench.GetPad<LogMonitorPad> ();
			pad.BringToFront (true);
		}
	}
}
