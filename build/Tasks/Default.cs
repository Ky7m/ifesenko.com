using Cake.Frosting;

namespace Build.Tasks
{
    [Dependency(typeof(NpmInstall))]
    [Dependency(typeof(GenerateBlog))]
    [Dependency(typeof(Publish))]
    [Dependency(typeof(ZipFiles))]
    public sealed class Default : FrostingTask<Context>
    {
    }
}