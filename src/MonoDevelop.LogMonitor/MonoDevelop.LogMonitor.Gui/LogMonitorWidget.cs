using System;
using System.Linq;
using MonoDevelop.Core;
using MonoDevelop.Core.Logging;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using Xwt.Drawing;

namespace MonoDevelop.LogMonitor.Gui
{
	partial class LogMonitorWidget
	{
		public LogMonitorWidget ()
		{
			Build ();

			listView.SelectionChanged += ListViewSelectionChanged;
			LogMonitorMessages.MessageLogged += LogMessageLogged;
		}

		void LogMessageLogged (object sender, LogMessageEventArgs e)
		{
			Runtime.RunInMainThread (() => AddLogMessage (e));
		}

		void AddLogMessage (LogMessageEventArgs e)
		{
			int row = 0;
			if (listStore.RowCount == 0) {
				row = listStore.AddRow ();
			} else {
				row = listStore.InsertRowBefore (0);
			}

			listStore.SetValues (
				row,
				iconField,
				GetIcon (e.Level),
				logMessageTypeField,
				GetTypeName (e.Level),
				logMessageTextField,
				GetListMessage (e.Message),
				logMessageField,
				e);
		}

		static string GetTypeName (LogLevel level)
		{
			switch (level) {
				case LogLevel.Error:
					return GettextCatalog.GetString ("Error");
				case LogLevel.Fatal:
					return GettextCatalog.GetString ("Fatal");
				case LogLevel.Warn:
					return GettextCatalog.GetString ("Warning");
				case LogLevel.Info:
					return GettextCatalog.GetString ("Info");
				case LogLevel.Debug:
					return GettextCatalog.GetString ("Debug");
				default:
					return string.Empty;
			}
		}

		static Image GetIcon (LogLevel level)
		{
			switch (level) {
				case LogLevel.Error:
				case LogLevel.Fatal:
					return ImageService.GetIcon (Stock.Error, Gtk.IconSize.Menu);
				case LogLevel.Warn:
					return ImageService.GetIcon (Stock.Warning, Gtk.IconSize.Menu);
				case LogLevel.Info:
					return ImageService.GetIcon (Stock.Information, Gtk.IconSize.Menu);
				case LogLevel.Debug:
					return ImageService.GetIcon (Stock.Console, Gtk.IconSize.Menu);
				default:
					return ImageService.GetIcon (Stock.Error, Gtk.IconSize.Menu);
			}
		}

		static string GetListMessage (string message)
		{
			return message.Split ('\n').FirstOrDefault () ?? string.Empty;
		}

		void ListViewSelectionChanged (object sender, EventArgs e)
		{
			logView.Clear ();

			int row = listView.SelectedRow;
			if (row < 0) {
				return;
			}

			LogMessageEventArgs logMessage = listStore.GetValue (row, logMessageField);
			if (logMessage != null) {
				logView.WriteText (null, logMessage.Message);
			}
		}
	}
}
