using Cake.Frosting;

namespace Build.Tasks;

[IsDependentOn(typeof(NpmInstall))]
[IsDependentOn(typeof(Publish))]
[IsDependentOn(typeof(ZipFiles))]
public sealed class Default : FrostingTask
{
}