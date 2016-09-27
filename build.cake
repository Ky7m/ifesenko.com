// Install addins.
#addin "nuget:https://www.nuget.org/api/v2?package=Newtonsoft.Json&version=9.0.1"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var outputDirectory = Directory("./output");
var packageDirectory= Directory("./publish");

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(outputDirectory);
		CleanDirectory(packageDirectory);
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
                    OutputDirectory = outputDirectory
                });
    });

Task("Zip-Files")
    .IsDependentOn("Publish")
    .Does(() =>
    {
        Zip(outputDirectory, "./publish/PersonalWebApp.zip");
    });


Task("Default")
    .IsDependentOn("Zip-Files");

RunTarget(target);