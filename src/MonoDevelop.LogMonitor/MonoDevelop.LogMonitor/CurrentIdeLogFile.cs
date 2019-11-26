using System;
using System.IO;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;

namespace MonoDevelop.LogMonitor
{
	static class CurrentIdeLogFile
	{
		static CurrentIdeLogFile()
		{
			FileName = UserProfile.Current.LogDir.Combine ("Ide.log");
		}

		public static FilePath FileName { get; private set; }

		public static void Update()
		{
			try {
				if (!File.Exists (FileName))
					return;

				FileName = FileName.ResolveLinks ();
			} catch (Exception ex) {
				LoggingService.LogError ("Unable to resolve IDE log filename", ex);
			}
		}

		public static void Open()
		{
			var fileInfo = new FileOpenInformation(FileName);
			IdeApp.Workbench.OpenDocument(fileInfo).Ignore();
		}
	}
}
