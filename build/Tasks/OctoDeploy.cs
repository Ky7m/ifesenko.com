using Cake.Common.Tools.OctopusDeploy;
using Cake.Frosting;

namespace Build.Tasks
{
    public sealed class OctoDeploy : FrostingTask<Context>
    {
        public override void Run(Context context) => context.OctoDeployRelease(context.OctoServer,
            context.OctoApiKey,
            context.OctoProject,
            context.OctoTargetEnvironment,
            context.BuildNumber,
            new OctopusDeployReleaseDeploymentSettings
            {
                ShowProgress = false,
                WaitForDeployment = true
            });

        public override bool ShouldRun(Context context) => context.IsContinuousIntegrationBuild;
    }
}