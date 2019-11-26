using System;
using Gtk;
using MonoDevelop.Components;
using MonoDevelop.Components.Docking;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;

namespace MonoDevelop.LogMonitor.Gui
{
	class LogMonitorPad : PadContent
	{
		LogMonitorWidget widget;
		Control control;

		public override Control Control {
			get {
				if (control == null) {
					widget = new LogMonitorWidget ();
					control = widget.ToGtkWidget ();
				}
				return control;
			}
		}

		protected override void Initialize (IPadWindow window)
		{
			var toolbar = window.GetToolbar (DockPositionType.Right);

			var openIdeLogButton = new Button (new ImageView (Ide.Gui.Stock.TextFileIcon, IconSize.Menu));
			openIdeLogButton.Clicked += OnOpenIdeLogButtonClick;
			openIdeLogButton.TooltipText = GettextCatalog.GetString ("Open IDE log");
			toolbar.Add (openIdeLogButton);

			toolbar.ShowAll ();
		}

		void OnOpenIdeLogButtonClick (object sender, EventArgs e)
		{
			CurrentIdeLogFile.Open ();
		}
	}
}
