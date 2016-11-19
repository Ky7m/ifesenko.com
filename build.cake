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

 Task("GenerateBlog")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var packageFiles = new []
            {
                "./src/PersonalWebApp",
                "./src/PersonalWebApp/Blog"
            };
        foreach(var package in packageFiles)
        {
            Npm.FromPath(package).Install();
        }

        Npm.Install(settings => settings.Package("hexo-cli").Globally());
        ExecuteCommand("\"hexo clean\"","./src/PersonalWebApp/Blog");
        ExecuteCommand("\"hexo generate\"","./src/PersonalWebApp/Blog");
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
    .IsDependentOn("GenerateBlog")
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
            WaitForDeployment = false
      });
  });

Task("Default")
    .IsDependentOn("OctoDeploy");

RunTarget(target);


void ExecuteCommand(string command, string workingDir = null)
{
    if (string.IsNullOrEmpty(workingDir))
        workingDir = System.IO.Directory.GetCurrentDirectory();

    System.Diagnostics.ProcessStartInfo processStartInfo;

    if (IsRunningOnWindows())
    {
        processStartInfo = new System.Diagnostics.ProcessStartInfo
        {
            UseShellExecute = false,
            WorkingDirectory = workingDir,
            FileName = "cmd",
            Arguments = "/C " + command,
        };
    }
    else
    {
        processStartInfo = new System.Diagnostics.ProcessStartInfo
        {
            UseShellExecute = false,
            WorkingDirectory = workingDir,
            Arguments = command,
        };
    }

    using (var process = System.Diagnostics.Process.Start(processStartInfo))
    {
        process.WaitForExit();

        if (process.ExitCode != 0)
            throw new Exception(string.Format("Exit code {0} from {1}", process.ExitCode, command));
    }
}