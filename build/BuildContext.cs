using Cake.Common;
using Cake.Common.Build;
using Cake.Common.IO;
using Cake.Core;
using Cake.Frosting;
using JetBrains.Annotations;

namespace Build
{
    [UsedImplicitly]
    public class BuildContext : FrostingContext
    {
        public string Target { get; set; }
        public string BuildConfiguration { get; set; }
        public string BuildNumber { get; set; }
        public string BinariesDirectoryPath { get; set; }
        public string ArtifactDirectoryPath { get; set; }
        public string ProjectPath { get; set; }
        public string BlogPath { get; set; }
        public string PackageFullName { get; set; }

        public BuildContext(ICakeContext context) 
            : base(context)
        {
            // arguments
            Target = context.Argument("target", "Default");
            BuildConfiguration = context.Argument("configuration", "Release");
            
            // global variables
            BuildNumber = context.AzurePipelines().Environment.Build.Number;
            if (string.IsNullOrEmpty(BuildNumber))
            {
                BuildNumber = "255.255.255.255";
            }

            BinariesDirectoryPath = context.EnvironmentVariable("BUILD_BINARIESDIRECTORY");
            if (string.IsNullOrEmpty(BinariesDirectoryPath))
            {
                BinariesDirectoryPath = "./Build.Binaries";
                context.CleanDirectory(BinariesDirectoryPath);
            }
            
            ArtifactDirectoryPath = context.EnvironmentVariable("BUILD_ARTIFACTSTAGINGDIRECTORY");
            if (string.IsNullOrEmpty(ArtifactDirectoryPath))
            {
                ArtifactDirectoryPath = "./Build.ArtifactStagingDirectory";
                context.CleanDirectory(ArtifactDirectoryPath);
            }
            
            ProjectPath = "../src/PersonalWebApp";
            BlogPath = $"{ProjectPath}/Blog";

            PackageFullName = $"{ArtifactDirectoryPath}/PersonalWebApp.{BuildNumber}.zip";
        }
    }
}