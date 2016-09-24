var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var artifactsDirectory = Directory("./artifacts");

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var projects = GetFiles("./**/*.xproj");
        foreach(var project in projects)
        {
            DotNetCoreBuild(
                project.GetDirectory().FullPath,
                new DotNetCoreBuildSettings()
                {
                    Configuration = configuration
                });
        }
    });

Task("Publish")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCorePublish(
                "./src/PersonalWebApp",
                new DotNetCorePublishSettings()
                {
                    Configuration = configuration,
                    OutputDirectory = artifactsDirectory
                });
    });

Task("Default")
    .IsDependentOn("Publish");

RunTarget(target);