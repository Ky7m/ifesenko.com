using Cake.Common.Build;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Cake.Npm;
using Cake.Npm.Ci;
using Cake.Npm.Install;

namespace Build.Tasks;

public sealed class NpmInstall: FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if(context.GitHubActions().IsRunningOnGitHubActions)
        {
            context.Log.Information("command: npm ci");
            context.NpmCi(new NpmCiSettings
            {
                WorkingDirectory = context.ProjectPath
            });
        }
        else
        {
            context.Log.Information("command: npm install");
            context.NpmInstall(new NpmInstallSettings
            {
                WorkingDirectory = context.ProjectPath
            });
        }
    }
}