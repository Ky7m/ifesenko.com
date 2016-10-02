#addin "nuget:https://www.nuget.org/api/v2?package=Newtonsoft.Json&version=9.0.1"
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var version = Argument("version", "1.0.0");

var outputDirectory = Directory("./output");
var packageDirectory= Directory("./publish");

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(outputDirectory);
        CleanDirectory(packageDirectory);
    });

Task("Patch-Project-Json")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        if(string.IsNullOrEmpty(version))
        {
            Warning("No version specified.");
        }
        else
        {
            var projects = GetFiles("./src/**/project.json");
            foreach(var project in projects)
            {
                var content = System.IO.File.ReadAllText(project.FullPath, Encoding.UTF8);
                var node = Newtonsoft.Json.Linq.JObject.Parse(content);
                if(node["version"] != null)
                {
                    node["version"].Replace(string.Concat(version, "-*"));
                    System.IO.File.WriteAllText(project.FullPath, node.ToString(), Encoding.UTF8);
                };
            }
        }
    });

Task("Restore")
    .IsDependentOn("Patch-Project-Json")
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