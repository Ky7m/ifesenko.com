using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Frosting;

namespace Build.Tasks
{
    [Dependency(typeof(GenerateBlog))]
    public sealed class Publish : FrostingTask<Context>
    {
        public override void Run(Context context)
        {
            context.DotNetCorePublish(
                context.ProjectPath,
                new DotNetCorePublishSettings
                {
                    //Runtime = "win10-x64",
                    //NoRestore = true,
                    Configuration = context.Configuration,
                    OutputDirectory = context.BinariesDirectoryPath
                });
        }
    }
}