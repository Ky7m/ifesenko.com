using Cake.Frosting;
using Cake.Npm;
using Cake.Npm.Install;

namespace Build.Tasks
{
    public sealed class NpmInstall: FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var packageFiles = new []
            {
                context.ProjectPath,
                context.BlogPath
            };
            
            var settings = new NpmInstallSettings();
            foreach(var packagePath in packageFiles)
            {
                settings.WorkingDirectory = packagePath;
                context.NpmInstall(settings);
            }
        }
    }
}