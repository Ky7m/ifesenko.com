using Cake.Common.Tools.OctopusDeploy;
using Cake.Frosting;

namespace Build.Tasks
{
    public sealed class OctoRelease : FrostingTask<Context>
    {
        public override void Run(Context context) => context.OctoCreateRelease(context.OctoProject,
            new CreateReleaseSettings
            {
                Server = context.OctoServer,
                ApiKey = context.OctoApiKey,
                ReleaseNumber = context.BuildNumber
            });

        public override bool ShouldRun(Context context) => context.IsContinuousIntegrationBuild;
    }
}