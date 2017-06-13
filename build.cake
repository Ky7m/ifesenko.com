//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////
#tool "nuget:?package=OctopusTools"

//////////////////////////////////////////////////////////////////////
// ADDINS
//////////////////////////////////////////////////////////////////////
#addin "Cake.Npm"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var buildNumber = Argument("buildNumber", "1.0.0.0");
var octoServer = Argument("octoServer", "http://ifesenko.westeurope.cloudapp.azure.com");
var octoApiKey = Argument("octoApiKey", "API-42NRB0K7W3L85TIBVULGVNE32S");
var octoProject = Argument("octoProject", "www.ifesenko.com");
var octoTargetEnvironment = Argument("octoTargetEnvironment", "Staging");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var outputDirectory = Directory("./build");
var packageDirectory= Directory("./publish");

var packageName = string.Format("./publish/PersonalWebApp.{0}.zip",buildNumber);

var isContinuousIntegrationBuild = !BuildSystem.IsLocalBuild;

var blogPath = "./src/PersonalWebApp/Blog";
var solutionPath = "./PersonalWebApp.sln";


Task("InstallTools")
  .WithCriteria(isContinuousIntegrationBuild)
  .Does(() => 
  {
      var settings = new NpmInstallSettings();
      settings.Global = true;
      settings.AddPackage("hexo-cli");
      settings.AddPackage("gulp");
      NpmInstall(settings);
  });

Task("Clean")
    .IsDependentOn("InstallTools")
    .Does(() =>
    {
        CleanDirectory(outputDirectory);
        CleanDirectory(packageDirectory);
    });

Task("NpmInstall")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var packageFiles = new []
            {
                "./src/PersonalWebApp",
                blogPath
            };
            
        var settings = new NpmInstallSettings();
        foreach(var packagePath in packageFiles)
        {
            settings.WorkingDirectory = packagePath;
            NpmInstall(settings);
        }
    });

Task("GenerateBlog")
    .IsDependentOn("NpmInstall")
    .Does(() =>
    {
        StartProcess("hexo", new ProcessSettings { Arguments = "clean", WorkingDirectory =  blogPath});
        StartProcess("hexo", new ProcessSettings { Arguments = "generate", WorkingDirectory =  blogPath});
    });

Task("Publish")
    .IsDependentOn("GenerateBlog")
    .Does(() =>
    {
		DotNetCoreRestore(solutionPath);
        DotNetCorePublish(
                solutionPath,
                new DotNetCorePublishSettings()
                {
                    Configuration = configuration,
                    OutputDirectory = outputDirectory
                });
    });

Task("Zip-Files")
    .WithCriteria(isContinuousIntegrationBuild)
    .IsDependentOn("Publish")
    .Does(() =>
    {
        Zip(outputDirectory, packageName);
    });

Task("OctoPush")
  .WithCriteria(isContinuousIntegrationBuild)
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
  .WithCriteria(isContinuousIntegrationBuild)
  .IsDependentOn("OctoPush")
  .Does(() => {
    OctoCreateRelease(octoProject, new CreateReleaseSettings {
        Server = octoServer,
        ApiKey = octoApiKey,
        ReleaseNumber = buildNumber
      });
  });

Task("OctoDeploy")
  .WithCriteria(isContinuousIntegrationBuild)
  .IsDependentOn("OctoRelease")
  .Does(() => {
    OctoDeployRelease(octoServer,
        octoApiKey,
        octoProject,
        octoTargetEnvironment,
        buildNumber,
        new OctopusDeployReleaseDeploymentSettings {
            ShowProgress = false,
            WaitForDeployment = true
      });
  });

Task("Default")
    .IsDependentOn("OctoDeploy");

RunTarget(target);