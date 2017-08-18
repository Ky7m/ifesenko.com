using Cake.Frosting;

namespace Build.Tasks
{
    [Dependency(typeof(Clean))]
    [Dependency(typeof(NpmInstall))]
    [Dependency(typeof(GenerateBlog))]
    [Dependency(typeof(Publish))]
    [Dependency(typeof(ZipFiles))]
    [Dependency(typeof(OctoPush))]
    [Dependency(typeof(OctoRelease))]
    [Dependency(typeof(OctoDeploy))]
    public sealed class Default : FrostingTask<Context>
    {
    }
}