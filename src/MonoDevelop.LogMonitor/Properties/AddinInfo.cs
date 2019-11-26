using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin (
	"LogMonitor",
	Namespace = "MonoDevelop",
	Version = "0.2",
	Category = "IDE extensions"
)]

[assembly: AddinName("Log Monitor")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("Monitors the IDE log for errors")]

[assembly: AddinDependency("Core", "8.1")]
[assembly: AddinDependency("Ide", "8.1")]
