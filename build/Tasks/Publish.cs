using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Core;
using Cake.Frosting;

namespace Build.Tasks;

[IsDependentOn(typeof(NpmInstall))]
public sealed class Publish : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetPublish(
            context.ProjectPath,
            new DotNetCorePublishSettings
            {
                Configuration = context.BuildConfiguration,
                OutputDirectory = context.BinariesDirectoryPath,
                ArgumentCustomization = args => args.Append($"-p:Version={context.BuildNumber}")
            });
    }
}