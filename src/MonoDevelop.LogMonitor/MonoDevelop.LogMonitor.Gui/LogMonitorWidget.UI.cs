using Xwt;
using Xwt.Drawing;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui.Components;

namespace MonoDevelop.LogMonitor.Gui
{
	partial class LogMonitorWidget : Widget
	{
		HPaned paned;
		ListView listView;
		ListStore listStore;
		DataField<Image> iconField = new DataField<Image> ();
		DataField<string> logMessageTypeField = new DataField<string> ();
		DataField<string> logMessageTextField = new DataField<string> ();
		DataField<LogMessageEventArgs> logMessageField = new DataField<LogMessageEventArgs> ();
		LogView logView;

		void Build ()
		{
			paned = new HPaned ();

			var mainVBox = new VBox ();

			listView = new ListView ();
			listView.BorderVisible = false;
			listView.HeadersVisible = true;
			listStore = new ListStore (iconField, logMessageTypeField, logMessageTextField, logMessageField);
			listView.DataSource = listStore;

			paned.Panel1.Content = listView;

			var column = new ListViewColumn ();
			column.Views.Add (new ImageCellView (iconField));
			column.CanResize = false;
			listView.Columns.Add (column);

			column = new ListViewColumn ();
			column.Title = GettextCatalog.GetString ("Type");
			column.Views.Add (new TextCellView (logMessageTypeField));
			column.CanResize = true;
			listView.Columns.Add (column);

			column = new ListViewColumn ();
			column.Title = GettextCatalog.GetString ("Message");
			column.Views.Add (new TextCellView (logMessageTextField));
			column.CanResize = true;
			listView.Columns.Add (column);

			logView = new LogView ();
			logView.ShowAll ();
			paned.Panel2.Content = Toolkit.CurrentEngine.WrapWidget (logView, NativeWidgetSizing.DefaultPreferredSize);

			Content = paned;
		}
	}
}
