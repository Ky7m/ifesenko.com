using Cake.Common;
using Cake.Common.Build;
using Cake.Common.IO;
using Cake.Frosting;
using JetBrains.Annotations;

namespace Build
{
    [UsedImplicitly]
    public sealed class Lifetime : FrostingLifetime<Context>
    {
        public override void Setup(Context context)
        {
            // arguments
            context.Target = context.Argument("target", "Default");
            context.Configuration = context.Argument("configuration", "Release");
            
            // global variables
            context.BuildNumber = context.TFBuild().Environment.Build.Number;
            if (string.IsNullOrEmpty(context.BuildNumber))
            {
                context.BuildNumber = "255.255.255.255";
            }

            context.BinariesDirectoryPath = context.EnvironmentVariable("BUILD_BINARIESDIRECTORY");
            if (string.IsNullOrEmpty(context.BinariesDirectoryPath))
            {
                context.BinariesDirectoryPath = "./Build.Binaries";
                context.CleanDirectory(context.BinariesDirectoryPath);
            }
            
            context.ArtifactDirectoryPath = context.EnvironmentVariable("BUILD_ARTIFACTSTAGINGDIRECTORY");
            if (string.IsNullOrEmpty(context.ArtifactDirectoryPath))
            {
                context.ArtifactDirectoryPath = "./Build.ArtifactStagingDirectory";
                context.CleanDirectory(context.ArtifactDirectoryPath);
            }
            
            context.ProjectPath = "./src/PersonalWebApp";
            context.BlogPath = $"{context.ProjectPath}/Blog";

            context.PackageFullName = $"{context.ArtifactDirectoryPath}/PersonalWebApp.{context.BuildNumber}.zip";
        }
    }
}