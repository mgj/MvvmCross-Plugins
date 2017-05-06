#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=gitlink"

var sln = new FilePath("artm.MvxPlugins.sln");
var binDir = new DirectoryPath("bin");
var outputDir = new DirectoryPath("artifacts");
var target = Argument("target", "Default");

var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;

Task("Clean").Does(() =>
{
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
	CleanDirectories(binDir.FullPath);
	CleanDirectories(outputDir.FullPath);
});

GitVersion versionInfo = null;
Task("Version").Does(() => {
	GitVersion(new GitVersionSettings {
		UpdateAssemblyInfo = true,
		OutputType = GitVersionOutput.BuildServer
	});

	versionInfo = GitVersion(new GitVersionSettings{ OutputType = GitVersionOutput.Json });
	Information("VI:\t{0}", versionInfo.FullSemVer);
});

Task("Restore").Does(() => {
	NuGetRestore(sln);
});

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Version")
	.IsDependentOn("Restore")
	.Does(() =>  {
	
	DotNetBuild(sln, 
		settings => settings.SetConfiguration("Release")
	);
});

Task("GitLink")
	.WithCriteria(() => IsRunningOnWindows())
	.IsDependentOn("Build")
	.Does(() => {

	GitLink(sln.GetDirectory(), new GitLinkSettings {
		RepositoryUrl = "https://github.com/mgj/MvvmCross-plugins",
		ArgumentCustomization = args => args.Append(
			"-ignore playground.core,playground.droid,playground.touch")
	});
});

Task("PackageAll")
	.IsDependentOn("GitLink")
	.Does(() => {

	EnsureDirectoryExists(outputDir);

	var nugetSettings = new NuGetPackSettings {
		Authors = new [] { "Mikkel Jensen" },
		Owners = new [] { "Mikkel Jensen" },
		IconUrl = new Uri("https://artm.dk/images/android-logo.png"),
		ProjectUrl = new Uri("https://github.com/mgj/MvvmCross-Plugins"),
		LicenseUrl = new Uri("https://github.com/mgj/MvvmCross-Plugins/blob/master/LICENSE"),
		Copyright = "Copyright (c) Mikkel Jensen",
		RequireLicenseAcceptance = false,
		Version = versionInfo.NuGetVersion,
		Symbols = false,
		NoPackageAnalysis = true,
		OutputDirectory = outputDir,
		Verbosity = NuGetVerbosity.Detailed,
		BasePath = "./nuspec"
	};

	NuGetPack("./nuspec/artm.MvxPlugins.Fetcher.nuspec", nugetSettings);

	nugetSettings.ReleaseNotes = ParseReleaseNotes("./releasenotes/logger.md").Notes.ToArray();
	NuGetPack("./nuspec/artm.MvxPlugins.Logger.nuspec", nugetSettings);

	nugetSettings.ReleaseNotes = ParseReleaseNotes("./releasenotes/dialog.md").Notes.ToArray();
	NuGetPack("./nuspec/artm.MvxPlugins.Dialog.nuspec", nugetSettings);
});


Task("UploadAppVeyorArtifact")
	.IsDependentOn("PackageAll")
	.WithCriteria(() => !isPullRequest)
	.WithCriteria(() => isRunningOnAppVeyor)
	.Does(() => {

	Information("Artifacts Dir: {0}", outputDir.FullPath);

	foreach(var file in GetFiles(outputDir.FullPath + "/*")) {
		Information("Uploading {0}", file.FullPath);
		AppVeyor.UploadArtifact(file.FullPath);
	}
});

Task("Default").IsDependentOn("UploadAppVeyorArtifact").Does(() => {});

RunTarget(target);