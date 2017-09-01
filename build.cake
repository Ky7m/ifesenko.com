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
var buildNumber = Argument("buildNumber", "255.255.255.255");
var octoServer = Argument("octoServer", "http://ifesenko.westeurope.cloudapp.azure.com");
var octoApiKey = Argument("octoApiKey", "API-42NRB0K7W3L85TIBVULGVNE32S");
var octoProject = Argument("octoProject", "www.ifesenko.com");
var octoTargetEnvironment = Argument("octoTargetEnvironment", "Staging");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var outputDirectory = Directory("./.build");
var packageDirectory= Directory("./publish");

var packageName = string.Format("./publish/PersonalWebApp.{0}.zip",buildNumber);

var isContinuousIntegrationBuild = !BuildSystem.IsLocalBuild;

var blogPath = "./src/PersonalWebApp/Blog";
var projectPath = "./src/PersonalWebApp";

Task("Clean")
    .Does(() =>
    {
        CleanDirectories(GetDirectories(projectPath + "/obj") + 
                             GetDirectories(projectPath + "/bin") +
                             outputDirectory +
                             packageDirectory);
    });

Task("NpmInstall")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var packageFiles = new []
            {
                projectPath,
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
        ExecuteCommand("\"./node_modules/.bin/hexo clean\"", blogPath);
        ExecuteCommand("\"./node_modules/.bin/hexo generate\"", blogPath);
    });

Task("Publish")
    .IsDependentOn("GenerateBlog")
    .Does(() =>
    {
        DotNetCorePublish(
                projectPath,
                new DotNetCorePublishSettings()
                {
                    Configuration = configuration,
                    OutputDirectory = outputDirectory
                    //ArgumentCustomization = args => args.Append($"/p:ShowLinkerSizeComparison=true")
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


void ExecuteCommand(string command, string workingDir = null)
{
    if (string.IsNullOrEmpty(workingDir))
    {
        workingDir = System.IO.Directory.GetCurrentDirectory();
    }

    var processStartInfo = new System.Diagnostics.ProcessStartInfo
                                {
                                    UseShellExecute = false,
                                    WorkingDirectory = workingDir,
                                    FileName = IsRunningOnWindows() ? "powershell" : "bash",
                                    Arguments = (IsRunningOnWindows() ? "-Command " : "-c ") + command
                                };

    using (var process = System.Diagnostics.Process.Start(processStartInfo))
    {
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception(string.Format("Exit code {0} from {1}", process.ExitCode, command));
        }
    }
}