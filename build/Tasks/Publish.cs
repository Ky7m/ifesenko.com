using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Frosting;

namespace Build.Tasks
{
    [Dependency(typeof(GenerateBlog))]
    public sealed class Publish : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.DotNetCorePublish(
                context.ProjectPath,
                new DotNetCorePublishSettings
                {
                    Configuration = context.BuildConfiguration,
                    OutputDirectory = context.BinariesDirectoryPath
                });
        }
    }
}