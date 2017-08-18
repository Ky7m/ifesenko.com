using Cake.Frosting;
using Cake.Npm;
using Cake.Npm.Install;

namespace Build.Tasks
{
    public sealed class NpmInstall: FrostingTask<Context>
    {
        public override void Run(Context context)
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