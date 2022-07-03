using Cake.Frosting;
using Cake.Npm;
using Cake.Npm.Install;

namespace Build.Tasks;

public sealed class NpmInstall: FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.NpmInstall(new NpmInstallSettings
        {
            WorkingDirectory = context.ProjectPath
        });
        
    }
}