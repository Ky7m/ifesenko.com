using Cake.Common.Tools.OctopusDeploy;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks
{
    [Dependency(typeof(ZipFiles))]
    public sealed class OctoPush : FrostingTask<Context>
    {
        public override void Run(Context context) => context.OctoPush(context.OctoServer,
            context.OctoApiKey,
            new FilePath(context.PackageFullName),
            new OctopusPushSettings
            {
                ReplaceExisting = true
            });

        public override bool ShouldRun(Context context) => context.IsContinuousIntegrationBuild;
    }
}