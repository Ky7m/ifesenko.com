using Cake.Common;
using Cake.Common.Build;
using Cake.Frosting;

namespace Build
{
    public sealed class Lifetime : FrostingLifetime<Context>
    {
        public override void Setup(Context context)
        {
            // arguments
            context.Target = context.Argument("target", "Default");
            context.Configuration = context.Argument("configuration", "Release");
            context.BuildNumber = context.Argument("buildNumber", "255.255.255.255");
            context.OctoServer = context.Argument("octoServer", "http://ifesenko.westeurope.cloudapp.azure.com");
            context.OctoApiKey = context.Argument("octoApiKey", "API-42NRB0K7W3L85TIBVULGVNE32S");
            context.OctoProject = context.Argument("octoProject", "www.ifesenko.com");
            context.OctoTargetEnvironment = context.Argument("octoTargetEnvironment", "Staging");
            
            // global variables
            context.OutputPath = "./.build";
            context.PackagePath = "./publish";
            context.ProjectPath = "./src/PersonalWebApp";
            context.BlogPath = $"{context.ProjectPath}/Blog";

            context.PackageFullName = $"{context.PackagePath}/PersonalWebApp.{context.BuildNumber}.zip";
            context.IsContinuousIntegrationBuild = !context.BuildSystem().IsLocalBuild;
        }
    }
}