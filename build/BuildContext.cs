using Cake.Common;
using Cake.Common.IO;
using Cake.Core;
using Cake.Frosting;
using JetBrains.Annotations;

namespace Build;

[UsedImplicitly]
public class BuildContext : FrostingContext
{
    public string Target { get; set; }
    public string BuildConfiguration { get; set; }
    public string BuildNumber { get; set; }
    public string BinariesDirectoryPath { get; set; }
    public string ArtifactDirectoryPath { get; set; }
    public string ProjectPath { get; set; }
    public string PackageFullName { get; set; }

    public BuildContext(ICakeContext context) 
        : base(context)
    {
        // arguments
        Target = context.Argument("target", "Default");
        BuildConfiguration = context.Argument("configuration", "Release");
        BuildNumber = context.Argument("buildNumber", "255.255.255.255");;

        // global variables
        BinariesDirectoryPath = "./artifacts/Build.Binaries";
        context.CleanDirectory(BinariesDirectoryPath);
        
        ArtifactDirectoryPath = "./artifacts/Build.StagingDirectory";
        context.CleanDirectory(ArtifactDirectoryPath);
        
        ProjectPath = "../src/PersonalWebApp";

        PackageFullName = $"{ArtifactDirectoryPath}/{BuildNumber}.zip";
    }
}