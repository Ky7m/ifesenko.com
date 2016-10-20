#tool "nuget:?package=OctopusTools"
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var buildNumber = Argument("buildNumber", "1.0.0.0");
var octoServer = Argument("octoServer", "http://ifesenko.westeurope.cloudapp.azure.com");
var octoApiKey = Argument("octoApiKey", "API-42NRB0K7W3L85TIBVULGVNE32S");

var outputDirectory = Directory("./build");
var packageDirectory= Directory("./publish");

var packageName = string.Format("./publish/PersonalWebApp.{0}.zip",buildNumber);

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
        Zip(outputDirectory, packageName);
    });

Task("OctoPush")
  .IsDependentOn("Zip-Files")
  .Does(() => {
    OctoPush(octoServer, 
			 octoApiKey, 
			 new FilePath(packageName),
			 new OctopusPushSettings {
				ReplaceExisting = true
			 });
  });

Task("OctoRelease")
  .IsDependentOn("OctoPush")
  .Does(() => {
    OctoCreateRelease("www.ifesenko.com", new CreateReleaseSettings {
        Server = octoServer,
        ApiKey = octoApiKey,
        ReleaseNumber = buildNumber
      });
  });

Task("Default")
    .IsDependentOn("OctoRelease");

RunTarget(target);